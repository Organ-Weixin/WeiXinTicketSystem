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
    public class CinemaTicketSystemAccountService
    {
        #region ctor
        private readonly IRepository<CinemaTicketSystemAccountEntity> _cinemaTicketSystemAccountRepository;

        public CinemaTicketSystemAccountService()
        {
            //TODO: 移除内部依赖
            _cinemaTicketSystemAccountRepository = new Repository<CinemaTicketSystemAccountEntity>();
        }
        #endregion


        /// <summary>
        /// 根据影院编码获取影院系统对接账号配置信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public CinemaTicketSystemAccountEntity GetCinemaTicketSystemAccountByCinemaCode(string CinemaCode)
        {
            return _cinemaTicketSystemAccountRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有影院系统对接账号配置列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CinemaTicketSystemAccountEntity>> GetAllCinemaTicketSystemAccountAsync()
        {
            return await _cinemaTicketSystemAccountRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取影院系统对接账号配置信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<CinemaTicketSystemAccountEntity>> GetCinemaTicketSystemAccountPagedAsync(string cinemaCode, string CinemaName, string keyword, int offset, int perPage)
        {
            var query = _cinemaTicketSystemAccountRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
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
                query.Where(x => x.Url.Contains(keyword) || x.UserName.Contains(keyword) );
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取CinemaTicketSystemAccountEntity实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<CinemaTicketSystemAccountEntity> GetCinemaTicketSystemAccountByCinemaCodeAsync(string CinemaCode)
        {
            return await _cinemaTicketSystemAccountRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 根据影院系统对接账号配置ID获取影院系统对接账号配置信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CinemaTicketSystemAccountEntity> GetCinemaTicketSystemAccountByIdAsync(int Id)
        {
            return await _cinemaTicketSystemAccountRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(CinemaTicketSystemAccountEntity entity)
        {
            _cinemaTicketSystemAccountRepository.Update(entity);
        }

        /// <summary>
        /// 更新影院系统对接账号配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CinemaTicketSystemAccountEntity entity)
        {
            await _cinemaTicketSystemAccountRepository.UpdateAsync(entity);
        }


        /// <summary>
        /// 新增影院系统对接账号配置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(CinemaTicketSystemAccountEntity entity)
        {
            await _cinemaTicketSystemAccountRepository.InsertAsync(entity);
        }

    }
}
