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
    public class SnackOrderService
    {
        #region ctor
        private readonly IRepository<SnackOrderEntity> _snackOrderRepository;
        private readonly IRepository<SnackOrderDetailEntity> _snackOrderDetailRepository;
        private readonly IRepository<SnackEntity> _snackRepository;
        private readonly IRepository<AdminSnackOrdersViewEntity> _adminSnackOrderViewRepository;
        public SnackOrderService()
        {
            _snackOrderRepository = new Repository<SnackOrderEntity>();
            _snackOrderDetailRepository = new Repository<SnackOrderDetailEntity>();
            _snackRepository = new Repository<SnackEntity>();
            _adminSnackOrderViewRepository = new Repository<AdminSnackOrdersViewEntity>();
        }
        #endregion

        public SnackOrderViewEntity GetSnacksOrderWithOrderCode(string CinemaCode, string OrderCode)
        {
            var order = _snackOrderRepository.Query.Where(x => x.CinemaCode == CinemaCode
                && (x.OrderCode == OrderCode)).SingleOrDefault();
            if (order == null)
            {
                return null;
            }
            var orderdetails = _snackOrderDetailRepository.Query.Where(x => x.OrderId == order.Id).ToList();
            var snacks = _snackRepository.Query.Where(x => x.CinemaCode == CinemaCode).WhereIsIn(x => x.SnackCode, orderdetails.Select(x => x.SnackCode)).ToList();
            return new SnackOrderViewEntity
            {
                OrderBaseInfo = order,
                SnackOrderDetails = orderdetails.ToList(),
                Snacks = snacks.ToList()
            };
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="orderView"></param>
        /// <returns></returns>
        public void Insert(SnackOrderViewEntity orderView)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        orderView.Snacks.ForEach(x =>
                        {
                            _snackRepository.UpdateWithTransaction(x, connection, transaction);
                        });

                        int orderId = _snackOrderRepository.InsertWithTransaction(orderView.OrderBaseInfo,
                            connection, transaction);

                        orderView.SnackOrderDetails.ForEach(x =>
                        {
                            x.OrderId = orderId;
                            _snackOrderDetailRepository.InsertWithTransaction(x, connection, transaction);
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
        /// 更新订单
        /// </summary>
        /// <param name="orderView"></param>
        public void Update(SnackOrderViewEntity orderView)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        orderView.Snacks.ForEach(x =>
                        {
                            _snackRepository.UpdateWithTransaction(x, connection, transaction);
                        });


                        orderView.OrderBaseInfo.Updated = DateTime.Now;
                        _snackOrderRepository.UpdateWithTransaction(orderView.OrderBaseInfo,
                            connection, transaction);

                        orderView.SnackOrderDetails.ForEach(x =>
                        {
                            x.Updated = DateTime.Now;
                            _snackOrderDetailRepository.UpdateWithTransaction(x, connection, transaction);
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
        /// 后台分页获取订单信息
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
        public async Task<IPageList<AdminSnackOrdersViewEntity>> GetOrdersPagedAsync(string cinemaCode,
            int offset, int perPage, string keyword, SnackOrderStatusEnum? orderStatus,
            DateTime? startDate, DateTime? endDate)
        {
            var query = _adminSnackOrderViewRepository.Query
                .OrderByDescending(x => x.Id)
                .Skip(offset)
                .Take(perPage);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaName.Contains(keyword) || x.OrderCode.Contains(keyword) || x.VoucherCode.Contains(keyword)
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
        public async Task<IList<AdminSnackOrdersViewEntity>> GetOrdersAsync(string cinemaCode, string keyword, SnackOrderStatusEnum? orderStatus, DateTime? startDate, DateTime? endDate)
        {
            var query = _adminSnackOrderViewRepository.Query.OrderByDescending(x => x.Id);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaName.Contains(keyword) || x.OrderCode.Contains(keyword) || x.VoucherCode.Contains(keyword)
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
    }
}
