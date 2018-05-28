using WeiXinTicketSystem.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class TicketUsersQueryModel : DynatablePageQueryModel
    {
        /// <summary>
        ///手机号
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
    }
}