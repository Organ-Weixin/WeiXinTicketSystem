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
    public class RecommendGradeService
    {
        #region ctor
        private readonly IRepository<RecommendGradeEntity> _recommendGradeRepository;

        public RecommendGradeService()
        {
            //TODO: 移除内部依赖
            _recommendGradeRepository = new Repository<RecommendGradeEntity>();
        }
        #endregion


        /// <summary>
        /// 获取所有推荐等级列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<RecommendGradeEntity>> GetAllRecommendGradeAsync()
        {
            return await _recommendGradeRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取推荐等级信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<RecommendGradeEntity>> GetRecommendGradePagedAsync(string keyword, int offset, int perPage)
        {
            try
            {
                var query = _recommendGradeRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
                
                ////等级编号
                //if (!string.IsNullOrEmpty(gradeCode))
                //{
                //    query.Where(x => x.GradeCode == gradeCode);
                //}
                ////等级名称
                //if (!string.IsNullOrEmpty(gradeName))
                //{
                //    query.Where(x => x.GradeName.Contains(gradeName));
                //}
                //其他数据
                if (!string.IsNullOrEmpty(keyword))
                {
                    query.Where(x => x.GradeName.Contains(keyword) || x.Remark.Contains(keyword) || x.GradeCode.Contains(keyword) );
                }
                query.Where(x => !x.IsDel);
                return await query.ToPageListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 根据推荐等级ID获取推荐等级信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<RecommendGradeEntity> GetRecommendGradeByIdAsync(int Id)
        {
            return await _recommendGradeRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(RecommendGradeEntity entity)
        {
            _recommendGradeRepository.Update(entity);
        }


        /// <summary>
        /// 更新推荐等级
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(RecommendGradeEntity entity)
        {
            await _recommendGradeRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增推荐等级
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(RecommendGradeEntity entity)
        {
            await _recommendGradeRepository.InsertAsync(entity);
        }
    }
}
