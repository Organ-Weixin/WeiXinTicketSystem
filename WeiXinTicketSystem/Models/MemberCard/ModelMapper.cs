using WeiXinTicketSystem.Entity.Enum;
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
        public static dynamic ToDynatableItem(this MemberCardEntity membercard)
        {
            return new
            {
                id = membercard.Id,
                CinemaCode = membercard.CinemaCode,
                Openid = membercard.OpenID,
                CardNo = membercard.CardNo,
                CardPassword = membercard.CardPassword,
                Balance = membercard.Balance,
                Score = membercard.Score,
                MemberGrade = membercard.MemberGrade.GetDescription(),
                Status = membercard.Status.GetDescription(),
                Created = membercard.Created.ToFormatString()

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
            membercard.CardNo = model.CardNo;
            membercard.CardPassword = model.CardPassword;
            membercard.Balance = model.Balance;
            membercard.Score = (int?)model.Score;
            membercard.MemberGrade = (MemberCardGradeEnum)model.MemberGrade;
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
            model.CardNo = membercard.CardNo;
            model.CardPassword = membercard.CardPassword;
            model.Balance = membercard.Balance;
            model.Score = membercard.Score;
            model.MemberGrade = (int)membercard.MemberGrade;
            model.Status = (int)membercard.Status;

        }
    }
}