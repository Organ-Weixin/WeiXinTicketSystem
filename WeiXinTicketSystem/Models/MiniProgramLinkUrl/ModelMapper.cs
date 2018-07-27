using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.MiniProgramLinkUrl
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="MiniProgramLinkUrl"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this MiniProgramLinkUrlEntity module)
        {
            return new
            {
                id = module.Id,
                LinkName = module.LinkName,
                LinkUrl = module.LinkUrl,

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="MiniProgramLinkUrl"></param>
        /// <param name="model"></param>
        public static void MapFrom(this MiniProgramLinkUrlEntity module, CreateOrUpdateMiniProgramLinkUrlViewModel model)
        {
            module.LinkName = model.LinkName;
            module.LinkUrl = model.LinkUrl;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="MiniProgramLinkUrl"></param>
        public static void MapFrom(this CreateOrUpdateMiniProgramLinkUrlViewModel model, MiniProgramLinkUrlEntity module)
        {
            model.Id = module.Id;
            model.LinkName = module.LinkName;
            model.LinkUrl = module.LinkUrl;

        }
    }
}