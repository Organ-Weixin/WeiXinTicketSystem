using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.CinemaPrePay
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="CinemaPrePaySetting"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this CinemaPrePaySettingEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                CinemaName = module.CinemaName,
                IsPrePay = module.IsPrePay.GetDescription(),

                SurplusPayment = module.SurplusPayment,
                LowestPrePayment = module.LowestPrePayment,
                backgroundClass = GetBackgroundClass(module.SurplusPayment, module.LowestPrePayment)

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaPrePaySetting"></param>
        /// <param name="model"></param>
        public static void MapFrom(this CinemaPrePaySettingEntity module, CreateOrUpdateCinemaPrePayViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.CinemaName = model.CinemaName;
            module.IsPrePay = (YesOrNoEnum)model.IsPrePay;
            module.SurplusPayment = model.SurplusPayment;
            module.LowestPrePayment = model.LowestPrePayment;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaPrePaySetting"></param>
        public static void MapFrom(this CreateOrUpdateCinemaPrePayViewModel model, CinemaPrePaySettingEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.CinemaName = module.CinemaName;
            model.IsPrePay = (int)module.IsPrePay;
            model.SurplusPayment = module.SurplusPayment;
            model.LowestPrePayment = module.LowestPrePayment;

        }

        public static string GetBackgroundClass(decimal? surplusPayment, decimal? lowestPrePayment)
        {
            if (lowestPrePayment > surplusPayment)
            {
                return "yellow";
            }
            return "";
        }
    }
}