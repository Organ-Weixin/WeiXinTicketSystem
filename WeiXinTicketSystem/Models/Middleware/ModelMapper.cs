using WeiXinTicketSystem.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Util;

namespace WeiXinTicketSystem.Models.Middleware
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this MiddlewareEntity middleware)
        {
            return new
            {
                id = middleware.Id,
                Title = middleware.Title,
                Url = middleware.Url,
                username = middleware.UserName,
                password = middleware.Password,
                Type = middleware.Type.GetDescription(),
                cinemaCount=middleware.CinemaCount
            };
        }
        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="role"></param>
        /// <param name="model"></param>
        public static void MapFrom(this MiddlewareEntity middle, CreateOrUpdateMiddlewareViewModel model)
        {
            middle.Title = model.Title;
            middle.Url = model.Url;
            middle.UserName = model.UserName;
            middle.Password = model.Password;
            middle.Type = (CinemaTypeEnum)model.Type;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        public static void MapFrom(this CreateOrUpdateMiddlewareViewModel model, MiddlewareEntity middle)
        {
            model.Id = middle.Id;
            model.Title = middle.Title;
            model.Url = middle.Url;
            model.UserName = middle.UserName;
            model.Password = middle.Password;
            model.Type = (int)middle.Type;
        }
    }
}