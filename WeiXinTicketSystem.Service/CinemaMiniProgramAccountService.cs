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
    public class CinemaMiniProgramAccountService
    {
        #region ctor
        private readonly IRepository<CinemaMiniProgramAccountEntity> _cinemaMiniProgramAccountRepository;

        public CinemaMiniProgramAccountService()
        {
            //TODO: 移除内部依赖
            _cinemaMiniProgramAccountRepository = new Repository<CinemaMiniProgramAccountEntity>();
        }
        #endregion


        /// <summary>
        /// 根据影院编码获取小程序对接账号配置信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public CinemaMiniProgramAccountEntity GetCinemaMiniProgramAccountByCinemaCode(string CinemaCode)
        {
            return _cinemaMiniProgramAccountRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.IsDel).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有小程序对接账号配置列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CinemaMiniProgramAccountEntity>> GetAllCinemaMiniProgramAccountAsync()
        {
            return await _cinemaMiniProgramAccountRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取小程序对接账号配置信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<CinemaMiniProgramAccountEntity>> GetCinemaMiniProgramAccountPagedAsync(string cinemaCode, string CinemaName, string keyword, int offset, int perPage)
        {
            var query = _cinemaMiniProgramAccountRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
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
                query.Where(x => x.AppId.Contains(keyword) || x.AppSecret.Contains(keyword) );
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// CinemaMiniProgramAccountEntity
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<CinemaMiniProgramAccountEntity> GetCinemaMiniProgramAccountByCinemaCodeAsync(string CinemaCode)
        {
            return await _cinemaMiniProgramAccountRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.IsDel).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 根据小程序对接账号配置ID获取小程序对接账号配置信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CinemaMiniProgramAccountEntity> GetCinemaMiniProgramAccountByIdAsync(int Id)
        {
            return await _cinemaMiniProgramAccountRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(CinemaMiniProgramAccountEntity entity)
        {
            _cinemaMiniProgramAccountRepository.Update(entity);
        }

        /// <summary>
        /// 更新小程序对接账号配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CinemaMiniProgramAccountEntity entity)
        {
            await _cinemaMiniProgramAccountRepository.UpdateAsync(entity);
        }


        /// <summary>
        /// 新增小程序对接账号配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(CinemaMiniProgramAccountEntity entity)
        {
            await _cinemaMiniProgramAccountRepository.InsertAsync(entity);
        }

    }
}
