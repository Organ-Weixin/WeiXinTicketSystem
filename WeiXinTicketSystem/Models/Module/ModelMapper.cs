using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System.Linq;

namespace WeiXinTicketSystem.Models.Module
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this SystemModuleEntity module)
        {
            return new
            {
                id = module.Id,
                ModuleName = module.ModuleName,
                ModuleSequence=module.ModuleSequence,
                ModuleFlag=module.ModuleFlag,
                created = module.Created.ToFormatString()
            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="role"></param>
        /// <param name="model"></param>
        public static void MapFrom(this SystemModuleEntity module, CreateOrUpdateModuleViewModel model)
        {
            module.ModuleName = model.ModuleName;
            module.ModuleIcon = model.ModuleIcon;
            module.ModuleParentId = model.ModuleParentId;
            module.ModuleSequence = int.Parse(model.ModuleSequence.ToString());
            module.ModuleFlag = model.ModuleFlag;
        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        public static void MapFrom(this CreateOrUpdateModuleViewModel model, SystemModuleEntity module)
        {
            model.Id = module.Id;
            model.ModuleName = module.ModuleName;
            model.ModuleIcon = module.ModuleIcon;
            model.ModuleParentId = module.ModuleParentId;
            model.ModuleSequence = module.ModuleSequence.HasValue? (decimal)module.ModuleSequence.Value:0;
            model.ModuleFlag = module.ModuleFlag;
        }
    }
}