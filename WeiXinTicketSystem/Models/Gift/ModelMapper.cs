using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace WeiXinTicketSystem.Models.Gift
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this GiftEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                Title = module.Title,
                Details = module.Details,
                OriginalPrice = module.OriginalPrice,
                Price = module.Price,
                Image = module.Image,
                Stock = module.Stock,
                StartDate = module.StartDate.ToFormatDateString(),
                EndDate = module.EndDate.ToFormatDateString(),
                Status = module.Status.GetDescription()

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="Gift"></param>
        /// <param name="model"></param>
        public static void MapFrom(this GiftEntity module, CreateOrUpdateGiftViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.Title = model.Title;
            module.Details = model.Details;
            module.OriginalPrice = model.OriginalPrice;
            module.Price = model.Price;
            module.Stock =(int)model.Stock;
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                module.StartDate =DateTime.Parse(model.StartDate);
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                module.EndDate =DateTime.Parse(model.EndDate);
            }
            module.Status = (YesOrNoEnum)model.Status;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Gift"></param>
        public static void MapFrom(this CreateOrUpdateGiftViewModel model, GiftEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.Title = module.Title;
            model.Details = module.Details;
            model.OriginalPrice = module.OriginalPrice;
            model.Price = module.Price;
            model.Stock = (double)module.Stock;
            model.StartDate = module.StartDate.ToFormatDateString();
            model.EndDate = module.EndDate.ToFormatDateString();
            model.Status = (int)module.Status;
        }
    }
}