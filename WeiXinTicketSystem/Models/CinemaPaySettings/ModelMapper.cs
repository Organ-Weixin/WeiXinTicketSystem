using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.CinemaPaySettings
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this CinemaPaymentSettingEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                CinemaName = module.CinemaName,
                IsUseAlipay = module.IsUseAlipay.GetDescription(),


                AlipaySellerEmail = module.AlipaySellerEmail,
                AlipayPartner = module.AlipayPartner,
                AlipayKey = module.AlipayKey,
                AlipayAPPID = module.AlipayAPPID,
                IsUseBfbpay = module.IsUseBfbpay.GetDescription(),
                BfbpaySpno = module.BfbpaySpno,
                BfbpayKey = module.BfbpayKey,

                IsUseWxpay = module.IsUseWxpay.GetDescription(),
                WxpayAppId = module.WxpayAppId,
                WxpayMchId = module.WxpayMchId,
                WxpayKey = module.WxpayKey,
                WxpayRefundCert = module.WxpayRefundCert,
                IsUserMemberCard = module.IsUserMemberCard.GetDescription()

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <param name="model"></param>
        public static void MapFrom(this CinemaPaymentSettingEntity module, CreateOrUpdateCinemaPaySettingsViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.CinemaName = model.CinemaName;
            module.IsUseAlipay = (YesOrNoEnum)model.IsUseAlipay;
            module.AlipaySellerEmail = model.AlipaySellerEmail;
            module.AlipayPartner = model.AlipayPartner;
            module.AlipayKey = model.AlipayKey;
            module.AlipayAPPID = model.AlipayAPPID;
            //module.IsUseBfbpay = model.IsUseBfbpay ? YesOrNoEnum.Yes : YesOrNoEnum.No;
            module.IsUseBfbpay = (YesOrNoEnum)model.IsUseBfbpay;
            module.BfbpaySpno = model.BfbpaySpno;
            module.BfbpayKey = model.BfbpayKey;
            //module.IsUseWxpay = model.IsUseWxpay ? YesOrNoEnum.Yes : YesOrNoEnum.No;
            module.IsUseWxpay = (YesOrNoEnum)model.IsUseWxpay;
            module.WxpayAppId = model.WxpayAppId;
            module.WxpayMchId = model.WxpayMchId;
            module.WxpayKey = model.WxpayKey;
            module.WxpayRefundCert = model.WxpayRefundCert;
            //module.IsUserMemberCard = model.IsUserMemberCard ? YesOrNoEnum.Yes : YesOrNoEnum.No;
            module.IsUserMemberCard = (YesOrNoEnum)model.IsUserMemberCard;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaPaySetting"></param>
        public static void MapFrom(this CreateOrUpdateCinemaPaySettingsViewModel model, CinemaPaymentSettingEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.CinemaName = module.CinemaName;
            //model.IsUseAlipay = module.IsUseAlipay == YesOrNoEnum.Yes ? true:false ;
            model.IsUseAlipay = (int)module.IsUseAlipay;
            model.AlipaySellerEmail = module.AlipaySellerEmail;
            model.AlipayPartner = module.AlipayPartner;
            model.AlipayKey = module.AlipayKey;
            model.AlipayAPPID = module.AlipayAPPID;
            //model.IsUseBfbpay = module.IsUseBfbpay == YesOrNoEnum.Yes ? true : false;
            model.IsUseBfbpay = (int)module.IsUseBfbpay;
            model.BfbpaySpno = module.BfbpaySpno;
            model.BfbpayKey = module.BfbpayKey;
            //model.IsUseWxpay = module.IsUseWxpay == YesOrNoEnum.Yes ? true : false;
            model.IsUseWxpay = (int)module.IsUseWxpay;
            model.WxpayAppId = module.WxpayAppId;
            model.WxpayMchId = module.WxpayMchId;
            model.WxpayKey = module.WxpayKey;
            model.WxpayRefundCert = module.WxpayRefundCert;
            //model.IsUserMemberCard = module.IsUserMemberCard == YesOrNoEnum.Yes ? true : false;
            model.IsUserMemberCard = (int)module.IsUserMemberCard;

        }
    }
}