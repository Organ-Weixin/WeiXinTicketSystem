using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System.Linq;

namespace WeiXinTicketSystem.Models.PriceSettings
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this CinemaPriceSettingEntity pricesetting)
        {
            return new
            {
                id = pricesetting.Id,
                CinemaCode = pricesetting.CinemaCode,
                CinemaName = pricesetting.CinemaName,
                WeChatDiscount = pricesetting.WeChatDiscount,
                FackPriceUpperLimit = pricesetting.FackPriceUpperLimit.ToString("0.##"),
                Fee = pricesetting.Fee.ToString("0.##"),
                MemberFee = pricesetting.MemberFee.ToString("0.##"),
                FeePayType = pricesetting.FeePayType.GetDescription(),
                FeeGatherType = pricesetting.FeeGatherType.GetDescription()
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="role"></param>
        /// <param name="model"></param>
        public static void MapFrom(this CinemaPriceSettingEntity pricesetting, CreateOrUpdatePriceSettingViewModel model)
        {
            pricesetting.CinemaCode = model.CinemaCode;
            pricesetting.CinemaName = model.CinemaName;
            pricesetting.WeChatDiscount = (int)model.WeChatDiscount;
            pricesetting.FackPriceUpperLimit = model.FackPriceUpperLimit;
            pricesetting.IsSellByStandardPrice = model.IsSellByStandardPrice==1 ? YesOrNoEnum.Yes : YesOrNoEnum.No;
            pricesetting.LoveSeatPriceDifferences = model.LoveSeatPriceDifferences;
            pricesetting.Fee = model.Fee;
            pricesetting.MemberFee = model.MemberFee;
            pricesetting.FeePayType = (CinemaFeePayTypeEnum)model.FeePayType;
            pricesetting.FeeGatherType = (CinemaFeeGatherTypeEnum)model.FeeGatherType;
        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        public static void MapFrom(this CreateOrUpdatePriceSettingViewModel model, CinemaPriceSettingEntity pricesetting)
        {
            model.Id = pricesetting.Id;
            model.CinemaCode = pricesetting.CinemaCode;
            model.CinemaName = pricesetting.CinemaName;
            model.WeChatDiscount = pricesetting.WeChatDiscount.HasValue ? (decimal)pricesetting.WeChatDiscount : 0;
            model.FackPriceUpperLimit = pricesetting.FackPriceUpperLimit;
            model.IsSellByStandardPrice = pricesetting.IsSellByStandardPrice == YesOrNoEnum.Yes ? 1 : 0;
            model.LoveSeatPriceDifferences = pricesetting.LoveSeatPriceDifferences.HasValue?(decimal)pricesetting.LoveSeatPriceDifferences:0;
            model.Fee = pricesetting.Fee;
            model.MemberFee = pricesetting.MemberFee;
            model.FeePayType = (int)pricesetting.FeePayType;
            model.FeeGatherType = (int)pricesetting.FeeGatherType;
        }
    }
}