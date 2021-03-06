﻿using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.MemberCard
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="MemberCard"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminMemberCardViewEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                NickName = module.NickName,
                MobilePhone = module.MobilePhone,
                CardNo = module.CardNo,
                CardPassword = module.CardPassword,
                Balance = module.Balance,
                Score = module.Score,
                LevelCode = module.LevelCode,
                Status = module.Status.GetDescription(),
                Created = module.Created.ToFormatString()

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="MemberCard"></param>
        /// <param name="model"></param>
        public static void MapFrom(this MemberCardEntity membercard, CreateOrUpdateMemberCardViewModel model)
        {
            membercard.CinemaCode = model.CinemaCode;
            membercard.OpenID =model.OpenID;
            membercard.MobilePhone = model.MobilePhone;
            membercard.CardNo = model.CardNo;
            membercard.CardPassword = model.CardPassword;
            membercard.Balance = model.Balance;
            membercard.Score = (int?)model.Score;
            membercard.LevelCode = model.LevelCode;
            membercard.Status = (MemberCardStatusEnum)model.Status;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="MemberCard"></param>
        public static void MapFrom(this CreateOrUpdateMemberCardViewModel model, MemberCardEntity membercard)
        {
            model.Id = membercard.Id;
            model.CinemaCode = membercard.CinemaCode;
            model.OpenID = membercard.OpenID;
            model.MobilePhone = membercard.MobilePhone;
            model.CardNo = membercard.CardNo;
            model.CardPassword = membercard.CardPassword;
            model.Balance = membercard.Balance;
            model.Score = membercard.Score;
            model.LevelCode = membercard.LevelCode;
            model.Status = (int)membercard.Status;

        }
    }
}