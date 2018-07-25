using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Models;
using System.Threading.Tasks;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System.Collections.Generic;
using WeiXinTicketSystem.Entity.Models.PageList;

namespace WeiXinTicketSystem.Service
{
    public class MiddlewareService
    {
        #region ctor
        private readonly IRepository<MiddlewareEntity> _middlewareRepository;

        public MiddlewareService()
        {
            //TODO: 移除内部依赖
            _middlewareRepository = new Repository<MiddlewareEntity>();
        }
        #endregion

        public async Task<IList<MiddlewareEntity>> GetAllMiddlewaresAsync()
        {
            return await _middlewareRepository.Query.Where(x => x.IsDel==0).ToListAsync();
        }

        public async Task<IPageList<MiddlewareEntity>> GetMiddlewaresPagedAsync(string Title, string keyword, int offset, int perPage)
        {
            var query = _middlewareRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //名称
            if (!string.IsNullOrEmpty(Title))
            {
                query.Where(x => x.Title.Contains(Title));
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Url.Contains(keyword) || x.UserName.Contains(keyword)
                    || x.CinemaCode.Contains(keyword) || x.Password.Contains(keyword));
            }
            query.Where(x => x.IsDel == 0);
            return await query.ToPageListAsync();
        }
        /// <summary>
        /// 根据中件间ID获取中间件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<MiddlewareEntity> GetMiddlewareByIdAsync(int Id)
        {
            return await _middlewareRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 根据url获取中间件
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public async Task<MiddlewareEntity> GettMiddlewareByUrlAsync(string Url)
        {
            return await _middlewareRepository.Query.Where(x => x.Url == Url).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新中间件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(MiddlewareEntity entity)
        {
            await _middlewareRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增中间件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(MiddlewareEntity entity)
        {
            await _middlewareRepository.InsertAsync(entity);
        }
    }
}
