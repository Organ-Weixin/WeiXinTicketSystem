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
        public static dynamic ToDynatableItem(this PricePlanEntity pricePlan)
        {
            return new
            {
                id = pricePlan.Id,
                CinemaCode = pricePlan.CinemaCode,
                Code = pricePlan.Code,
                Type = pricePlan.Type.GetDescription(),
                Price = pricePlan.Price.ToString("0.##")
            };
        }
    }
}