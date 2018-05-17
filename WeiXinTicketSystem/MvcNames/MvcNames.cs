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
    }
}