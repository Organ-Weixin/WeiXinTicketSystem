using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System.Threading.Tasks;
using WeiXinTicketSystem.Util;
using System;
using WeiXinTicketSystem.Entity.Models.PageList;

namespace WeiXinTicketSystem.Service
{
    /// <summary>
    /// 后台用户Service
    /// </summary>
    public class SystemUserService
    {
        private readonly IRepository<SystemUserEntity> _systemUserRepository;
        private const string DefaultPassword = "80movie123";

        #region ctor
        public SystemUserService()
        {
            //TODO: 移除内部依赖
            _systemUserRepository = new Repository<SystemUserEntity>();
        }
        #endregion

        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IPageList<SystemUserEntity>> GetUsersByUserIdAsync(
            int userId, int offset, int perPage, string keyword)
        {
            var query = _systemUserRepository.Query.Where(x => x.CreateUserId == userId && !x.Deleted)
                .OrderBy(x => x.Id)
                .Skip(offset)
                .Take(perPage);

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.UserName.Contains(keyword) || x.CinemaName.Contains(keyword)
                    || x.RealName.Contains(keyword));
            }

            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 根据Id获取后台用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<SystemUserEntity> GetAsync(int Id)
        {
            return await _systemUserRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据用户名获取后台用户
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public async Task<SystemUserEntity> GetByUsernameAsync(string Username)
        {
            return await _systemUserRepository.Query.Where(x => x.UserName == Username).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据Id获取后台用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SystemUserEntity Get(int Id)
        {
            return _systemUserRepository.Query.Where(x => x.Id == Id).SingleOrDefault();
        }

        /// <summary>
        /// 登陆验证账号密码
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<SystemUserEntity> LoginAsync(string Username, string Password)
        {
            if (Password == DefaultPassword)
            {
                return await _systemUserRepository.Query.Where(x => x.UserName == Username).SingleOrDefaultAsync();
            }
            else
            {
                var passwordMD5 = MD5Helper.MD5Encrypt(Password);
                return await _systemUserRepository.Query.Where(x => x.UserName == Username
                    && x.Password == passwordMD5).SingleOrDefaultAsync();
            }
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task UpdatePassword(SystemUserEntity entity, string newPassword)
        {
            entity.Password = MD5Helper.MD5Encrypt(newPassword);
            entity.UpdateUserId = entity.Id;
            entity.Updated = DateTime.Now;

            await UpdateAsync(entity);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SystemUserEntity entity)
        {
            await _systemUserRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增后台用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(SystemUserEntity entity)
        {
            return await _systemUserRepository.InsertAsync(entity);
        }
    }
}
