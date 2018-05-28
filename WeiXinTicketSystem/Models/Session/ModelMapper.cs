﻿using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.Session
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminSessionViewEntity session)
        {
            return new
            {
                id = session.Id,
                cinemaName = session.CinemaName,
                ScreenName = session.ScreenName,
                filmName = session.FilmName,
                startTime = session.StartTime.ToString("yyyy-MM-dd HH:mm"),
                duration = session.Duration,
                language = session.Language,
                standardPrice = session.StandardPrice.ToString("0.##"),
                lowestPrice = session.LowestPrice.ToString("0.##"),
                pricePlanPrice = session.PriceSettingPrice.HasValue ? session.PriceSettingPrice.Value.ToString("0.##") : "0"
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="role"></param>
        /// <param name="model"></param>
        public static void MapFrom(this SessionPriceSettingEntity plan, SetPriceViewModel model)
        {
            plan.CinemaCode = model.CinemaCode;
            if (model.Type == 1)
            {
                plan.Code = model.FilmCode;
            }
            else if(model.Type==2)
            {
                plan.Code = model.SessionCode;
            }
            else if (model.Type == 3)
            {
                plan.Code = model.FilmCode;
            }
            plan.Type = (PricePlanTypeEnum)model.Type;
            
            
            plan.Price = model.Price.HasValue?model.Price.Value:0;
        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        public static void MapFrom(this SetPriceViewModel model, AdminSessionViewEntity session)
        {
            model.PriceSettingId = session.PriceSettingID.HasValue ? session.PriceSettingID.Value:0;
            model.CinemaCode = session.CinemaCode;
            model.SessionCode = session.SessionCode;
            model.FilmCode = session.FilmCode;
            if(session.PriceSettingType==null)
            {
                session.PriceSettingType = PricePlanTypeEnum.Film;
            }
            model.Type = (int)session.PriceSettingType;
            model.Price = session.PriceSettingPrice.HasValue? session.PriceSettingPrice:session.StandardPrice;
        }
    }
}