// This file was automatically generated by the Dapper.SimpleCRUD T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `ModelGeneratorConnectionString`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=115.29.250.241;Initial Catalog=WeiXinTicketSys;User ID=sa;Password=80piao123`
//     Include Views:          `True`

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LambdaSqlBuilder;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.Entity.Models
{
    /// <summary>
    /// A class which represents the Gifts table.
    /// </summary>
    [Table("Gifts")]
    [SqlLamTable(Name = "Gifts")]
    public partial class GiftEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string Title { get; set; }
		public virtual string Details { get; set; }
		public virtual decimal? OriginalPrice { get; set; }
		public virtual decimal? Price { get; set; }
		public virtual string Image { get; set; }
		public virtual int? Stock { get; set; }
		public virtual DateTime? StartDate { get; set; }
		public virtual DateTime? EndDate { get; set; }
		public virtual YesOrNoEnum Status { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the FilmComments table.
    /// </summary>
    [Table("FilmComments")]
    [SqlLamTable(Name = "FilmComments")]
    public partial class FilmCommentEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string FilmCode { get; set; }
		public virtual string FilmName { get; set; }
		public virtual decimal Score { get; set; }
		public virtual string CommentContent { get; set; }
		public virtual string OpenID { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual bool Deleted { get; set; }
	}

    /// <summary>
    /// A class which represents the SessionPriceSettings table.
    /// </summary>
    [Table("SessionPriceSettings")]
    [SqlLamTable(Name = "SessionPriceSettings")]
    public partial class SessionPriceSettingEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string Code { get; set; }
		public virtual PricePlanTypeEnum Type { get; set; }
		public virtual decimal Price { get; set; }
	}

    /// <summary>
    /// A class which represents the Conpons table.
    /// </summary>
    [Table("Conpons")]
    [SqlLamTable(Name = "Conpons")]
    public partial class ConponEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual ConponTypeEnum ConponType { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string OpenID { get; set; }
		public virtual decimal? Price { get; set; }
		public virtual string ConponCode { get; set; }
		public virtual DateTime? ValidityDate { get; set; }
		public virtual ConponStatusEnum Status { get; set; }
		public virtual DateTime? UseDate { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool Deleted { get; set; }
		public virtual string Title { get; set; }
		public virtual string Image { get; set; }
	}

    /// <summary>
    /// A class which represents the TicketUsers table.
    /// </summary>
    [Table("TicketUsers")]
    [SqlLamTable(Name = "TicketUsers")]
    public partial class TicketUserEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string MobilePhone { get; set; }
		public virtual string OpenID { get; set; }
		public virtual string VerifyCode { get; set; }
		public virtual YesOrNoEnum IsActive { get; set; }
		public virtual DateTime? Created { get; set; }
		public virtual string NickName { get; set; }
		public virtual UserSexEnum Sex { get; set; }
		public virtual string Country { get; set; }
		public virtual string Province { get; set; }
		public virtual string City { get; set; }
		public virtual string HeadImgUrl { get; set; }
	}

    /// <summary>
    /// A class which represents the CinemaPriceSettings table.
    /// </summary>
    [Table("CinemaPriceSettings")]
    [SqlLamTable(Name = "CinemaPriceSettings")]
    public partial class CinemaPriceSettingEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual int? WeChatDiscount { get; set; }
		public virtual decimal FackPriceUpperLimit { get; set; }
		public virtual YesOrNoEnum? IsSellByStandardPrice { get; set; }
		public virtual decimal? LoveSeatPriceDifferences { get; set; }
		public virtual decimal Fee { get; set; }
		public virtual decimal MemberFee { get; set; }
		public virtual CinemaFeePayTypeEnum FeePayType { get; set; }
		public virtual CinemaFeeGatherTypeEnum FeeGatherType { get; set; }
	}

    /// <summary>
    /// A class which represents the FilmInfo table.
    /// </summary>
    [Table("FilmInfo")]
    [SqlLamTable(Name = "FilmInfo")]
    public partial class FilmInfoEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string FilmCode { get; set; }
		public virtual string FilmName { get; set; }
		public virtual string Version { get; set; }
		public virtual string Duration { get; set; }
		public virtual DateTime? PublishDate { get; set; }
		public virtual string Publisher { get; set; }
		public virtual string Producer { get; set; }
		public virtual string Director { get; set; }
		public virtual string Cast { get; set; }
		public virtual string Introduction { get; set; }
		public virtual decimal? Score { get; set; }
		public virtual string Area { get; set; }
		public virtual string Type { get; set; }
		public virtual string Language { get; set; }
		public virtual YesOrNoEnum Status { get; set; }
		public virtual string Image { get; set; }
		public virtual string Trailer { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the CinemaWeChatOfficialAccounts table.
    /// </summary>
    [Table("CinemaWeChatOfficialAccounts")]
    [SqlLamTable(Name = "CinemaWeChatOfficialAccounts")]
    public partial class CinemaWeChatOfficialAccountEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual string OriginalID { get; set; }
		public virtual string Token { get; set; }
		public virtual string AppId { get; set; }
		public virtual string AppSecret { get; set; }
		public virtual string Micro_signal { get; set; }
		public virtual WeChatOfficialAccountTypeEnum? AccountType { get; set; }
		public virtual string vice_OriginalID { get; set; }
		public virtual string vice_Token { get; set; }
		public virtual string vice_AppId { get; set; }
		public virtual string vice_AppSecret { get; set; }
		public virtual string vice_Micro_signal { get; set; }
	}

    /// <summary>
    /// A class which represents the SystemMenuView view.
    /// </summary>
    [Table("SystemMenuView")]
    [SqlLamTable(Name = "SystemMenuView")]
    public partial class SystemMenuViewEntity : EntityBase
    {
		public virtual int Id { get; set; }
		public virtual int RoleId { get; set; }
		public virtual string Permissions { get; set; }
		public virtual string ModuleName { get; set; }
		public virtual string ModuleIcon { get; set; }
		public virtual int ModuleId { get; set; }
		public virtual int ModuleParentId { get; set; }
		public virtual int? ModuleSequence { get; set; }
		public virtual string ModuleFlag { get; set; }
	}

    /// <summary>
    /// A class which represents the CinemaMemberCardSettings table.
    /// </summary>
    [Table("CinemaMemberCardSettings")]
    [SqlLamTable(Name = "CinemaMemberCardSettings")]
    public partial class CinemaMemberCardSettingEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual YesOrNoEnum IsCardUse { get; set; }
		public virtual YesOrNoEnum IsCardRegister { get; set; }
		public virtual YesOrNoEnum IsCardReCharge { get; set; }
		public virtual MemberCardTypeEnum CardType { get; set; }
		public virtual string ThirdMemberUrl { get; set; }
		public virtual string InitialCardPassword { get; set; }
		public virtual int? OscarDiscount { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the CinemaPrintSettings table.
    /// </summary>
    [Table("CinemaPrintSettings")]
    [SqlLamTable(Name = "CinemaPrintSettings")]
    public partial class CinemaPrintSettingEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual YesOrNoEnum IsPrintName { get; set; }
		public virtual YesOrNoEnum IsCustomTicketTemplet { get; set; }
		public virtual YesOrNoEnum IsCustomPackageTemplet { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the SystemUsers table.
    /// </summary>
    [Table("SystemUsers")]
    [SqlLamTable(Name = "SystemUsers")]
    public partial class SystemUserEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string UserName { get; set; }
		public virtual string Password { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual string RealName { get; set; }
		public virtual int RoleId { get; set; }
		public virtual string RoleName { get; set; }
		public virtual int? CreateUserId { get; set; }
		public virtual DateTime? Created { get; set; }
		public virtual int? UpdateUserId { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool Deleted { get; set; }
	}

    /// <summary>
    /// A class which represents the SystemRoles table.
    /// </summary>
    [Table("SystemRoles")]
    [SqlLamTable(Name = "SystemRoles")]
    public partial class SystemRoleEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Description { get; set; }
		public virtual SystemRoleTypeEnum? Type { get; set; }
		public virtual int? CreateUserId { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool Deleted { get; set; }
	}

    /// <summary>
    /// A class which represents the SystemRolePermissionsView view.
    /// </summary>
    [Table("SystemRolePermissionsView")]
    [SqlLamTable(Name = "SystemRolePermissionsView")]
    public partial class SystemRolePermissionsViewEntity : EntityBase
    {
		public virtual int Id { get; set; }
		public virtual int? RoleId { get; set; }
		public virtual string RoleName { get; set; }
		public virtual string ModuleName { get; set; }
		public virtual string Permissions { get; set; }
	}

    /// <summary>
    /// A class which represents the SystemRolePermissions table.
    /// </summary>
    [Table("SystemRolePermissions")]
    [SqlLamTable(Name = "SystemRolePermissions")]
    public partial class SystemRolePermissionEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual int RoleId { get; set; }
		public virtual int ModuleId { get; set; }
		public virtual string Permissions { get; set; }
	}

    /// <summary>
    /// A class which represents the AdminSnacksView view.
    /// </summary>
    [Table("AdminSnacksView")]
    [SqlLamTable(Name = "AdminSnacksView")]
    public partial class AdminSnacksViewEntity : EntityBase
    {
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string SnackCode { get; set; }
		public virtual int? TypeId { get; set; }
		public virtual string TypeName { get; set; }
		public virtual string SnackName { get; set; }
		public virtual string Remark { get; set; }
		public virtual decimal StandardPrice { get; set; }
		public virtual decimal SalePrice { get; set; }
		public virtual SnackStatusEnum Status { get; set; }
		public virtual int? Stock { get; set; }
		public virtual DateTime? ExpDate { get; set; }
		public virtual bool IsDel { get; set; }
		public virtual bool? IsRecommand { get; set; }
		public virtual string CinemaName { get; set; }
	}

    /// <summary>
    /// A class which represents the ScreenInfo table.
    /// </summary>
    [Table("ScreenInfo")]
    [SqlLamTable(Name = "ScreenInfo")]
    public partial class ScreenInfoEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string ScreenCode { get; set; }
		public virtual string ScreenName { get; set; }
		public virtual DateTime? UpdateTime { get; set; }
		public virtual int? SeatCount { get; set; }
		public virtual string Type { get; set; }
	}

    /// <summary>
    /// A class which represents the ScreenSeatInfo table.
    /// </summary>
    [Table("ScreenSeatInfo")]
    [SqlLamTable(Name = "ScreenSeatInfo")]
    public partial class ScreenSeatInfoEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string ScreenCode { get; set; }
		public virtual string SeatCode { get; set; }
		public virtual string GroupCode { get; set; }
		public virtual string RowNum { get; set; }
		public virtual string ColumnNum { get; set; }
		public virtual int XCoord { get; set; }
		public virtual int YCoord { get; set; }
		public virtual string Status { get; set; }
		public virtual string LoveFlag { get; set; }
		public virtual string Type { get; set; }
		public virtual DateTime UpdateTime { get; set; }
	}

    /// <summary>
    /// A class which represents the Snacks table.
    /// </summary>
    [Table("Snacks")]
    [SqlLamTable(Name = "Snacks")]
    public partial class SnackEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string SnackCode { get; set; }
		public virtual int TypeId { get; set; }
		public virtual string SnackName { get; set; }
		public virtual string Remark { get; set; }
		public virtual decimal StandardPrice { get; set; }
		public virtual decimal SalePrice { get; set; }
		public virtual SnackStatusEnum Status { get; set; }
		public virtual int Stock { get; set; }
		public virtual DateTime? ExpDate { get; set; }
		public virtual bool IsDel { get; set; }
		public virtual bool? IsRecommand { get; set; }
		public virtual string Image { get; set; }
	}

    /// <summary>
    /// A class which represents the SnackTypes table.
    /// </summary>
    [Table("SnackTypes")]
    [SqlLamTable(Name = "SnackTypes")]
    public partial class SnackTypeEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string TypeName { get; set; }
		public virtual string Remark { get; set; }
		public virtual bool IsDel { get; set; }
		public virtual string Image { get; set; }
	}

    /// <summary>
    /// A class which represents the AdminSnackTypesView view.
    /// </summary>
    [Table("AdminSnackTypesView")]
    [SqlLamTable(Name = "AdminSnackTypesView")]
    public partial class AdminSnackTypesViewEntity : EntityBase
    {
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual string TypeName { get; set; }
		public virtual int? SnacksCount { get; set; }
		public virtual string Remark { get; set; }
		public virtual bool IsDel { get; set; }
		public virtual string Image { get; set; }
	}

    /// <summary>
    /// A class which represents the AdminSnackOrdersView view.
    /// </summary>
    [Table("AdminSnackOrdersView")]
    [SqlLamTable(Name = "AdminSnackOrdersView")]
    public partial class AdminSnackOrdersViewEntity : EntityBase
    {
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string OrderCode { get; set; }
		public virtual string MobilePhone { get; set; }
		public virtual int? SnacksCount { get; set; }
		public virtual decimal TotalPrice { get; set; }
		public virtual DateTime? SubmitTime { get; set; }
		public virtual string VoucherCode { get; set; }
		public virtual SnackOrderStatusEnum OrderStatus { get; set; }
		public virtual DateTime? RevokeTime { get; set; }
		public virtual DateTime? PickupTime { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual DateTime? Created { get; set; }
		public virtual string DeliveryAddress { get; set; }
		public virtual DateTime? SendTime { get; set; }
		public virtual bool? OrderPayFlag { get; set; }
		public virtual byte? OrderPayType { get; set; }
		public virtual DateTime? OrderPayTime { get; set; }
		public virtual string OrderTradeNo { get; set; }
		public virtual bool? IsUseConpons { get; set; }
		public virtual string ConponsID { get; set; }
		public virtual string CinemaName { get; set; }
	}

    /// <summary>
    /// A class which represents the DefaultActions table.
    /// </summary>
    [Table("DefaultActions")]
    [SqlLamTable(Name = "DefaultActions")]
    public partial class DefaultActionEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string ActionName { get; set; }
		public virtual DefaultActionTypeEnum? ActionType { get; set; }
		public virtual string ActionNewsTitle { get; set; }
		public virtual string ActionNewsCover { get; set; }
		public virtual string ActionNewsDesc { get; set; }
		public virtual string ActionUrl { get; set; }
		public virtual bool? IsActive { get; set; }
	}

    /// <summary>
    /// A class which represents the CinemaCustomMenu table.
    /// </summary>
    [Table("CinemaCustomMenu")]
    [SqlLamTable(Name = "CinemaCustomMenu")]
    public partial class CinemaCustomMenuEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string MenuName { get; set; }
		public virtual CustomMenuTypeEnum? MenuType { get; set; }
		public virtual string MenuKey { get; set; }
		public virtual string MenuUrl { get; set; }
		public virtual int? MenuParentId { get; set; }
		public virtual int? MenuSequence { get; set; }
		public virtual CustomMenuReTypeEnum? MenuReType { get; set; }
		public virtual string MenuReText { get; set; }
		public virtual int? MenuReArticleID { get; set; }
		public virtual bool? IsActive { get; set; }
		public virtual bool? IsPublish { get; set; }
		public virtual DateTime? PublishTime { get; set; }
		public virtual int? MenuDefaultActionId { get; set; }
	}

    /// <summary>
    /// A class which represents the Activity table.
    /// </summary>
    [Table("Activity")]
    [SqlLamTable(Name = "Activity")]
    public partial class ActivityEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string Title { get; set; }
		public virtual string Image { get; set; }
		public virtual string ActivityContent { get; set; }
		public virtual DateTime? StartDate { get; set; }
		public virtual DateTime? EndDate { get; set; }
		public virtual YesOrNoEnum Status { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the SystemModule table.
    /// </summary>
    [Table("SystemModule")]
    [SqlLamTable(Name = "SystemModule")]
    public partial class SystemModuleEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string ModuleName { get; set; }
		public virtual string ModuleIcon { get; set; }
		public virtual int ModuleParentId { get; set; }
		public virtual int? ModuleSequence { get; set; }
		public virtual string ModuleFlag { get; set; }
		public virtual int? CreateUserId { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool Deleted { get; set; }
	}

    /// <summary>
    /// A class which represents the MemberCard table.
    /// </summary>
    [Table("MemberCard")]
    [SqlLamTable(Name = "MemberCard")]
    public partial class MemberCardEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string OpenID { get; set; }
		public virtual string CardNo { get; set; }
		public virtual string CardPassword { get; set; }
		public virtual decimal? Balance { get; set; }
		public virtual int? Score { get; set; }
		public virtual MemberCardGradeEnum MemberGrade { get; set; }
		public virtual MemberCardStatusEnum Status { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the Orders table.
    /// </summary>
    [Table("Orders")]
    [SqlLamTable(Name = "Orders")]
    public partial class OrderEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string SessionCode { get; set; }
		public virtual string ScreenCode { get; set; }
		public virtual DateTime SessionTime { get; set; }
		public virtual string FilmCode { get; set; }
		public virtual string FilmName { get; set; }
		public virtual int TicketCount { get; set; }
		public virtual decimal TotalPrice { get; set; }
		public virtual decimal TotalFee { get; set; }
		public virtual decimal TotalSalePrice { get; set; }
		public virtual decimal? TotalLoveSeatDifferences { get; set; }
		public virtual CinemaFeePayTypeEnum? FeePayType { get; set; }
		public virtual decimal? TotalGuestPayFee { get; set; }
		public virtual OrderStatusEnum OrderStatus { get; set; }
		public virtual string MobilePhone { get; set; }
		public virtual DateTime? LockTime { get; set; }
		public virtual DateTime? AutoUnlockDatetime { get; set; }
		public virtual string LockOrderCode { get; set; }
		public virtual DateTime? SubmitTime { get; set; }
		public virtual string SubmitOrderCode { get; set; }
		public virtual string PrintNo { get; set; }
		public virtual string VerifyCode { get; set; }
		public virtual bool? PrintStatus { get; set; }
		public virtual DateTime? PrintTime { get; set; }
		public virtual string RefundBatchNo { get; set; }
		public virtual DateTime? RefundTime { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool Deleted { get; set; }
		public virtual string ErrorMessage { get; set; }
		public virtual string SerialNum { get; set; }
		public virtual bool? OrderPayFlag { get; set; }
		public virtual PayTypeEnum OrderPayType { get; set; }
		public virtual DateTime? OrderPayTime { get; set; }
		public virtual string PayType { get; set; }
		public virtual string Printpassword { get; set; }
		public virtual string OrderTradeNo { get; set; }
		public virtual string CardNo { get; set; }
		public virtual string PaySeqNo { get; set; }
		public virtual string OpenID { get; set; }
	}

    /// <summary>
    /// A class which represents the AdminMemberCardView view.
    /// </summary>
    [Table("AdminMemberCardView")]
    [SqlLamTable(Name = "AdminMemberCardView")]
    public partial class AdminMemberCardViewEntity : EntityBase
    {
		public virtual string NickName { get; set; }
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string OpenID { get; set; }
		public virtual string CardNo { get; set; }
		public virtual string CardPassword { get; set; }
		public virtual decimal? Balance { get; set; }
		public virtual int? Score { get; set; }
		public virtual MemberCardGradeEnum MemberGrade { get; set; }
		public virtual MemberCardStatusEnum Status { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the AdminConponView view.
    /// </summary>
    [Table("AdminConponView")]
    [SqlLamTable(Name = "AdminConponView")]
    public partial class AdminConponViewEntity : EntityBase
    {
		public virtual ConponTypeEnum ConponType { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string OpenID { get; set; }
		public virtual decimal? Price { get; set; }
		public virtual string ConponCode { get; set; }
		public virtual DateTime? ValidityDate { get; set; }
		public virtual ConponStatusEnum Status { get; set; }
		public virtual DateTime? UseDate { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool Deleted { get; set; }
		public virtual string NickName { get; set; }
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Image { get; set; }
	}

    /// <summary>
    /// A class which represents the AdminOrderView view.
    /// </summary>
    [Table("AdminOrderView")]
    [SqlLamTable(Name = "AdminOrderView")]
    public partial class AdminOrderViewEntity : EntityBase
    {
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string SessionCode { get; set; }
		public virtual string ScreenCode { get; set; }
		public virtual DateTime SessionTime { get; set; }
		public virtual string FilmCode { get; set; }
		public virtual string FilmName { get; set; }
		public virtual int TicketCount { get; set; }
		public virtual decimal TotalPrice { get; set; }
		public virtual decimal TotalFee { get; set; }
		public virtual decimal TotalSalePrice { get; set; }
		public virtual decimal? TotalLoveSeatDifferences { get; set; }
		public virtual OrderStatusEnum OrderStatus { get; set; }
		public virtual byte? FeePayType { get; set; }
		public virtual decimal? TotalGuestPayFee { get; set; }
		public virtual string MobilePhone { get; set; }
		public virtual DateTime? LockTime { get; set; }
		public virtual DateTime? AutoUnlockDatetime { get; set; }
		public virtual string LockOrderCode { get; set; }
		public virtual DateTime? SubmitTime { get; set; }
		public virtual string SubmitOrderCode { get; set; }
		public virtual string PrintNo { get; set; }
		public virtual string VerifyCode { get; set; }
		public virtual bool? PrintStatus { get; set; }
		public virtual DateTime? PrintTime { get; set; }
		public virtual DateTime? RefundTime { get; set; }
		public virtual string RefundBatchNo { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool Deleted { get; set; }
		public virtual string ErrorMessage { get; set; }
		public virtual string SerialNum { get; set; }
		public virtual string PayType { get; set; }
		public virtual bool? OrderPayFlag { get; set; }
		public virtual byte OrderPayType { get; set; }
		public virtual DateTime? OrderPayTime { get; set; }
		public virtual string Printpassword { get; set; }
		public virtual string PaySeqNo { get; set; }
		public virtual string OrderTradeNo { get; set; }
		public virtual string CardNo { get; set; }
		public virtual string OpenID { get; set; }
		public virtual string CinemaName { get; set; }
	}

    /// <summary>
    /// A class which represents the AdminScreenView view.
    /// </summary>
    [Table("AdminScreenView")]
    [SqlLamTable(Name = "AdminScreenView")]
    public partial class AdminScreenViewEntity : EntityBase
    {
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual string ScreenCode { get; set; }
		public virtual string ScreenName { get; set; }
		public virtual DateTime? UpdateTime { get; set; }
		public virtual int? SeatCount { get; set; }
		public virtual string Type { get; set; }
	}

    /// <summary>
    /// A class which represents the SnackOrderDetails table.
    /// </summary>
    [Table("SnackOrderDetails")]
    [SqlLamTable(Name = "SnackOrderDetails")]
    public partial class SnackOrderDetailEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual int OrderId { get; set; }
		public virtual string SnackCode { get; set; }
		public virtual decimal StandardPrice { get; set; }
		public virtual decimal SalePrice { get; set; }
		public virtual int Number { get; set; }
		public virtual decimal SubTotalPrice { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool? Deleted { get; set; }
	}

    /// <summary>
    /// A class which represents the CinemaPaymentSettings table.
    /// </summary>
    [Table("CinemaPaymentSettings")]
    [SqlLamTable(Name = "CinemaPaymentSettings")]
    public partial class CinemaPaymentSettingEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual YesOrNoEnum IsUseAlipay { get; set; }
		public virtual string AlipaySellerEmail { get; set; }
		public virtual string AlipayPartner { get; set; }
		public virtual string AlipayKey { get; set; }
		public virtual string AlipayAPPID { get; set; }
		public virtual YesOrNoEnum IsUseBfbpay { get; set; }
		public virtual string BfbpaySpno { get; set; }
		public virtual string BfbpayKey { get; set; }
		public virtual YesOrNoEnum IsUseWxpay { get; set; }
		public virtual string WxpayMchId { get; set; }
		public virtual string WxpayKey { get; set; }
		public virtual string WxpayRefundCert { get; set; }
		public virtual YesOrNoEnum IsUserMemberCard { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the CinemaPrePaySettings table.
    /// </summary>
    [Table("CinemaPrePaySettings")]
    [SqlLamTable(Name = "CinemaPrePaySettings")]
    public partial class CinemaPrePaySettingEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual YesOrNoEnum IsPrePay { get; set; }
		public virtual decimal? SurplusPayment { get; set; }
		public virtual decimal? LowestPrePayment { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the Cinema table.
    /// </summary>
    [Table("Cinema")]
    [SqlLamTable(Name = "Cinema")]
    public partial class CinemaEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual CinemaTypeEnum TicketSystem { get; set; }
		public virtual string ContactName { get; set; }
		public virtual string ContactMobile { get; set; }
		public virtual TheaterChainEnum? TheaterChain { get; set; }
		public virtual string Address { get; set; }
		public virtual DateTime? Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool IsDel { get; set; }
		public virtual CinemaStatusEnum Status { get; set; }
		public virtual string DingXinId { get; set; }
		public virtual string YueKeId { get; set; }
		public virtual double? Latitude { get; set; }
		public virtual double? Longitude { get; set; }
		public virtual YesOrNoEnum OpenSnacks { get; set; }
	}

    /// <summary>
    /// A class which represents the MemberCardChargeRecords table.
    /// </summary>
    [Table("MemberCardChargeRecords")]
    [SqlLamTable(Name = "MemberCardChargeRecords")]
    public partial class MemberCardChargeRecordEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual int? MemberCardId { get; set; }
		public virtual string CardNo { get; set; }
		public virtual PayTypeEnum? ChargeType { get; set; }
		public virtual decimal? ChargeAmount { get; set; }
		public virtual string ChargeOrderCode { get; set; }
		public virtual string TradeNo { get; set; }
		public virtual bool? Status { get; set; }
		public virtual DateTime? Created { get; set; }
	}

    /// <summary>
    /// A class which represents the SnackOrders table.
    /// </summary>
    [Table("SnackOrders")]
    [SqlLamTable(Name = "SnackOrders")]
    public partial class SnackOrderEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string OrderCode { get; set; }
		public virtual string MobilePhone { get; set; }
		public virtual int SnacksCount { get; set; }
		public virtual decimal TotalPrice { get; set; }
		public virtual DateTime? ReleaseTime { get; set; }
		public virtual DateTime? SubmitTime { get; set; }
		public virtual string VoucherCode { get; set; }
		public virtual SnackOrderStatusEnum OrderStatus { get; set; }
		public virtual DateTime? RefundTime { get; set; }
		public virtual DateTime? FetchTime { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime AutoUnLockDateTime { get; set; }
		public virtual string DeliveryAddress { get; set; }
		public virtual DateTime SendTime { get; set; }
		public virtual bool? OrderPayFlag { get; set; }
		public virtual byte? OrderPayType { get; set; }
		public virtual DateTime? OrderPayTime { get; set; }
		public virtual string OrderTradeNo { get; set; }
		public virtual bool? IsUseConpons { get; set; }
		public virtual string ConponCode { get; set; }
		public virtual decimal? ConponPrice { get; set; }
		public virtual string OpenID { get; set; }
	}

    /// <summary>
    /// A class which represents the OrderSeatDetails table.
    /// </summary>
    [Table("OrderSeatDetails")]
    [SqlLamTable(Name = "OrderSeatDetails")]
    public partial class OrderSeatDetailEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual int OrderId { get; set; }
		public virtual string SeatCode { get; set; }
		public virtual string RowNum { get; set; }
		public virtual string ColumnNum { get; set; }
		public virtual decimal Price { get; set; }
		public virtual decimal SalePrice { get; set; }
		public virtual decimal Fee { get; set; }
		public virtual string FilmTicketCode { get; set; }
		public virtual string TicketInfoCode { get; set; }
		public virtual bool? PrintFlag { get; set; }
		public virtual string YkSeatCode { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? Updated { get; set; }
		public virtual bool Deleted { get; set; }
	}

    /// <summary>
    /// A class which represents the CinemaTicketSystemAccounts table.
    /// </summary>
    [Table("CinemaTicketSystemAccounts")]
    [SqlLamTable(Name = "CinemaTicketSystemAccounts")]
    public partial class CinemaTicketSystemAccountEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual string Url { get; set; }
		public virtual string UserName { get; set; }
		public virtual string Password { get; set; }
		public virtual string PayType { get; set; }
		public virtual CinemaTypeEnum TicketSystem { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the AdminSessionView view.
    /// </summary>
    [Table("AdminSessionView")]
    [SqlLamTable(Name = "AdminSessionView")]
    public partial class AdminSessionViewEntity : EntityBase
    {
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string SessionCode { get; set; }
		public virtual string ScreenCode { get; set; }
		public virtual DateTime StartTime { get; set; }
		public virtual string FilmCode { get; set; }
		public virtual string FilmName { get; set; }
		public virtual int? Duration { get; set; }
		public virtual string Language { get; set; }
		public virtual DateTime? UpdateTime { get; set; }
		public virtual decimal StandardPrice { get; set; }
		public virtual decimal LowestPrice { get; set; }
		public virtual bool? IsAvalible { get; set; }
		public virtual string PlaythroughFlag { get; set; }
		public virtual string Dimensional { get; set; }
		public virtual int Sequence { get; set; }
		public virtual string DingXinUpdateTime { get; set; }
		public virtual decimal? ListingPrice { get; set; }
		public virtual string FeatureNo { get; set; }
		public virtual string CinemaName { get; set; }
		public virtual string ScreenName { get; set; }
		public virtual int? PriceSettingID { get; set; }
		public virtual PricePlanTypeEnum? PriceSettingType { get; set; }
		public virtual decimal? PriceSettingPrice { get; set; }
	}

    /// <summary>
    /// A class which represents the Banner table.
    /// </summary>
    [Table("Banner")]
    [SqlLamTable(Name = "Banner")]
    public partial class BannerEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string Title { get; set; }
		public virtual string Image { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime? StartDate { get; set; }
		public virtual DateTime? EndDate { get; set; }
		public virtual YesOrNoEnum Status { get; set; }
		public virtual bool IsDel { get; set; }
	}

    /// <summary>
    /// A class which represents the SessionInfo table.
    /// </summary>
    [Table("SessionInfo")]
    [SqlLamTable(Name = "SessionInfo")]
    public partial class SessionInfoEntity : EntityBase
    {
		[Key]
		public virtual int Id { get; set; }
		public virtual string CinemaCode { get; set; }
		public virtual string SessionCode { get; set; }
		public virtual string ScreenCode { get; set; }
		public virtual DateTime StartTime { get; set; }
		public virtual string FilmCode { get; set; }
		public virtual string FilmName { get; set; }
		public virtual int? Duration { get; set; }
		public virtual string Language { get; set; }
		public virtual DateTime? UpdateTime { get; set; }
		public virtual decimal StandardPrice { get; set; }
		public virtual decimal LowestPrice { get; set; }
		public virtual decimal SettlePrice { get; set; }
		public virtual decimal TicketFee { get; set; }
		public virtual YesOrNoEnum IsAvalible { get; set; }
		public virtual string PlaythroughFlag { get; set; }
		public virtual string Dimensional { get; set; }
		public virtual int Sequence { get; set; }
		public virtual string DingXinUpdateTime { get; set; }
		public virtual decimal? ListingPrice { get; set; }
		public virtual decimal? SalePrice { get; set; }
		public virtual string FeatureNo { get; set; }
		public virtual string YueKeSaleStatus { get; set; }
		public virtual DateTime? YueKeStopSellingTime { get; set; }
		public virtual string YueKeScheduleId { get; set; }
		public virtual string YueKeScheduleKey { get; set; }
	}

}
