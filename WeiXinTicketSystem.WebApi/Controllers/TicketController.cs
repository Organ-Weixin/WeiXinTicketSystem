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

using NetSaleSvc.Api.Models;
using NetSaleSvc.Api.Core;
using System.Xml.Linq;

namespace WeiXinTicketSystem.WebApi.Controllers
{
    public class TicketController : ApiController
    {
        #region 私有变量
        /// <summary>
        /// 服务引用
        /// </summary>
        private NetSaleSvcCore netSaleService = NetSaleSvcCore.Instance;
        OrderService _orderService = new OrderService();
        UserInfoService _userInfoService = new UserInfoService();
        CinemaService _cinemaService = new CinemaService();
        UserCinemaService _userCinemaService = new UserCinemaService();
        MemberCardService _memberCardService = new MemberCardService();
        CinemaPaySettingsService _cinemaPaySettingsService = new CinemaPaySettingsService();
        ConponService _conponService = new ConponService();
        #endregion

        [HttpPost]
        public LockSeatReply LockSeat(NetSaleQueryJson QueryJson)
        {
            LockSeatReply lockSeatReply = netSaleService.LockSeat(QueryJson.UserName, QueryJson.Password, QueryJson.QueryXml);
            //锁座时新增订单需要传入OpenID,之后修改订单就不需要再操作
            if (lockSeatReply.Status == "Success")
            {
                var orderbase = _orderService.GetOrderBaseInfoByLockOrderCode(lockSeatReply.Order.OrderCode);
                orderbase.OpenID = QueryJson.OpenID;
                _orderService.UpdateOrderBaseInfo(orderbase);
            }
            return lockSeatReply;
        }

        [HttpPost]
        public ReleaseSeatReply ReleaseSeat(NetSaleQueryJson QueryJson)
        {
            return netSaleService.ReleaseSeat(QueryJson.UserName, QueryJson.Password, QueryJson.QueryXml);
        }

