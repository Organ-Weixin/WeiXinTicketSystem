using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.CinemaMiniProgramAccount
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="CinemaTicketSystemAccount"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this CinemaMiniProgramAccountEntity account)
        {
            return new
            {
                id = account.Id,
                CinemaCode = account.CinemaCode,
                CinemaName = account.CinemaName,
                AppId = account.AppId,
                AppSecret = account.AppSecret
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaTicketSystemAccount"></param>
        /// <param name="model"></param>
        public static void MapFrom(this CinemaMiniProgramAccountEntity entity, CreateOrUpdateMiniProgramAccountViewModel model)
        {
            entity.CinemaCode = model.CinemaCode;
            entity.CinemaName = model.CinemaName;
            entity.AppId = model.AppId;
            entity.AppSecret = model.AppSecret;
        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaTicketSystemAccount"></param>
        public static void MapFrom(this CreateOrUpdateMiniProgramAccountViewModel model, CinemaMiniProgramAccountEntity entity)
        {
            model.Id = entity.Id;
            model.CinemaCode = entity.CinemaCode;
            model.CinemaName = entity.CinemaName;
            model.AppId = entity.AppId;
            model.AppSecret = entity.AppSecret;
        }
    }
}