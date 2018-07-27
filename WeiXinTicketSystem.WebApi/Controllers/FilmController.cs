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
            if (film == null)
            {
                queryFilmsReply.SetFilmCodeNotExistReply();
                return queryFilmsReply;
            }
            queryFilmsReply.data = new QueryFilmsReplyFilm();
            queryFilmsReply.data.MapFrom(film);
            queryFilmsReply.SetSuccessReply();
            return queryFilmsReply;
        }
    }
}