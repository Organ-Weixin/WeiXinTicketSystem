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
    public class StampService
    {
        #region ctor
        private readonly IRepository<StampEntity> _stampRepository;

        public StampService()
        {
            //TODO: 移除内部依赖
            _stampRepository = new Repository<StampEntity>();
        }
        #endregion

        /// <summary>
        /// 根据印章编码获取印章信息
        /// </summary>
        /// <param name="StampCode"></param>
        /// <returns></returns>
        public StampEntity GetStampByStampCode(string StampCode)
        {
            return _stampRepository.Query.Where(x => x.StampCode == StampCode).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有印章列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<StampEntity>> GetAllStampAsync()
        {
            return await _stampRepository.Query.Where(x => !x.Deleted).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取印章信息
        /// </summary>
        /// <param name="StampCode"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<StampEntity>> GetStampPagedAsync(string StampCode, string keyword, int offset, int perPage)
        {
            var query = _stampRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //印章编码
            if (!string.IsNullOrEmpty(StampCode))
            {
                query.Where(x => x.StampCode == StampCode);
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Title.Contains(keyword));
            }
            query.Where(x => !x.Deleted);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 根据印章编码获取印章信息(异步)
        /// </summary>
        /// <param name="StampCode"></param>
        /// <returns></returns>
        public async Task<StampEntity> GetStampByStampCodeAsync(string StampCode)
        {
            return await _stampRepository.Query.Where(x => x.StampCode == StampCode).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 根据印章ID获取印章信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<StampEntity> GetStampByIdAsync(int Id)
        {
            return await _stampRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(StampEntity entity)
        {
            _stampRepository.Update(entity);
        }

        /// <summary>
        /// 更新印章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(StampEntity entity)
        {
            await _stampRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增印章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(StampEntity entity)
        {
            await _stampRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 新增印章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(StampEntity entity)
        {
            _stampRepository.Insert(entity);
        }
    }
}
