using WeiXinTicketSystem.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetSaleSvc.Entity.Enum;
using NetSaleSvc.Util;

namespace WeiXinTicketSystem.Models.UserInfo
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this UserInfoEntity userInfo)
        {
            return new
            {
                id = userInfo.Id,
                UserName = userInfo.UserName,
                Password = userInfo.Password,
                Company = userInfo.Company,
                Tel = userInfo.Tel,
                Advance = userInfo.Advance
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="role"></param>
        /// <param name="model"></param>
        public static void MapFrom(this UserInfoEntity userInfo, CreateOrUpdateUserInfoViewModel model)
        {
            userInfo.UserName = model.UserName;
            userInfo.Password = model.Password;
            userInfo.Company = model.Company;
            userInfo.Address = model.Address;
            userInfo.Tel = model.Tel;
            if (!string.IsNullOrEmpty(model.OverdueDateRange))
            {
                var dates = model.OverdueDateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                userInfo.BeginDate = DateTime.Parse(dates[0]);
                userInfo.EndDate = DateTime.Parse(dates[1]);
            }
            else
            {
                userInfo.BeginDate = DateTime.Now;
                userInfo.EndDate = DateTime.Now.AddYears(1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        public static void MapFrom(this CreateOrUpdateUserInfoViewModel model, UserInfoEntity userInfo)
        {
            model.Id = userInfo.Id;
            model.UserName = userInfo.UserName;
            model.Password = userInfo.Password;
            model.Company = userInfo.Company;
            model.Address = userInfo.Address;
            model.Tel = userInfo.Tel;
            if(!string.IsNullOrEmpty(userInfo.BeginDate.ToString())&&!string.IsNullOrEmpty(userInfo.EndDate.ToString()))
            {
                model.OverdueDateRange = DateTime.Parse(userInfo.BeginDate.ToString()).ToString("MM/dd/yyyy") + " - " + DateTime.Parse(userInfo.EndDate.ToString()).ToString("MM/dd/yyyy");
            }
            else
            {
                model.OverdueDateRange = "";
            }
        }
    }
}