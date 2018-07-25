using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Service
{
    public class SnackService
    {
        #region ctor
        private readonly IRepository<SnackEntity> _snackRepository;
        private readonly IRepository<AdminSnacksViewEntity> _snackViewRepository;
        public SnackService()
        {
            _snackRepository = new Repository<SnackEntity>();
            _snackViewRepository = new Repository<AdminSnacksViewEntity>();
        }
        #endregion

        public IList<SnackEntity> GetSnacksByCinemaCodeAndStatus(string CinemaCode,SnackStatusEnum?Status)
        {
            DateTime Now = DateTime.Now.Date;
            var query = _snackRepository.Query.Where(x => x.CinemaCode == CinemaCode && (x.ExpDate == null || x.ExpDate > Now));
            if(Status.HasValue&&Status.Value!=SnackStatusEnum.All)
            {
                query.Where(x => x.Status == Status.Value);
            }
            return query.ToList();
        }

        public IList<SnackEntity> GetSnacks(string CinemaCode, IEnumerable<string> SnackCodes)
        {
           return _snackRepository.Query.Where(x => x.CinemaCode == CinemaCode)
                    .WhereIsIn(x => x.SnackCode,SnackCodes).ToList();
        }
        public async Task<IPageList<AdminSnacksViewEntity>> GetSnacksPagedAsync(string cinemaCode,string snackcode, string typeid,
            int offset, int perPage, string keyword)
        {
            var query = _snackViewRepository.Query
                .OrderByDescending(x => x.Id)
                .Skip(offset)
                .Take(perPage);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            if (!string.IsNullOrEmpty(snackcode))
            {
                query.Where(x => x.SnackCode == snackcode);
            }
            if (!string.IsNullOrEmpty(typeid))
            {
                query.Where(x => x.TypeId == int.Parse(typeid));
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Remark.Contains(keyword) || x.SnackName.Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }
        public async Task<IPageList<SnackEntity>> QuerySnacksPagedAsync(string cinemaCode, string typeCode, int currentpage, int pagesize)
        {
            int offset = (currentpage - 1) * pagesize;
            var query = _snackRepository.Query
                .OrderByDescending(x => x.Id)
                .Skip(offset)
                .Take(pagesize);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            if (!string.IsNullOrEmpty(typeCode))
            {
                query.Where(x => x.TypeCode == typeCode);
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 根据Id获取套餐
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<SnackEntity> GetSnackByIdAsync(int Id)
        {
            return await _snackRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 根据cinemacode和name获取套餐
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public async Task<SnackEntity> GetSnackByCinemaCodeAndNameAsync(string CinemaCode,string SnackName)
        {
            return await _snackRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.SnackName == SnackName).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新套餐
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SnackEntity entity)
        {
            await _snackRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增套餐
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(SnackEntity entity)
        {
            await _snackRepository.InsertAsync(entity);
        }
    }
}
