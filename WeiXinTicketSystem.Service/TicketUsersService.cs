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
    public class TicketUsersService
    {
        #region ctor
        private readonly IRepository<TicketUserEntity> _ticketUsersRepository;

        public TicketUsersService()
        {
            //TODO: 移除内部依赖
            _ticketUsersRepository = new Repository<TicketUserEntity>();
        }
        #endregion


        /// <summary>
        /// 根据手机号获取购票用户信息
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <returns></returns>
        public TicketUserEntity GetTicketUserByCinemaCode(string mobilePhone)
        {
            return _ticketUsersRepository.Query.Where(x => x.MobilePhone == mobilePhone).SingleOrDefault();
        }
        /// <summary>
        /// 根据openid获取购票用户信息
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public TicketUserEntity GetTicketUserByOpenID(string OpenID)
        {
            return _ticketUsersRepository.Query.Where(x => x.OpenID == OpenID).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有用户信息列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TicketUserEntity>> GetAllTicketUserAsync()
        {
            return await _ticketUsersRepository.Query.ToListAsync();
        }

        /// <summary>
        /// 后台分页读取用户信息信息
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <param name="nickName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<TicketUserEntity>> GetTicketUserPagedAsync(string mobilePhone, string nickName, string keyword, int offset, int perPage)
        {
            var query = _ticketUsersRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //手机号
            if (!string.IsNullOrEmpty(mobilePhone))
            {
                query.Where(x => x.MobilePhone == mobilePhone);
            }
            //用户昵称
            if (!string.IsNullOrEmpty(nickName))
            {
                query.Where(x => x.NickName.Contains(nickName));
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Country.Contains(keyword) || x.Province.Contains(keyword) || x.City.Contains(keyword));
            }
            
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取TicketUserEntity实体
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <returns></returns>
        public async Task<TicketUserEntity> GetTicketUserByCinemaCodeAsync(string mobilePhone)
        {
            return await _ticketUsersRepository.Query.Where(x => x.MobilePhone == mobilePhone).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据用户信息ID获取用户信息信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TicketUserEntity> GetTicketUserByIdAsync(int Id)
        {
            return await _ticketUsersRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TicketUserEntity entity)
        {
            _ticketUsersRepository.Update(entity);
        }


        /// <summary>
        /// 更新会员卡
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TicketUserEntity entity)
        {
            await _ticketUsersRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TicketUserEntity entity)
        {
            _ticketUsersRepository.Insert(entity);
        }

        /// <summary>
        /// 新增会员卡
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TicketUserEntity entity)
        {
            await _ticketUsersRepository.InsertAsync(entity);
        }


    }
}
