using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Models
{
    public class TicketCountAccordMonth
    {
        /// <summary>
        /// 每月票量
        /// </summary>
        public int TicketCount { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        public string CurrentMonth { get; set; }
    }
}
