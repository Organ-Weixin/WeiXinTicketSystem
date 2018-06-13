using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Enum;

namespace WeiXinTicketSystem.Service
{
    public class GiftService
    {
        #region ctor
        private readonly IRepository<GiftEntity> _giftRepository;

        public GiftService()
        {
            //TODO: 移除内部依赖
            _giftRepository = new Repository<GiftEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影院编码获取赠品信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<GiftEntity> GetGiftByCinemaCode(string CinemaCode)
        {
            return _giftRepository.Query.Where(x => x.CinemaCode == CinemaCode).ToList();
        }

        /// <summary>
        /// 获取所有赠品列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<GiftEntity>> GetAllGiftAsync()
        {
            return await _giftRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取赠品信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="title"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<GiftEntity>> GetGiftPagedAsync(string cinemaCode, string title, string keyword, int offset, int perPage)
        {
            var query = _giftRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            //标题
            if (!string.IsNullOrEmpty(title))
            {
                query.Where(x => x.Title.Contains(title));
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Details.Contains(keyword) );
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取GiftEntity实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<IList<GiftEntity>> GetGiftByCinemaCodeAsync(string CinemaCode)
        {
            return await _giftRepository.Query.Where(x => x.CinemaCode == CinemaCode).ToListAsync();
        }

        /// <summary>
        /// 根据赠品ID获取赠品信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<GiftEntity> GetGiftByIdAsync(int Id)
        {
            return await _giftRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(GiftEntity entity)
        {
            _giftRepository.Update(entity);
        }


        /// <summary>
        /// 更新赠品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(GiftEntity entity)
        {
            await _giftRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增赠品
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(GiftEntity entity)
        {
            await _giftRepository.InsertAsync(entity);
        }

    }
}
