using WeiXinTicketSystem.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Util;

namespace WeiXinTicketSystem.Models.UserCinema
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this UserCinemaViewEntity usercinema)
        {
            return new
            {
                id = usercinema.Id,
                userId=usercinema.UserId,
                cinemaCode = usercinema.CinemaCode,
                cinemaName = usercinema.CinemaName,
                cinemaType = usercinema.CinemaType.GetDescription(),
                expdate = usercinema.ExpDate,
                payType = usercinema.PayType,
                openSnacks=usercinema.OpenSnacks.GetDescription(),
                statusClass = GetStatusClass(usercinema.OpenSnacks)
            };
        }
        /// <summary>
        /// 套餐接口样式
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetStatusClass(SnackInterfaceEnum status)
        {
            switch (status)
            {
                case SnackInterfaceEnum.Close:
                    return "darkorange";
                case SnackInterfaceEnum.Open:
                    return "green";
                default:
                    return "darkorange";
            }
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="role"></param>
        /// <param name="model"></param>
        public static void MapFrom(this UserCinemaEntity userCinema, CreateOrUpdateUserCinemaViewModel model)
        {
            userCinema.UserId = model.UserId;
            userCinema.CinemaCode = model.CinemaCode;
            userCinema.UserName = model.UserName;
            userCinema.Password = model.Password;
            userCinema.Fee = model.Fee;
            userCinema.CinemaFee = model.CinemaFee;
            userCinema.PayType = model.MemberPayType + "," + model.NonMemberPayType;
            userCinema.RealPrice = model.RealPrice;
            userCinema.OpenSnacks = (SnackInterfaceEnum)model.OpenSnacks;
            if (!string.IsNullOrEmpty(model.ExpDateRange))
            {
                var dates = model.ExpDateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                userCinema.ExpDate = DateTime.Parse(dates[1]);
            }
            else
            {
                userCinema.ExpDate = DateTime.Now.AddYears(1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        public static void MapFrom(this CreateOrUpdateUserCinemaViewModel model, UserCinemaEntity userCinema)
        {
            model.Id = userCinema.Id;
            model.UserId = userCinema.UserId;
            model.CinemaCode = userCinema.CinemaCode;
            model.UserName = userCinema.UserName;
            model.Password = userCinema.Password;
            model.Fee = userCinema.Fee.HasValue?userCinema.Fee.Value:0;
            model.CinemaFee = userCinema.CinemaFee.HasValue ? userCinema.CinemaFee.Value : 0;
            var types = userCinema.PayType.Split(',');
            model.MemberPayType = types[0];
            model.NonMemberPayType = types[1];
            model.RealPrice = userCinema.RealPrice.HasValue?userCinema.RealPrice.Value:0;
            model.OpenSnacks = (int)userCinema.OpenSnacks;
            if (!string.IsNullOrEmpty(userCinema.ExpDate.ToString()))
            {
                model.ExpDateRange = DateTime.Now.ToString("MM/dd/yyyy") + " - " + DateTime.Parse(userCinema.ExpDate.ToString()).ToString("MM/dd/yyyy");
            }
            else
            {
                model.ExpDateRange = "";
            }
        }
    }
}