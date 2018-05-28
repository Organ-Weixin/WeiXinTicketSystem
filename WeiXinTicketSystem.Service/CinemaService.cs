using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.Service
{
    public class CinemaService
    {
        #region ctor
        //private readonly IRepository<CinemaViewEntity> _cinemaViewRepository;
        private readonly IRepository<CinemaEntity> _cinemaRepository;

        public CinemaService()
        {
            //TODO: 移除内部依赖
            //_cinemaViewRepository = new Repository<CinemaViewEntity>();
            _cinemaRepository = new Repository<CinemaEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影院编码获取影院信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public CinemaEntity GetCinemaByCinemaCode(string CinemaCode)
        {
            return _cinemaRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有影院列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CinemaEntity>> GetAllCinemasAsync()
        {
            return await _cinemaRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }
        /// <summary>
        /// 后台分页读取影院信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<CinemaEntity>> GetCinemasPagedAsync(string cinemaCode, string CinemaName,CinemaStatusEnum? IsOpen, string keyword, int offset, int perPage)
        {
            var query = _cinemaRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            //影院名称
            if (!string.IsNullOrEmpty(CinemaName))
            {
                query.Where(x => x.CinemaName.Contains(CinemaName));
            }
            //是否开通接口
            if (IsOpen.HasValue)
            {
                query.Where(x => x.Status == IsOpen.Value);
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Address.Contains(keyword) || x.DingXinId.Contains(keyword) || x.ContactName.Contains(keyword) || x.ContactMobile.Contains(keyword) || x.YueKeId.Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取Cinema实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<CinemaEntity> GetCinemaByCinemaCodeAsync(string CinemaCode)
        {
            return await _cinemaRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 根据影院ID获取影院
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CinemaEntity> GetCinemaByIdAsync(int Id)
        {
            return await _cinemaRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(CinemaEntity entity)
        {
            _cinemaRepository.Update(entity);
        }

        /// <summary>
        /// 更新影院
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CinemaEntity entity)
        {
            await _cinemaRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增影院
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(CinemaEntity entity)
        {
            await _cinemaRepository.InsertAsync(entity);
        }
    }
}
