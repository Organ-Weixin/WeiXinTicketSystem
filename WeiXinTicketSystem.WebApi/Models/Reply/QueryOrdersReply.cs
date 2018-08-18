using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryOrdersReply : BaseReply
    {
        public QueryOrdersReplyOrders data { get; set; }
    }

    public class QueryOrdersReplyOrders
    {
        public int OrderCount { get; set; }
        public List<QueryOrdersReplyOrder> Orders { get; set; }
    }

    public class QueryOrdersReplyOrder
    {
        public int OrderId { get; set; }
        /// <summary>
        /// 影院编码
        /// </summary>
        public string CinemaCode { get; set; }
        /// <summary>
        /// 放映计划编码
        /// </summary>
        public string SessionCode { get; set; }
        /// <summary>
        /// 影厅编码
        /// </summary>
        public string ScreenCode { get; set; }
        /// <summary>
        /// 放映计划时间
        /// </summary>
        public DateTime SessionTime { get; set; }
        /// <summary>
        /// 影片编码
        /// </summary>
        public string FilmCode { get; set; }
        /// <summary>
        /// 影片名称
        /// </summary>
        public string FilmName { get; set; }
        /// <summary>
        /// 座位数量
        /// </summary>
        public int TicketCount { get; set; }
        /// <summary>
        /// 总的上报价格
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 总服务费
        /// </summary>
        public decimal TotalFee { get; set; }
        /// <summary>
        /// 总实际销售价格
        /// </summary>
        public decimal TotalSalePrice { get; set; }
        /// <summary>
        /// 订单状态(New : 新建订单，SeatLocked: 已锁座， Payed: 已支付， Complete: 订单完成，TicketRefund 退票， Refund：退款)
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 锁座时间
        /// </summary>
        public DateTime? LockTime { get; set; }
        /// <summary>
        /// 自动解锁时间
        /// </summary>
        public DateTime? AutoUnlockDatetime { get; set; }
        /// <summary>
        /// 锁座订单号
        /// </summary>
        public string LockOrderCode { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime? SubmitTime { get; set; }
        /// <summary>
        /// 提交订单号
        /// </summary>
        public string SubmitOrderCode { get; set; }
        /// <summary>
        /// 取票码
        /// </summary>
        public string PrintNo { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerifyCode { get; set; }
        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime? PrintTime { get; set; }
        /// <summary>
        /// 退单时间
        /// </summary>
        public DateTime? RefundTime { get; set; }
        
    }

}