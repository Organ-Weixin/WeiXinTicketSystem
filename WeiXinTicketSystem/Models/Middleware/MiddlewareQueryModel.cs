using WeiXinTicketSystem.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class MiddlewareQueryModel: DynatablePageQueryModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}