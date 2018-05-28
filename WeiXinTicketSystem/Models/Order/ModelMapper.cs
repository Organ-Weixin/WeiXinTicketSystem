using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.Order
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminOrderViewEntity order)
        {
            return new
            {
                id = order.Id,
                cinemaName = order.CinemaName,
                filmName = order.FilmName,
                sessionTime = order.SessionTime.ToString("yyyy-MM-dd HH:mm"),
                ticketCount = order.TicketCount,
                price = order.TotalPrice.ToString("0.##"),
                fee = order.TotalFee.ToString("0.##"),
                orderCode = order.SubmitOrderCode ?? order.LockOrderCode,
                orderTime = order.Created.ToFormatString(),
                mobile = order.MobilePhone,
                orderStatus = order.OrderStatus.GetDescription(),
                statusClass = GetStatusClass(order.OrderStatus),
                backgroundClass= GetBackgroundClass(order.TotalPrice,order.TotalSalePrice)
            };
        }

        /// <summary>
        /// 获取状态样式
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetStatusClass(OrderStatusEnum status)
        {
            switch (status)
            {
                case OrderStatusEnum.Created:
                case OrderStatusEnum.Locked:
                case OrderStatusEnum.Released:
                case OrderStatusEnum.Submited:
                case OrderStatusEnum.Refund:
                    return "darkorange";
                case OrderStatusEnum.LockFail:
                case OrderStatusEnum.ReleaseFail:
                case OrderStatusEnum.SubmitFail:
                    return "red";
                case OrderStatusEnum.Complete:
                    return "green";
                default:
                    return "darkorange";
            }
        }
        public static string GetBackgroundClass(decimal Price,decimal SalePrice)
        {
            if(Price>SalePrice)
            {
                return "yellow";
            }
            return "";
        }
    }
}