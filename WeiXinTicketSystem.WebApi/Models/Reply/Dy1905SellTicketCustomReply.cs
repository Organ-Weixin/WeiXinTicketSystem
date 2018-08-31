using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.WebApi.Models
{
    [XmlRoot("SellTicketResult")]
    public class Dy1905SellTicketCustomReply
    {
        [XmlElement]
        public string ResultCode { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [XmlElement]
        public string OrderNo { get; set; }

        /// <summary>
        /// 打印编号
        /// </summary>
        [XmlElement]
        public string PrintNo { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [XmlElement]
        public string VerifyCode { get; set; }
    }
}