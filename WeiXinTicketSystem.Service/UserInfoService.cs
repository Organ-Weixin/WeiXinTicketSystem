using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Models;
using System.Threading.Tasks;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System.Collections.Generic;
using WeiXinTicketSystem.Entity.Models.PageList;

namespace WeiXinTicketSystem.Service
{
    public class UserInfoService
    {
        #region ctor
        private readonly IRepository<UserInfoEntity> _userInfoRepository;

        public UserInfoService()
        {
            //TODO: 移除内部依赖
            _userInfoRepository = new Repository<UserInfoEntity>();
        }
        #endregion

        /// <summary>
        /// 根据用户名和密码获取用户信息（同步）
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public UserInfoEntity GetUserInfoByUserCredential(string Username, string Password)
        {
            return _userInfoRepository.Query.Where(
                x => x.UserName == Username && x.Password == Password && !x.IsDel).SingleOrDefault();
        }

        /// <summary>
        /// 根据用户名和密码获取用户信息（异步）
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<UserInfoEntity> GetUserInfoByUserCredentialAsync(string Username, string Password)
        {
            return await _userInfoRepository.Query.Where(
                x => x.UserName == Username && x.Password == Password && !x.IsDel).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 根据ID获取接入商
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<UserInfoEntity> GetUserInfoByIdAsync(int Id)
        {
            return await _userInfoRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 根据登录名获取接入商
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task<UserInfoEntity> GettUserInfoByUserNameAsync(string UserName)
        {
            return await _userInfoRepository.Query.Where(x => x.UserName == UserName).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 获取所有接入商列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UserInfoEntity>> GetAllUserInfosAsync()
        {
            return await _userInfoRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        public async Task<IPageList<UserInfoEntity>> GetUserInfosPagedAsync(string UserName,string Company, string keyword, int offset, int perPage)
        {
            var query = _userInfoRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //登录名
            if (!string.IsNullOrEmpty(UserName))
            {
                query.Where(x => x.UserName == UserName);
            }
            //公司名称
            if (!string.IsNullOrEmpty(Company))
            {
                query.Where(x => x.Company.Contains(Company));
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Password.Contains(keyword) || x.Address.Contains(keyword)
                    || x.Tel.Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 更新接入商
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UserInfoEntity entity)
        {
            await _userInfoRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增接入商
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(UserInfoEntity entity)
        {
            await _userInfoRepository.InsertAsync(entity);
        }
    }
}
