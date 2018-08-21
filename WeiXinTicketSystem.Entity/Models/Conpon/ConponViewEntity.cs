using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Models
{
    public class ConponViewEntity
    {
        /// <summary>
        /// 优惠券组信息
        /// </summary>
        public ConponGroupEntity ConponGroupInfo { get; set; }
        /// <summary>
        /// 优惠券组下的优惠券
        /// </summary>
        public List<ConponEntity> ConponGroupConpons { get; set; }
    }
}
