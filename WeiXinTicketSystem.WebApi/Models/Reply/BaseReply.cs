using WeiXinTicketSystem.Entity.Enum;
using System.Xml.Serialization;
using WeiXinTicketSystem.Util;
using NetSaleSvc.Api.Models;

namespace WeiXinTicketSystem.WebApi.Models
{
    /// <summary>
    /// Reply基类
    /// </summary>
    public class BaseReply
    {
        #region ctor
        public BaseReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.Exception.GetValueString();
            ErrorMessage = ErrorCodeEnum.Exception.GetDescription();
        }
        #endregion

        #region public methods
        /// <summary>
        /// 从中间平台返回中获取错误信息
        /// </summary>
        /// <param name="CTMSReply"></param>
        public void GetErrorFromNetSaleReply(NetSaleSvc.Api.Models.BaseReply NetSaleReply)
        {
            
            Status = NetSaleReply.Status;
            ErrorCode = NetSaleReply.ErrorCode;
            ErrorMessage = NetSaleReply.ErrorMessage;
        }

        /// <summary>
        /// 成功返回
        /// </summary>
        public void SetSuccessReply()
        {
            Status = StatusEnum.Success.GetDescription();
            ErrorCode = ErrorCodeEnum.Success.GetValueString();
            ErrorMessage = ErrorCodeEnum.Success.GetDescription();
        }

        /// <summary>
        /// 参数缺失返回内容
        /// </summary>
        public void SetNecessaryParamMissReply(string ParamName)
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.NecessaryParamMiss.GetValueString();
            ErrorMessage = $"{ParamName}{ErrorCodeEnum.NecessaryParamMiss.GetDescription()}";
        }

        /// <summary>
        /// 设置用户名/密码错误时的返回内容
        /// </summary>
        public void SetUserCredentialInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.UserCredentialInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.UserCredentialInvalid.GetDescription();
        }

        /// <summary>
        /// 影院不存在或不可访问
        /// </summary>
        public void SetCinemaInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.CinemaInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.CinemaInvalid.GetDescription();
        }

        /// <summary>
        /// 影厅编码错误
        /// </summary>
        public void SetScreenInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.ScreenInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.ScreenInvalid.GetDescription();
        }

        /// <summary>
        /// 开始日期错误
        /// </summary>
        public void SetStartDateInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.StartDateInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.StartDateInvalid.GetDescription();
        }

        /// <summary>
        /// 结束日期错误
        /// </summary>
        public void SetEndDateInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.EndDateInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.EndDateInvalid.GetDescription();
        }

        /// <summary>
        /// 开始日期大于结束日期
        /// </summary>
        public void SetDateInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.DateInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.DateInvalid.GetDescription();
        }

        /// <summary>
        /// 排期不存在
        /// </summary>
        public void SetSessionInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.SessionInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.SessionInvalid.GetDescription();
        }

        /// <summary>
        /// 座位售出状态非法
        /// </summary>
        public void SetSessionSeatStatusInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.SessionSeatStatusInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.SessionSeatStatusInvalid.GetDescription();
        }

        /// <summary>
        /// XML解析失败
        /// </summary>
        /// <param name="ParamName"></param>
        public void SetXmlDeserializeFailReply(string ParamName)
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.XmlDeserializeFail.GetValueString();
            ErrorMessage = $"{ParamName}{ErrorCodeEnum.XmlDeserializeFail.GetDescription()}";
        }

        /// <summary>
        /// 座位数量错误
        /// </summary>
        public void SetSeatCountInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.SeatCountInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.SeatCountInvalid.GetDescription();
        }

        /// <summary>
        /// 卖品数量错误
        /// </summary>
        public void SetSnackNumberInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.SnackNumberInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.SnackNumberInvalid.GetDescription();
        }

        /// <summary>
        /// 订单不存在或状态不合法
        /// </summary>
        public void SetOrderNotExistReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.OrderNotExist.GetValueString();
            ErrorMessage = ErrorCodeEnum.OrderNotExist.GetDescription();
        }

        /// <summary>
        /// 会员卡支付的订单没有传入会员卡支付流水号
        /// </summary>
        public void SetMemberPaySeqNoNotExistReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.PaySeqNoNotExist.GetValueString();
            ErrorMessage = ErrorCodeEnum.PaySeqNoNotExist.GetDescription();
        }
        /// <summary>
        /// 渠道订单号已存在
        /// </summary>
        public void SetChannelOrderCodeAlreadyExistReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.ChannelOrderCodeAlreadyExist.GetValueString();
            ErrorMessage = ErrorCodeEnum.ChannelOrderCodeAlreadyExist.GetDescription();
        }
        /// <summary>
        /// 套餐订单超时
        /// </summary>
        public void SetSnacksOrderTimeout()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.SnacksOrderTimeout.GetValueString();
            ErrorMessage = ErrorCodeEnum.SnacksOrderTimeout.GetDescription();
        }
        /// <summary>
        /// 套餐价格不合法
        /// </summary>
        public void SetSnackPriceInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.SnackPriceInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.SnackPriceInvalid.GetDescription();
        }
        /// <summary>
        /// 套餐接口不可访问
        /// </summary>
        public void SetSnackInterfaceInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.SnackInterfaceInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.SnackInterfaceInvalid.GetDescription();
        }
        /// <summary>
        /// 套餐类型非法
        /// </summary>
        public void SetSnackTypeInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.SnackTypeInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.SnackTypeInvalid.GetDescription();
        }
        /// <summary>
        /// 当前页数非法
        /// </summary>
        public void SetCurrentPageInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.CurrentPageInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.CurrentPageInvalid.GetDescription();
        }
        /// <summary>
        /// 每页数量非法
        /// </summary>
        public void SetPageSizeInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.PageSizeInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.PageSizeInvalid.GetDescription();
        }
        public void SetOpenIDNotExistReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.OpenIDNotExist.GetValueString();
            ErrorMessage = ErrorCodeEnum.OpenIDNotExist.GetDescription();
        }
        public void SetConponNotExistOrUsedReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.ConponNotExistOrUsed.GetValueString();
            ErrorMessage = ErrorCodeEnum.ConponNotExistOrUsed.GetDescription();
        }
        /// <summary>
        /// 优惠券使用状态非法
        /// </summary>
        public void SetConponStatusInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.ConponStatusInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.ConponStatusInvalid.GetDescription();
        }
        public void SetGiftStatusInvalidReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.GiftStatusInvalid.GetValueString();
            ErrorMessage = ErrorCodeEnum.GiftStatusInvalid.GetDescription();
        }
        public void SetFilmCodeNotExistReply()
        {
            Status = StatusEnum.Failure.GetDescription();
            ErrorCode = ErrorCodeEnum.FilmCodeNotExist.GetValueString();
            ErrorMessage = ErrorCodeEnum.FilmCodeNotExist.GetDescription();
        }
        #endregion

        /// <summary>
        /// 状态
        /// </summary>
        [XmlAttribute]
        public string Status { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlAttribute]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [XmlAttribute]
        public string ErrorMessage { get; set; }
    }
}