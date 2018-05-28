using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using WeiXinTicketSystem.Util;
using WeiXinTicketSystem.NetSale;
//using WeiXinClient.NetSale.Models;
using NetSaleSvc.Api.Models;
using System.Configuration;

namespace WeiXinTicketSystem.Service
{
    public class ScreenInfoService
    {
        #region ctor
        private readonly IRepository<ScreenInfoEntity> _screenInfoRepository;
        private readonly IRepository<AdminScreenViewEntity> _adminScreenRepository;
        private NetSaleSvcApi _netSaleSvcApi;

        public ScreenInfoService()
        {
            //TODO: 移除内部依赖
            _screenInfoRepository = new Repository<ScreenInfoEntity>();
            _adminScreenRepository = new Repository<AdminScreenViewEntity>();
            _netSaleSvcApi = new NetSaleSvcApi();
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
            return _screenInfoRepository.Query.Where(x => x.CinemaCode == CinemaCode
                && x.ScreenCode == ScreenCode).SingleOrDefault();
        }
        /// <summary>
        /// 获取影院影厅列表
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<ScreenInfoEntity> GetScreenListByCinemaCode(string CinemaCode)
        {
            return _screenInfoRepository.Query.Where(x => x.CinemaCode == CinemaCode).ToList();
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

        public async Task<IPageList<AdminScreenViewEntity>> GetScreenInfoPagedAsync(string CinemaCode, string keyword, int offset, int perPage)
        {
            var query = _adminScreenRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(CinemaCode))
            {
                query.Where(x => x.CinemaCode == CinemaCode);
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.ScreenCode.Contains(keyword) || x.ScreenName.Contains(keyword)
                    || x.Type.Contains(keyword));
            }
            return await query.ToPageListAsync();
        }

        public ReturnData QueryCinema(string CinemaCode)
        {
            ReturnData returnData = new ReturnData();
            QueryCinemaReply queryCinemaReply = _netSaleSvcApi.QueryCinema(CinemaCode);
            if (queryCinemaReply != null)
            {
                if (queryCinemaReply.Status == "Success")
                {
                    //更新影厅信息
                    var oldScreens = GetScreenListByCinemaCode(CinemaCode);

                    var newScreens = queryCinemaReply.Cinema.Screen.Select(
                        x => x.MapToEntity(
                            oldScreens.Where(y => y.ScreenCode == x.Code).SingleOrDefault()
                                ?? new ScreenInfoEntity { CinemaCode = CinemaCode })).ToList();

                    //插入或更新最新影厅信息
                    BulkMerge(newScreens, oldScreens);

                    returnData.Status = true;
                    returnData.Info = "获取影厅信息成功";
                }
                else
                {
                    returnData.Status = false;
                    returnData.Info = "获取影厅信息失败！";
                }
            }
            return returnData;
        }

        /// <summary>
        /// 批量合并
        /// </summary>
        /// <param name="entities"></param>
        public void BulkMerge(IEnumerable<ScreenInfoEntity> NewEntities
            , IEnumerable<ScreenInfoEntity> OldEntities)
        {
            _screenInfoRepository.BulkMerge(NewEntities, x => x.Id,
                OldEntities, x => x.Id);
        }
    }
}