        //暂时都是1分钱
        [HttpPost]
        public PrePayOrderReply PrePayOrder(PrePayOrderQueryJson QueryJson)
        {
            PrePayOrderReply prePayOrderReply = new PrePayOrderReply();
            //校验参数
            if (!prePayOrderReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.OrderCode, QueryJson.Seats))
            {
                return prePayOrderReply;
            }
            //获取用户信息(渠道)
            UserInfoEntity UserInfo = _userInfoService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                prePayOrderReply.SetUserCredentialInvalidReply();
                return prePayOrderReply;
            }
            //获取影院的支付配置
            var cinemaPaymentSetting = _cinemaPaySettingsService.GetCinemaPaySettingsByCinemaCode(QueryJson.CinemaCode);
            if (cinemaPaymentSetting == null || cinemaPaymentSetting.WxpayAppId == "" || cinemaPaymentSetting.WxpayMchId == "")
            {
                prePayOrderReply.SetCinemaPaySettingInvalidReply();
                return prePayOrderReply;
            }
            //验证订单是否存在
            var order = _orderService.GetOrderWithLockOrderCode(QueryJson.CinemaCode, QueryJson.OrderCode);
            if (order == null || (order.orderBaseInfo.OrderStatus != OrderStatusEnum.Locked && order.orderBaseInfo.OrderStatus != OrderStatusEnum.PayFail))
            {
                prePayOrderReply.SetOrderNotExistReply();
                return prePayOrderReply;
            }
            //验证座位数量
            if (QueryJson.Seats.Count != order.orderBaseInfo.TicketCount)
            {
                prePayOrderReply.SetSeatCountInvalidReply();
                return prePayOrderReply;
            }
            //验证优惠券是否使用
            foreach(var seat in QueryJson.Seats)
            {
                if(!string.IsNullOrEmpty(seat.ConponCode))
                {
                    var conpon = _conponService.GetConponByConponCode(seat.ConponCode);
                    if(conpon.Status!=ConponStatusEnum.NotUsed)
                    {
                        prePayOrderReply.SetConponNotExistOrUsedReply();
                        return prePayOrderReply;
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
            packageReqHandler.setParameter("body", cinemaPaymentSetting.CinemaName + "-" + order.orderBaseInfo.SessionTime.Month.ToString().PadLeft(2, '0')
            + "月" + order.orderBaseInfo.SessionTime.Day.ToString().PadLeft(2, '0') + "日" + order.orderBaseInfo.SessionTime.ToString("HH:mm") + " " + order.orderBaseInfo.FilmName
            + " 电影票（" + order.orderBaseInfo.TicketCount.ToString() + "张）"); //商品信息
            packageReqHandler.setParameter("mch_id", cinemaPaymentSetting.WxpayMchId);
            packageReqHandler.setParameter("nonce_str", nonceStr.ToLower());
            packageReqHandler.setParameter("notify_url", "https://xcx.80piao.com/api/Ticket/WxPayNotify");
            packageReqHandler.setParameter("openid", order.orderBaseInfo.OpenID);
            packageReqHandler.setParameter("out_trade_no", DateTime.Now.ToString("yyyyMMddHHmmss") + QueryJson.CinemaCode + order.orderBaseInfo.Id.ToString()); //商家订单号
            packageReqHandler.setParameter("time_expire", order.orderBaseInfo.AutoUnlockDatetime.Value.ToString("yyyyMMddHHmmss"));
            packageReqHandler.setParameter("spbill_create_ip", System.Web.HttpContext.Current.Request.UserHostAddress); //用户的公网ip，不是商户服务器IP
            //总的支付金额=总的销售价格+总的情侣座差价+总的客人支付服务费-总的优惠金额
            decimal totalPrice = order.orderBaseInfo.TotalSalePrice + order.orderBaseInfo.TotalLoveSeatDifferences ?? 0 + order.orderBaseInfo.TotalGuestPayFee ?? 0 - order.orderBaseInfo.TotalConponPrice ?? 0;
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

                prePayOrderReply.Status = "Failure";
                prePayOrderReply.ErrorCode = errorCode;
                prePayOrderReply.ErrorMessage = errorMsg;
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
                prePayOrderReply.data = new PrePayOrderReplyParameters();
                prePayOrderReply.data.timeStamp = timeStamp;
                prePayOrderReply.data.nonceStr = nonceStr;
                prePayOrderReply.data.package = package;
                prePayOrderReply.data.signType = "MD5";
                prePayOrderReply.data.paySign = paySign;

                prePayOrderReply.SetSuccessReply();
            }
            //更新订单信息
            _orderService.Update(order);
            return prePayOrderReply;

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
                var order = _orderService.GetOrderWithId(OrderID);
                //支付成功
                if (return_code.Equals("SUCCESS") && result_code.Equals("SUCCESS"))
                {
                    //更新订单主表
                    if (order.orderBaseInfo.PayFlag != 1)
                    {
                        order.orderBaseInfo.OrderStatus = OrderStatusEnum.Payed;
                        order.orderBaseInfo.Updated = DateTime.Now;
                        order.orderBaseInfo.OrderPayType = PayTypeEnum.WxPay;
                        order.orderBaseInfo.PayFlag = 1;
                        order.orderBaseInfo.PayTime = DateTime.Now;
                        order.orderBaseInfo.OrderTradeNo = transaction_id;
                        _orderService.UpdateOrderBaseInfo(order.orderBaseInfo);
                    }
                    //更新优惠券已使用
                    foreach(var seat in order.orderSeatDetails)
                    {
                        if(!string.IsNullOrEmpty(seat.ConponCode))
                        {
                            var conpon = _conponService.GetConponByConponCode(seat.ConponCode);
                            conpon.Status = ConponStatusEnum.Used;
                            conpon.Updated = DateTime.Now;
                            conpon.Price = seat.ConponPrice;
                            conpon.UseDate = DateTime.Now;
                            _conponService.Update(conpon);
                        }
                    }
                }
                else
                {
                    order.orderBaseInfo.OrderStatus = OrderStatusEnum.PayFail;
                    order.orderBaseInfo.Updated = DateTime.Now;
                    order.orderBaseInfo.ErrorMessage = err_code_des;
                    _orderService.UpdateOrderBaseInfo(order.orderBaseInfo);
                }
            }
        }

