using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.Snack
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminSnacksViewEntity snack)
        {
            return new
            {
                id = snack.Id,
                cinemaName = snack.CinemaName,
                code = snack.SnackCode,
                type = snack.TypeName,
                name = snack.SnackName,
                standardPrice = snack.StandardPrice.ToString("0.##"),
                salePrice = snack.SalePrice.ToString("0.##"),
                stock = snack.Stock,
                Image = snack.Image,
                status = snack.Status.GetDescription(),
                IsRecommand = snack.IsRecommand.GetDescription()
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="role"></param>
        /// <param name="model"></param>
        public static void MapFrom(this SnackEntity snack, CreateOrUpdateSnackViewModel model)
        {
            snack.CinemaCode = model.CinemaCode;
            snack.TypeCode = model.TypeCode;
            snack.SnackName = model.SnackName;
            //snack.Image = model.Image;
            snack.Remark = model.Remark;
            snack.StandardPrice = model.StandardPrice;
            snack.SalePrice = model.SalePrice;
            snack.Stock = (int)model.Stock;
            snack.IsRecommand = (YesOrNoEnum)model.IsRecommand;
        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        public static void MapFrom(this CreateOrUpdateSnackViewModel model, SnackEntity snack)
        {
            model.Id = snack.Id;
            model.CinemaCode = snack.CinemaCode;
            model.TypeCode = snack.TypeCode;
            model.SnackName = snack.SnackName;
            //model.Image = snack.Image;
            model.Remark = snack.Remark;
            model.StandardPrice = snack.StandardPrice;
            model.SalePrice = snack.SalePrice;
            model.Stock = snack.Stock;
            model.IsRecommand = (int)snack.IsRecommand;
        }
    }
}