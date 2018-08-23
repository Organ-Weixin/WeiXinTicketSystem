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
    public class CinemaPaySettingsService
    {
        #region ctor
        private readonly IRepository<CinemaPaymentSettingEntity> _cinemaPaySettingsRepository;

        public CinemaPaySettingsService()
        {
            //TODO: 移除内部依赖
            _cinemaPaySettingsRepository = new Repository<CinemaPaymentSettingEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影院编码获取影院支付方式配置信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public CinemaPaymentSettingEntity GetCinemaPaySettingsByCinemaCode(string CinemaCode)
        {
            return _cinemaPaySettingsRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.IsDel).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有影院支付方式配置列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CinemaPaymentSettingEntity>> GetAllCinemaPaySettingsAsync()
        {
            return await _cinemaPaySettingsRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取影院支付方式配置信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<CinemaPaymentSettingEntity>> GetCinemaPaySettingsPagedAsync(string cinemaCode, string CinemaName, string keyword, int offset, int perPage)
        {
            var query = _cinemaPaySettingsRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
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
                query.Where(x => x.AlipaySellerEmail.Contains(keyword) || x.AlipayPartner.Contains(keyword) || x.BfbpaySpno.Contains(keyword) || x.WxpayMchId.Contains(keyword) || x.WxpayRefundCert.Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取CinemaPaymentSetting实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<CinemaPaymentSettingEntity> GetCinemaPaySettingsByCinemaCodeAsync(string CinemaCode)
        {
            return await _cinemaPaySettingsRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.IsDel).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据影院支付方式配置ID获取影院支付方式配置信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CinemaPaymentSettingEntity> GetCinemaPaySettingsByIdAsync(int Id)
        {
            return await _cinemaPaySettingsRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(CinemaPaymentSettingEntity entity)
        {
            _cinemaPaySettingsRepository.Update(entity);
        }

        /// <summary>
        /// 更新影院支付方式配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CinemaPaymentSettingEntity entity)
        {
            await _cinemaPaySettingsRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增影院支付方式配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(CinemaPaymentSettingEntity entity)
        {
            await _cinemaPaySettingsRepository.InsertAsync(entity);
        }
    }
}
