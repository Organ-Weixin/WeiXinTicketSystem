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
        /// 订单
        /// </summary>
        public static class Order
        {
            public static string Name { get { return nameof(Order); } }
            public static string Index { get { return nameof(OrderController.Index); } }
            public static string List { get { return nameof(OrderController.List); } }

            public static string ExportExcelDetail { get { return nameof(OrderController.ExportExcelDetail); } }
        }
        /// <summary>
        /// 影厅
        /// </summary>
        public static class Screen
        {
            public static string Name { get { return nameof(Screen); } }
            public static string Index { get { return nameof(ScreenController.Index); } }
            public static string List { get { return nameof(ScreenController.List); } }
            public static string Update { get { return nameof(ScreenController.Update); } }
            public static string _Update { get { return nameof(ScreenController._Update); } }
            public static string UdateScreenSeat { get { return nameof(ScreenController.UdateScreenSeat); } }
        }
        /// <summary>
        /// 排期
        /// </summary>
        public static class Session
        {
            public static string Name { get { return nameof(Session); } }
            public static string Index { get { return nameof(SessionController.Index); } }
            public static string List { get { return nameof(SessionController.List); } }
            public static string SetPrice { get { return nameof(SessionController.SetPrice); } }
            public static string Update { get { return nameof(SessionController.Update); } }
            public static string _Update { get { return nameof(SessionController._Update); } }
        }
        public static class PricePlan
        {
            public static string Name { get { return nameof(PricePlan); } }
            public static string Index { get { return nameof(PricePlanController.Index); } }
            public static string List { get { return nameof(PricePlanController.List); } }
            public static string Delete { get { return nameof(PricePlanController.Delete); } }
        }
        public static class PriceSettings
        {
            public static string Name { get { return nameof(PriceSettings); } }
            public static string Index { get { return nameof(PriceSettingsController.Index); } }
            public static string List { get { return nameof(PriceSettingsController.List); } }
            public static string Create { get { return nameof(PriceSettingsController.Create); } }
            public static string Update { get { return nameof(PriceSettingsController.Update); } }
            public static string Delete { get { return nameof(PriceSettingsController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(PriceSettingsController._CreateOrUpdate); } }
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
        /// 小程序账号
        /// </summary>
        public static class MiniProgramAccount
        {
            public static string Name { get { return nameof(MiniProgramAccount); } }
            public static string Index { get { return nameof(MiniProgramAccountController.Index); } }
            public static string List { get { return nameof(MiniProgramAccountController.List); } }
            public static string Create { get { return nameof(MiniProgramAccountController.Create); } }
            public static string Update { get { return nameof(MiniProgramAccountController.Update); } }
            public static string Delete { get { return nameof(MiniProgramAccountController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(MiniProgramAccountController._CreateOrUpdate); } }
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
            public static string GrabFilmData { get { return nameof(FilmInfoController.GrabFilmData); } }
        }

        /// <summary>
        /// 套餐
        /// </summary>
        public static class Snack
        {
            public static string Name { get { return nameof(Snack); } }
            public static string Index { get { return nameof(SnackController.Index); } }
            public static string List { get { return nameof(SnackController.List); } }
            public static string Create { get { return nameof(SnackController.Create); } }
            public static string Update { get { return nameof(SnackController.Update); } }
            public static string Delete { get { return nameof(SnackController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(SnackController._CreateOrUpdate); } }
        }
        /// <summary>
        /// 套餐类型
        /// </summary>
        public static class SnackType
        {
            public static string Name { get { return nameof(SnackType); } }
            public static string Index { get { return nameof(SnackTypeController.Index); } }
            public static string List { get { return nameof(SnackTypeController.List); } }
            public static string Create { get { return nameof(SnackTypeController.Create); } }
            public static string Update { get { return nameof(SnackTypeController.Update); } }
            public static string Delete { get { return nameof(SnackTypeController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(SnackTypeController._CreateOrUpdate); } }
        }
        /// <summary>
        /// 套餐订单
        /// </summary>
        public static class SnackOrder
        {
            public static string Name { get { return nameof(SnackOrder); } }
            public static string Index { get { return nameof(SnackOrderController.Index); } }
            public static string List { get { return nameof(SnackOrderController.List); } }

            public static string ExportExcelDetail { get { return nameof(SnackOrderController.ExportExcelDetail); } }
        }
		/// <summary>
        /// 影城会员卡设置
        /// </summary>
        public static class MemberCardSetting
        {
            public static string Name { get { return nameof(MemberCardSetting); } }
            public static string Index { get { return nameof(MemberCardSettingController.Index); } }
            public static string List { get { return nameof(MemberCardSettingController.List); } }
            public static string Create { get { return nameof(MemberCardSettingController.Create); } }
            public static string Update { get { return nameof(MemberCardSettingController.Update); } }
            public static string Delete { get { return nameof(MemberCardSettingController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(MemberCardSettingController._CreateOrUpdate); } }
        }
		/// <summary>
        /// 影院打印设置
        /// </summary>
        public static class PrintSetting
        {
            public static string Name { get { return nameof(PrintSetting); } }
            public static string Index { get { return nameof(PrintSettingController.Index); } }
            public static string List { get { return nameof(PrintSettingController.List); } }
            public static string Create { get { return nameof(PrintSettingController.Create); } }
            public static string Update { get { return nameof(PrintSettingController.Update); } }
            public static string Delete { get { return nameof(PrintSettingController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(PrintSettingController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 活动表
        /// </summary>
        public static class Activity
        {
            public static string Name { get { return nameof(Activity); } }
            public static string Index { get { return nameof(ActivityController.Index); } }
            public static string List { get { return nameof(ActivityController.List); } }
            public static string Create { get { return nameof(ActivityController.Create); } }
            public static string Update { get { return nameof(ActivityController.Update); } }
            public static string Delete { get { return nameof(ActivityController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(ActivityController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        public static class Banner
        {
            public static string Name { get { return nameof(Banner); } }
            public static string Index { get { return nameof(BannerController.Index); } }
            public static string List { get { return nameof(BannerController.List); } }
            public static string Create { get { return nameof(BannerController.Create); } }
            public static string Update { get { return nameof(BannerController.Update); } }
            public static string Delete { get { return nameof(BannerController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(BannerController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 优惠券
        /// </summary>
        public static class Conpon
        {
            public static string Name { get { return nameof(Conpon); } }
            public static string Index { get { return nameof(ConponController.Index); } }
            public static string List { get { return nameof(ConponController.List); } }
            public static string Create { get { return nameof(ConponController.Create); } }
            public static string Update { get { return nameof(ConponController.Update); } }
            public static string Delete { get { return nameof(ConponController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(ConponController._CreateOrUpdate); } }
            public static string GenerateCoupon { get { return nameof(ConponController.GenerateCoupon); } }
            public static string _GenerateCoupon { get { return nameof(ConponController._GenerateCoupon); } }
            public static string GroupList { get { return nameof(ConponController.GroupList); } }
            public static string CouponIndex { get { return nameof(ConponController.CouponIndex); } }
            public static string DeleteGroup { get { return nameof(ConponController.DeleteGroup); } }

        }

        /// <summary>
        /// 优惠券类型
        /// </summary>
        public static class ConponType
        {
            public static string Name { get { return nameof(ConponType); } }
            public static string Index { get { return nameof(ConponTypeController.Index); } }
            public static string List { get { return nameof(ConponTypeController.List); } }
            public static string Create { get { return nameof(ConponTypeController.Create); } }
            public static string Update { get { return nameof(ConponTypeController.Update); } }
            public static string Delete { get { return nameof(ConponTypeController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(ConponTypeController._CreateOrUpdate); } }

        }


        /// <summary>
        /// 印章
        /// </summary>
        public static class Stamp
        {
            public static string Name { get { return nameof(Stamp); } }
            public static string Index { get { return nameof(StampController.Index); } }
            public static string List { get { return nameof(StampController.List); } }
            public static string Create { get { return nameof(StampController.Create); } }
            public static string Update { get { return nameof(StampController.Update); } }
            public static string Delete { get { return nameof(StampController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(StampController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 链接地址
        /// </summary>
        public static class MiniProgramLinkUrl
        {
            public static string Name { get { return nameof(MiniProgramLinkUrl); } }
            public static string Index { get { return nameof(MiniProgramLinkUrlController.Index); } }
            public static string List { get { return nameof(MiniProgramLinkUrlController.List); } }
            public static string Create { get { return nameof(MiniProgramLinkUrlController.Create); } }
            public static string Update { get { return nameof(MiniProgramLinkUrlController.Update); } }
            public static string Delete { get { return nameof(MiniProgramLinkUrlController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(MiniProgramLinkUrlController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 赠送条件
        /// </summary>
        public static class GivingConditions
        {
            public static string Name { get { return nameof(GivingConditions); } }
            public static string Index { get { return nameof(GivingConditionsController.Index); } }
            public static string List { get { return nameof(GivingConditionsController.List); } }
            public static string Create { get { return nameof(GivingConditionsController.Create); } }
            public static string Update { get { return nameof(GivingConditionsController.Update); } }
            public static string Delete { get { return nameof(GivingConditionsController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(GivingConditionsController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 推荐等级
        /// </summary>
        public static class RecommendGrade
        {
            public static string Name { get { return nameof(RecommendGrade); } }
            public static string Index { get { return nameof(RecommendGradeController.Index); } }
            public static string List { get { return nameof(RecommendGradeController.List); } }
            public static string Create { get { return nameof(RecommendGradeController.Create); } }
            public static string Update { get { return nameof(RecommendGradeController.Update); } }
            public static string Delete { get { return nameof(RecommendGradeController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(RecommendGradeController._CreateOrUpdate); } }
        }
		
		/// <summary>
        /// 中间件
        /// </summary>
        public static class Middleware
        {
            public static string Name { get { return nameof(Middleware); } }
            public static string Index { get { return nameof(MiddlewareController.Index); } }
            public static string List { get { return nameof(MiddlewareController.List); } }
            public static string Create { get { return nameof(MiddlewareController.Create); } }
            public static string Update { get { return nameof(MiddlewareController.Update); } }
            public static string Delete { get { return nameof(MiddlewareController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(MiddlewareController._CreateOrUpdate); } }
        }
        /// <summary>
        /// 接入商
        /// </summary>
        public static class UserInfo
        {
            public static string Name { get { return nameof(UserInfo); } }
            public static string Index { get { return nameof(UserInfoController.Index); } }
            public static string List { get { return nameof(UserInfoController.List); } }
            public static string Create { get { return nameof(UserInfoController.Create); } }
            public static string Update { get { return nameof(UserInfoController.Update); } }
            public static string Delete { get { return nameof(UserInfoController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(UserInfoController._CreateOrUpdate); } }
        }
        /// <summary>
        /// 接入商可访问影院
        /// </summary>
        public static class UserCinema
        {
            public static string Name { get { return nameof(UserCinema); } }
            public static string Index { get { return nameof(UserCinemaController.Index); } }
            public static string List { get { return nameof(UserCinemaController.List); } }
            public static string Create { get { return nameof(UserCinemaController.Create); } }
            public static string Update { get { return nameof(UserCinemaController.Update); } }
            public static string Delete { get { return nameof(UserCinemaController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(UserCinemaController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 活动弹窗
        /// </summary>
        public static class ActivityPopup
        {
            public static string Name { get { return nameof(ActivityPopup); } }
            public static string Index { get { return nameof(ActivityPopupController.Index); } }
            public static string List { get { return nameof(ActivityPopupController.List); } }
            public static string Create { get { return nameof(ActivityPopupController.Create); } }
            public static string Update { get { return nameof(ActivityPopupController.Update); } }
            public static string Delete { get { return nameof(ActivityPopupController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(ActivityPopupController._CreateOrUpdate); } }
        }

        /// <summary>
        /// 会员充值设置
        /// </summary>
        public static class MemberChargeSetting
        {
            public static string Name { get { return nameof(MemberChargeSetting); } }
            public static string Index { get { return nameof(MemberChargeSettingController.Index); } }
            public static string List { get { return nameof(MemberChargeSettingController.List); } }
            public static string Create { get { return nameof(MemberChargeSettingController.Create); } }
            public static string Update { get { return nameof(MemberChargeSettingController.Update); } }
            public static string Delete { get { return nameof(MemberChargeSettingController.Delete); } }
            public static string _CreateOrUpdate { get { return nameof(MemberChargeSettingController._CreateOrUpdate); } }
        }
    }
}