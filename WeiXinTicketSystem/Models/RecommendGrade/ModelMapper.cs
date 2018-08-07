using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.RecommendGrade
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this RecommendGradeEntity module)
        {
            return new
            {
                id = module.Id,
                GradeCode = module.GradeCode,
                GradeName = module.GradeName,
                Remark = module.Remark,

            };
        }

        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="module"></param>
        /// <param name="model"></param>
        public static void MapFrom(this RecommendGradeEntity module, CreateOrUpdateRecommendGradeViewModel model)
        {
            module.GradeCode = model.GradeCode;
            module.GradeName = model.GradeName;
            module.Remark = model.Remark;

        }

        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="module"></param>
        public static void MapFrom(this CreateOrUpdateRecommendGradeViewModel model, RecommendGradeEntity module)
        {
            model.Id = module.Id;
            model.GradeCode = module.GradeCode;
            model.GradeName = module.GradeName;
            model.Remark = module.Remark;

        }
    }
}