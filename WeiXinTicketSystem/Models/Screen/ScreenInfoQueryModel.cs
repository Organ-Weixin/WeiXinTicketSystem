using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.Models
{
    public class ScreenInfoQueryModel: DynatablePageQueryModel
    {
        public string CinemaCode_dd { get; set; }
    }
}