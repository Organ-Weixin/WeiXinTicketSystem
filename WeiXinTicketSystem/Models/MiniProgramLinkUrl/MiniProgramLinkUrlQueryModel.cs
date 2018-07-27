using WeiXinTicketSystem.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class MiniProgramLinkUrlQueryModel : DynatablePageQueryModel
    {
        /// <summary>
        /// 链接名称
        /// </summary>
        public string LinkName { get; set; }
    }
}