using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Entity.Models
{
    public class SessionInfoWithCustomPrice
    {
        public SessionInfoWithCustomPrice()
        {
            customPrice = new CustomPrice();
            sessionInfo = new SessionInfoEntity();
        }

        /// <summary>
        /// 影院在中间平台设置的价格
        /// </summary>
        public CustomPrice customPrice { get; set; }

        /// <summary>
        /// 排期信息
        /// </summary>
        public SessionInfoEntity sessionInfo { get; set; }

        /// <summary>
        /// 真实标准价，影院可能在中间平台会为排期或影片设置价格
        /// </summary>
        public string RealStandardPrice
        {
            get
            {
                if (customPrice.CustomSessionPrice.HasValue)
                {
                    return customPrice.CustomSessionPrice.Value.ToString("0.##");
                }
                if (customPrice.CustomFilmPrice.HasValue)
                {
                    return customPrice.CustomFilmPrice.Value.ToString("0.##");
                }
                return sessionInfo.StandardPrice.ToString("0.##");
            }
        }

        /// <summary>
        /// 真实最低票价，仅用于满天星不能从接口获取到最低票价使用（满天星也可以从接口获取，需要影院申请）
        /// </summary>
        public string RealLowestPrice
        {
            get
            {
                if (customPrice.CustomLowestPrice.HasValue)
                {
                    return customPrice.CustomLowestPrice.Value.ToString("0.##");
                }
                return sessionInfo.LowestPrice.ToString("0.##");
            }
        }
    }

    /// <summary>
    /// 影院在中间平台设置的价格
    /// </summary>
    public class CustomPrice
    {
        /// <summary>
        /// 影片价格
        /// </summary>
        public decimal? CustomFilmPrice { get; set; }

        /// <summary>
        /// 排期价格
        /// </summary>
        public decimal? CustomSessionPrice { get; set; }

        /// <summary>
        /// 最低票价
        /// </summary>
        public decimal? CustomLowestPrice { get; set; }
    }
}
