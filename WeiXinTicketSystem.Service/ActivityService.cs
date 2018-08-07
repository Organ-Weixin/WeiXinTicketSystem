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
    public class ActivityService
    {
        #region ctor
        private readonly IRepository<ActivityEntity> _activityRepository;
        private readonly IRepository<AdminActivityViewEntity> _adminActivityViewRepository;

        public ActivityService()
        {
            //TODO: 移除内部依赖
            _activityRepository = new Repository<ActivityEntity>();
            _adminActivityViewRepository = new Repository<AdminActivityViewEntity>();
        }
        #endregion


        /// <summary>
        /// 根据影院编码获取活动表信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public ActivityEntity GetActivityByCinemaCode(string CinemaCode)
        {
            return _activityRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有活动表列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ActivityEntity>> GetAllActivityAsync()
        {
            return await _activityRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取活动表信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<AdminActivityViewEntity>> GetActivityPagedAsync(string cinemaCode, string keyword, int offset, int perPage)
        {
            var query = _adminActivityViewRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Title.Contains(keyword) || x.ActivityContent.Contains(keyword) );
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 查询活动
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public async Task<IPageList<ActivityEntity>> QueryActivitysPagedAsync(string cinemaCode, int currentpage, int pagesize)
        {
            int offset = (currentpage - 1) * pagesize;
            var query = _activityRepository.Query
                .OrderByDescending(x => x.Id)
                .Skip(offset)
                .Take(pagesize);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 根据推荐等级查询活动
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="GradeCode"></param>
        /// <returns></returns>
        public async Task<IList<ActivityEntity>> QueryActivitysByGradeCode(string cinemaCode, string GradeCode)
        {
            try
            {
                var query = _activityRepository.Query.OrderBy(x => x.ActivitySequence);

                if (!string.IsNullOrEmpty(cinemaCode))
                {
                    query.Where(x => x.CinemaCode == cinemaCode);
                }
                if (!string.IsNullOrEmpty(GradeCode))
                {
                    query.Where(x => x.GradeCode == GradeCode);
                }
                query.Where(x => !x.IsDel);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据推荐等级和次序查询活动
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="GradeCode"></param>
        /// <param name="ActivitySequence"></param>
        /// <returns></returns>
        public async Task<ActivityEntity> QueryActivitysByGradeCodeAndSequence(string cinemaCode, string GradeCode,int ActivitySequence)
        {
            var query = _activityRepository.Query.OrderByDescending(x => x.Id);

            if (!string.IsNullOrEmpty(cinemaCode) && !string.IsNullOrEmpty(GradeCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode && x.GradeCode == GradeCode);
            }
            if (ActivitySequence != null)
            {
                query.Where(x => x.ActivitySequence == ActivitySequence);
            }

            query.Where(x => !x.IsDel);
            return await query.SingleOrDefaultAsync();
        }


        /// <summary>
        /// 获取ActivityEntity实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<ActivityEntity> GetActivityByCinemaCodeAsync(string CinemaCode)
        {
            return await _activityRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefaultAsync();
        }



        /// <summary>
        /// 根据活动表ID获取活动表信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ActivityEntity> GetActivityByIdAsync(int Id)
        {
            return await _activityRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(ActivityEntity entity)
        {
            _activityRepository.Update(entity);
        }

        /// <summary>
        /// 更新活动表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ActivityEntity entity)
        {
            await _activityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增活动表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(ActivityEntity entity)
        {
            await _activityRepository.InsertAsync(entity);
        }
    }
}
