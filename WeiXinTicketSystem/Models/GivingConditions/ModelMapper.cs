using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Service;

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
                Conditions = "满"+module.Price+"元送"+module.TypeName,
                ConponType = module.ConponTypeCode,
                Number = module.Number,
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
            module.Price = model.Price;
            module.ConponTypeCode = model.ConponTypeCode;
            if (!string.IsNullOrEmpty(model.Number))
            {
                module.Number = int.Parse(model.Number);
            }

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
            model.Price = module.Price;
            if (GetConponTypeParentId(module.ConponTypeCode) != null)
            {
                model.ConponTypeParentId = GetConponTypeParentId(module.ConponTypeCode).ToString();
            }
            model.ConponTypeCode = module.ConponTypeCode;
            if (module.Number != null)
            {
                model.Number = module.Number.ToString();
            }
            model.StartDate = module.StartDate.ToFormatDateString();
            model.EndDate = module.EndDate.ToFormatDateString();

        }

        private static int? GetConponTypeParentId(string ConponTypeCode)
        {
            ConponTypeService _conponTypeService = new ConponTypeService();
            ConponTypeEntity conponType = _conponTypeService.GetConponTypeByTypeCode(ConponTypeCode);
            return conponType.TypeParentId;
        }

    }
}