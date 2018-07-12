using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Util;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using WeiXinTicketSystem.NetSale;
//using WeiXinClient.NetSale.Models;
using NetSaleSvc.Api.Models;

namespace WeiXinTicketSystem.Service
{
    public class SeatInfoService
    {
        #region ctor
        private readonly IRepository<ScreenSeatInfoEntity> _screenSeatInfoRepository;
        private NetSaleSvcApi _netSaleSvcApi;

        public SeatInfoService()
        {
            //TODO: 移除内部依赖
            _screenSeatInfoRepository = new Repository<ScreenSeatInfoEntity>();
            _netSaleSvcApi = new NetSaleSvcApi();
        }
        #endregion

        /// <summary>
        /// 获取影厅座位信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="ScreenCode"></param>
        /// <returns></returns>
        public IList<ScreenSeatInfoEntity> GetScreenSeats(string CinemaCode, string ScreenCode)
        {
            return _screenSeatInfoRepository.Query.Where(x => x.CinemaCode == CinemaCode
            && x.ScreenCode == ScreenCode).OrderBy(x => x.SeatCode).ToList();
        }

        /// <summary>
        /// 根据座位编码列表获取座位信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="SeatCodes"></param>
        /// <returns></returns>
        public IList<ScreenSeatInfoEntity> GetSeats(string CinemaCode, IEnumerable<string> SeatCodes, string ScreenCode = null)
        {
            if (!string.IsNullOrEmpty(ScreenCode))
            {
                return _screenSeatInfoRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.ScreenCode == ScreenCode)
                    .WhereIsIn(x => x.SeatCode, SeatCodes).ToList();
            }
            else
            {
                return _screenSeatInfoRepository.Query.Where(x => x.CinemaCode == CinemaCode)
                    .WhereIsIn(x => x.SeatCode, SeatCodes).ToList();
            }
        }

        public IList<ScreenSeatInfoEntity> GetUnavalibleSeats(string CinemaCode,string SessionCode)
        {
            QuerySessionSeatReply querySessionSeat = _netSaleSvcApi.QuerySessionSeat(CinemaCode, SessionCode, "All");
            if(querySessionSeat != null)
            {
                if(querySessionSeat.Status== "Failure")
                {
                    return new List<ScreenSeatInfoEntity>();
                }
                else
                {
                    return querySessionSeat.SessionSeat.Seat.Where(s => s.Status != "Available").Select(
                            s => new ScreenSeatInfoEntity
                            {
                                CinemaCode = CinemaCode,
                                SeatCode = s.Code,
                                RowNum = s.RowNum,
                                ColumnNum = s.ColumnNum,
                                Status = s.Status
                            }).ToList();
                }
            }
            else
            {
                return new List<ScreenSeatInfoEntity>();
            }
        }

        public QuerySessionSeatReply QuerySessionSeat(string CinemaCode, string SessionCode, string Status)
        {
            return _netSaleSvcApi.QuerySessionSeat(CinemaCode, SessionCode, Status);
        }

        public ReturnData QuerySeat(string CinemaCode,string ScreenCode)
        {
            ReturnData returnData = new ReturnData();
            QuerySeatReply querySeatReply = _netSaleSvcApi.QuerySeat(CinemaCode,ScreenCode);
            if (querySeatReply != null)
            {
                if (querySeatReply.Status == "Success")
                {
                    //更新影厅座位
                    var oldSeats = GetScreenSeats(CinemaCode, ScreenCode).NotNull();

                    var newSeats = querySeatReply.Cinema.Screen.Seat.Select(
                        x => x.MapToEntity(
                            oldSeats.Where(y => y.SeatCode == x.Code).SingleOrDefault()
                                ?? new ScreenSeatInfoEntity
                                {
                                    CinemaCode = CinemaCode,
                                    ScreenCode = ScreenCode,
                                    LoveFlag = LoveFlagEnum.Normal.GetDescription()
                                })).ToList();
                    
                    //插入或更新最新座位
                    BulkMerge(newSeats,CinemaCode,ScreenCode);

                    returnData.Status = true;
                    returnData.Info = "获取影厅座位信息成功";
                }
                else
                {
                    returnData.Status = false;
                    returnData.Info = "获取影厅座位信息失败！";
                }
            }
            return returnData;
        }

        /// <summary>
        /// 批量合并
        /// </summary>
        /// <param name="entities"></param>
        public void BulkMerge(IEnumerable<ScreenSeatInfoEntity> Entities, string CinemaCode, string ScreenCode)
        {
            using (var connection = DbConnectionFactory.OpenSqlConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.MergeScreenSeat";
                cmd.Parameters.AddWithValue("@seats", Entities.ToList().ToDataTable());
                cmd.Parameters.AddWithValue("@CinemaCode", CinemaCode);
                cmd.Parameters.AddWithValue("@ScreenCode", ScreenCode);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
