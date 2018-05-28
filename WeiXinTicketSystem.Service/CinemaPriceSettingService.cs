using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System;
using System.Collections.Generic;
using WeiXinTicketSystem.Entity.Models.PageList;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Service
{
    public class CinemaPriceSettingService
    {
        #region ctor
        private readonly IRepository<CinemaPriceSettingEntity> _priceSettingRepository;
        public CinemaPriceSettingService()
        {
            _priceSettingRepository = new Repository<CinemaPriceSettingEntity>();
        }
        #endregion

        /// <summary>
        /// 获取影院价格设置列表
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<CinemaPriceSettingEntity>> GetPriceSettingsPagedAsync(int offset, int perPage, string keyword)
        {
            var query = _priceSettingRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaCode.Contains(keyword) || x.CinemaName.Contains(keyword));
            }
            return await query.ToPageListAsync();
        }
        /// <summary>
        /// 读出所有价格设置
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CinemaPriceSettingEntity>> GetAllPriceSettingsAsync()
        {
            return await _priceSettingRepository.Query.ToListAsync();
        }

        /// <summary>
        /// 根据Id读取价格设置
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CinemaPriceSettingEntity> GetAsync(int Id)
        {
            return await _priceSettingRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新价格设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CinemaPriceSettingEntity entity)
        {
            await _priceSettingRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增价格设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(CinemaPriceSettingEntity entity)
        {
            await _priceSettingRepository.InsertAsync(entity);
        }
        /// <summary>
        /// 删除价格设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(CinemaPriceSettingEntity entity)
        {
            await _priceSettingRepository.DeleteAsync(entity);
        }
    }
}
