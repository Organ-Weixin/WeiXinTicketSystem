using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXinTicketSystem.NetSale;
using NetSaleSvc.Api.Models;

namespace WeiXinTicketSystem.Service
{
    public class OrderService
    {
        #region ctor
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderSeatDetailEntity> _orderSeatRepository;
        private readonly IRepository<AdminOrderViewEntity> _adminOrderRepository;
        private NetSaleSvcApi _netSaleSvcApi;

        public OrderService()
        {
            //TODO: 移除内部依赖
            _orderRepository = new Repository<OrderEntity>();
            _orderSeatRepository = new Repository<OrderSeatDetailEntity>();
            _adminOrderRepository = new Repository<AdminOrderViewEntity>();
            _netSaleSvcApi = new NetSaleSvcApi();
        }
        #endregion

        /// <summary>
        /// 根据影院编码和锁座返回的订单编码获取订单
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="OrderCode"></param>
        /// <returns></returns>
        public OrderViewEntity GetOrderWithLockOrderCode(string CinemaCode, string LockOrderCode)
        {
            var order = _orderRepository.Query.Where(x => x.CinemaCode == CinemaCode
                && x.LockOrderCode == LockOrderCode).SingleOrDefault();
            if (order == null)
            {
                return null;
            }

            var orderSeats = _orderSeatRepository.Query.Where(x => x.OrderId == order.Id).ToList();
            return new OrderViewEntity
            {
                orderBaseInfo = order,
                orderSeatDetails = orderSeats.ToList()
            };
        }

        /// <summary>
        /// 根据订单编码（可以是LockOrderCode，也可以是SubmitOrderCode）获取订单信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="OrderCode"></param>
        /// <returns></returns>
        public OrderViewEntity GetOrderWithOrderCode(string CinemaCode, string OrderCode)
        {
            var order = _orderRepository.Query.Where(x => x.CinemaCode == CinemaCode
                && (x.LockOrderCode == OrderCode || x.SubmitOrderCode == OrderCode)).SingleOrDefault();
            if (order == null)
            {
                return null;
            }

            var orderSeats = _orderSeatRepository.Query.Where(x => x.OrderId == order.Id).ToList();
            return new OrderViewEntity
            {
                orderBaseInfo = order,
                orderSeatDetails = orderSeats.ToList()
            };
        }

        /// <summary>
        /// 根据取票信息获取订单
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="PrintNo"></param>
        /// <param name="VerifyCode"></param>
        /// <returns></returns>
        public OrderViewEntity GetOrderWithPrintNo(string CinemaCode, string PrintNo, string VerifyCode)
        {
            var order = _orderRepository.Query.Where(x => x.CinemaCode == CinemaCode
                && x.PrintNo == PrintNo && x.VerifyCode == VerifyCode).SingleOrDefault();
            if (order == null)
            {
                return null;
            }
            var orderSeats = _orderSeatRepository.Query.Where(x => x.OrderId == order.Id).ToList();
            return new OrderViewEntity
            {
                orderBaseInfo = order,
                orderSeatDetails = orderSeats.ToList()
            };
        }

