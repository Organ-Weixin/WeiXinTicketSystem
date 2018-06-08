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

        public ActivityService()
        {
            //TODO: 移除内部依赖
            _activityRepository = new Repository<ActivityEntity>();
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
        public async Task<IPageList<ActivityEntity>> GetActivityPagedAsync(string cinemaCode, string keyword, int offset, int perPage)
        {
            var query = _activityRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
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
