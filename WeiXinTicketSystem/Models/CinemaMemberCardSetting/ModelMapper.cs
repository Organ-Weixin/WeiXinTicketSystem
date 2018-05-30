using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.CinemaMemberCardSetting
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="CinemaMemberCardSetting"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this CinemaMemberCardSettingEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                CinemaName = module.CinemaName,
                IsCardUse = module.IsCardUse.GetDescription(),
                IsCardRegister = module.IsCardRegister.GetDescription(),
                IsCardReCharge = module.IsCardReCharge.GetDescription(),
                CardType = module.CardType.GetDescription(),
                ThirdMemberUrl = module.ThirdMemberUrl,
                InitialCardPassword = module.InitialCardPassword,
                OscarDiscount = module.OscarDiscount

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaMemberCardSetting"></param>
        /// <param name="model"></param>
        public static void MapFrom(this CinemaMemberCardSettingEntity module, CreateOrUpdateCinemaMemberCardSettingViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.CinemaName = model.CinemaName;
            module.IsCardUse = (YesOrNoEnum)model.IsCardUse;
            module.IsCardRegister = (YesOrNoEnum)model.IsCardRegister;
            module.IsCardReCharge = (YesOrNoEnum)model.IsCardReCharge;
            module.CardType = (MemberCardTypeEnum)model.CardType;
            module.ThirdMemberUrl = model.ThirdMemberUrl;
            module.InitialCardPassword = model.InitialCardPassword;
            module.OscarDiscount =(int?)model.OscarDiscount;
           

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaMemberCardSetting"></param>
        public static void MapFrom(this CreateOrUpdateCinemaMemberCardSettingViewModel model, CinemaMemberCardSettingEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.CinemaName = module.CinemaName;
            model.IsCardUse = (int)module.IsCardUse;
            model.IsCardRegister = (int)module.IsCardRegister;
            model.IsCardReCharge = (int)module.IsCardReCharge;
            model.CardType = (int)module.CardType;
            model.ThirdMemberUrl = module.ThirdMemberUrl;
            model.InitialCardPassword = module.InitialCardPassword;
            model.OscarDiscount =(double)module.OscarDiscount;

        }
    }
}