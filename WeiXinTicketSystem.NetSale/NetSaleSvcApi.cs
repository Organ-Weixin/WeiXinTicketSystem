using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetSaleSvc.Api.Core;
using NetSaleSvc.Api.Models;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;

namespace WeiXinTicketSystem.NetSale
{
    public class NetSaleSvcApi
    {
        #region 私有变量
        /// <summary>
        /// 服务引用
        /// </summary>
        private NetSaleSvcCore netSaleService=NetSaleSvcCore.Instance;
        /// <summary>
        /// 合作商户号
        /// </summary>
        public static string pAppCode = "80movie";
        /// <summary>
        /// 密钥
        /// </summary>
        public static string pKeyInfo = "4279CABCAD78D648736945BF10875A8B";
        #endregion

        #region 获取影厅信息
        public QueryCinemaReply QueryCinema(string CinemaCode)
        {
            QueryCinemaReply queryCinemaReply = new QueryCinemaReply();
            try
            {
                queryCinemaReply = netSaleService.QueryCinema(pAppCode, pKeyInfo, CinemaCode);
            }
            catch (Exception e)
            {
                //do nothing
            }
            return queryCinemaReply;
        }
        #endregion

        #region 获取影厅座位
        public QuerySeatReply QuerySeat(string CinemaCode,string ScreenCode)
        {
            QuerySeatReply querySeatReply = new QuerySeatReply();
            try
            {
                querySeatReply= netSaleService.QuerySeat(pAppCode, pKeyInfo, CinemaCode, ScreenCode);
            }
            catch (Exception e)
            {
                //do nothing
            }
            return querySeatReply;
        }
        #endregion

        #region 获取排期
        public QuerySessionReply QuerySession(string CinemaCode,DateTime StartDate,DateTime EndDate)
        {
            QuerySessionReply querySessionReply = new QuerySessionReply();
            try
            {
                querySessionReply= netSaleService.QuerySession(pAppCode, pKeyInfo, CinemaCode, StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"));
            }
            catch (Exception e)
            {
                //do nothing
            }
            return querySessionReply;
        }
        #endregion

        #region 获取场次座位信息
        /// <summary>
        /// 获取场次座位信息
        /// </summary>
        /// <param name="pAppCode"></param>
        /// <param name="pKeyInfo"></param>
        /// <param name="url"></param>
        /// <param name="soap"></param>
        /// <param name="CinemaCode"></param>
        /// <param name="SessionCode"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public QuerySessionSeatReply QuerySessionSeat(string CinemaCode, string SessionCode, string Status)
        {
            QuerySessionSeatReply querySessionSeatReply = new QuerySessionSeatReply();
            try
            {
                querySessionSeatReply= netSaleService.QuerySessionSeat(pAppCode, pKeyInfo, CinemaCode, SessionCode, Status);
            }
            catch
            {
                return null;
            }
            return querySessionSeatReply;
        }
        #endregion

        #region 锁座
        /// <summary>
        /// 锁座
        /// </summary>
        /// <param name="pAppCode"></param>
        /// <param name="pKeyInfo"></param>
        /// <param name="url"></param>
        /// <param name="soap"></param>
        /// <param name="CinemaCode"></param>
        /// <param name="SessionCode"></param>
        /// <param name="Seats">多个座位用"|"隔开</param>
        /// <returns></returns>
        public LockSeatReply LockSeat(OrderViewEntity order)
        {
            LockSeatReply lockSeatReply = new LockSeatReply();
            LockSeatQueryXml param = new LockSeatQueryXml
            {
                CinemaCode = order.orderBaseInfo.CinemaCode,
                Order = new LockSeatQueryXmlOrder
                {
                    PayType = "0",//现金支付
                    SessionCode = order.orderBaseInfo.SessionCode,
                    Count = order.orderBaseInfo.TicketCount,
                    Seat = order.orderSeatDetails.Select(x => new LockSeatQueryXmlSeat
                    {
                        SeatCode = x.SeatCode,
                        Price = x.Price,
                        Fee = x.Fee
                    }).ToList()
                }
            };
            try
            {
                string QueryXml = ToXml(param);
                lockSeatReply= netSaleService.LockSeat(pAppCode, pKeyInfo, QueryXml);
            }
            catch
            {
                //do nothing
            }

            return lockSeatReply;
        }
        #endregion

        #region 确认订单
        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="pAppCode"></param>
        /// <param name="pKeyInfo"></param>
        /// <param name="url"></param>
        /// <param name="soap"></param>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public SubmitOrderReply SubmitOrder(OrderViewEntity order)
        {
            SubmitOrderReply submitOrderReply = new SubmitOrderReply();
            SubmitOrderQueryXml param = new SubmitOrderQueryXml
            {
                CinemaCode = order.orderBaseInfo.CinemaCode,
                Order = new SubmitOrderQueryXmlOrder
                {
                    PaySeqNo = "",
                    OrderCode = order.orderBaseInfo.LockOrderCode,
                    SessionCode = order.orderBaseInfo.SessionCode,
                    Count = order.orderBaseInfo.TicketCount,
                    MobilePhone = order.orderBaseInfo.MobilePhone,
                    Seat = order.orderSeatDetails.Select(x => new SubmitOrderQueryXmlSeat
                    {
                        SeatCode = x.SeatCode,
                        Price = x.Price,
                        RealPrice = x.SalePrice,
                        Fee = x.Fee
                    }).ToList()
                }
            };
            try
            {
                string QueryXml = ToXml(param);
                submitOrderReply= netSaleService.SubmitOrder(pAppCode, pKeyInfo, QueryXml);
            }
            catch (Exception e)
            {
                //do nothing
            }
            return submitOrderReply;
        }
        #endregion

        #region 查询影票信息
        public QueryTicketReply QueryTicket(string CinemaCode,string PrintNo,string VerifyCode)
        {
            QueryTicketReply queryTicketReply = new QueryTicketReply();
            try
            {
                queryTicketReply= netSaleService.QueryTicket(pAppCode, pKeyInfo, CinemaCode, PrintNo, VerifyCode);
            }
            catch(Exception e)
            {
                //do nothing
            }
            return queryTicketReply;
        }
        #endregion

        #region 确认出票
        public FetchTicketReply FetchTicket(string CinemaCode, string PrintNo, string VerifyCode)
        {
            FetchTicketReply fetchTicketReply = new FetchTicketReply();
            try
            {
                fetchTicketReply= netSaleService.FetchTicket(pAppCode, pKeyInfo, CinemaCode, PrintNo, VerifyCode);
            }
            catch (Exception e)
            {
                //do nothing
            }
            return fetchTicketReply;
        }
        #endregion

        #region 退票
        public RefundTicketReply RefundTicket(string CinemaCode,string PrintNo,string VerifyCode)
        {
            RefundTicketReply refundTicketReply = new RefundTicketReply();
            try
            {
                refundTicketReply= netSaleService.RefundTicket(pAppCode, pKeyInfo, CinemaCode, PrintNo, VerifyCode);
            }
            catch (Exception e)
            {
                //do nothing
            }
            return refundTicketReply;
        }
        #endregion

        #region
        public static string ToXml<T>(T t)
        {
            return t.Serialize();
        }
        #endregion
    }
}
