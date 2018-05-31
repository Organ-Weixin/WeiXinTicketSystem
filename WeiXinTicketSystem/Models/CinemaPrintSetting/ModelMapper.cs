using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.CinemaPrintSetting
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="CinemaPrintSetting"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this CinemaPrintSettingEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                CinemaName = module.CinemaName,
                IsPrintName = module.IsPrintName.GetDescription(),
                IsCustomTicketTemplet = module.IsCustomTicketTemplet.GetDescription(),
                IsCustomPackageTemplet = module.IsCustomPackageTemplet.GetDescription()

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaPrintSetting"></param>
        /// <param name="model"></param>
        public static void MapFrom(this CinemaPrintSettingEntity module, CreateOrUpdateCinemaPrintSettingViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.CinemaName = model.CinemaName;
            module.IsPrintName = (YesOrNoEnum)model.IsPrintName;
            module.IsCustomTicketTemplet = (YesOrNoEnum)model.IsCustomTicketTemplet;
            module.IsCustomPackageTemplet = (YesOrNoEnum)model.IsCustomPackageTemplet;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaPrintSetting"></param>
        public static void MapFrom(this CreateOrUpdateCinemaPrintSettingViewModel model, CinemaPrintSettingEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.CinemaName = module.CinemaName;
            model.IsPrintName = (int)module.IsPrintName;
            model.IsCustomTicketTemplet = (int)module.IsCustomTicketTemplet;
            model.IsCustomPackageTemplet = (int)module.IsCustomPackageTemplet;

        }
    }
}