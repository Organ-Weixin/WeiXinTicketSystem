using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System;
using System.Collections.Generic;
using WeiXinTicketSystem.Entity.Models.PageList;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Service
{
    public class UserCinemaService
    {
        #region ctor
        private readonly IRepository<UserCinemaViewEntity> _userCinemaViewRepository;
        private readonly IRepository<UserCinemaEntity> _userCinemaRepository;

        public UserCinemaService()
        {
            //TODO: 移除内部依赖
            _userCinemaViewRepository = new Repository<UserCinemaViewEntity>();
            _userCinemaRepository = new Repository<UserCinemaEntity>();
        }
        #endregion

        /// <summary>
        /// 获取用户能访问的所有影院
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<UserCinemaViewEntity> GetUserCinemasByUserId(int UserId)
        {
            DateTime Now = DateTime.Now.Date;
            return _userCinemaViewRepository.Query.Where(x => x.UserId == UserId 
                && (x.ExpDate == null || x.ExpDate > Now)).ToList();
        }

        /// <summary>
        /// 根据用户Id和影院编码获取用户是否可访问
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public UserCinemaViewEntity GetUserCinema(int UserId, string CinemaCode)
        {
            DateTime Now = DateTime.Now.Date;
            return _userCinemaViewRepository.Query.Where(x => x.UserId == UserId
                && x.CinemaCode == CinemaCode && x.CinemaOpen==1 && (x.ExpDate == null || x.ExpDate > Now)).SingleOrDefault();
        }
        /// <summary>
        /// 判断记录是否已经存在
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<UserCinemaEntity> GetUserCinemaByUserIdAndCinemaCodeAsync(int UserId,string CinemaCode)
        {
            return await _userCinemaRepository.Query.Where(x => x.UserId == UserId&&x.CinemaCode==CinemaCode).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 根据ID获取接入商影院
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<UserCinemaEntity> GetUserCinemaByIdAsync(int Id)
        {
            return await _userCinemaRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        public async Task<IPageList<UserCinemaViewEntity>> GetUserCinemasPagedAsync(int UserId,string CinemaCode, string CinemaName, string keyword, int offset, int perPage)
        {
            var query = _userCinemaViewRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //接入商ID
            query.Where(x => x.UserId == UserId);
            //影院编码
            if (!string.IsNullOrEmpty(CinemaCode))
            {
                query.Where(x => x.CinemaCode == CinemaCode);
            }
            //影院名称
            if (!string.IsNullOrEmpty(CinemaName))
            {
                query.Where(x => x.CinemaName.Contains(CinemaName));
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.UserName.Contains(keyword) || x.CinemaAddress.Contains(keyword)
                    || x.DefaultUserName.Contains(keyword));
            }
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 更新接入商影院
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UserCinemaEntity entity)
        {
            await _userCinemaRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增接入商影院
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(UserCinemaEntity entity)
        {
            await _userCinemaRepository.InsertAsync(entity);
        }
        /// <summary>
        /// 删除接入商影院
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(UserCinemaEntity entity)
        {
            await _userCinemaRepository.DeleteAsync(entity);
        }
    }
}