        [HttpPost]
        public SubmitOrderReply SubmitOrder(NetSaleQueryJson QueryJson)
        {
            return netSaleService.SubmitOrder(QueryJson.UserName, QueryJson.Password, QueryJson.QueryXml);
        }

        /// <summary>
        /// 1905的会员卡支付提交另外实现
        /// </summary>
        /// <param name="QueryJson"></param>
        /// <returns></returns>
        [HttpPost]
        public SubmitOrderReply SubmitOrder_1905CardPay(SubmitOrder_1905CardPayQueryJson QueryJson)
        {
            SubmitOrderReply submitOrderReply = new SubmitOrderReply();
            //校验参数
            if (!submitOrderReply.RequestInfoGuard(QueryJson.UserName, QueryJson.Password, QueryJson.CinemaCode, QueryJson.CardNo, QueryJson.CardPassword, QueryJson.OrderCode, QueryJson.LowestPrice.ToString(), QueryJson.Seats))
            {
                return submitOrderReply;
            }
            //获取用户信息(渠道)
            UserInfoEntity UserInfo = _userInfoService.GetUserInfoByUserCredential(QueryJson.UserName, QueryJson.Password);
            if (UserInfo == null)
            {
                submitOrderReply.SetUserCredentialInvalidReply();
                return submitOrderReply;
            }
            //验证影院是否存在且可访问(包含接口账号信息)
            var userCinema = _userCinemaService.GetUserCinema(UserInfo.Id, QueryJson.CinemaCode);
            if (userCinema == null)
            {
                submitOrderReply.SetCinemaInvalidReply();
                return submitOrderReply;
            }
            //验证会员卡是否存在
            var memberCard = _memberCardService.GetMemberCardByCardNo(QueryJson.CinemaCode, QueryJson.CardNo);
            if (memberCard == null)
            {
                submitOrderReply.SetMemberCardInvalidReply();
                return submitOrderReply;
            }
            //验证订单是否存在
            OrderViewEntity order = null;
            if (!string.IsNullOrEmpty(QueryJson.OrderCode))
            {
                order = _orderService.GetOrderWithLockOrderCode(QueryJson.CinemaCode
                    , QueryJson.OrderCode);
            }
            if (order == null
                || (order.orderBaseInfo.OrderStatus != OrderStatusEnum.Locked
                && order.orderBaseInfo.OrderStatus != OrderStatusEnum.SubmitFail))
            {
                submitOrderReply.SetOrderNotExistReply();
                return submitOrderReply;
            }
            //验证座位数量
            if (QueryJson.Seats.Count != order.orderBaseInfo.TicketCount)
            {
                submitOrderReply.SetSeatCountInvalidReply();
                return submitOrderReply;
            }
            //更新订单信息
            order.MapFrom(QueryJson);
            //开始调用1905接口
            string pSeatNo = string.Join(",", order.orderSeatDetails.Select(x => x.SeatCode));
            string pMemberPrice = string.Join(",", order.orderSeatDetails.Select(x => x.SalePrice));
            string pFee = string.Join(",", order.orderSeatDetails.Select(x => x.Fee));
            string pVerifyInfo = MD5Helper.MD5Encrypt(userCinema.RealUserName + QueryJson.CardNo + QueryJson.CardPassword + pFee + QueryJson.LowestPrice.ToString("0.##") + pMemberPrice + order.orderBaseInfo.LockOrderCode + pSeatNo + userCinema.RealPassword).ToLower();
            SortedDictionary<string, string> paramDic = new SortedDictionary<string, string>
            {
                { "pAppCode", userCinema.RealUserName },
                { "pCardNo",QueryJson.CardNo },
                { "pCardPwd",QueryJson.CardPassword },
                { "pFee",pFee },
                { "pLowestPrice",QueryJson.LowestPrice.ToString("0.##") },
                { "pMemberPrice",pMemberPrice },
                { "pOrderID", order.orderBaseInfo.LockOrderCode },
                { "pSeatNo",pSeatNo },
                { "pVerifyInfo", pVerifyInfo }
            };
            string SellTicketCustomResult = HttpHelper.VisitUrl(userCinema.Url + "/SellTicketCustom/member", FormatParam(paramDic));
            Dy1905SellTicketCustomReply Dy1905Reply = SellTicketCustomResult.Deserialize<Dy1905SellTicketCustomReply>();
            if (Dy1905Reply.ResultCode == "0")
            {
                //更新订单信息
                order.orderBaseInfo.SubmitOrderCode = Dy1905Reply.OrderNo;
                order.orderBaseInfo.PrintNo = Dy1905Reply.PrintNo;
                order.orderBaseInfo.VerifyCode = Dy1905Reply.VerifyCode;
                //同时更新订单支付信息，会员卡支付和提交是一起的
                order.orderBaseInfo.PayFlag = 1;
                order.orderBaseInfo.OrderPayType = PayTypeEnum.MemberCardPay;
                order.orderBaseInfo.PayTime = DateTime.Now;
                order.orderBaseInfo.CardNo = QueryJson.CardNo;

                order.orderBaseInfo.OrderStatus = OrderStatusEnum.Submited;
                order.orderBaseInfo.SubmitTime = DateTime.Now;

                //准备返回数据
                submitOrderReply.Order = new SubmitOrderReplyOrder();
                submitOrderReply.Order.CinemaType = (NetSaleSvc.Entity.Enum.CinemaTypeEnum)userCinema.CinemaType;
                submitOrderReply.Order.OrderCode = order.orderBaseInfo.SubmitOrderCode;
                submitOrderReply.Order.SessionCode = order.orderBaseInfo.SessionCode;
                submitOrderReply.Order.Count = order.orderBaseInfo.TicketCount;
                submitOrderReply.Order.PrintNo = order.orderBaseInfo.PrintNo;
                submitOrderReply.Order.VerifyCode = order.orderBaseInfo.VerifyCode;
                submitOrderReply.Order.Seat = order.orderSeatDetails.Select(x =>
                    new SubmitOrderReplySeat
                    {
                        SeatCode = x.SeatCode,
                        FilmTicketCode = x.FilmTicketCode
                    }).ToList();

                submitOrderReply.SetSuccessReply();
            }
            else
            {
                order.orderBaseInfo.OrderStatus = OrderStatusEnum.SubmitFail;
                order.orderBaseInfo.ErrorMessage = Dy1905Reply.ResultCode;

                submitOrderReply.Status = "Failure";
                submitOrderReply.ErrorCode = Dy1905Reply.ResultCode;
            }
            //更新订单信息
            _orderService.Update(order);

            return submitOrderReply;
        }

