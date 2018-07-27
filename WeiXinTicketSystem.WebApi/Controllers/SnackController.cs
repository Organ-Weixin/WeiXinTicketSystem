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
        TicketUsersService _ticketUserService;
        BannerService _bannerService;
        ConponService _conponService;
        #region ctor
        public SnackController()
        {
            _snackTypeService = new SnackTypeService();
            _snackService = new SnackService();
            _userService = new SystemUserService();
            _cinemaService = new CinemaService();
            _snacksOrderService = new SnackOrderService();
            _ticketUserService = new TicketUsersService();
            _bannerService = new BannerService();
            _conponService = new ConponService();
        }
        #endregion

        #region 查询套餐类别
        [HttpGet]
        public async Task<QuerySnackTypesReply> QuerySnackType(string UserName, string Password, string CinemaCode)
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
            if (SnackTypes == null || SnackTypes.Count == 0)
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
        #endregion

        #region 查询套餐
        [HttpGet]
        public async Task<QuerySnacksReply> QuerySnacks(string UserName, string Password, string CinemaCode, string TypeCode, string CurrentPage, string PageSize)
        {
            QuerySnacksReply querySnacksReply = new QuerySnacksReply();
            //校验参数
            if (!querySnacksReply.RequestInfoGuard(UserName, Password, CinemaCode, TypeCode, CurrentPage, PageSize))
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
            var Snacks = await _snackService.QuerySnacksPagedAsync(CinemaCode, TypeCode, int.Parse(CurrentPage), int.Parse(PageSize));

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
        #endregion

        #region 预定套餐
        [HttpPost]
        public BookSnacksReply BookSnacks(BookSnacksQueryJson QueryJson)
        {
            BookSnacksReply bookSnacksReply = new BookSnacksReply();
            //校验参数
            if (!bookSnacksReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.MobilePhone, QueryJson.DeliveryAddress, QueryJson.SendTime.ToString(), QueryJson.OpenID, QueryJson.Snacks))
            {
                return bookSnacksReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
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
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(QueryJson.OpenID);
            if (ticketuser == null)
            {
                bookSnacksReply.SetOpenIDNotExistReply();
                return bookSnacksReply;
            }
            //验证套餐接口是否可访问
            if (cinema.IsOpenSnacks == CinemaOpenEnum.Closed)
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
                if (snack.StandardPrice != snackDetail.StandardPrice)//标准价与原标准价不符
                {
                    bookSnacksReply.SetSnackPriceInvalidReply();
                    return bookSnacksReply;
                }
                if (snack.SalePrice != snackDetail.SalePrice)//销售价与原销售价不符
                {
                    bookSnacksReply.SetSnackPriceInvalidReply();
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
        #endregion

        #region 取消套餐
        [HttpGet]
        public ReleaseSnacksReply ReleaseSnacks(string UserName, string Password, string CinemaCode, string OrderCode)
        {
            ReleaseSnacksReply releaseSnackReply = new ReleaseSnacksReply();
            //校验参数
            if (!releaseSnackReply.RequestInfoGuard(UserName, Password, CinemaCode, OrderCode))
            {
                return releaseSnackReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                releaseSnackReply.SetUserCredentialInvalidReply();
                return releaseSnackReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                releaseSnackReply.SetCinemaInvalidReply();
                return releaseSnackReply;
            }
            //验证卖品套餐是否存在
            SnackOrderViewEntity snacksorder = _snacksOrderService.GetSnacksOrderWithOrderCode(CinemaCode, OrderCode);
            if (snacksorder.OrderBaseInfo == null || (snacksorder.OrderBaseInfo.OrderStatus != SnackOrderStatusEnum.Booked
                && snacksorder.OrderBaseInfo.OrderStatus != SnackOrderStatusEnum.ReleaseFail))
            {
                releaseSnackReply.SetOrderNotExistReply();
                return releaseSnackReply;
            }

            //更新订单状态
            snacksorder.OrderBaseInfo.OrderStatus = SnackOrderStatusEnum.Released;
            snacksorder.OrderBaseInfo.ReleaseTime = DateTime.Now;
            //更新套餐数量
            snacksorder.Snacks.ForEach(x =>
            {
                var snackOrderDetail = snacksorder.SnackOrderDetails.Where(y => y.SnackCode == x.SnackCode).SingleOrDefault();
                if (snackOrderDetail != null)
                {
                    x.Stock = x.Stock + snackOrderDetail.Number;
                }
            });
            _snacksOrderService.Update(snacksorder);

            releaseSnackReply.data = new ReleaseSnacksReplyOrder();
            releaseSnackReply.data.CinemaCode = CinemaCode;
            releaseSnackReply.data.OrderCode = snacksorder.OrderBaseInfo.OrderCode;
            releaseSnackReply.data.OrderStatus = snacksorder.OrderBaseInfo.OrderStatus.GetDescription();
            releaseSnackReply.data.ReleaseTime = snacksorder.OrderBaseInfo.ReleaseTime.GetValueOrDefault();
            releaseSnackReply.SetSuccessReply();
            return releaseSnackReply;

        }
        #endregion

        #region 支付套餐
        [HttpPost]
        public PaySnackOrderReply PaySnackOrder(PaySnackOrderQueryJson QueryJson)
        {
            PaySnackOrderReply payOrderReply = new PaySnackOrderReply();
            //校验参数
            if (!payOrderReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.OrderCode, QueryJson.OrderPayType.ToString(), QueryJson.OrderPayTime.ToString(), QueryJson.OrderTradeNo, QueryJson.IsUseConpons.ToString(), QueryJson.ConponCode, QueryJson.OpenID))
            {
                return payOrderReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                payOrderReply.SetUserCredentialInvalidReply();
                return payOrderReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                payOrderReply.SetCinemaInvalidReply();
                return payOrderReply;
            }
            //验证卖品套餐是否存在
            var snackOrder = _snacksOrderService.GetSnackOrderByOrderCode(QueryJson.OrderCode);
            if (snackOrder == null)
            {
                payOrderReply.SetOrderNotExistReply();
                return payOrderReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(QueryJson.OpenID);
            if (ticketuser == null)
            {
                payOrderReply.SetOpenIDNotExistReply();
                return payOrderReply;
            }
            var conpon = new ConponEntity();
            if (QueryJson.IsUseConpons)
            {
                conpon = _conponService.GetConponByConponCode(QueryJson.ConponCode);
                if (conpon.Status == ConponStatusEnum.Used || conpon.Deleted)
                {
                    payOrderReply.SetConponNotExistOrUsedReply();
                    return payOrderReply;
                }
            }

            //更新卖品订单
            snackOrder.OrderPayFlag = true;
            snackOrder.OrderPayType = (PayTypeEnum)QueryJson.OrderPayType;
            snackOrder.OrderPayTime = QueryJson.OrderPayTime;
            snackOrder.OrderTradeNo = QueryJson.OrderTradeNo;
            snackOrder.IsUseConpons = QueryJson.IsUseConpons;
            snackOrder.ConponCode = QueryJson.ConponCode;
            snackOrder.ConponPrice = QueryJson.ConponPrice;
            snackOrder.OrderStatus = SnackOrderStatusEnum.Payed;
            snackOrder.Updated = DateTime.Now;
            //更新优惠券
            conpon.Status = ConponStatusEnum.Used;
            conpon.Updated = DateTime.Now;
            conpon.Price = QueryJson.ConponPrice;
            conpon.UseDate = DateTime.Now;

            _snacksOrderService.Update(snackOrder, conpon);

            payOrderReply.data = new PaySnackOrderReplyOrder();
            payOrderReply.data.MapFrom(snackOrder);
            payOrderReply.SetSuccessReply();

            return payOrderReply;
        }
        #endregion

        #region 提交套餐订单
        [HttpPost]
        public SubmitSnacksReply SubmitSnacks(SubmitSnacksQueryJson QueryJson)
        {
            SubmitSnacksReply submitSnacksReply = new SubmitSnacksReply();
            //校验参数
            if (!submitSnacksReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.OrderCode, QueryJson.OrderTradeNo, QueryJson.MobilePhone, QueryJson.OpenID, QueryJson.Snacks))
            {
                return submitSnacksReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                submitSnacksReply.SetUserCredentialInvalidReply();
                return submitSnacksReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(QueryJson.CinemaCode);
            if (cinema == null)
            {
                submitSnacksReply.SetCinemaInvalidReply();
                return submitSnacksReply;
            }
            //验证卖品套餐是否存在
            var snacksOrder = _snacksOrderService.GetSnacksOrderWithOrderCode(QueryJson.CinemaCode, QueryJson.OrderCode);
            if (snacksOrder.OrderBaseInfo == null || (snacksOrder.OrderBaseInfo.OrderStatus != SnackOrderStatusEnum.Payed
                && snacksOrder.OrderBaseInfo.OrderStatus != SnackOrderStatusEnum.SubmitFail))
            {
                submitSnacksReply.SetOrderNotExistReply();
                return submitSnacksReply;
            }
            //验证订单是否超时
            if (snacksOrder.OrderBaseInfo.AutoUnLockDateTime <= DateTime.Now)
            {
                submitSnacksReply.SetSnacksOrderTimeout();
                return submitSnacksReply;
            }
            //验证套餐数量
            if (QueryJson.Snacks.Count != snacksOrder.OrderBaseInfo.SnacksCount)
            {
                submitSnacksReply.SetSnackNumberInvalidReply();
                return submitSnacksReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(QueryJson.OpenID);
            if (ticketuser == null)
            {
                submitSnacksReply.SetOpenIDNotExistReply();
                return submitSnacksReply;
            }
            //验证结算价
            foreach (var snack in QueryJson.Snacks)
            {
                var snackDetail = snacksOrder.Snacks.Where(y => y.SnackCode == snack.SnackCode).SingleOrDefault();

                if (snack.Number < 1)//数量不正确
                {
                    submitSnacksReply.SetSnackNumberInvalidReply();
                    return submitSnacksReply;
                }

                if (snack.Number > snackDetail.Stock + snack.Number)//数量超出库存总数
                {
                    submitSnacksReply.SetSnackNumberInvalidReply();
                    return submitSnacksReply;
                }
                if (snack.SalePrice != snackDetail.SalePrice)//销售价与原销售价不符
                {
                    submitSnacksReply.SetSnackPriceInvalidReply();
                    return submitSnacksReply;
                }
                if (snack.StandardPrice != snackDetail.StandardPrice)//标准价与原标准价不符
                {
                    submitSnacksReply.SetSnackPriceInvalidReply();
                    return submitSnacksReply;
                }
            }

            //更新卖品订单
            snacksOrder.OrderBaseInfo.OrderStatus = SnackOrderStatusEnum.Complete;
            snacksOrder.OrderBaseInfo.SubmitTime = DateTime.Now;
            snacksOrder.OrderBaseInfo.VoucherCode = QueryJson.CinemaCode + RandomHelper.CreatePwd(8);
            _snacksOrderService.Update(snacksOrder);
            submitSnacksReply.data = new SubmitSnacksReplyOrder();
            submitSnacksReply.data.CinemaCode = snacksOrder.OrderBaseInfo.CinemaCode;
            submitSnacksReply.data.OrderCode = snacksOrder.OrderBaseInfo.OrderCode;
            submitSnacksReply.data.OrderStatus = snacksOrder.OrderBaseInfo.OrderStatus.GetDescription();
            submitSnacksReply.data.SubmitTime = snacksOrder.OrderBaseInfo.SubmitTime.GetValueOrDefault();
            submitSnacksReply.data.VoucherCode = snacksOrder.OrderBaseInfo.VoucherCode;
            submitSnacksReply.SetSuccessReply();
            return submitSnacksReply;
        }
        #endregion

        #region 套餐取货
        [HttpGet]
        public FetchSnacksReply FetchSnacks(string UserName, string Password, string CinemaCode, string OrderCode, string VoucherCode)
        {
            FetchSnacksReply fetchSnacksReply = new FetchSnacksReply();
            //校验参数
            if (!fetchSnacksReply.RequestInfoGuard(UserName, Password, CinemaCode, OrderCode, VoucherCode))
            {
                return fetchSnacksReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                fetchSnacksReply.SetUserCredentialInvalidReply();
                return fetchSnacksReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                fetchSnacksReply.SetCinemaInvalidReply();
                return fetchSnacksReply;
            }
            //验证卖品套餐是否存在
            var snackOrder = _snacksOrderService.GetSnackOrderbyVoucherCode(OrderCode, VoucherCode);
            if (snackOrder == null)
            {
                fetchSnacksReply.SetOrderNotExistReply();
                return fetchSnacksReply;
            }

            //更新订单状态
            snackOrder.OrderStatus = SnackOrderStatusEnum.Fetched;
            snackOrder.FetchTime = DateTime.Now;
            snackOrder.Updated = DateTime.Now;

            _snacksOrderService.Update(snackOrder);

            fetchSnacksReply.data = new FetchSnacksReplyOrder();
            fetchSnacksReply.data.CinemaCode = CinemaCode;
            fetchSnacksReply.data.OrderCode = snackOrder.OrderCode;
            fetchSnacksReply.data.OrderStatus = snackOrder.OrderStatus.GetDescription();
            fetchSnacksReply.data.FetchTime = snackOrder.FetchTime.GetValueOrDefault();
            fetchSnacksReply.SetSuccessReply();
            return fetchSnacksReply;

        }
        #endregion

        #region 退订套餐
        [HttpGet]
        public RefundSnacksReply RefundSnacks(string UserName, string Password, string CinemaCode, string OrderCode)
        {
            RefundSnacksReply refundSnacksReply = new RefundSnacksReply();
            //校验参数
            if (!refundSnacksReply.RequestInfoGuard(UserName, Password, CinemaCode, OrderCode))
            {
                return refundSnacksReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                refundSnacksReply.SetUserCredentialInvalidReply();
                return refundSnacksReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                refundSnacksReply.SetCinemaInvalidReply();
                return refundSnacksReply;
            }
            //验证卖品套餐是否存在
            SnackOrderViewEntity snacksorder = _snacksOrderService.GetSnacksOrderWithOrderCode(CinemaCode, OrderCode);
            if (snacksorder.OrderBaseInfo == null || (snacksorder.OrderBaseInfo.OrderStatus != SnackOrderStatusEnum.Complete
                && snacksorder.OrderBaseInfo.OrderStatus != SnackOrderStatusEnum.RefundFail))
            {
                refundSnacksReply.SetOrderNotExistReply();
                return refundSnacksReply;
            }

            //更新订单状态
            snacksorder.OrderBaseInfo.OrderStatus = SnackOrderStatusEnum.Refund;
            snacksorder.OrderBaseInfo.RefundTime = DateTime.Now;
            snacksorder.OrderBaseInfo.Updated = DateTime.Now;
            //更新套餐数量
            snacksorder.Snacks.ForEach(x =>
            {
                var snackOrderDetail = snacksorder.SnackOrderDetails.Where(y => y.SnackCode == x.SnackCode).SingleOrDefault();
                if (snackOrderDetail != null)
                {
                    x.Stock = x.Stock + snackOrderDetail.Number;
                }
            });
            _snacksOrderService.Update(snacksorder);

            refundSnacksReply.data = new RefundSnacksReplyOrder();
            refundSnacksReply.data.CinemaCode = CinemaCode;
            refundSnacksReply.data.OrderCode = snacksorder.OrderBaseInfo.OrderCode;
            refundSnacksReply.data.OrderStatus = snacksorder.OrderBaseInfo.OrderStatus.GetDescription();
            refundSnacksReply.data.RefundTime = snacksorder.OrderBaseInfo.RefundTime.GetValueOrDefault();
            refundSnacksReply.SetSuccessReply();
            return refundSnacksReply;

        }
        #endregion

        #region 查询套餐轮播图片
        [HttpGet]
        public async Task<QueryBannersReply> QueryBanners(string UserName, string Password, string CinemaCode)
        {
            QueryBannersReply queryBannersReply = new QueryBannersReply();
            //校验参数
            if (!queryBannersReply.RequestInfoGuard(UserName, Password, CinemaCode))
            {
                return queryBannersReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryBannersReply.SetUserCredentialInvalidReply();
                return queryBannersReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryBannersReply.SetCinemaInvalidReply();
                return queryBannersReply;
            }
            var Banners = await _bannerService.GetBannerByCinemaCodeAsync(CinemaCode);

            queryBannersReply.data = new QueryBannersReplyBanners();
            if (Banners == null || Banners.Count == 0)
            {
                queryBannersReply.data.BannersCount = 0;
            }
            else
            {
                queryBannersReply.data.BannersCount = Banners.Count;
                queryBannersReply.data.Banners = Banners.Select(x => new QueryBannersReplyBanner().MapFrom(x)).ToList();
            }
            queryBannersReply.SetSuccessReply();
            return queryBannersReply;
        }
        #endregion

        #region 查询用户订单
        [HttpGet]
        public async Task<QueryUserSnackOrdersReply> QueryUserSnackOrders(string UserName, string Password, string CinemaCode, string OpenID, string CurrentPage, string PageSize)
        {
            QueryUserSnackOrdersReply queryUserOrdersReply = new QueryUserSnackOrdersReply();
            //校验参数
            if (!queryUserOrdersReply.RequestInfoGuard(UserName, Password, CinemaCode, OpenID, CurrentPage, PageSize))
            {
                return queryUserOrdersReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryUserOrdersReply.SetUserCredentialInvalidReply();
                return queryUserOrdersReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryUserOrdersReply.SetCinemaInvalidReply();
                return queryUserOrdersReply;
            }
            //验证用户OpenId是否存在
            var ticketuser = _ticketUserService.GetTicketUserByOpenID(OpenID);
            if (ticketuser == null)
            {
                queryUserOrdersReply.SetOpenIDNotExistReply();
                return queryUserOrdersReply;
            }
            var UserOrders = await _snacksOrderService.GetUserOrdersPagedAsync(CinemaCode, OpenID, int.Parse(CurrentPage), int.Parse(PageSize));

            queryUserOrdersReply.data = new QueryUserSnackOrdersReplyOrders();
            queryUserOrdersReply.data.OpenID = OpenID;
            if (UserOrders == null || UserOrders.Count == 0)
            {
                queryUserOrdersReply.data.OrdersCount = 0;
            }
            else
            {
                queryUserOrdersReply.data.OrdersCount = UserOrders.Count;
                queryUserOrdersReply.data.Orders = UserOrders.Select(x => new QueryUserSnackOrdersReplyOrder().MapFrom(x)).ToList();
            }
            queryUserOrdersReply.SetSuccessReply();
            return queryUserOrdersReply;
        }
        #endregion

        #region 查询订单详细
        [HttpGet]
        public QuerySnackOrderReply QuerySnackOrder(string UserName, string Password, string CinemaCode, string OrderCode)
        {
            QuerySnackOrderReply queryOrderReply = new QuerySnackOrderReply();
            //校验参数
            if (!queryOrderReply.RequestInfoGuard(UserName, Password, CinemaCode, OrderCode))
            {
                return queryOrderReply;
            }
            //获取用户信息
            SystemUserEntity UserInfo = _userService.GetUserInfoByUserCredential(UserName, Password);
            if (UserInfo == null)
            {
                queryOrderReply.SetUserCredentialInvalidReply();
                return queryOrderReply;
            }
            //验证影院是否存在且可访问
            var cinema = _cinemaService.GetCinemaByCinemaCode(CinemaCode);
            if (cinema == null)
            {
                queryOrderReply.SetCinemaInvalidReply();
                return queryOrderReply;
            }
            //验证卖品套餐是否存在
            SnackOrderViewEntity snacksorder = _snacksOrderService.GetSnacksOrderWithOrderCode(CinemaCode, OrderCode);
            if (snacksorder.OrderBaseInfo == null)
            {
                queryOrderReply.SetOrderNotExistReply();
                return queryOrderReply;
            }
            queryOrderReply.data = new QuerySnackOrderReplyOrder();
            queryOrderReply.data.MapFrom(snacksorder);
            queryOrderReply.SetSuccessReply();
            return queryOrderReply;
        }
        #endregion
    }
}
