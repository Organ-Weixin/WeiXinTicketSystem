using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Models
{
    public class SnackOrderViewEntity
    {
        /// <summary>
        /// 订单基本信息
        /// </summary>
        public SnackOrderEntity OrderBaseInfo { get; set; }
        /// <summary>
        /// 订单套餐详细信息
        /// </summary>
        public List<SnackOrderDetailEntity> SnackOrderDetails { get; set; }
        /// <summary>
        /// 订单套餐主信息修改
        /// </summary>
        public List<SnackEntity> Snacks { get; set; }
    }
}
