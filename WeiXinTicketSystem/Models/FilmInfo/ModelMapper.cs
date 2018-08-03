using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models.FilmInfo
{
    public static class ModelMapper
    {
        /// <summary>
        /// 转为Dynatable内容
        /// </summary>
        /// <param name="FilmInfo"></param>
        /// <returns></returns>
        public static dynamic ToDynatableItem(this FilmInfoEntity module)
        {
            return new
            {
                id = module.Id,
                FilmCode = module.FilmCode,
                FilmName = module.FilmName,
                Version = module.Version,
                Duration = module.Duration,
                PublishDate = module.PublishDate.ToFormatDateString(),
                Publisher = module.Publisher,
                Producer = module.Producer,
                Director = module.Director,
                Cast = module.Cast,
                Introduction = module.Introduction,
                Score = module.Score,
                Area = module.Area,
                Type = module.Type,
                Language = module.Language,
                Status = module.Status.GetDescription(),
                statusClass = GetStatusClass(module.Status),
                Image = module.Image,
                Trailer = module.Trailer

            };
        }

        /// <summary>
        /// 获取状态样式
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetStatusClass(FilmStatusEnum status)
        {
            switch (status)
            {
                case FilmStatusEnum.NoPassed:
                    return "red";
                case FilmStatusEnum.Passed:
                    return "green";
                default:
                    return "darkorange";
            }
        }


        /// <summary>
        /// ViewModel to Entity
        /// </summary>
        /// <param name="FilmInfo"></param>
        /// <param name="model"></param>
        public static void MapFrom(this FilmInfoEntity module, CreateOrUpdateFilmInfoViewModel model)
        {
            module.FilmCode = model.FilmCode;
            module.FilmName = model.FilmName;
            module.Version = model.Version;
            module.Duration = model.Duration;
            if (!string.IsNullOrEmpty(model.PublishDate))
            {
                module.PublishDate = DateTime.Parse(model.PublishDate);
            }
            module.Publisher = model.Publisher;
            module.Producer = model.Producer;
            module.Director = model.Director;
            module.Cast = model.Cast;
            module.Introduction = model.Introduction;
            module.Score = model.Score;
            module.Area = model.Area;
            module.Type = model.Type;
            module.Language = model.Language;
            module.Status = (FilmStatusEnum)model.Status;
            //module.Image = model.Image;
            module.Trailer = model.Trailer;
        }


        /// <summary>
        /// Entity to ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CinemaPaySetting"></param>
        public static void MapFrom(this CreateOrUpdateFilmInfoViewModel model, FilmInfoEntity module)
        {
            model.Id = module.Id;
            model.FilmCode = module.FilmCode;
            model.FilmName = module.FilmName;
            model.Version = module.Version;
            model.Duration = module.Duration;
            model.PublishDate =module.PublishDate.ToFormatDateString();
            model.Publisher = module.Publisher;
            model.Producer = module.Producer;
            model.Director = module.Director;
            model.Cast = module.Cast;
            model.Introduction = module.Introduction;
            model.Score = module.Score;
            model.Area = module.Area;
            model.Type = module.Type;
            model.Language = module.Language;
            model.Status = (int)module.Status;
            //model.Image = module.Image;
            model.Trailer = module.Trailer;

        }

        public static void MapFrom(this FilmInfoEntity entity, FilmInfomation film)
        {
            entity.FilmCode = film.ID;
            entity.FilmName = film.Name;
            //entity.Version = film.Version;
            entity.Duration = "0";
            entity.PublishDate = film.PublishDate;
            entity.Publisher = film.Publisher;
            entity.Producer = film.Producer;
            entity.Director = film.Director;
            entity.Cast = film.Cast;
            entity.Introduction = film.Brief;
        }
    }
}