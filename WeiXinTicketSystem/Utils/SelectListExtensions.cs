using WeiXinTicketSystem.Util;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Utils
{
    public static class SelectListExtensions
    {
        /// <summary>
        /// 根据枚举列表获得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enums"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToEnumSelectList<T>(this IEnumerable<T> enums)
            where T : struct
        {
            return enums.Select(x => new SelectListItem
            {
                Text = x.GetDescription(),
                Value = x.GetValueString()
            });
        }

        /// <summary>
        /// 根据枚举列表获得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enums"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToEnumSelectList<T>(this IEnumerable<T> enums, T selectedItem)
            where T : struct
        {
            return enums.Select(x => new SelectListItem
            {
                Text = x.GetDescription(),
                Value = x.GetValueString(),
                Selected = x.Equals(selectedItem) ? true : false
            });
        }
    }
}
