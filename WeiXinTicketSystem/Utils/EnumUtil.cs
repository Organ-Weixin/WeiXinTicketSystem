using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Utils
{
    public static class EnumUtil
    {
        public static IEnumerable<SelectListItem> GetSelectList<T>()
            where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToEnumSelectList();
        }

        public static IEnumerable<SelectListItem> GetSelectList<T>(T selectedItem)
            where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToEnumSelectList(selectedItem);
        }
    }
}