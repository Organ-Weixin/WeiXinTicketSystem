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
    public class CinemaService
    {
        #region ctor
        private readonly IRepository<CinemaViewEntity> _cinemaViewRepository;
        private readonly IRepository<CinemaEntity> _cinemaRepository;
        private readonly IRepository<CinemaMiniProgramAccountEntity> _cinemaMiniProgramAccountRepository;

        public CinemaService()
        {
            //TODO: 移除内部依赖
            _cinemaViewRepository = new Repository<CinemaViewEntity>();
            _cinemaRepository = new Repository<CinemaEntity>();
            _cinemaMiniProgramAccountRepository = new Repository<CinemaMiniProgramAccountEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影院编码获取影院信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public CinemaViewEntity GetCinemaViewByCinemaCode(string CinemaCode)
        {
            return _cinemaViewRepository.Query.Where(x => x.Code == CinemaCode && !x.IsDel).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有影院列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CinemaEntity>> GetAllCinemasAsync()
        {
            return await _cinemaRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }
        /// <summary>
        /// 后台分页读取影院信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<CinemaViewEntity>> GetCinemasPagedAsync(string cinemaCode, string CinemaName,CinemaOpenEnum? IsOpen, string keyword, int offset, int perPage)
        {
            var query = _cinemaViewRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.Code == cinemaCode);
            }
            //影院名称
            if (!string.IsNullOrEmpty(CinemaName))
            {
                query.Where(x => x.Name.Contains(CinemaName));
            }
            //是否开通接口
            if (IsOpen.HasValue)
            {
                query.Where(x => x.IsOpen == IsOpen.Value);
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Address.Contains(keyword) || x.DingXinId.ToString().Contains(keyword)
                    || x.ScreenCount.ToString().Contains(keyword) || x.MId.ToString().Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        public async Task<IPageList<CinemaViewEntity>> QueryCinemasByAppIdPagedAsync(string AppId, int currentpage, int pagesize)
        {
            //先读出影院编码列表
            IList<CinemaMiniProgramAccountEntity> accounts = _cinemaMiniProgramAccountRepository.Query.Where(x => x.AppId == AppId).ToList();
            string strCinemaCodes = string.Join(",", accounts.Select(x => x.CinemaCode));//这里一定要分开写，不能连写，否则获取不到单独的CinemaCode列
            IList<string> CinemaCodes = strCinemaCodes.Split(',').ToList();
            int offset = (currentpage - 1) * pagesize;
            var query = _cinemaViewRepository.Query
                .OrderByDescending(x => x.Id)
                .Skip(offset)
                .Take(pagesize);
            if(CinemaCodes.Count>0)
            {
                query.WhereIsIn(x=>x.Code,CinemaCodes);
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取Cinema实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public CinemaEntity GetCinemaByCinemaCode(string CinemaCode)
        {
            return _cinemaRepository.Query.Where(x => x.Code == CinemaCode && !x.IsDel).SingleOrDefault();
        }

        /// <summary>
        /// 获取Cinema实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<CinemaEntity> GetCinemaByCinemaCodeAsync(string CinemaCode)
        {
            return await _cinemaRepository.Query.Where(x => x.Code == CinemaCode && !x.IsDel).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 根据影院ID获取影院
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CinemaEntity> GetCinemaByIdAsync(int Id)
        {
            return await _cinemaRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(CinemaEntity entity)
        {
            _cinemaRepository.Update(entity);
        }

        /// <summary>
        /// 更新影院
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CinemaEntity entity)
        {
            await _cinemaRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增影院
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(CinemaEntity entity)
        {
            await _cinemaRepository.InsertAsync(entity);
        }
    }
}
