using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.ActivityPopup
{
    public static class ModelMapper
    {

        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="Activity"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminActivityPopupViewEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                CinemaName = module.CinemaName,
                Popup = module.Popup.GetDescription(),
                GradeName = module.GradeName,
                Image = module.Image,
                StartDate = module.StartDate.ToFormatDateString(),
                EndDate = module.EndDate.ToFormatDateString()

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <param name="model"></param>
        public static void MapFrom(this ActivityPopupEntity module, CreateOrUpdateActivityPopupViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.Popup = (ActivityPopupEnum)model.Popup;
            module.GradeCode = model.GradeCode;
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                module.StartDate = DateTime.Parse(model.StartDate);
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                module.EndDate = DateTime.Parse(model.EndDate);
            }

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaPaySetting"></param>
        public static void MapFrom(this CreateOrUpdateActivityPopupViewModel model, ActivityPopupEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.Popup = (int)module.Popup;
            model.GradeCode = module.GradeCode;
            model.StartDate = module.StartDate.ToFormatDateString();
            model.EndDate = module.EndDate.ToFormatDateString();
            
        }
    }
}