using WeiXinTicketSystem.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Util;

namespace WeiXinTicketSystem.Models.RolePermission
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this SystemRolePermissionsViewEntity rolepermission)
        {
            return new
            {
                id = rolepermission.Id,
                RoleId = rolepermission.RoleId,
                RoleName = rolepermission.RoleName,
                ModuleName = rolepermission.ModuleName,
                canRetrieve = rolepermission.Permissions.Contains("1") == true ? "√" : "×",
                canRetrieveClass = rolepermission.Permissions.Contains("1") == true ? "green" : "darkorange",
                canCreate = rolepermission.Permissions.Contains("2") == true ? "√" : "×",
                canCreateClass = rolepermission.Permissions.Contains("2") == true ? "green" : "darkorange",
                canUpdate = rolepermission.Permissions.Contains("3") == true ? "√" : "×",
                canUpdateClass = rolepermission.Permissions.Contains("3") == true ? "green" : "darkorange",
                canDelete = rolepermission.Permissions.Contains("4") == true ? "√" : "×",
                canDeleteClass = rolepermission.Permissions.Contains("4") == true ? "green" : "darkorange"
            };
        }


        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="role"></param>
        /// <param name="model"></param>
        public static void MapFrom(this SystemRolePermissionEntity rolePermission, CreateOrUpdateRolePermissionViewModel model)
        {
            rolePermission.RoleId = model.RoleId;
            rolePermission.ModuleId = int.Parse(model.ModuleId);
            rolePermission.Permissions = string.Join(",", model.Permissions);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        public static void MapFrom(this CreateOrUpdateRolePermissionViewModel model, SystemRolePermissionEntity rolePermission)
        {
            model.Id = rolePermission.Id;
            model.RoleId = rolePermission.RoleId;
            model.ModuleId = rolePermission.ModuleId.ToString();
            model.Permissions = rolePermission.Permissions?.Split(',').Select(x => int.Parse(x)).ToList();
        }
    }
}