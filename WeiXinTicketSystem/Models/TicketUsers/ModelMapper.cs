using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.TicketUsers
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="TicketUsers"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this TicketUserEntity module)
        {
            return new
            {
                id = module.Id,
                MobilePhone = module.MobilePhone,
                OpenID = module.OpenID,
                VerifyCode = module.VerifyCode,
                IsActive = module.IsActive.GetDescription(),
                NickName = module.NickName,
                Sex = module.Sex.GetDescription(),
                Country = module.Country,
                Province = module.Province,
                City = module.City,
                HeadImgUrl = module.HeadImgUrl,
                
            };
        }
    }
}