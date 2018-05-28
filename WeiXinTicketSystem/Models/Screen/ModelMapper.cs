using WeiXinTicketSystem.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Util;

namespace WeiXinTicketSystem.Models.Screen
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminScreenViewEntity screen)
        {
            return new
            {
                id = screen.Id,
                cinemaName = screen.CinemaName,
                screenCode = screen.ScreenCode,
                screenName = screen.ScreenName,
                updateTime = screen.UpdateTime,
                seatCount = screen.SeatCount,
                type = screen.Type
            };
        }
    }
}