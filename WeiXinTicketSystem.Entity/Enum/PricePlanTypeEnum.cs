using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum PricePlanTypeEnum:byte
    {
        /// <summary>
        /// 影片价格
        /// </summary>
        [Description("影片")]
        Film = 1,

        /// <summary>
        /// 排期价格
        /// </summary>
        [Description("排期")]
        Session = 2,

        /// <summary>
        /// 最低价格
        /// </summary>
        [Description("最低价")]
        LowestPrice = 3
    }
}
