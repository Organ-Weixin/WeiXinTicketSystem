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
    public class SnackController : ApiController
    {
        SnackTypeService _snackTypeService;
        SnackService _snackService;
        SystemUserService _userService;
        CinemaService _cinemaService;
        SnackOrderService _snacksOrderService;
        #region ctor
        public SnackController()
        {
            _snackTypeService = new SnackTypeService();
            _snackService = new SnackService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _snacksOrderService = new SnackOrderService();
        }
        #endregion

        [HttpGet]
        public async Task<QuerySnackTypesReply> QuerySnackType(string UserName, string Password,string CinemaCode)
        {
            QuerySnackTypesReply querySnackTypesReply = new QuerySnackTypesReply();

            //校验参数
            if (!querySnackTypesReply.RequestInfoGuard(UserName, Password, CinemaCode))
            {
                return querySnackTypesReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                querySnackTypesReply.SetUserCredentialInvalidReply();
                return querySnackTypesReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                querySnackTypesReply.SetCinemaInvalidReply();
                return querySnackTypesReply;
            }

            var SnackTypes = await _snackTypeService.GetAllSnacksTypesAsync(CinemaCode);
            querySnackTypesReply.data = new QuerySnackTypesReplyTypes();
            if(SnackTypes==null||SnackTypes.Count==0)
            {
                querySnackTypesReply.data.TypeCount = 0;
            }
            else
            {
                querySnackTypesReply.data.TypeCount = SnackTypes.Count;
                querySnackTypesReply.data.Types = SnackTypes.Select(x => new QuerySnackTypesReplyType().MapFrom(x)).ToList();
            }
            querySnackTypesReply.SetSuccessReply();
            return querySnackTypesReply;
        }

        [HttpGet]
        public async Task<QuerySnacksReply> QuerySnacks(string UserName, string Password, string CinemaCode,string TypeId,string CurrentPage,string PageSize)
        {
            QuerySnacksReply querySnacksReply = new QuerySnacksReply();
            //校验参数
            if (!querySnacksReply.RequestInfoGuard(UserName, Password, CinemaCode,TypeId,CurrentPage,PageSize))
            {
                return querySnacksReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                querySnacksReply.SetUserCredentialInvalidReply();
                return querySnacksReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                querySnacksReply.SetCinemaInvalidReply();
                return querySnacksReply;
            }
            var Snacks = await _snackService.QuerySnacksPagedAsync(CinemaCode, TypeId, int.Parse(CurrentPage), int.Parse(PageSize));

            querySnacksReply.data = new QuerySnacksReplySnacks();
            if (Snacks == null || Snacks.Count == 0)
            {
                querySnacksReply.data.SnackCount = 0;
            }
            else
            {
                querySnacksReply.data.SnackCount = Snacks.Count;
                querySnacksReply.data.Snacks = Snacks.Select(x => new QuerySnacksReplySnack().MapFrom(x)).ToList();
            }
            querySnacksReply.SetSuccessReply();
            return querySnacksReply;
        }
        [HttpPost]
        public BookSnacksReply BookSnacks(BookSnacksQueryJson QueryJson)
        {
            BookSnacksReply bookSnacksReply = new BookSnacksReply();
            //校验参数
            if (!bookSnacksReply.RequestInfoGuard(QueryJson))
            {
                return bookSnacksReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName,QueryJson.Password);
            if (UserInfo == null)
            {
                bookSnacksReply.SetUserCredentialInvalidReply();
                return bookSnacksReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                bookSnacksReply.SetCinemaInvalidReply();
                return bookSnacksReply;
            }
            //验证套餐接口是否可访问
            if (cinema.OpenSnacks == YesOrNoEnum.No)
            {
                bookSnacksReply.SetSnackInterfaceInvalidReply();
                return bookSnacksReply;
            }

            List<SnackEntity> snackDetails = _snackService.GetSnacks(QueryJson.CinemaCode, QueryJson.Snacks.Select(x => x.SnackCode)).ToList();
            //验证套餐数量
            foreach (var snack in QueryJson.Snacks)
            {
                var snackDetail = snackDetails.Where(y => y.SnackCode == snack.SnackCode).SingleOrDefault();

                if (snack.Number < 1)//数量不正确
                {
                    bookSnacksReply.SetSnackNumberInvalidReply();
                    return bookSnacksReply;
                }

                if (snack.Number > snackDetail.Stock)//数量超出库存总数
                {
                    bookSnacksReply.SetSnackNumberInvalidReply();
                    return bookSnacksReply;
                }
            }

            //将请求参数转为订单
            var snacksorder = new SnackOrderViewEntity();
            snacksorder.MapFrom(QueryJson);
            //更新套餐表数量
            snackDetails.ForEach(x =>
            {
                var snackOrderDetail = snacksorder.SnackOrderDetails.Where(y => y.SnackCode == x.SnackCode).SingleOrDefault();
                if (snackOrderDetail != null)
                {
                    x.Stock = x.Stock - snackOrderDetail.Number;
                }
            });
            snacksorder.Snacks = snackDetails;

            _snacksOrderService.Insert(snacksorder);

            bookSnacksReply.data = new BookSnacksReplySnacks();
            bookSnacksReply.data.MapFrom(snacksorder);
            bookSnacksReply.SetSuccessReply();

            return bookSnacksReply;

        }
    }
}
