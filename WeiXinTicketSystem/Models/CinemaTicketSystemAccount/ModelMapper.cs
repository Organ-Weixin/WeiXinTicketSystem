using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.CinemaTicketSystemAccount
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="CinemaTicketSystemAccount"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this CinemaTicketSystemAccountEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                CinemaName = module.CinemaName,
                Url = module.Url,
                UserName = module.UserName,
                Password = module.Password,
                PayType = module.PayType,
                TicketSystem = module.TicketSystem.GetDescription()
                
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaTicketSystemAccount"></param>
        /// <param name="model"></param>
        public static void MapFrom(this CinemaTicketSystemAccountEntity module, CreateOrUpdateTicketSystemAccountViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.CinemaName = model.CinemaName;
            module.Url = model.Url;
            module.UserName = model.UserName;
            module.Password = model.Password;
            module.PayType = model.PayType;
            module.TicketSystem = (CinemaTypeEnum)model.TicketSystem;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaTicketSystemAccount"></param>
        public static void MapFrom(this CreateOrUpdateTicketSystemAccountViewModel model, CinemaTicketSystemAccountEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            model.CinemaName = module.CinemaName;
            model.Url = module.Url;
            model.UserName = module.UserName;
            model.Password = module.Password;
            model.PayType = module.PayType;
            model.TicketSystem = (int)module.TicketSystem;

        }
    }
}