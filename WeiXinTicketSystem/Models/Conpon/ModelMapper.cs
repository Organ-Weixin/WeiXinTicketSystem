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
        public static dynamic ToDynatableItemGroup(this ConponGroupViewEntity module)
        {
            return new
            {
                id = module.Id,
                CinemaCode = module.CinemaCode,
                GroupName = module.GroupName,
                Price = module.Price,
                ConponNumber = module.ConponNumber,
                ValidityDate = module.ValidityDate.ToFormatDateString(),
                UsedNumber = module.UsedNumber,
                NotUsedNumber = module.NotUsedNumber,
                ReceivedNumber = module.ReceivedNumber,
                GroupCode = module.GroupCode,
                Remark = module.Remark,
                statusClass = GetStatusClass(module.UsedNumber ?? 0, module.ReceivedNumber ?? 0)
            };
        }

        public static string GetStatusClass(int UsedNumber, int ReceivedNumber)
        {
            if (UsedNumber > 0 || ReceivedNumber > 0)
            {
                return "display:none;";
            }
            else
            {
                return "";
            }
        }

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
                ConponType = module.Title,
                Price = module.Price,
                ConponCode = module.ConponCode,
                ValidityDate = module.ValidityDate.ToFormatDateString(),
                Status = module.Status.GetDescription(),
                UseDate = module.UseDate.ToFormatDateString(),
                Title = module.Title,
                Remark =module.Remark,
                StatusClassConpon = GetStatusClassConpon(module.Status)

            };
        }

        public static string GetStatusClassConpon(ConponStatusEnum status)
        {
            if (status == ConponStatusEnum.AlreadyReceived || status == ConponStatusEnum.Used)
            {
                return "display:none;";
            }
            else
            {
                return "";
            }
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
            module.TypeCode = model.TypeCode;

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
            module.Remark = model.Remark;

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
            model.TypeCode = module.TypeCode;
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
            model.Remark = module.Remark;

        }

        ////private static int? GetConponTypeParentId(string ConponTypeCode)
        ////{
        ////    ConponTypeService _conponTypeService = new ConponTypeService();
        ////    ConponTypeEntity conponType = _conponTypeService.GetConponTypeByTypeCode(ConponTypeCode);
        ////    return conponType.TypeParentId;
        ////}

        ////private static string GetConponTypeName(string ConponTypeCode)
        ////{
        ////    ConponTypeService _conponTypeService = new ConponTypeService();
        ////    ConponTypeEntity conponType = _conponTypeService.GetConponTypeByTypeCode(ConponTypeCode);
        ////    if (conponType == null)
        ////        return "";
        ////    return conponType.TypeName;
        ////}

    }
}