using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Linq;
using NetSaleSvc.Api.Models;

namespace WeiXinTicketSystem.Service
{
    public static class ModelMapper
    {

        /// <summary>
        /// 影厅信息转为entity
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ScreenInfoEntity MapToEntity(this QueryCinemaReplyScreen model, ScreenInfoEntity entity)
        {
            entity.SCode = model.Code;
            entity.SName = model.Name;
            entity.SeatCount = model.SeatCount;
            entity.Type = model.Type;
            entity.UpdateTime = DateTime.Now;
            return entity;
        }

        /// <summary>
        /// 影厅座位信息转为entity
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static ScreenSeatInfoEntity MapToEntity(this QuerySeatReplySeat model, ScreenSeatInfoEntity entity)
        {
            entity.SeatCode = model.Code;
            entity.GroupCode = model.GroupCode;
            entity.RowNum = model.RowNum;
            entity.ColumnNum = model.ColumnNum;
            entity.XCoord = model.XCoord;
            entity.YCoord = model.YCoord;
            entity.Status = model.Status;
            entity.UpdateTime = DateTime.Now;
            return entity;
        }

        /// <summary>
        /// 影片信息转为entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public static FilmInfoEntity MapToEntity(this CxQueryFilmInfoResultFilmInfoVO model, FilmInfoEntity entity)
        //{
        //    entity.FilmCode = model.FilmCode;
        //    entity.FilmName = model.FilmName;
        //    entity.Version = model.Version;
        //    entity.Duration = model.Duration;

        //    DateTime PublishDate;
        //    if (DateTime.TryParse(model.PublishDate, out PublishDate))
        //    {
        //        entity.PublishDate = PublishDate;
        //    }
        //    else
        //    {
        //        entity.PublishDate = null;
        //    }
        //    entity.Publisher = model.Publisher;
        //    entity.Producer = model.Producer;
        //    entity.Director = model.Director;
        //    entity.Cast = model.Cast;
        //    entity.Introduction = model.Introduction;

        //    return entity;
        //}

        /// <summary>
        /// 放映计划信息转为entity
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SessionInfoEntity MapToEntity(this QuerySessionReplySession model, SessionInfoEntity entity)
        {
            entity.ScreenCode = model.ScreenCode;
            entity.StartTime = DateTime.Parse(model.StartTime);

            var filmInfo = model.Films.Film.FirstOrDefault() ?? new QuerySessionReplyFilm();
            entity.FilmCode = filmInfo.Code;
            entity.FilmName = filmInfo.Name;
            entity.Duration = filmInfo.Duration == "" ? 0 : int.Parse(filmInfo.Duration);
            entity.Language = filmInfo.Language;
            entity.StandardPrice = model.Price.StandardPrice == "" ? 0 : decimal.Parse(model.Price.StandardPrice);
            entity.LowestPrice = model.Price.LowestPrice == "" ? 0 : decimal.Parse(model.Price.LowestPrice);
            entity.IsAvalible = true;
            entity.PlaythroughFlag = model.PlaythroughFlag;
            entity.Dimensional = filmInfo.Dimensional;
            entity.Sequence = filmInfo.Sequence == "" ? 0 : int.Parse(filmInfo.Sequence);
            entity.ListingPrice = model.Price.ListingPrice == null ? 0 : decimal.Parse(model.Price.ListingPrice);

            entity.SettlePrice = 0m;
            entity.TicketFee = 0m;

            return entity;
        }
    }
}
