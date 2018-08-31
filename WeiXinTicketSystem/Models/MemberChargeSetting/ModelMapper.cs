using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Service;

namespace WeiXinTicketSystem.Models.MemberChargeSetting
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminMemberChargeSettingViewEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaName = module.CinemaName,
                Conditions = "充值" + module.Price + "元送" + module.GroupName,
                TypeCode = module.TypeCode,
                Number = module.Number,
                StartDate = module.StartDate.ToFormatDateString(),
                EndDate = module.EndDate.ToFormatDateString(),
                Remark = module.Remark,
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="module"></param>
        /// <param name="model"></param>
        public static void MapFrom(this MemberChargeSettingEntity module, CreateOrUpdateMemberChargeSettingViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.Price = model.Price;
            module.TypeCode = model.TypeCode;
            module.GroupCode = model.GroupCode;
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
        public static void MapFrom(this CreateOrUpdateMemberChargeSettingViewModel model, MemberChargeSettingEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.Price = module.Price;
            model.TypeCode = module.TypeCode;
            model.GroupCode = module.GroupCode;
            if (module.Number != null)
            {
                model.Number = module.Number.ToString();
            }
            model.StartDate = module.StartDate.ToFormatDateString();
            model.EndDate = module.EndDate.ToFormatDateString();

        }

    }
}