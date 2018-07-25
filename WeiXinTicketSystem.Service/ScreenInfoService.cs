using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System.Collections.Generic;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Models.PageList;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Service
{
    public class ScreenInfoService
    {
        #region ctor
        private readonly IRepository<ScreenInfoEntity> _screenInfoRepository;
        private readonly IRepository<AdminScreenViewEntity> _adminScreenRepository;
        public ScreenInfoService()
        {
            //TODO: 移除内部依赖
            _screenInfoRepository = new Repository<ScreenInfoEntity>();
            _adminScreenRepository = new Repository<AdminScreenViewEntity>();
        }
        #endregion

        /// <summary>
        /// 获取影厅信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="ScreenCode"></param>
        /// <returns></returns>
        public ScreenInfoEntity GetScreenInfo(string CinemaCode, string ScreenCode)
        {
            return _screenInfoRepository.Query.Where(x => x.CCode == CinemaCode
                && x.SCode == ScreenCode).SingleOrDefault();
        }

        /// <summary>
        /// 获取影院影厅列表
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<ScreenInfoEntity> GetScreenListByCinemaCode(string CinemaCode)
        {
            return _screenInfoRepository.Query.Where(x => x.CCode == CinemaCode).ToList();
        }

        public async Task<IPageList<AdminScreenViewEntity>> GetScreenInfoPagedAsync(string CinemaCode, string keyword, int offset, int perPage)
        {
            var query = _adminScreenRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(CinemaCode))
            {
                query.Where(x => x.CCode == CinemaCode);
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.SCode.Contains(keyword) || x.SName.Contains(keyword)
                    || x.Type.Contains(keyword));
            }
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 根据Id获取影厅信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ScreenInfoEntity> GetScreenInfoByIdAsync(int Id)
        {
            return await _screenInfoRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 新增影厅信息
        /// </summary>
        /// <param name="entity"></param>
        public void InsertScreenInfo(ScreenInfoEntity entity)
        {
            _screenInfoRepository.Insert(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateScreenInfo(ScreenInfoEntity entity)
        {
            _screenInfoRepository.Update(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteScreenInfo(ScreenInfoEntity entity)
        {
            _screenInfoRepository.Delete(entity);
        }

        /// <summary>
        /// 批量合并
        /// </summary>
        /// <param name="entities"></param>
        public void BulkMerge(IEnumerable<ScreenInfoEntity> NewEntities
            , IEnumerable<ScreenInfoEntity> OldEntities)
        {
            _screenInfoRepository.BulkMerge(NewEntities, x=>x.Id,
                OldEntities, x=>x.Id);
        }
    }
}
