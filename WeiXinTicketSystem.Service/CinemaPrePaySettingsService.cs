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
    public class CinemaPrePaySettingsService
    {
        #region ctor
        private readonly IRepository<CinemaPrePaySettingEntity> _cinemaPrePayRepository;

        public CinemaPrePaySettingsService()
        {
            //TODO: 移除内部依赖
            _cinemaPrePayRepository = new Repository<CinemaPrePaySettingEntity>();
        }
        #endregion


        /// <summary>
        /// 根据影院编码获取影院预付款配置信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public CinemaPrePaySettingEntity GetCinemaPrePaySettingsByCinemaCode(string CinemaCode)
        {
            return _cinemaPrePayRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有影院预付款配置列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CinemaPrePaySettingEntity>> GetAllCinemaPrePaySettingsAsync()
        {
            return await _cinemaPrePayRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取影院预付款配置信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<CinemaPrePaySettingEntity>> GetCinemaPrePaySettingsPagedAsync(string cinemaCode, string CinemaName, string keyword, int offset, int perPage)
        {
            var query = _cinemaPrePayRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
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
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaName.Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取CinemaPrePaymentSetting实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<CinemaPrePaySettingEntity> GetCinemaPrePaySettingsByCinemaCodeAsync(string CinemaCode)
        {
            return await _cinemaPrePayRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据影院预付款配置ID获取影院预付款配置信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CinemaPrePaySettingEntity> GetCinemaPrePaySettingsByIdAsync(int Id)
        {
            return await _cinemaPrePayRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(CinemaPrePaySettingEntity entity)
        {
            _cinemaPrePayRepository.Update(entity);
        }

        /// <summary>
        /// 更新影院预付款配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CinemaPrePaySettingEntity entity)
        {
            await _cinemaPrePayRepository.UpdateAsync(entity);
        }


        /// <summary>
        /// 新增影院预付款配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(CinemaPrePaySettingEntity entity)
        {
            await _cinemaPrePayRepository.InsertAsync(entity);
        }
    }
}
