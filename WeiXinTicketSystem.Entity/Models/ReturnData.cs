using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Models
{
    public class ReturnData
    {
        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Info { get; set; }

        public ReturnData()
        {
            Status = false;
            Info = string.Empty;
        }
    }
}
