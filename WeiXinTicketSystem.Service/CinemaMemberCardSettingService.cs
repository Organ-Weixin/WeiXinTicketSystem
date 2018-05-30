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
    public class CinemaMemberCardSettingService
    {
        #region ctor
        private readonly IRepository<CinemaMemberCardSettingEntity> _cinemaMemberCardSettingRepository;

        public CinemaMemberCardSettingService()
        {
            //TODO: 移除内部依赖
            _cinemaMemberCardSettingRepository = new Repository<CinemaMemberCardSettingEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影院编码获取影城会员卡设置信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public CinemaMemberCardSettingEntity GetCinemaMemberCardSettingByCinemaCode(string CinemaCode)
        {
            return _cinemaMemberCardSettingRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有影城会员卡设置列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CinemaMemberCardSettingEntity>> GetAllCinemaMemberCardSettingAsync()
        {
            return await _cinemaMemberCardSettingRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取会员卡设置信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<CinemaMemberCardSettingEntity>> GetCinemaMemberCardSettingPagedAsync(string cinemaCode, string CinemaName, string keyword, int offset, int perPage)
        {
            var query = _cinemaMemberCardSettingRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
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
                query.Where(x => x.CinemaName.Contains(keyword) || x.ThirdMemberUrl.Contains(keyword) );
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取CinemaMemberCardSettingEntity实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<CinemaMemberCardSettingEntity> GetCinemaMemberCardSettingByCinemaCodeAsync(string CinemaCode)
        {
            return await _cinemaMemberCardSettingRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据影城会员卡设置ID获取影城会员卡设置信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CinemaMemberCardSettingEntity> GetCinemaMemberCardSettingByIdAsync(int Id)
        {
            return await _cinemaMemberCardSettingRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(CinemaMemberCardSettingEntity entity)
        {
            _cinemaMemberCardSettingRepository.Update(entity);
        }

        /// <summary>
        /// 更新影城会员卡设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CinemaMemberCardSettingEntity entity)
        {
            await _cinemaMemberCardSettingRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增影城会员卡设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(CinemaMemberCardSettingEntity entity)
        {
            await _cinemaMemberCardSettingRepository.InsertAsync(entity);
        }

    }
}
