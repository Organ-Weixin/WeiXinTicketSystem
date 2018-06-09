using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.SnackOrder
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminSnackOrdersViewEntity order)
        {
            return new
            {
                id = order.Id,
                cinemaName = order.CinemaName,
                orderCode = order.OrderCode,
                snacksCount = order.SnacksCount,
                totalPrice = order.TotalPrice.ToString("0.##"),
                voucherCode = order.VoucherCode,
                SubmitTime = order.SubmitTime.GetValueOrDefault().ToFormatString(),
                mobile = order.MobilePhone,
                orderStatus = order.OrderStatus.GetDescription(),
                statusClass = GetStatusClass(order.OrderStatus)
            };
        }

        /// <summary>
        /// 获取状态样式
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetStatusClass(SnackOrderStatusEnum status)
        {
            switch (status)
            {
                case SnackOrderStatusEnum.Created:
                case SnackOrderStatusEnum.PayFail:
                case SnackOrderStatusEnum.BookFail:
                case SnackOrderStatusEnum.SubmitFail:
                case SnackOrderStatusEnum.Refund:
                    return "darkorange";
                case SnackOrderStatusEnum.Complete:
                case SnackOrderStatusEnum.Fetched:
                case SnackOrderStatusEnum.Booked:
                case SnackOrderStatusEnum.Payed:
                case SnackOrderStatusEnum.Submited:
                      return "green";
                default:
                    return "darkorange";
            }
        }
    }
}