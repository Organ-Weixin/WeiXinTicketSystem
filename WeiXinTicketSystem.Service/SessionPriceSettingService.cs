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
    public class SessionPriceSettingService
    {
        #region ctor
        private readonly IRepository<SessionPriceSettingEntity> _priceSettingsRepository;
        public SessionPriceSettingService()
        {
            _priceSettingsRepository = new Repository<SessionPriceSettingEntity>();
        }
        #endregion

        /// <summary>
        /// 根据Id获取价格设置
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<SessionPriceSettingEntity> GetAsync(int Id)
        {
            return await _priceSettingsRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据条件获取价格设置
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="UserID"></param>
        /// <param name="Type"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public async Task<SessionPriceSettingEntity> GetAsync(string CinemaCode, PricePlanTypeEnum Type, string Code)
        {
            return await _priceSettingsRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.Type == Type && x.Code == Code).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 读出价格设置列表
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IPageList<SessionPriceSettingEntity>> GetPriceSettingsPagedAsync(int offset, int perPage, string keyword)
        {
            var query = _priceSettingsRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaCode.Contains(keyword) || x.Code.Contains(keyword));
            }
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 更新价格设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SessionPriceSettingEntity entity)
        {
            await _priceSettingsRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增价格设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(SessionPriceSettingEntity entity)
        {
            await _priceSettingsRepository.InsertAsync(entity);
        }
        /// <summary>
        /// 删除价格设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(SessionPriceSettingEntity entity)
        {
            await _priceSettingsRepository.DeleteAsync(entity);
        }
    }
}
