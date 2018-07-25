using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.Activity
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="Activity"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this ActivityEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                Title = module.Title,
                Image = module.Image,
                ActivityContent = module.ActivityContent,
                StartDate = module.StartDate.ToFormatDateString(),
                EndDate = module.EndDate.ToFormatDateString(),
                Status = module.Status.GetDescription()
               
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <param name="model"></param>
        public static void MapFrom(this ActivityEntity module, CreateOrUpdateActivityViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.Title = model.Title;
            //module.Image = model.Image;
            module.ActivityContent = model.ActivityContent;
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                module.StartDate =DateTime.Parse(model.StartDate);
            }
            if(!string.IsNullOrEmpty(model.EndDate))
            {
                module.EndDate =DateTime.Parse(model.EndDate);
            }
            module.LinkUrl = model.LinkUrl;


            module.Status = (YesOrNoEnum)model.Status;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaPaySetting"></param>
        public static void MapFrom(this CreateOrUpdateActivityViewModel model, ActivityEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.Title = module.Title;
            //model.Image = module.Image;
            model.ActivityContent = module.ActivityContent;
            model.StartDate = module.StartDate.ToFormatDateString();
            model.EndDate = module.EndDate.ToFormatDateString();
            model.LinkUrl = module.LinkUrl;
            model.Status = (int)module.Status;

        }
    }
}