using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.GivingConditions
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminGivingConditionsViewEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaName = module.CinemaName,
                Conditions = module.Conditions,
                ConponType = module.ConponType.GetDescription(),
                Price = module.Price,
                StartDate = module.StartDate.ToFormatDateString(),
                EndDate = module.EndDate.ToFormatDateString(),

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="module"></param>
        /// <param name="model"></param>
        public static void MapFrom(this GivingConditionEntity module, CreateOrUpdateGivingConditionsViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.Conditions = model.Conditions;
            module.ConponType = (ConponTypeEnum)model.ConponType;
            module.Price = model.Price;
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
        /// <param name="module"></param>
        public static void MapFrom(this CreateOrUpdateGivingConditionsViewModel model, GivingConditionEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.Conditions = module.Conditions;
            model.ConponType = (int)module.ConponType;
            model.Price = module.Price;
            model.StartDate = module.StartDate.ToFormatDateString();
            model.EndDate = module.EndDate.ToFormatDateString();

        }
    }
}