using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.Stamp
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this StampEntity module)
        {
            return new
            {
                id = module.Id,
                Title = module.Title,
                StampCode = module.StampCode,
                Image = module.Image,
                ValidityDate = module.ValidityDate.ToFormatDateString(),
                Created = module.Created.ToString("yyyy-MM-dd HH:mm")

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <param name="model"></param>
        public static void MapFrom(this StampEntity module, CreateOrUpdateStampViewModel model)
        {
            module.Title = model.Title;
            //module.StampCode = model.StampCode;
            if(!string.IsNullOrEmpty(model.ValidityDate))
            {
                module.ValidityDate =DateTime.Parse(model.ValidityDate);
            }
            
        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaPaySetting"></param>
        public static void MapFrom(this CreateOrUpdateStampViewModel model, StampEntity module)
        {
            model.Id = module.Id;
            model.Title = module.Title;
            //model.StampCode = module.StampCode;
            model.ValidityDate = module.ValidityDate.ToFormatDateString();

        }
    }
}