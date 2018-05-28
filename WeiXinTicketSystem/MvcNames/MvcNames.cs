using WeiXinTicketSystem.Controllers;

namespace WeiXinTicketSystem
{
    public static class MvcNames
    {
        /// <summary>
        /// 登陆页
        /// </summary>
        public static class Login
        {
            public static string Name { get { return nameof(Login); } }
            public static string Index { get { return nameof(LoginController.Index); } }
            public static string LogOut { get { return nameof(LoginController.LogOut); } }
        }

        /// <summary>
        /// 首页
        /// </summary>
        public static class Home
        {
            public static string Name { get { return nameof(Home); } }
            public static string Index { get { return nameof(HomeController.Index); } }
        }

        /// <summary>
        /// 用户
        /// </summary>
        public static class User
        {
            public static string Name { get { return nameof(User); } }
            public static string Index { get { return nameof(UserController.Index); } }
            public static string List { get { return nameof(UserController.List); } }
            public static string Create { get { return nameof(UserController.Create); } }
            public static string Update { get { return nameof(UserController.Update); } }
            public static string Delete { get { return nameof(UserController.Delete); } }
            public static string ModifyPassword { get { return nameof(UserController.ModifyPassword); } }
            public static string ChangePassword { get { return nameof(UserController.ChangePassword); } }
        }

        /// <summary>
        /// 角色
        /// </summary>
        public static class Role
        {
            public static string Name { get { return nameof(Role); } }
            public static string Index { get { return nameof(RoleController.Index); } }
            public static string List { get { return nameof(RoleController.List); } }
            public static string Create { get { return nameof(RoleController.Create); } }
            public static string Update { get { return nameof(RoleController.Update); } }
            public static string Delete { get { return nameof(RoleController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(RoleController._CreateOrUpdate); } }
        }

        public static class RolePermissions
        {
            public static string Name { get { return nameof(RolePermissions); } }
            public static string Index { get { return nameof(RolePermissionsController.Index); } }
            public static string List { get { return nameof(RolePermissionsController.List); } }
            public static string Create { get { return nameof(RolePermissionsController.Create); } }
            public static string Update { get { return nameof(RolePermissionsController.Update); } }
            public static string Delete { get { return nameof(RolePermissionsController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(RolePermissionsController._CreateOrUpdate); } }
        }
        /// <summary>
        /// 模块
        /// </summary>
        public static class Module
        {
            public static string Name { get { return nameof(Module); } }
            public static string Index { get { return nameof(ModuleController.Index); } }
            public static string List { get { return nameof(ModuleController.List); } }
            public static string Create { get { return nameof(ModuleController.Create); } }
            public static string Update { get { return nameof(ModuleController.Update); } }
            public static string Delete { get { return nameof(ModuleController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(ModuleController._CreateOrUpdate); } }
        }
        /// <summary>
        /// 影院
        /// </summary>
        public static class Cinema
        {
            public static string Name { get { return nameof(Cinema); } }
            public static string Index { get { return nameof(CinemaController.Index); } }
            public static string List { get { return nameof(CinemaController.List); } }
            public static string Create { get { return nameof(CinemaController.Create); } }
            public static string Update { get { return nameof(CinemaController.Update); } }
            public static string Delete { get { return nameof(CinemaController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(CinemaController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 影院支付配置
        /// </summary>
        public static class PaySettings
        {
            public static string Name { get { return nameof(PaySettings); } }
            public static string Index { get { return nameof(PaySettingsController.Index); } }
            public static string List { get { return nameof(PaySettingsController.List); } }
            public static string Create { get { return nameof(PaySettingsController.Create); } }
            public static string Update { get { return nameof(PaySettingsController.Update); } }
            public static string Delete { get { return nameof(PaySettingsController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(PaySettingsController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 影院预付款配置
        /// </summary>
        public static class PrePaySettings
        {
            public static string Name { get { return nameof(PrePaySettings); } }
            public static string Index { get { return nameof(PrePaySettingsController.Index); } }
            public static string List { get { return nameof(PrePaySettingsController.List); } }
            public static string Create { get { return nameof(PrePaySettingsController.Create); } }
            public static string Update { get { return nameof(PrePaySettingsController.Update); } }
            public static string Delete { get { return nameof(PrePaySettingsController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(PrePaySettingsController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 影院系统对接账号配置
        /// </summary>
        public static class TicketSystemAccount
        {
            public static string Name { get { return nameof(TicketSystemAccount); } }
            public static string Index { get { return nameof(TicketSystemAccountController.Index); } }
            public static string List { get { return nameof(TicketSystemAccountController.List); } }
            public static string Create { get { return nameof(TicketSystemAccountController.Create); } }
            public static string Update { get { return nameof(TicketSystemAccountController.Update); } }
            public static string Delete { get { return nameof(TicketSystemAccountController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(TicketSystemAccountController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 会员卡
        /// </summary>
        public static class MemberCard
        {
            public static string Name { get { return nameof(MemberCard); } }
            public static string Index { get { return nameof(MemberCardController.Index); } }
            public static string List { get { return nameof(MemberCardController.List); } }
            public static string Create { get { return nameof(MemberCardController.Create); } }
            public static string Update { get { return nameof(MemberCardController.Update); } }
            public static string Delete { get { return nameof(MemberCardController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(MemberCardController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 会员卡
        /// </summary>
        public static class TicketUsers
        {
            public static string Name { get { return nameof(TicketUsers); } }
            public static string Index { get { return nameof(TicketUsersController.Index); } }
            public static string List { get { return nameof(TicketUsersController.List); } }
           
        }

        /// <summary>
        /// 影片信息
        /// </summary>
        public static class FilmInfo
        {
            public static string Name { get { return nameof(FilmInfo); } }
            public static string Index { get { return nameof(FilmInfoController.Index); } }
            public static string List { get { return nameof(FilmInfoController.List); } }
            public static string Create { get { return nameof(FilmInfoController.Create); } }
            public static string Update { get { return nameof(FilmInfoController.Update); } }
            public static string Delete { get { return nameof(FilmInfoController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(FilmInfoController._CreateOrUpdate); } }
            public static string UpdateFilm { get { return nameof(FilmInfoController.UpdateFilm); } }
        }
    }
}