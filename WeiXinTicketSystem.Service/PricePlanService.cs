using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Service
{
    public class PricePlanService
    {
        #region ctor
        private readonly IRepository<PricePlanEntity> _pricePlanRepository;
        public PricePlanService()
        {
            _pricePlanRepository = new Repository<PricePlanEntity>();
        }
        #endregion

        /// <summary>
        /// 根据Id获取价格设置
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<PricePlanEntity> GetAsync(int Id)
        {
            return await _pricePlanRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 读出价格设置列表
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IPageList<PricePlanEntity>> GetPricePlanPagedAsync(int offset, int perPage, string keyword)
        {
            var query = _pricePlanRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaCode.Contains(keyword) || x.Code.Contains(keyword));
            }
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 根据条件获取价格设置
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="UserID"></param>
        /// <param name="Type"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public async Task<PricePlanEntity> GetAsync(string CinemaCode,int UserID,PricePlanTypeEnum Type,string Code)
        {
            return await _pricePlanRepository.Query.Where(x => x.CinemaCode == CinemaCode&&x.UserID==UserID&&x.Type==Type&&x.Code==Code).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新价格设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(PricePlanEntity entity)
        {
            await _pricePlanRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增价格设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(PricePlanEntity entity)
        {
            await _pricePlanRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 删除价格设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(PricePlanEntity entity)
        {
            await _pricePlanRepository.DeleteAsync(entity);
        }
    }
}
