using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Models;
using System.Threading.Tasks;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System.Collections.Generic;
using WeiXinTicketSystem.Entity.Models.PageList;
using System;
using System.Data;
using System.Linq;

namespace WeiXinTicketSystem.Service
{
    public class HomeService
    {
        #region ctor
        private readonly IRepository<OrderEntity> _orderRepository;
        public HomeService()
        {
            //TODO: 移除内部依赖
            _orderRepository = new Repository<OrderEntity>();
        }
        #endregion

        
    }
}
