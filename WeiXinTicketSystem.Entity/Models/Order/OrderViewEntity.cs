using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Models
{
    public class OrderViewEntity
    {
        /// <summary>
        /// 订单基本信息
        /// </summary>
        public OrderEntity orderBaseInfo { get; set; }

        /// <summary>
        /// 订单座位信息
        /// </summary>
        public List<OrderSeatDetailEntity> orderSeatDetails { get; set; }
    }
}
