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
    public class FilmCommentService
    {
        #region ctor
        private readonly IRepository<FilmCommentEntity> _filmCommentRepository;

        public FilmCommentService()
        {
            //TODO: 移除内部依赖
            _filmCommentRepository = new Repository<FilmCommentEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影片编码获取影片评论信息
        /// </summary>
        /// <param name="FilmCode"></param>
        /// <returns></returns>
        public IList<FilmCommentEntity> GetFilmCommentByFilmCode(string FilmCode)
        {
            return _filmCommentRepository.Query.Where(x => x.FilmCode == FilmCode).ToList();
        }

        /// <summary>
        /// 获取所有影片评论列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<FilmCommentEntity>> GetAllFilmCommentAsync()
        {
            return await _filmCommentRepository.Query.Where(x => !x.Deleted).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取影片评论信息
        /// </summary>
        /// <param name="filmCode"></param>
        /// <param name="filmName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<FilmCommentEntity>> GetFilmCommentPagedAsync(string filmCode, string filmName, string keyword, int offset, int perPage)
        {
            var query = _filmCommentRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影片编码
            if (!string.IsNullOrEmpty(filmCode))
            {
                query.Where(x => x.FilmCode == filmCode);
            }
            //影片名称
            if (!string.IsNullOrEmpty(filmName))
            {
                query.Where(x => x.FilmName.Contains(filmName));
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CommentContent.Contains(keyword) );
            }
            query.Where(x => !x.Deleted);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取FilmCommentEntity实体
        /// </summary>
        /// <param name="FilmCode"></param>
        /// <returns></returns>
        public async Task<IList<FilmCommentEntity>> GetFilmCommentByFilmCodeAsync(string FilmCode)
        {
            return await _filmCommentRepository.Query.Where(x => x.FilmCode == FilmCode).ToListAsync();
        }


        /// <summary>
        /// 根据影片评论ID获取影片评论信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<FilmCommentEntity> GetFilmCommentByIdAsync(int Id)
        {
            return await _filmCommentRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(FilmCommentEntity entity)
        {
            _filmCommentRepository.Update(entity);
        }

        /// <summary>
        /// 更新影片评论
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(FilmCommentEntity entity)
        {
            await _filmCommentRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增影片评论
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(FilmCommentEntity entity)
        {
            await _filmCommentRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 新增影片评论
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(FilmCommentEntity entity)
        {
            _filmCommentRepository.Insert(entity);
        }
    }
}
