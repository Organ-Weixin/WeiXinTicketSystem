using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System.Linq;

namespace WeiXinTicketSystem.Models.PricePlan
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this SessionPriceSettingEntity pricesetting)
        {
            return new
            {
                id = pricesetting.Id,
                CinemaCode = pricesetting.CinemaCode,
                Code = pricesetting.Code,
                Type = pricesetting.Type.GetDescription(),
                Price = pricesetting.Price.ToString("0.##")
            };
        }
    }
}