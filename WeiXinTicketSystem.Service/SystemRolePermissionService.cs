using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System;
using System.Collections.Generic;
using WeiXinTicketSystem.Entity.Models.PageList;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Service
{
    public class SystemRolePermissionService
    {
        #region ctor
        private readonly IRepository<SystemRolePermissionEntity> _rolePermissionRepository;
        private readonly IRepository<SystemRolePermissionsViewEntity> _rolePermissionsViewRepository;
        public SystemRolePermissionService()
        {
            _rolePermissionRepository = new Repository<SystemRolePermissionEntity>();
            _rolePermissionsViewRepository = new Repository<SystemRolePermissionsViewEntity>();
        }
        #endregion
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<SystemRolePermissionsViewEntity>> GetRolePermissionsPagedAsync(int RoleId, string ModuleName, string keyword, int offset, int perPage)
        {
            var query = _rolePermissionsViewRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //角色ID
            query.Where(x => x.RoleId == RoleId);
            if (!string.IsNullOrEmpty(ModuleName))
            {
                query.Where(x => x.ModuleName.Contains(ModuleName));
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Permissions.Contains(keyword));
            }
            return await query.ToPageListAsync();
        }
        /// <summary>
        /// 根据id获取角色权限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<SystemRolePermissionEntity> GetRolePermissionByIdAsync(int Id)
        {
            return await _rolePermissionRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 判断当前角色下是否已经有该模块权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public async Task<SystemRolePermissionEntity> GetRolePermissionByRoleIdAndModuleId(int RoleId,int ModuleId)
        {
            return await _rolePermissionRepository.Query.Where(x => x.RoleId == RoleId && x.ModuleId == ModuleId).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SystemRolePermissionEntity entity)
        {
            await _rolePermissionRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增角色权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(SystemRolePermissionEntity entity)
        {
            await _rolePermissionRepository.InsertAsync(entity);
        }
        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(SystemRolePermissionEntity entity)
        {
            await _rolePermissionRepository.DeleteAsync(entity);
        }
    }
}
