using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System.Linq;


namespace WeiXinTicketSystem.Models.SnackType
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this AdminSnackTypesViewEntity snackstype)
        {
            return new
            {
                id = snackstype.Id,
                cinemaname = snackstype.CinemaName,
                typename = snackstype.TypeName,
                remark=snackstype.Remark,
                snacksCount = snackstype.SnacksCount,
                Image=snackstype.Image,
                candelete = snackstype.SnacksCount > 0 ? false : true
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="role"></param>
        /// <param name="model"></param>
        public static void MapFrom(this SnackTypeEntity type, CreateOrUpdateSnackTypeViewModel model)
        {
            type.CinemaCode = model.CinemaCode;
            type.TypeName = model.TypeName;
            type.Remark = model.Remark;
            //type.Image = model.Image;
        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        public static void MapFrom(this CreateOrUpdateSnackTypeViewModel model, SnackTypeEntity type)
        {
            model.Id = type.Id;
            model.CinemaCode = type.CinemaCode;
            model.TypeName = type.TypeName;
            model.Remark = type.Remark;
            //model.Image = type.Image;
        }
    }
}