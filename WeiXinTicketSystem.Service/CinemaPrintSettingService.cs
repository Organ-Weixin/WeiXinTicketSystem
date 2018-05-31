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
    public class CinemaPrintSettingService
    {
        #region ctor
        private readonly IRepository<CinemaPrintSettingEntity> _cinemaPrintSettingRepository;

        public CinemaPrintSettingService()
        {
            //TODO: 移除内部依赖
            _cinemaPrintSettingRepository = new Repository<CinemaPrintSettingEntity>();
        }
        #endregion


        /// <summary>
        /// 根据影院编码获取影院打印设置信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public CinemaPrintSettingEntity GetCinemaPrintSettingByCinemaCode(string CinemaCode)
        {
            return _cinemaPrintSettingRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefault();
        }


        /// <summary>
        /// 获取所有影院打印设置列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CinemaPrintSettingEntity>> GetAllCinemaPrintSettingAsync()
        {
            return await _cinemaPrintSettingRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取影院打印设置信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<CinemaPrintSettingEntity>> GetCinemaPrintSettingPagedAsync(string cinemaCode, string CinemaName, string keyword, int offset, int perPage)
        {
            var query = _cinemaPrintSettingRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
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
        /// 获取CinemaPrintSettingEntity实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<CinemaPrintSettingEntity> GetCinemaPrintSettingByCinemaCodeAsync(string CinemaCode)
        {
            return await _cinemaPrintSettingRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据影院打印设置ID获取影院打印设置信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CinemaPrintSettingEntity> GetCinemaPrintSettingByIdAsync(int Id)
        {
            return await _cinemaPrintSettingRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(CinemaPrintSettingEntity entity)
        {
            _cinemaPrintSettingRepository.Update(entity);
        }

        /// <summary>
        /// 更新影院打印设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CinemaPrintSettingEntity entity)
        {
            await _cinemaPrintSettingRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增影院打印设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(CinemaPrintSettingEntity entity)
        {
            await _cinemaPrintSettingRepository.InsertAsync(entity);
        }

    }
}