        /// <summary>
        /// 后台分页获取订单
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <param name="keyword"></param>
        /// <param name="orderStatus"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPageList<AdminOrderViewEntity>> GetOrdersPagedAsync(string cinemaCode,
            int offset, int perPage, string keyword, OrderStatusEnum? orderStatus,
            DateTime? startDate, DateTime? endDate)
        {
            var query = _adminOrderRepository.Query
                .OrderByDescending(x => x.Created)
                .Skip(offset)
                .Take(perPage);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaName.Contains(keyword) || x.LockOrderCode.Contains(keyword)
                    || x.SubmitOrderCode.Contains(keyword) || x.FilmName.Contains(keyword)
                    || x.MobilePhone.Contains(keyword));
            }

            if (orderStatus.HasValue)
            {
                query.Where(x => x.OrderStatus == orderStatus.Value);
            }

            if (startDate.HasValue)
            {
                query.Where(x => x.Created > startDate.Value);
            }

            if (endDate.HasValue)
            {
                DateTime deadline = endDate.Value.AddDays(1);
                query.Where(x => x.Created < deadline);
            }

            return await query.ToPageListAsync();
        }
        /// <summary>
        /// 后台不分页获取订单
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="keyword"></param>
        /// <param name="thirdUserId"></param>
        /// <param name="orderStatus"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IList<AdminOrderViewEntity>> GetOrdersAsync(string cinemaCode, string keyword, OrderStatusEnum? orderStatus, DateTime? startDate, DateTime? endDate)
        {
            var query = _adminOrderRepository.Query.OrderByDescending(x => x.Created);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaName.Contains(keyword) || x.LockOrderCode.Contains(keyword)
                    || x.SubmitOrderCode.Contains(keyword) || x.FilmName.Contains(keyword)
                    || x.MobilePhone.Contains(keyword));
            }

            if (orderStatus.HasValue)
            {
                query.Where(x => x.OrderStatus == orderStatus.Value);
            }

            if (startDate.HasValue)
            {
                query.Where(x => x.Created > startDate.Value);
            }

            if (endDate.HasValue)
            {
                DateTime deadline = endDate.Value.AddDays(1);
                query.Where(x => x.Created < deadline);
            }
            return await query.ToListAsync();
        }

        public LockSeatReply LockSeat(OrderViewEntity order)
        {
            LockSeatReply lockSeatReply = _netSaleSvcApi.LockSeat(order);
            if (lockSeatReply != null)
            {
                //从返回中更新到当前数据库
                if (lockSeatReply.Status == "Success")
                {
                    order.orderBaseInfo.LockOrderCode = lockSeatReply.Order.OrderCode;
                    order.orderBaseInfo.AutoUnlockDatetime = DateTime.Parse(lockSeatReply.Order.AutoUnlockDatetime);
                    order.orderBaseInfo.LockTime = DateTime.Now;
                    order.orderBaseInfo.OrderStatus = OrderStatusEnum.Locked;
                }
                else
                {
                    order.orderBaseInfo.OrderStatus = OrderStatusEnum.LockFail;
                    order.orderBaseInfo.ErrorMessage = lockSeatReply.ErrorMessage;
                }

                //将订单保存到数据库
                Insert(order);
            }
            return lockSeatReply;
        }

        public ReleaseSeatReply ReleaseSeat(OrderViewEntity order)
        {
            ReleaseSeatReply releaseSeatReply = _netSaleSvcApi.ReleaseSeat(order);
            if (releaseSeatReply != null)
            {
                //从返回中更新到当前数据库
                if (releaseSeatReply.Status == "Success")
                {
                    order.orderBaseInfo.OrderStatus = OrderStatusEnum.Released;
                }
                else
                {
                    order.orderBaseInfo.OrderStatus = OrderStatusEnum.ReleaseFail;
                    order.orderBaseInfo.ErrorMessage = releaseSeatReply.ErrorMessage;
                }

                //更新订单信息
                UpdateOrderBaseInfo(order.orderBaseInfo);
            }
            return releaseSeatReply;
        }

        public SubmitOrderReply SubmitOrder(OrderViewEntity order)
        {
            SubmitOrderReply submitOrderReply = _netSaleSvcApi.SubmitOrder(order);
            if (submitOrderReply != null)
            {
                //从返回中更新到当前数据库
                if (submitOrderReply.Status == "Success")
                {
                    order.orderBaseInfo.SubmitOrderCode = submitOrderReply.Order.OrderCode;
                    order.orderBaseInfo.PrintNo = submitOrderReply.Order.PrintNo;
                    order.orderBaseInfo.VerifyCode = submitOrderReply.Order.VerifyCode;
                    order.orderSeatDetails.ForEach(x =>
                    {
                        var newSeat = submitOrderReply.Order.Seat.Where(y => y.SeatCode == x.SeatCode).SingleOrDefault();
                        if (newSeat != null)
                        {
                            x.FilmTicketCode = newSeat.FilmTicketCode;
                        }
                    });
                    order.orderBaseInfo.OrderStatus = OrderStatusEnum.Complete;
                    order.orderBaseInfo.SubmitTime = DateTime.Now;
                }
                else
                {
                    order.orderBaseInfo.OrderStatus = OrderStatusEnum.SubmitFail;
                    order.orderBaseInfo.ErrorMessage = submitOrderReply.ErrorMessage;
                }

                //更新订单信息
                Update(order);
            }
            return submitOrderReply;
        }
        public RefundTicketReply RefundTicket(OrderViewEntity order)
        {
            RefundTicketReply refundTicketReply = _netSaleSvcApi.RefundTicket(order.orderBaseInfo.CinemaCode, order.orderBaseInfo.PrintNo, order.orderBaseInfo.VerifyCode);
            if (refundTicketReply != null)
            {
                if (refundTicketReply.Status == "Success")
                {
                    order.orderBaseInfo.OrderStatus = OrderStatusEnum.Refund;
                    order.orderBaseInfo.RefundTime = DateTime.Now;
                }
                else
                {
                    order.orderBaseInfo.ErrorMessage = refundTicketReply.ErrorMessage;
                }
                //更新订单信息
                UpdateOrderBaseInfo(order.orderBaseInfo);
            }
            return refundTicketReply;
        }
        public QueryPrintReply QueryPrint(OrderViewEntity order)
        {
            QueryPrintReply queryPrintReply = _netSaleSvcApi.QueryPrint(order.orderBaseInfo.CinemaCode, order.orderBaseInfo.PrintNo, order.orderBaseInfo.VerifyCode);
            if(queryPrintReply!=null)
            {
                if(queryPrintReply.Status== "Success")
                {
                    order.orderBaseInfo.PrintStatus = queryPrintReply.Order.Status == NetSaleSvc.Entity.Enum.YesOrNoEnum.Yes ? true : false;
                    order.orderBaseInfo.PrintTime = DateTime.Parse(queryPrintReply.Order.PrintTime);
                }
                else
                {
                    order.orderBaseInfo.ErrorMessage = queryPrintReply.ErrorMessage;
                }
                //更新订单信息
                UpdateOrderBaseInfo(order.orderBaseInfo);
            }
            return queryPrintReply;
        }
        public FetchTicketReply FetchTicket(OrderViewEntity order)
        {
            FetchTicketReply fetchTicketReply = _netSaleSvcApi.FetchTicket(order.orderBaseInfo.CinemaCode, order.orderBaseInfo.PrintNo, order.orderBaseInfo.VerifyCode);
            if(fetchTicketReply!=null)
            {
                if(fetchTicketReply.Status== "Success")
                {
                    order.orderBaseInfo.PrintStatus = true;
                    order.orderBaseInfo.PrintTime = DateTime.Now;
                }
                else
                {
                    order.orderBaseInfo.ErrorMessage = fetchTicketReply.ErrorMessage;
                }
                //更新订单信息
                UpdateOrderBaseInfo(order.orderBaseInfo);
            }
            return fetchTicketReply;
        }
        public QueryTicketReply QueryTicket(OrderViewEntity order)
        {
            QueryTicketReply queryTicketReply = _netSaleSvcApi.QueryTicket(order.orderBaseInfo.CinemaCode, order.orderBaseInfo.PrintNo, order.orderBaseInfo.VerifyCode);
            if(queryTicketReply!=null)
            {
                if(queryTicketReply.Status== "Success")
                {
                    if (queryTicketReply.Tickets != null && queryTicketReply.Tickets.Ticket != null && queryTicketReply.Tickets.Ticket.Count > 0)
                    {
                        order.orderSeatDetails.ForEach(x =>
                        {
                            var ticket = queryTicketReply.Tickets.Ticket.Where(y => y.SeatCode == x.SeatCode).SingleOrDefault();
                            if (ticket != null)
                            {
                                x.TicketInfoCode = ticket.TicketInfoCode;
                                x.FilmTicketCode = ticket.TicketCode;
                                x.PrintFlag = ticket.PrintFlag == "1" ? true : false;
                            }
                        });
                    }
                }
                else
                {
                    order.orderBaseInfo.ErrorMessage = queryTicketReply.ErrorMessage;
                }

                //更新订单信息
                Update(order);
            }
            return queryTicketReply;
        }
        public QueryOrderReply QueryOrder(OrderViewEntity order)
        {
            QueryOrderReply queryOrderReply = _netSaleSvcApi.QueryOrder(order.orderBaseInfo.CinemaCode, order.orderBaseInfo.SubmitOrderCode);
            if (queryOrderReply != null)
            {
                if (queryOrderReply.Status == "Success")
                {
                    if (queryOrderReply.Order.Seats.Seat != null && queryOrderReply.Order.Seats.Seat.Count > 0)
                    {
                        order.orderSeatDetails.ForEach(x =>
                        {
                            var ticket = queryOrderReply.Order.Seats.Seat.Where(y => y.FilmTicketCode == x.FilmTicketCode).SingleOrDefault();
                            if (ticket != null)
                            {
                                x.PrintFlag = ticket.PrintStatus == NetSaleSvc.Entity.Enum.YesOrNoEnum.Yes ? true : false;
                            }
                        });

                        var firstTicket = queryOrderReply.Order.Seats.Seat.First();
                        order.orderBaseInfo.PrintStatus = firstTicket.PrintStatus == NetSaleSvc.Entity.Enum.YesOrNoEnum.Yes?true:false;
                        if (order.orderBaseInfo.PrintStatus == true)
                        {
                            order.orderBaseInfo.PrintTime = DateTime.Parse(firstTicket.PrintTime);
                        }

                        //退票状态
                        if (firstTicket.RefundStatus == NetSaleSvc.Entity.Enum.YesOrNoEnum.Yes)
                        {
                            order.orderBaseInfo.OrderStatus = OrderStatusEnum.Refund;
                            order.orderBaseInfo.RefundTime = DateTime.Parse(firstTicket.RefundTime);
                        }
                    }
                }
                else
                {
                    order.orderBaseInfo.ErrorMessage = queryOrderReply.ErrorMessage;
                }

                //更新订单信息
                Update(order);
            }
            return queryOrderReply;
        }

        /// <summary>
        /// 新增订单（包括订单座位信息）
        /// </summary>
        /// <param name="orderView"></param>
        public void Insert(OrderViewEntity orderView)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int orderId = _orderRepository.InsertWithTransaction(orderView.orderBaseInfo,
                            connection, transaction);

                        orderView.orderSeatDetails.ForEach(x =>
                        {
                            x.OrderId = orderId;
                            _orderSeatRepository.InsertWithTransaction(x, connection, transaction);
                        });

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 更新订单（包括座位信息）
        /// </summary>
        /// <param name="orderView"></param>
        public void Update(OrderViewEntity orderView)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        orderView.orderBaseInfo.Updated = DateTime.Now;
                        _orderRepository.UpdateWithTransaction(orderView.orderBaseInfo,
                            connection, transaction);

                        orderView.orderSeatDetails.ForEach(x =>
                        {
                            x.Updated = DateTime.Now;
                            _orderSeatRepository.UpdateWithTransaction(x, connection, transaction);
                        });

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 只更新订单信息，不更新订单座位信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateOrderBaseInfo(OrderEntity entity)
        {
            entity.Updated = DateTime.Now;
            _orderRepository.Update(entity);
        }
    }
}
