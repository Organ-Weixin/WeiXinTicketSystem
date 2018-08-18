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
    public class ConponService
    {
        #region ctor
        private readonly IRepository<ConponEntity> _conponRepository;
        private readonly IRepository<AdminConponViewEntity> _adminConponViewRepository;

        public ConponService()
        {
            //TODO: 移除内部依赖
            _conponRepository = new Repository<ConponEntity>();
            _adminConponViewRepository = new Repository<AdminConponViewEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影院编码获取优惠券信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<ConponEntity> GetConponByCinemaCode(string CinemaCode)
        {
            return _conponRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.Deleted).ToList();
        }

        /// <summary>
        /// 获取所有优惠券列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ConponEntity>> GetAllConponAsync()
        {
            return await _conponRepository.Query.Where(x => !x.Deleted).ToListAsync();
        }
        /// <summary>
        /// 根据优惠券编号获取优惠券
        /// </summary>
        /// <param name="ConponCode"></param>
        /// <returns></returns>
        public ConponEntity GetConponByConponCode(string ConponCode)
        {
            return _conponRepository.Query.Where(x => x.ConponCode == ConponCode && !x.Deleted).SingleOrDefault();
        }

        /// <summary>
        /// 后台分页读取优惠券信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="ConponCode"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<ConponEntity>> GetConponPagedAsync(string cinemaCode, string ConponCode, string GroupCode,string keyword, int offset, int perPage)
        {
            var query = _conponRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            //优惠券编码
            if (!string.IsNullOrEmpty(ConponCode))
            {
                query.Where(x => x.ConponCode.Contains(ConponCode));
            }
            //优惠券组编码
            if (!string.IsNullOrEmpty(GroupCode))
            {
                query.Where(x => x.GroupCode == GroupCode);
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Title.Contains(keyword));
            }
            query.Where(x => !x.Deleted);
            return await query.ToPageListAsync();
        }

        public async Task<IPageList<ConponEntity>> QueryConponsPagedAsync(string cinemaCode, string OpenID, ConponStatusEnum status, int currentpage, int pagesize)
        {
            int offset = (currentpage - 1) * pagesize;
            var query = _conponRepository.Query
                .OrderByDescending(x => x.Id)
                .Skip(offset)
                .Take(pagesize);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            if (!string.IsNullOrEmpty(OpenID))
            {
                query.Where(x => x.OpenID == OpenID);
            }
            if (status != ConponStatusEnum.All)
            {
                query.Where(x => x.Status == status);
            }
            query.Where(x => !x.Deleted);
            return await query.ToPageListAsync();
        }


        /// <summary>
        /// 获取ConponEntity实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<IList<ConponEntity>> GetConponByCinemaCodeAsync(string CinemaCode)
        {
            return await _conponRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.Deleted).ToListAsync();
        }

        /// <summary>
        /// 根据优惠券ID获取优惠券信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ConponEntity> GetConponByIdAsync(int Id)
        {
            return await _conponRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(ConponEntity entity)
        {
            _conponRepository.Update(entity);
        }

        /// <summary>
        /// 更新优惠券
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ConponEntity entity)
        {
            await _conponRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增优惠券
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(ConponEntity entity)
        {
            await _conponRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 根据优惠券类型编号随机抽取优惠券
        /// </summary>
        /// <param name="conponTypeCode"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public IList<ConponEntity> GetConponByTypeCodeAsync(string cinemaCode, string groupCode)
        {
            try
            {
                DateTime Now = DateTime.Now.Date;
                var query = _conponRepository.Query.OrderByDescending(x => x.Id);
                //影院编码
                if (!string.IsNullOrEmpty(cinemaCode))
                {
                    query.Where(x => x.CinemaCode == cinemaCode);
                }
                //优惠券组编号
                if (!string.IsNullOrEmpty(groupCode))
                {
                    query.Where(x => x.GroupCode == groupCode);
                }

                query.Where(x => !x.Deleted && x.Status == ConponStatusEnum.NotUsed && (x.ValidityDate==null || x.ValidityDate >= Now));
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据影院编码和优惠券组编号获取优惠券组下面所有未使用的优惠券信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<ConponEntity> GetConponByCinemaCodeAndGroupCode(string CinemaCode,string GroupCode)
        {
            if (string.IsNullOrEmpty(CinemaCode) || string.IsNullOrEmpty(GroupCode))
                return new List<ConponEntity>();
            return _conponRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.GroupCode == GroupCode && x.Status == ConponStatusEnum.NotUsed && !x.Deleted).ToList();
        }

    }
}
