using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Service
{
    public class SystemRoleService
    {
        private readonly IRepository<SystemRoleEntity> _roleRepository;
        private readonly IRepository<SystemRolePermissionEntity> _rolePerssionRepository;
        private readonly IRepository<SystemMenuViewEntity> _systemMenuViewRepository;

        #region ctor
        public SystemRoleService()
        {
            //TODO: 移除内部依赖
            _roleRepository = new Repository<SystemRoleEntity>();
            _rolePerssionRepository = new Repository<SystemRolePermissionEntity>();
            _systemMenuViewRepository = new Repository<SystemMenuViewEntity>();
        }
        #endregion

        /// <summary>
        /// 获取当前用户所创建的角色
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<IPageList<SystemRoleEntity>> GetRolesByUserIdAsync(
            int userId, int offset, int perPage, string keyword)
        {
            var query = _roleRepository.Query.Where(x => x.CreateUserId == userId && !x.Deleted)
                .OrderBy(x => x.Id)
                .Skip(offset)
                .Take(perPage);

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }

            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取用户创建的所有角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IList<SystemRoleEntity>> GetAllRolesAsync(int userId)
        {
            return await _roleRepository.Query.Where(x => x.CreateUserId == userId && !x.Deleted).ToListAsync();
        }

        /// <summary>
        /// 根据Id和UserId获取角色
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SystemRoleEntity Get(int Id)
        {
            return _roleRepository.Query.Where(x => x.Id == Id
                && !x.Deleted).SingleOrDefault();
        }

        /// <summary>
        /// 根据Id和UserId获取角色
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<SystemRoleEntity> GetAsync(int Id)
        {
            return await _roleRepository.Query.Where(x => x.Id == Id 
                && !x.Deleted).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SystemRoleEntity entity)
        {
            await _roleRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(SystemRoleEntity entity)
        {
            await _roleRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 得到角色的所有菜单
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public IList<SystemMenuViewEntity> GetSystemMenuByRoleId(int RoleId)
        {
            return _systemMenuViewRepository.Query.Where(x => x.RoleId == RoleId).ToList();
        }
    }
}
