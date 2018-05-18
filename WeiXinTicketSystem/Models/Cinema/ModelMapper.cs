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
        public static dynamic ToDynatableItem(this CinemaEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                CinemaName = module.CinemaName,
                TicketSystem = module.TicketSystem,

                ContactName = module.ContactName,
                ContactMobile = module.ContactMobile,
                TheaterChain = module.TheaterChain,
                Address = module.Address,
                Status = module.Status,
                Latitude = module.Latitude,
                Longitude = module.Longitude,

                Created = module.Created
            };
        }


        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="Cinema"></param>
        /// <param name="model"></param>
        public static void MapFrom(this CinemaEntity module, CreateOrUpdateCinemaViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            module.CinemaName = model.CinemaName;
            module.TicketSystem = (CinemaTypeEnum)model.TicketSystem;
            module.ContactName = model.ContactName;
            module.ContactMobile = model.ContactMobile;
            module.TheaterChain = (TheaterChainEnum)model.TheaterChain;
            module.Address = model.Address;
            module.Status = (CinemaStatusEnum)model.Status;
            module.Latitude = model.Latitude;
            module.Longitude = model.Longitude;
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
            model.CinemaCode = module.CinemaCode;
            model.CinemaName = module.CinemaName;
            model.TicketSystem = (int)module.TicketSystem;
            model.ContactName = module.ContactName;
            model.ContactMobile = module.ContactMobile;
            model.TheaterChain = (int)module.TheaterChain;
            model.Address = module.Address;
            model.Status = (int)module.Status;
            model.Latitude = module.Latitude;
            model.Longitude = module.Longitude;
            //model.Created = module.Created;
            
        }
    }
}