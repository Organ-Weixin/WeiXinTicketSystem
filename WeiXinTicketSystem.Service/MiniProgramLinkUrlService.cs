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
    public class MiniProgramLinkUrlService
    {
        #region ctor
        private readonly IRepository<MiniProgramLinkUrlEntity> _miniProgramLinkUrlRepository;

        public MiniProgramLinkUrlService()
        {
            //TODO: 移除内部依赖
            _miniProgramLinkUrlRepository = new Repository<MiniProgramLinkUrlEntity>();
        }
        #endregion

        /// <summary>
        /// 获取链接地址列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<MiniProgramLinkUrlEntity>> GetAllMiniProgramLinkUrlAsync()
        {
            return await _miniProgramLinkUrlRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取链接地址信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<MiniProgramLinkUrlEntity>> GetMiniProgramLinkUrlPagedAsync(string LinkName, string keyword, int offset, int perPage)
        {
            var query = _miniProgramLinkUrlRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //链接名称
            if (!string.IsNullOrEmpty(LinkName))
            {
                query.Where(x => x.LinkName.Contains(LinkName));
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.LinkName.Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }


        /// <summary>
        /// 根据链接地址ID获取链接地址信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<MiniProgramLinkUrlEntity> GetMiniProgramLinkUrlByIdAsync(int Id)
        {
            return await _miniProgramLinkUrlRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(MiniProgramLinkUrlEntity entity)
        {
            _miniProgramLinkUrlRepository.Update(entity);
        }

        /// <summary>
        /// 更新链接地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(MiniProgramLinkUrlEntity entity)
        {
            await _miniProgramLinkUrlRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增链接地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(MiniProgramLinkUrlEntity entity)
        {
            await _miniProgramLinkUrlRepository.InsertAsync(entity);
        }

    }
}
