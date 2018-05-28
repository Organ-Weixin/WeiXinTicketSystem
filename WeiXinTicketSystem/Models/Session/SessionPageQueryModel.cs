using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.Models
{
    public class SessionPageQueryModel: DynatablePageQueryModel
    {
        /// <summary>
        /// 时间范围
        /// </summary>
        public string SessionDateRange { get; set; }
    }
}