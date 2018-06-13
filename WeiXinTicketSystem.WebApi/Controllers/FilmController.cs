using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using WeiXinTicketSystem.Service;
using WeiXinTicketSystem.Entity;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using WeiXinTicketSystem.WebApi.Models;
using WeiXinTicketSystem.WebApi.Extension;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.WebApi.Controllers
{
    public class FilmController : ApiController
    {
        FilmInfoService _filmInfoService;
        SystemUserService _userService;
        CinemaService _cinemaService;

        #region ctor
        public FilmController()
        {
            _filmInfoService = new FilmInfoService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
        }
        #endregion

        [HttpGet]
        public async Task<QueryFilmsReply> QueryFilm(string UserName, string Password, string CinemaCode,string FilmCode)
        {
            QueryFilmsReply queryFilmsReply = new QueryFilmsReply();

            //校验参数
            if (!queryFilmsReply.RequestInfoGuard(UserName, Password, CinemaCode, FilmCode))
            {
                return queryFilmsReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryFilmsReply.SetUserCredentialInvalidReply();
                return queryFilmsReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryFilmsReply.SetCinemaInvalidReply();
                return queryFilmsReply;
            }

            FilmInfoEntity film =await _filmInfoService.GetFilmInfoByFilmCodeAsync(FilmCode);
            queryFilmsReply.data = new QueryFilmsReplyFilm();
            queryFilmsReply.data.Id = film.Id;
            queryFilmsReply.data.FilmCode = film.FilmCode;
            queryFilmsReply.data.FilmName = film.FilmName;
            queryFilmsReply.data.Version = film.Version;
            queryFilmsReply.data.Duration = film.Duration;
            queryFilmsReply.data.PublishDate = film.PublishDate;
            queryFilmsReply.data.Publisher = film.Publisher;
            queryFilmsReply.data.Producer = film.Producer;
            queryFilmsReply.data.Director = film.Director;
            queryFilmsReply.data.Cast = film.Cast;
            queryFilmsReply.data.Introduction = film.Introduction;
            queryFilmsReply.data.Score = film.Score;
            queryFilmsReply.data.Area = film.Area;
            queryFilmsReply.data.Type = film.Type;
            queryFilmsReply.data.Language = film.Language;
            queryFilmsReply.data.Status = film.Status.GetDescription();
            queryFilmsReply.data.Image = film.Image;
            queryFilmsReply.data.Trailer = film.Trailer;

            queryFilmsReply.SetSuccessReply();
            return queryFilmsReply;
        }
    }
}