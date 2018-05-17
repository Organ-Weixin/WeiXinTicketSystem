using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;

namespace WeiXinTicketSystem.Models.User
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this SystemUserEntity user)
        {
            return new
            {
                id = user.Id,
                username = user.UserName,
                realname = user.RealName,
                cinemaname = user.CinemaName,
                rolename = user.RoleName
            };
        }

        /// <summary>
        /// model To Entity
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        public static void MapFrom(this SystemUserEntity user, CreateUserViewModel model,
            CinemaEntity cinema, SystemRoleEntity role)
        {
            user.UserName = model.Username;
            user.Password = MD5Helper.MD5Encrypt(model.Password);
            user.CinemaCode = cinema.CinemaCode;
            user.CinemaName = cinema.CinemaName;
            user.RealName = model.RealName;
            user.RoleId = model.RoleId;
            user.RoleName = role.Name;
        }

        /// <summary>
        /// entity to model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        public static void MapFrom(this UpdateUserViewModel model, SystemUserEntity user)
        {
            model.Id = user.Id;
            model.CinemaCode = user.CinemaCode;
            model.Username = user.UserName;
            model.RealName = user.RealName;
            model.RoleId = user.RoleId;
        }
    }
}