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
        public static dynamic ToDynatableItem(this MemberCardEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                TicketUserId = module.TicketUserId,
                CardNo = module.CardNo,
                CardPassword = module.CardPassword,
                Balance = module.Balance,
                Score = module.Score,
                MemberGrade = module.MemberGrade.GetDescription(),
                Status = module.Status.GetDescription(),
                Created = module.Created.ToFormatString()

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="MemberCard"></param>
        /// <param name="model"></param>
        public static void MapFrom(this MemberCardEntity module, CreateOrUpdateMemberCardViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.TicketUserId =(int?)model.TicketUserId;
            module.CardNo = model.CardNo;
            module.CardPassword = model.CardPassword;
            module.Balance = model.Balance;
            module.Score = (int?)model.Score;
            module.MemberGrade = (MemberCardGradeEnum)model.MemberGrade;
            module.Status = (MemberCardStatusEnum)model.Status;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="MemberCard"></param>
        public static void MapFrom(this CreateOrUpdateMemberCardViewModel model, MemberCardEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.TicketUserId = module.TicketUserId;
            model.CardNo = module.CardNo;
            model.CardPassword = module.CardPassword;
            model.Balance = module.Balance;
            model.Score = module.Score;
            model.MemberGrade = (int)module.MemberGrade;
            model.Status = (int)module.Status;

        }
    }
}