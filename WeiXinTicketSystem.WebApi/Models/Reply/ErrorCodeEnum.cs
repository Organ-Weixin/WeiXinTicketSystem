using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.WebApi.Models
{
    public enum ErrorCodeEnum : int
    {
        /// <summary>
        /// 异常
        /// </summary>
        [Description("本宝宝心情不好，出现了异常！（*+﹏+*）~~")]
        Exception = -1,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,

        /// <summary>
        /// 必要参数缺失
        /// </summary>
        [Description("参数缺失！")]
        NecessaryParamMiss = 10000001,

        /// <summary>
        /// 用户名/密码错误
        /// </summary>
        [Description("用户名或密码错误！")]
        UserCredentialInvalid = 10000002,

        /// <summary>
        /// 影院不存在或无权限访问
        /// </summary>
        [Description("影院不存在或无权限访问！")]
        CinemaInvalid = 10000003,

        /// <summary>
        /// 影厅不存在
        /// </summary>
        [Description("影厅不存在！")]
        ScreenInvalid = 10000004,

        /// <summary>
        /// 开始日期错误
        /// </summary>
        [Description("开始日期非法！")]
        StartDateInvalid = 10000005,

        /// <summary>
        /// 结束日期错误
        /// </summary>
        [Description("结束日期非法！")]
        EndDateInvalid = 10000006,

        /// <summary>
        /// 开始日期大于结束日期
        /// </summary>
        [Description("开始日期大于结束日期！")]
        DateInvalid = 10000007,

        /// <summary>
        /// 排期不存在
        /// </summary>
        [Description("排期不存在！")]
        SessionInvalid = 10000008,

        /// <summary>
        /// 排期座位状态不合法
        /// </summary>
        [Description("座位售出状态非法！合法取值包括：All，Available，Locked，Sold，Booked，Unavailable")]
        SessionSeatStatusInvalid = 10000009,

        /// <summary>
        /// XML解析失败
        /// </summary>
        [Description("解析失败")]
        XmlDeserializeFail = 10000010,

        /// <summary>
        /// 座位数量与实际座位不匹配
        /// </summary>
        [Description("座位数量与实际座位不匹配！")]
        SeatCountInvalid = 10000011,

        /// <summary>
        /// 订单不存在或状态不合法
        /// </summary>
        [Description("订单不存在或状态不合法！")]
        OrderNotExist = 10000012,

        /// <summary>
        /// 订单不存在或状态不合法
        /// </summary>
        [Description("此订单使用会员卡支付，请传入会员卡支付流水号！")]
        PaySeqNoNotExist = 10000013,

        /// <summary>
        /// 套餐卖品的数量不合法
        /// </summary>
        [Description("套餐卖品的数量不合法,或已超出库存总数！")]
        SnackNumberInvalid = 10000014,

        /// <summary>
        /// 渠道订单号已存在
        /// </summary>
        [Description("此渠道订单号已存在！")]
        ChannelOrderCodeAlreadyExist = 10000015,

        /// <summary>
        /// 套餐订单超时
        /// </summary>
        [Description("套餐订单已超时！")]
        SnacksOrderTimeout = 10000016,

        /// <summary>
        /// 套餐价格不合法
        /// </summary>
        [Description("套餐价格不合法！")]
        SnackPriceInvalid = 10000017,

        /// <summary>
        /// 套餐接口不可访问
        /// </summary>
        [Description("影院套餐不存在或套餐不可访问！")]
        SnackInterfaceInvalid = 10000018,

        /// <summary>
        /// 套餐类型不合法
        /// </summary>
        [Description("影院套餐类型非法，合法的取值应是数字！")]
        SnackTypeInvalid = 10000019,

        /// <summary>
        /// 当前页数不合法
        /// </summary>
        [Description("当前页数非法，合法的取值应是数字！")]
        CurrentPageInvalid = 10000020,

        /// <summary>
        /// 每页数量不合法
        /// </summary>
        [Description("每页数量非法，合法的取值应是数字！")]
        PageSizeInvalid = 10000021,

        /// <summary>
        /// 用户openid不存在
        /// </summary>
        [Description("用户OpenID不存在或不合法！")]
        OpenIDNotExist = 10000022,

        /// <summary>
        /// 优惠券不存在或已使用
        /// </summary>
        [Description("优惠券不存在或已使用！")]
        ConponNotExistOrUsed = 10000023,

        /// <summary>
        /// 优惠券使用状态非法
        /// </summary>
        [Description("优惠券状态非法！合法取值包括：All，Used，NotUsed")]
        ConponStatusInvalid = 10000024,

        /// <summary>
        /// 赠品状态非法
        /// </summary>
        [Description("赠品状态非法！合法取值包括：All，On，Off")]
        GiftStatusInvalid = 10000025,

        /// <summary>
        /// 影片编码不存在
        /// </summary>
        [Description("影片编码不存在或不合法！")]
        FilmCodeNotExist = 10000026,

        /// <summary>
        /// 小程序appid不存在
        /// </summary>
        [Description("影院小程序APPID不存在或不合法！")]
        CinemaMiniProgramAccountNotExist = 10000027
    }
}
