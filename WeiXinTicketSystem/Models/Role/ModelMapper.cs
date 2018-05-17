using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System.Linq;

namespace WeiXinTicketSystem.Models.Role
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this SystemRoleEntity role)
        {
            return new
            {
                id = role.Id,
                name = role.Name,
                description = role.Description,
                created = role.Created.ToFormatString(),
                canedit = role.Type != SystemRoleTypeEnum.SystemAdmin,
                candelete = role.Type == SystemRoleTypeEnum.Customized
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="role"></param>
        /// <param name="model"></param>
        public static void MapFrom(this SystemRoleEntity role, CreateOrUpdateRoleViewModel model)
        {
            role.Name = model.Name;
            role.Description = model.Description;
            //role.Permissions = string.Join(",", model.Permissions);
        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        public static void MapFrom(this CreateOrUpdateRoleViewModel model, SystemRoleEntity role)
        {
            model.Id = role.Id;
            model.Name = role.Name;
            model.Description = role.Description;
            //model.Permissions = role.Permissions?.Split(',').Select(x => int.Parse(x)).ToList();
        }
    }
}