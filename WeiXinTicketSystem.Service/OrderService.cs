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

namespace WeiXinTicketSystem.Service
{
    public class OrderService
    {
        #region ctor
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderSeatDetailEntity> _orderSeatRepository;
        private readonly IRepository<AdminOrderViewEntity> _adminOrderRepository;

        public OrderService()
        {
            //TODO: 移除内部依赖
            _orderRepository = new Repository<OrderEntity>();
            _orderSeatRepository = new Repository<OrderSeatDetailEntity>();
            _adminOrderRepository =new Repository<AdminOrderViewEntity>();
        }
        #endregion

        /// <summary>
        /// 根据订单号获取订单基本信息
        /// </summary>
        /// <param name="LockOrderCode"></param>
        /// <returns></returns>
        public OrderEntity GetOrderBaseInfoByLockOrderCode(string LockOrderCode)
        {
            return _orderRepository.Query.Where(x => x.LockOrderCode == LockOrderCode).SingleOrDefault();
        }
        /// <summary>
        /// 根据id获取订单基本信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public OrderViewEntity GetOrderWithId(int Id)
        {
            var order = _orderRepository.Query.Where(x => x.Id == Id).SingleOrDefault();
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
        /// <param name="thirdUserId"></param>
        /// <param name="orderStatus"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPageList<AdminOrderViewEntity>> GetOrdersPagedAsync(string cinemaCode,
            int offset, int perPage, string keyword, int? thirdUserId, OrderStatusEnum? orderStatus,
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

            if (thirdUserId.HasValue)
            {
                query.Where(x => x.UserId == thirdUserId.Value);
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
        /// 根据用户openID获取用户购票订单列表
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <param name="openID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPageList<OrderEntity>> GetOrdersByOpenIDPagedAsync(string cinemaCode,
            string openID, DateTime? startDate, DateTime? endDate, int currentpage, int pagesize)
        {
            int offset = (currentpage - 1) * pagesize;
            var query = _orderRepository.Query
                .OrderByDescending(x => x.Created)
                .Skip(offset)
                .Take(pagesize);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }

            if (!string.IsNullOrEmpty(openID))
            {
                query.Where(x => x.OpenID == openID);
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
            query.Where(x => x.UserId == 12);
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
        public async Task<IList<AdminOrderViewEntity>> GetOrdersAsync(string cinemaCode,string keyword, int? thirdUserId, OrderStatusEnum? orderStatus,DateTime? startDate, DateTime? endDate)
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

            if (thirdUserId.HasValue)
            {
                query.Where(x => x.UserId == thirdUserId.Value);
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
