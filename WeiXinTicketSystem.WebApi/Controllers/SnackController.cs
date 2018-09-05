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
using System.Xml.Linq;

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
        UserInfoService _userInfoService;
        CinemaPaySettingsService _cinemaPaySettingsService;
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
            _userInfoService = new UserInfoService();
            _cinemaPaySettingsService = new CinemaPaySettingsService();
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

        //目前价格为1分
        #region 支付套餐
        [HttpPost]
        public PrePaySnackOrderReply PrePaySnackOrder(PrePaySnackOrderQueryJson QueryJson)
        {
            PrePaySnackOrderReply prePaySnackOrderReply = new PrePaySnackOrderReply();
            //校验参数
            if (!prePaySnackOrderReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.OrderCode,QueryJson.Snacks))
            {
                return prePaySnackOrderReply;
            }
            //获取用户信息(渠道)
            UserInfoEntity UserInfo = _userInfoService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                prePaySnackOrderReply.SetUserCredentialInvalidReply();
                return prePaySnackOrderReply;
            }
            //获取影院的支付配置
            var cinemaPaymentSetting = _cinemaPaySettingsService.GetCinemaPaySettingsByCinemaCode(QueryJson.CinemaCode);
            if (cinemaPaymentSetting == null || cinemaPaymentSetting.WxpayAppId == "" || cinemaPaymentSetting.WxpayMchId == "")
            {
                prePaySnackOrderReply.SetCinemaPaySettingInvalidReply();
                return prePaySnackOrderReply;
            }
            //验证订单是否存在
            var order = _snacksOrderService.GetSnacksOrderWithOrderCode(QueryJson.CinemaCode, QueryJson.OrderCode);
            if (order == null || (order.OrderBaseInfo.OrderStatus != SnackOrderStatusEnum.Booked && order.OrderBaseInfo.OrderStatus != SnackOrderStatusEnum.PayFail))
            {
                prePaySnackOrderReply.SetOrderNotExistReply();
                return prePaySnackOrderReply;
            }
            //验证套餐数量
            if (QueryJson.Snacks.Count != order.OrderBaseInfo.SnacksCount)
            {
                prePaySnackOrderReply.SetSnackNumberInvalidReply();
                return prePaySnackOrderReply;
            }
            //验证优惠券是否使用
            foreach (var snack in QueryJson.Snacks)
            {
                if (!string.IsNullOrEmpty(snack.ConponCode))
                {
                    var conpon = _conponService.GetConponByConponCode(snack.ConponCode);
                    if (conpon.Status != ConponStatusEnum.NotUsed)
                    {
                        prePaySnackOrderReply.SetConponNotExistOrUsedReply();
                        return prePaySnackOrderReply;
                    }
                }
            }
            //更新订单信息
            order.MapFrom(QueryJson);
            //开始调用微信支付接口
            //微信支付统一下单接口
            string UnifiedOrderUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            //时间戳
            string timeStamp = WxPayUtil.getTimestamp();
            //随机字符串 
            string nonceStr = WxPayUtil.getNoncestr();
            //创建支付应答对象
            var packageReqHandler = new RequestHandler(System.Web.HttpContext.Current);
            //初始化
            packageReqHandler.init();
            //设置package订单参数
            packageReqHandler.setParameter("appid", cinemaPaymentSetting.WxpayAppId);
            packageReqHandler.setParameter("body", cinemaPaymentSetting.CinemaName + "-" + order.OrderBaseInfo.SendTime.Month.ToString().PadLeft(2, '0')
            + "月" + order.OrderBaseInfo.SendTime.Day.ToString().PadLeft(2, '0') + "日" + order.OrderBaseInfo.SendTime.ToString("HH:mm") + " "
            + " 套餐（" + order.OrderBaseInfo.SnacksCount.ToString() + "个）"); //商品信息
            packageReqHandler.setParameter("mch_id", cinemaPaymentSetting.WxpayMchId);
            packageReqHandler.setParameter("nonce_str", nonceStr.ToLower());
            packageReqHandler.setParameter("notify_url", "https://xcx.80piao.com/api/Snack/WxPayNotify");
            packageReqHandler.setParameter("openid", order.OrderBaseInfo.OpenID);
            packageReqHandler.setParameter("out_trade_no", DateTime.Now.ToString("yyyyMMddHHmmss") + QueryJson.CinemaCode + order.OrderBaseInfo.Id.ToString()); //商家订单号
            packageReqHandler.setParameter("time_expire", order.OrderBaseInfo.AutoUnLockDateTime.ToString("yyyyMMddHHmmss"));
            packageReqHandler.setParameter("spbill_create_ip", System.Web.HttpContext.Current.Request.UserHostAddress); //用户的公网ip，不是商户服务器IP
            //总的支付金额=总的销售价格-总的优惠金额
            decimal totalPrice = order.OrderBaseInfo.TotalPrice - order.OrderBaseInfo.TotalConponPrice ?? 0;
            //packageReqHandler.setParameter("total_fee", (Convert.ToInt32(totalPrice * 100)).ToString()); //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.setParameter("total_fee", "1");
            packageReqHandler.setParameter("trade_type", "JSAPI");
            string sign = packageReqHandler.CreateMd5Sign("key", cinemaPaymentSetting.WxpayKey);
            packageReqHandler.setParameter("sign", sign);
            //把参数组装成xml
            string data = packageReqHandler.parseXML();
            string strPrepayXml = HttpUtil.Send(data, UnifiedOrderUrl);
            //获取prepay_id
            XElement prepayXml = XElement.Parse(strPrepayXml.Replace("<![CDATA[", "").Replace("]]>", ""));
            XElement prepayIdNode = prepayXml.Descendants("prepay_id").SingleOrDefault();
            if (prepayIdNode == null)
            {
                XElement errorCodeNode = prepayXml.Descendants("return_code").SingleOrDefault();
                XElement errorMsgNode = prepayXml.Descendants("return_msg").SingleOrDefault();
                string errorCode = errorCodeNode == null ? string.Empty : errorCodeNode.Value;
                string errorMsg = errorMsgNode == null ? string.Empty : errorMsgNode.Value;

                prePaySnackOrderReply.Status = "Failure";
                prePaySnackOrderReply.ErrorCode = errorCode;
                prePaySnackOrderReply.ErrorMessage = errorMsg;
            }
            else
            {
                string prepayId = prepayIdNode.Value;
                //支付扩展包
                string package = string.Format("prepay_id={0}", prepayId);
                var paySignReqHandler = new RequestHandler(System.Web.HttpContext.Current);
                paySignReqHandler.setParameter("appId", cinemaPaymentSetting.WxpayAppId);
                paySignReqHandler.setParameter("timeStamp", timeStamp);
                paySignReqHandler.setParameter("nonceStr", nonceStr);
                paySignReqHandler.setParameter("package", package);
                paySignReqHandler.setParameter("signType", "MD5");
                string paySign = paySignReqHandler.CreateMd5Sign("key", cinemaPaymentSetting.WxpayKey);
                //准备返回参数
                prePaySnackOrderReply.data = new PrePaySnackOrderReplyParameters();
                prePaySnackOrderReply.data.timeStamp = timeStamp;
                prePaySnackOrderReply.data.nonceStr = nonceStr;
                prePaySnackOrderReply.data.package = package;
                prePaySnackOrderReply.data.signType = "MD5";
                prePaySnackOrderReply.data.paySign = paySign;

                prePaySnackOrderReply.SetSuccessReply();
            }
            //更新订单信息
            _snacksOrderService.Update(order);
            return prePaySnackOrderReply;
        }

        /// <summary>
        /// 异步返回接收微信支付返回
        /// </summary>
        public void WxPayNotify()
        {
            //创建ResponseHandler实例
            ResponseHandler resHandler = new ResponseHandler(System.Web.HttpContext.Current);
            //商户系统的订单号，与请求一致。
            string out_trade_no = resHandler.getParameter("out_trade_no");
            string CinemaCode = out_trade_no.Substring("yyyyMMddHHmmss".Length, 8);
            var cinemaPaymentSetting = _cinemaPaySettingsService.GetCinemaPaySettingsByCinemaCode(CinemaCode);
            resHandler.setKey(cinemaPaymentSetting.WxpayKey); //appkey paysignkey(非appkey 在微信商户平台设置 (md5))
            //判断签名
            string error = "";
            if (resHandler.isWXsign(out error))
            {
                #region 协议参数=====================================
                //--------------协议参数--------------------------------------------------------
                //SUCCESS/FAIL此字段是通信标识，非交易标识，交易是否成功需要查
                string return_code = resHandler.getParameter("return_code");
                //返回信息，如非空，为错误原因签名失败参数格式校验错误
                string return_msg = resHandler.getParameter("return_msg");

                //以下字段在 return_code 为 SUCCESS 的时候有返回--------------------------------
                //微信分配的公众账号 ID
                string appid = resHandler.getParameter("appid");
                //微信支付分配的商户号
                string mch_id = resHandler.getParameter("mch_id");
                //微信支付分配的终端设备号
                string device_info = resHandler.getParameter("device_info");
                //微信分配的公众账号 ID
                string nonce_str = resHandler.getParameter("nonce_str");
                //业务结果 SUCCESS/FAIL
                string result_code = resHandler.getParameter("result_code");
                //错误代码 
                string err_code = resHandler.getParameter("err_code");
                //结果信息描述
                string err_code_des = resHandler.getParameter("err_code_des");

                //以下字段在 return_code 和 result_code 都为 SUCCESS 的时候有返回---------------
                //-------------业务参数---------------------------------------------------------
                //用户在商户 appid 下的唯一标识
                string openid = resHandler.getParameter("openid");
                //用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
                string is_subscribe = resHandler.getParameter("is_subscribe");
                //JSAPI、NATIVE、MICROPAY、APP
                string trade_type = resHandler.getParameter("trade_type");
                //银行类型，采用字符串类型的银行标识
                string bank_type = resHandler.getParameter("bank_type");
                //订单总金额，单位为分
                string total_fee = resHandler.getParameter("total_fee");
                //货币类型，符合 ISO 4217 标准的三位字母代码，默认人民币：CNY
                string fee_type = resHandler.getParameter("fee_type");
                //微信支付订单号
                string transaction_id = resHandler.getParameter("transaction_id");
                //商家数据包，原样返回
                string attach = resHandler.getParameter("attach");
                //支 付 完 成 时 间 ， 格 式 为yyyyMMddhhmmss，如 2009 年12 月27日 9点 10分 10 秒表示为 20091227091010。时区为 GMT+8 beijing。该时间取自微信支付服务器
                string time_end = resHandler.getParameter("time_end");
                #endregion

                int OrderID = int.Parse(out_trade_no.Substring("yyyyMMddHHmmss".Length + 8).Trim());
                var order = _snacksOrderService.GetSnacksOrderWithId(CinemaCode,OrderID);
                //支付成功
                if (return_code.Equals("SUCCESS") && result_code.Equals("SUCCESS"))
                {
                    //更新订单主表
                    if (!order.OrderBaseInfo.OrderPayFlag.Value)
                    {
                        order.OrderBaseInfo.OrderStatus = SnackOrderStatusEnum.Payed;
                        order.OrderBaseInfo.Updated = DateTime.Now;
                        order.OrderBaseInfo.OrderPayType = PayTypeEnum.WxPay;
                        order.OrderBaseInfo.OrderPayFlag = true;
                        order.OrderBaseInfo.OrderPayTime = DateTime.Now;
                        order.OrderBaseInfo.OrderTradeNo = transaction_id;
                        _snacksOrderService.Update(order.OrderBaseInfo);
                    }
                    //更新优惠券已使用
                    foreach (var snack in order.SnackOrderDetails)
                    {
                        if (!string.IsNullOrEmpty(snack.ConponCode))
                        {
                            var conpon = _conponService.GetConponByConponCode(snack.ConponCode);
                            conpon.Status = ConponStatusEnum.Used;
                            conpon.Updated = DateTime.Now;
                            conpon.Price = snack.ConponPrice;
                            conpon.UseDate = DateTime.Now;
                            _conponService.Update(conpon);
                        }
                    }
                }
                else
                {
                    order.OrderBaseInfo.OrderStatus = SnackOrderStatusEnum.PayFail;
                    order.OrderBaseInfo.Updated = DateTime.Now;
                    //order.OrderBaseInfo.ErrorMessage = err_code_des;
                    _snacksOrderService.Update(order.OrderBaseInfo);
                }
            }
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

        #region 查询推荐套餐

        [HttpGet]
        public async Task<QuerySnacksReply> QueryRecommandSnacks(string UserName, string Password, string CinemaCode, string TypeCode)
        {
            QuerySnacksReply querySnacksReply = new QuerySnacksReply();
            //校验参数
            if (!querySnacksReply.RequestInfoGuard(UserName, Password, CinemaCode, TypeCode))
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
            var Snacks = await _snackService.GetRecommandSnacksByTypeCodeAsync(CinemaCode, TypeCode);

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
    }
}
