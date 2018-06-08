using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.Conpon
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminConponViewEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                ConponType = module.ConponType.GetDescription(),
                NickName = module.NickName,
                Price = module.Price,
                ConponCode = module.ConponCode,
                ValidityDate = module.ValidityDate.ToFormatDateString(),
                IfUse = module.IfUse.GetDescription(),
                UseDate = module.UseDate.ToFormatDateString()

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <param name="model"></param>
        public static void MapFrom(this ConponEntity module, CreateOrUpdateConponViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            //module.ConponType = (ConponTypeEnum)Enum.Parse(typeof(ConponTypeEnum), model.ConponType);
            module.ConponType = (ConponTypeEnum)model.ConponType;
            module.OpenID = model.OpenID;
            module.Price = model.Price;
            module.ConponCode = model.ConponCode;
            if (!string.IsNullOrEmpty(model.ValidityDate))
            {
                module.ValidityDate = DateTime.Parse(model.ValidityDate);
            }

            module.IfUse = (YesOrNoEnum)model.IfUse;
            if (!string.IsNullOrEmpty(model.UseDate))
            {
                module.UseDate =DateTime.Parse(model.UseDate);
            }
            
        }


        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaPaySetting"></param>
        public static void MapFrom(this CreateOrUpdateConponViewModel model, ConponEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.ConponType = (int)module.ConponType; //Enum.GetName(module.ConponType.GetType(), module.ConponType); 
            model.OpenID = module.OpenID;
            model.Price = module.Price;
            model.ConponCode = module.ConponCode;
            model.ValidityDate = module.ValidityDate.ToFormatDateString();
            model.IfUse = (int)module.IfUse;
            model.UseDate = module.UseDate.ToFormatDateString();

        }


    }
}