using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Service;

namespace WeiXinTicketSystem.Models.Conpon
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this ConponEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                ConponType = GetConponTypeName(module.ConponTypeCode),
                Price = module.Price,
                ConponCode = module.ConponCode,
                ValidityDate = module.ValidityDate.ToFormatDateString(),
                Status = module.Status.GetDescription(),
                UseDate = module.UseDate.ToFormatDateString(),
                Title = module.Title,
                Image = module.Image

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="CinemaPaySetting"></param>
        /// <param name="model"></param>
        public static void MapFrom(this ConponEntity module, CreateOrUpdateConponViewModel model)
        {
            module.CinemaCode = model.CinemaCode;
            //module.ConponType = (ConponTypeEnum)Enum.Parse(typeof(ConponTypeEnum), model.ConponType);
            if (!string.IsNullOrEmpty(model.ConponTypeParentId))
            {
                module.ConponTypeParentId =int.Parse(model.ConponTypeParentId);
            }
            
            module.SnackCode = model.SnackCode;
            module.Price = model.Price;
            //module.ConponCode = model.ConponCode;
            if (!string.IsNullOrEmpty(model.ValidityDate))
            {
                module.ValidityDate = DateTime.Parse(model.ValidityDate);
            }

            module.Status = (ConponStatusEnum)model.Status;
            if (!string.IsNullOrEmpty(model.UseDate))
            {
                module.UseDate =DateTime.Parse(model.UseDate);
            }
            module.Title = model.Title;

        }


        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaPaySetting"></param>
        public static void MapFrom(this CreateOrUpdateConponViewModel model, ConponEntity module)
        {
            model.Id = module.Id;
            model.CinemaCode = module.CinemaCode;
            if (module.ConponTypeParentId != null)
            {
                model.ConponTypeParentId = module.ConponTypeParentId.ToString();
            }
            //if (GetConponTypeParentId(module.ConponTypeCode) != null)
            //{
            //    model.ConponTypeParentId = GetConponTypeParentId(module.ConponTypeCode).ToString();
            //}
            model.SnackCode = module.SnackCode; //Enum.GetName(module.ConponType.GetType(), module.ConponType); 
            model.Price = module.Price;
           // model.ConponCode = module.ConponCode;
            model.ValidityDate = module.ValidityDate.ToFormatDateString();
            model.Status = (int)module.Status;
            model.UseDate = module.UseDate.ToFormatDateString();
            model.Title = module.Title;

        }

        private static int? GetConponTypeParentId(string ConponTypeCode)
        {
            ConponTypeService _conponTypeService = new ConponTypeService();
            ConponTypeEntity conponType = _conponTypeService.GetConponTypeByTypeCode(ConponTypeCode);
            return conponType.TypeParentId;
        }

        private static string GetConponTypeName(string ConponTypeCode)
        {
            ConponTypeService _conponTypeService = new ConponTypeService();
            ConponTypeEntity conponType = _conponTypeService.GetConponTypeByTypeCode(ConponTypeCode);
            if (conponType == null)
                return "";
            return conponType.TypeName;
        }

    }
}