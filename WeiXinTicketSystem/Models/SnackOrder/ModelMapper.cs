using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Service;

namespace WeiXinTicketSystem.Models.SnackOrder
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this SnackOrderDetailsViewEntity orderDetail)
        {
            return new
            {
                id = orderDetail.Id,
                cinemaName = orderDetail.CinemaName,
                orderCode = orderDetail.OrderCode,
                SnackName = orderDetail.SnackName,
                Number = orderDetail.Number,
                //优惠券名称
                ConponTitle = orderDetail.Title,
                //销售金额
                SalePrice = orderDetail.SalePrice,
                //合计
                SubTotalPrice = orderDetail.SubTotalPrice,
                //实际支付金额
                ActualPrice =orderDetail.ActualPrice,
                Created = orderDetail.Created.ToFormatString(),
                mobile = orderDetail.MobilePhone,
                orderStatus = orderDetail.OrderStatus.GetDescription(),
                statusClass = GetStatusClass(orderDetail.OrderStatus),
                //优惠金额
                //ConponPrice = orderDetail.ConponPrice,
                //实际支付金额
                //ActualPrice = GetActualPrice(orderDetail.TotalPrice, orderDetail.ConponPrice, orderDetail.OrderPayFlag)
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

        //public static decimal? GetActualPrice(decimal totalPrice, decimal? conponPrice,bool? orderPayFlag)
        //{
        //    if (orderPayFlag ==null || orderPayFlag == false)
        //        return null;
        //    if (conponPrice == null)
        //    {
        //        conponPrice = 0;
        //    }
        //    decimal actualPrice = totalPrice - decimal.Parse(conponPrice.ToString());
        //    return actualPrice;
        //}

    }
}