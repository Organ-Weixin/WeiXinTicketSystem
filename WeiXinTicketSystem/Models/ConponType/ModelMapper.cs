using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System.Linq;

namespace WeiXinTicketSystem.Models.ConponType
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this ConponTypeEntity module)
        {
            return new
            {
                id = module.Id,
                TypeCode = module.TypeCode,
                TypeName = module.TypeName,
                Remark = module.Remark,
                created = module.Created.ToFormatString()
            };
        }


        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="module"></param>
        /// <param name="model"></param>
        public static void MapFrom(this ConponTypeEntity module, CreateOrUpdateConponTypeViewModel model)
        {
            module.TypeCode = model.TypeCode;
            module.TypeName = model.TypeName;
            module.TypeParentId = model.TypeParentId;
            module.Remark = model.Remark;
        }


        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="module"></param>
        public static void MapFrom(this CreateOrUpdateConponTypeViewModel model, ConponTypeEntity module)
        {
            model.Id = module.Id;
            model.TypeCode = module.TypeCode;
            model.TypeName = module.TypeName;
            model.TypeParentId = module.TypeParentId;
            model.Remark = module.Remark;
        }

    }
}