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
    public class UserStampService
    {
        #region ctor
        private readonly IRepository<UserStampEntity> _userStampRepository;
        private readonly IRepository<ApiUserStampViewEntity> _apiUserStampViewRepository;

        public UserStampService()
        {
            //TODO: 移除内部依赖
            _userStampRepository = new Repository<UserStampEntity>();
            _apiUserStampViewRepository = new Repository<ApiUserStampViewEntity>();
        }
        #endregion

        /// <summary>
        /// 根据用户OpenID获取用户印章信息
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public IList<UserStampEntity> GetUserStampByOpenID(string OpenID)
        {
            return _userStampRepository.Query.Where(x => x.OpenID == OpenID).ToList();
        }

        /// <summary>
        /// 获取所有用户印章列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UserStampEntity>> GetAllUserStampAsync()
        {
            return await _userStampRepository.Query.ToListAsync();
        }

        /// <summary>
        /// 后台分页读取用户印章信息
        /// </summary>
        /// <param name="StampCode"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<UserStampEntity>> GetUserStampPagedAsync(string StampCode, string keyword, int offset, int perPage)
        {
            var query = _userStampRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //印章编码
            if (!string.IsNullOrEmpty(StampCode))
            {
                query.Where(x => x.StampCode == StampCode);
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.StampCode.Contains(keyword));
            }
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 根据用户OpenID获取用户印章信息(异步)
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public async Task<IList<UserStampEntity>> GetUserStampByOpenIDAsync(string OpenID)
        {
            return await _userStampRepository.Query.Where(x => x.OpenID == OpenID).ToListAsync();
        }

        /// <summary>
        /// 根据用户OpenID获取用户印章信息(异步)--视图
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public async Task<IList<ApiUserStampViewEntity>> GetApiUserStampViewByOpenIDAsync(string OpenID)
        {
            return await _apiUserStampViewRepository.Query.Where(x => x.OpenID == OpenID).ToListAsync();
        }

        /// <summary>
        /// 根据用户印章ID获取用户印章信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<UserStampEntity> GetUserStampByIdAsync(int Id)
        {
            return await _userStampRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(UserStampEntity entity)
        {
            _userStampRepository.Update(entity);
        }

        /// <summary>
        /// 更新用户印章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(UserStampEntity entity)
        {
            await _userStampRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增用户印章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(UserStampEntity entity)
        {
            await _userStampRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 新增用户印章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(UserStampEntity entity)
        {
            _userStampRepository.Insert(entity);
        }

    }
}
