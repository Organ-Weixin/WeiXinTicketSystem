using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.Models
{
    public class SnackPageQueryModel: DynatablePageQueryModel
    {
        /// <summary>
        /// 影院编码
        /// </summary>
        public string CinemaCode_dd { get; set; }
        /// <summary>
        /// 套餐编码
        /// </summary>
        public string SnackCode { get; set; }
        /// <summary>
        /// 套餐类型
        /// </summary>
        public string TypeCode_dd { get; set; }
    }
}