        [HttpGet]
        public RefundTicketReply RefundTicket(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            return netSaleService.RefundTicket(UserName, Password, CinemaCode, PrintNo, VerifyCode);
        }

        [HttpGet]
        public QueryOrderReply QueryOrder(string UserName, string Password, string CinemaCode, string OrderCode)
        {
            return netSaleService.QueryOrder(UserName, Password, CinemaCode, OrderCode);
        }

        [HttpGet]
        public QueryTicketReply QueryTicket(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            return netSaleService.QueryTicket(UserName, Password, CinemaCode, PrintNo, VerifyCode);
        }

        [HttpGet]
        public QueryPrintReply QueryPrint(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            return netSaleService.QueryPrint(UserName, Password, CinemaCode, PrintNo, VerifyCode);
        }

        [HttpGet]
        public FetchTicketReply FetchTicket(string UserName, string Password, string CinemaCode, string PrintNo, string VerifyCode)
        {
            return netSaleService.FetchTicket(UserName, Password, CinemaCode, PrintNo, VerifyCode);
        }

        /// <summary>
        /// 参数格式化
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static string FormatParam(SortedDictionary<string, string> param)
        {
            string queryParams = "";
            foreach (var key in param.Keys)
            {
                string value = param[key];
                if (value != null)
                    queryParams += key + "=" + value.ToString() + "&";

            }
            if (queryParams.Length > 0)
                queryParams = queryParams.Substring(0, queryParams.Length - 1); //remove last '&'
            return queryParams;
        }
    }
}
