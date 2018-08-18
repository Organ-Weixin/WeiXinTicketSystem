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
    public class GivingConditionsService
    {
        #region ctor
        private readonly IRepository<GivingConditionEntity> _givingConditionRepository;
        private readonly IRepository<AdminGivingConditionsViewEntity> _adminGivingConditionsViewRepository;

        public GivingConditionsService()
        {
            //TODO: 移除内部依赖
            _givingConditionRepository = new Repository<GivingConditionEntity>();
            _adminGivingConditionsViewRepository = new Repository<AdminGivingConditionsViewEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影院编码获取该影院下所有赠送条件信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<GivingConditionEntity> GetGivingConditionByCinemaCode(string CinemaCode)
        {
            return _givingConditionRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.Deleted).ToList();
        }

        /// <summary>
        /// 获取所有赠送条件列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<GivingConditionEntity>> GetAllGivingConditionAsync()
        {
            return await _givingConditionRepository.Query.Where(x => !x.Deleted).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取赠送条件信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<AdminGivingConditionsViewEntity>> GetGivingConditionPagedAsync(string cinemaCode, string keyword, int offset, int perPage)
        {
            try
            {
                var query = _adminGivingConditionsViewRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
                //影院编码
                if (!string.IsNullOrEmpty(cinemaCode))
                {
                    query.Where(x => x.CinemaCode == cinemaCode);
                }
                //其他数据
                if (!string.IsNullOrEmpty(keyword))
                {
                    query.Where(x => x.GroupName.Contains(keyword) || x.Conditions.Contains(keyword));
                }
                query.Where(x => !x.Deleted);
                return await query.ToPageListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 根据影院编码获取该影院下所有赠送条件信息(异步)
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<IList<GivingConditionEntity>> GetGivingConditionByCinemaCodeAsync(string CinemaCode)
        {
            return await _givingConditionRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.Deleted).ToListAsync();
        }

        /// <summary>
        /// 根据影院编码获取该影院下所有赠送条件信息(视图异步)
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<IList<AdminGivingConditionsViewEntity>> GetGivingConditionViewByCinemaCodeAsync(string CinemaCode)
        {
            return await _adminGivingConditionsViewRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.Deleted).ToListAsync();
        }

        /// <summary>
        /// 根据赠送条件ID获取赠送条件信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<GivingConditionEntity> GetGivingConditionByIdAsync(int Id)
        {
            return await _givingConditionRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(GivingConditionEntity entity)
        {
            _givingConditionRepository.Update(entity);
        }

        /// <summary>
        /// 更新赠送条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(GivingConditionEntity entity)
        {
            await _givingConditionRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增赠送条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(GivingConditionEntity entity)
        {
            await _givingConditionRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 根据影院编码和满额获取赠送条件信息(异步)
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<IList<GivingConditionEntity>> GetGivingConditionByCinemaCodeAndPriceAsync(string CinemaCode,decimal? price)
        {
            return await _givingConditionRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.Price == price && !x.Deleted).ToListAsync();
        }

    }
}
