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
        private readonly IRepository<SnackOrderDetailsViewEntity> _snackOrderDetailsViewRepository;
        private readonly IRepository<ConponEntity> _conponRepository;
        public SnackOrderService()
        {
            _snackOrderRepository = new Repository<SnackOrderEntity>();
            _snackOrderDetailRepository = new Repository<SnackOrderDetailEntity>();
            _snackRepository = new Repository<SnackEntity>();
            _adminSnackOrderViewRepository = new Repository<AdminSnackOrdersViewEntity>();
            _snackOrderDetailsViewRepository = new Repository<SnackOrderDetailsViewEntity>();
            _conponRepository = new Repository<ConponEntity>();
        }
        #endregion
        /// <summary>
        /// 查询待支付订单是否存在
        /// </summary>
        /// <param name="OrderCode"></param>
        /// <returns></returns>
        public SnackOrderEntity GetSnackOrderByOrderCode(string OrderCode)
        {
            DateTime nowdata = DateTime.Now;
            return _snackOrderRepository.Query.Where(x => x.OrderCode == OrderCode && x.OrderStatus == SnackOrderStatusEnum.Booked && x.AutoUnLockDateTime > nowdata).SingleOrDefault();
        }
        /// <summary>
        /// 查询待取货订单
        /// </summary>
        /// <param name="OrderCode"></param>
        /// <param name="VoucherCode"></param>
        /// <returns></returns>
        public SnackOrderEntity GetSnackOrderbyVoucherCode(string OrderCode,string VoucherCode)
        {
            return _snackOrderRepository.Query.Where(x => x.OrderCode == OrderCode && x.VoucherCode == VoucherCode && x.OrderStatus == SnackOrderStatusEnum.Complete).SingleOrDefault();
        }

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
        public void Update(SnackOrderEntity snackorder)
        {
            _snackOrderRepository.Update(snackorder);
        }

        public void Update(SnackOrderEntity snackorder,ConponEntity conpon)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _snackOrderRepository.UpdateWithTransaction(snackorder,connection,transaction);
                        if(conpon.ConponCode!=null)
                        {
                            _conponRepository.UpdateWithTransaction(conpon, connection, transaction);
                        }
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
        public async Task<IPageList<SnackOrderDetailsViewEntity>> GetOrdersPagedAsync(string cinemaCode,
            int offset, int perPage, string keyword, SnackOrderStatusEnum? orderStatus,
            DateTime? startDate, DateTime? endDate)
        {
            var query = _snackOrderDetailsViewRepository.Query
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
        public async Task<IPageList<SnackOrderEntity>> GetUserOrdersPagedAsync(string cinemaCode,string OpenID, int currentpage, int pagesize)//DateTime? startDate, DateTime? endDate
        {
            int offset = (currentpage - 1) * pagesize;
            var query = _snackOrderRepository.Query
                .OrderByDescending(x => x.Id)
                .Skip(offset)
                .Take(pagesize);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }

            if (!string.IsNullOrEmpty(OpenID))
            {
                query.Where(x => x.OpenID==OpenID);
            }
            //if (startDate.HasValue)
            //{
            //    query.Where(x => x.Created > startDate.Value);
            //}

            //if (endDate.HasValue)
            //{
            //    DateTime deadline = endDate.Value.AddDays(1);
            //    query.Where(x => x.Created < deadline);
            //}
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
        public async Task<IList<SnackOrderDetailsViewEntity>> GetOrdersAsync(string cinemaCode, string keyword, SnackOrderStatusEnum? orderStatus, DateTime? startDate, DateTime? endDate)
        {
            var query = _snackOrderDetailsViewRepository.Query.OrderByDescending(x => x.Id);

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
