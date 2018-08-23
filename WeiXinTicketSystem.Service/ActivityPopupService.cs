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
    public class ActivityPopupService
    {
        #region ctor
        private readonly IRepository<ActivityPopupEntity> _activityPopupRepository;
        private readonly IRepository<AdminActivityPopupViewEntity> _adminActivityPopupViewRepository;

        public ActivityPopupService()
        {
            //TODO: 移除内部依赖
            _activityPopupRepository = new Repository<ActivityPopupEntity>();
            _adminActivityPopupViewRepository = new Repository<AdminActivityPopupViewEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影院编码获取活动弹窗信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<ActivityPopupEntity> GetActivityPopupByCinemaCode(string CinemaCode)
        {
            return _activityPopupRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.IsDel).ToList();
        }

        /// <summary>
        /// 获取所有活动弹窗列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ActivityPopupEntity>> GetAllActivityPopupAsync()
        {
            return await _activityPopupRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }


        /// <summary>
        /// 后台分页读取活动弹窗信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<AdminActivityPopupViewEntity>> GetActivityPopupPagedAsync(string cinemaCode, string keyword, int offset, int perPage)
        {
            var query = _adminActivityPopupViewRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaName.Contains(keyword) || x.GradeName.Contains(keyword) );
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 根据影院编码获取活动弹窗信息(异步)
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<IList<ActivityPopupEntity>> GetActivityPopupByCinemaCodeAsync(string CinemaCode)
        {
            return await _activityPopupRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 根据活动弹窗ID获取活动弹窗信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ActivityPopupEntity> GetActivityPopupByIdAsync(int Id)
        {
            return await _activityPopupRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(ActivityPopupEntity entity)
        {
            _activityPopupRepository.Update(entity);
        }

        /// <summary>
        /// 更新活动弹窗
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ActivityPopupEntity entity)
        {
            await _activityPopupRepository.UpdateAsync(entity);
        }


        /// <summary>
        /// 新增活动弹窗
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(ActivityPopupEntity entity)
        {
            await _activityPopupRepository.InsertAsync(entity);
        }
    }
}
