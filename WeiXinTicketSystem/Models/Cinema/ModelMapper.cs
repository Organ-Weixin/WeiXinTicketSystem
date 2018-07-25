using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System.Linq;

namespace WeiXinTicketSystem.Models.Cinema
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="Cinema"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this CinemaViewEntity entity)
        {
            return new
            {
                id = entity.Id,
                CinemaCode = entity.Code,
                CinemaName = entity.Name,
                cinemaType = entity.CinemaType.GetDescription(),
                ContactName = entity.ContactName,
                ContactMobile = entity.ContactMobile,
                TheaterChain = entity.TheaterChain,
                Address = entity.Address,
                Status = entity.IsOpen.GetDescription(),
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                OpenSnacks = entity.IsOpenSnacks.GetDescription(),

                Created = entity.Created
            };
        }


        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="Cinema"></param>
        /// <param name="model"></param>
        public static void MapFrom(this CinemaEntity module, CreateOrUpdateCinemaViewModel model)
        {
            module.Code = model.CinemaCode;
            module.Name = model.CinemaName;
            module.MId = model.Mid;
            module.ContactName = model.ContactName;
            module.ContactMobile = model.ContactMobile;
            module.TheaterChain = (TheaterChainEnum)model.TheaterChain;
            module.Address = model.Address;
            module.IsOpen = (CinemaOpenEnum)model.IsOpen;
            module.Latitude = model.Latitude;
            module.Longitude = model.Longitude;
            module.IsOpenSnacks = (CinemaOpenEnum)model.OpenSnacks;
            //module.Created = model.Created;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="Cinema"></param>
        /// <param name="role"></param>
        public static void MapFrom(this CreateOrUpdateCinemaViewModel model, CinemaEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.Code;
            model.CinemaName = module.Name;
            model.Mid = module.MId;
            model.ContactName = module.ContactName;
            model.ContactMobile = module.ContactMobile;
            model.TheaterChain = (int)module.TheaterChain;
            model.Address = module.Address;
            model.IsOpen = (int)module.IsOpen;
            model.Latitude = module.Latitude;
            model.Longitude = module.Longitude;
            model.OpenSnacks = (int)module.IsOpenSnacks;
            //model.Created = module.Created;

        }
    }
}