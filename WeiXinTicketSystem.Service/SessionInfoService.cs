using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WeiXinTicketSystem.NetSale;
//using WeiXinClient.NetSale.Models;
using NetSaleSvc.Api.Models;

namespace WeiXinTicketSystem.Service
{
    public class SessionInfoService
    {
        #region ctor
        private readonly IRepository<SessionInfoEntity> _sessionInfoRepository;
        private readonly IRepository<AdminSessionViewEntity> _adminSessionRepository;
        private NetSaleSvcApi _netSaleSvcApi;

        public SessionInfoService()
        {
            //TODO: 移除内部依赖
            _sessionInfoRepository = new Repository<SessionInfoEntity>();
            _adminSessionRepository = new Repository<AdminSessionViewEntity>();
            _netSaleSvcApi = new NetSaleSvcApi();
        }
        #endregion

        /// <summary>
        /// 获取影院在指定时间段内的排期
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public IList<SessionInfoEntity> GetSessions(string CinemaCode,DateTime StartDate, DateTime EndDate)
        {
            EndDate = EndDate.AddDays(1);
            return _sessionInfoRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.StartTime > StartDate && x.StartTime < EndDate).OrderBy(x => x.StartTime).ToList();
        }

        /// <summary>
        /// 根据排期编码获取排期信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="SessionCode"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public SessionInfoEntity GetSessionInfo(string CinemaCode, string SessionCode)
        {
            return _sessionInfoRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.SessionCode == SessionCode).SingleOrDefault();
        }

        /// <summary>
        /// 根据Id和UserId获取排期
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<AdminSessionViewEntity> GetAsync(int Id)
        {
            return await _adminSessionRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 后台分页获取排期
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <param name="keyword"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPageList<AdminSessionViewEntity>> GetSessionsPagedAsync(string cinemaCode,
            int offset, int perPage, string keyword,DateTime? startDate, DateTime? endDate)
        {
            var query = _adminSessionRepository.Query
                .OrderByDescending(x => x.FilmCode)
                .Skip(offset)
                .Take(perPage);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaName.Contains(keyword) || x.ScreenName.Contains(keyword) || x.FilmName.Contains(keyword));
            }

            if (startDate.HasValue)
            {
                query.Where(x => x.StartTime > startDate.Value);
            }

            if (endDate.HasValue)
            {
                DateTime deadline = endDate.Value.AddDays(1);
                query.Where(x => x.StartTime < deadline);
            }

            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 根据接入商获取影院指定时间内的排期（包括影院给该接入商设置的价格）
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="UserId"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public IList<SessionInfoWithCustomPrice> GetSessionWithSettingPrice(string CinemaCode,
            DateTime StartDate, DateTime EndDate)
        {
            const string sql = @"SELECT SessionInfo.*, SessionPriceSettings.* 
                FROM SessionInfo 
                LEFT JOIN SessionPriceSettings ON SessionPriceSettings.[CinemaCode] = SessionInfo.[CinemaCode] and (SessionPriceSettings.Code = SessionInfo.SessionCode or SessionPriceSettings.Code = SessionInfo.FilmCode) 
                WHERE SessionInfo.CinemaCode = @CinemaCode AND SessionInfo.StartTime >= @StartDate AND SessionInfo.StartTime < @EndDate
                ORDER BY StartTime";

            Dictionary<int, SessionInfoWithCustomPrice> resultDic = new Dictionary<int, SessionInfoWithCustomPrice>();

            _sessionInfoRepository.QueryDouble<SessionInfoEntity,SessionPriceSettingEntity, SessionInfoWithCustomPrice>(sql,
                (session, price) =>
                {
                    SessionInfoWithCustomPrice entity = default(SessionInfoWithCustomPrice);
                    if (session != null && !resultDic.TryGetValue(session.Id, out entity))
                    {
                        resultDic.Add(session.Id, entity = new SessionInfoWithCustomPrice { sessionInfo = session });
                    }
                    if (price != null)
                    {
                        if (price.Type == PricePlanTypeEnum.Film)
                        {
                            entity.customPrice.CustomFilmPrice = price.Price;
                        }
                        else if (price.Type == PricePlanTypeEnum.Session)
                        {
                            entity.customPrice.CustomSessionPrice = price.Price;
                        }
                        else if (price.Type == PricePlanTypeEnum.LowestPrice)
                        {
                            entity.customPrice.CustomLowestPrice = price.Price;
                        }
                    }
                    return entity;
                },
                param: new { CinemaCode = CinemaCode, StartDate = StartDate, EndDate = EndDate.AddDays(1) },
                commandType: CommandType.Text);

            return resultDic.Values.ToList();
        }

        /// <summary>
        /// 批量合并
        /// </summary>
        /// <param name="entities"></param>
        public void BulkMerge(IEnumerable<SessionInfoEntity> Entities, string CinemaCode,
            DateTime StartDate, DateTime EndDate)
        {
            if (StartDate < DateTime.Now)
            {
                StartDate = DateTime.Now;
            }
            EndDate = EndDate.AddDays(1);

            using (var connection = DbConnectionFactory.OpenSqlConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.MergeSession";
                cmd.Parameters.AddWithValue("@sessions", Entities.ToList().ToDataTable());
                cmd.Parameters.AddWithValue("@CinemaCode", CinemaCode);
                cmd.Parameters.AddWithValue("@StartTime", StartDate);
                cmd.Parameters.AddWithValue("@EndTime", EndDate);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 重新获取排期
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public ReturnData QuerySession(string CinemaCode, DateTime StartDate, DateTime EndDate)
        {
            ReturnData returnData = new ReturnData();
            int a = System.Environment.TickCount;
            QuerySessionReply querySessionReply = _netSaleSvcApi.QuerySession(CinemaCode, StartDate, EndDate);
            int QuerySessionUseTime = System.Environment.TickCount - a;//调接口消耗毫秒数
            if (querySessionReply != null)
            {
                if (querySessionReply.Status == "Success")
                {
                    var oldSessions = GetSessions(CinemaCode, StartDate, EndDate);

                    var newSessions = querySessionReply.Sessions.Session.Select(
                        x => x.MapToEntity(
                            oldSessions.Where(y => y.SessionCode == x.Code).SingleOrDefault()
                                ?? new SessionInfoEntity
                                {
                                    CinemaCode = CinemaCode,
                                    SessionCode = x.Code
                                })).ToList();

                    //插入或更新最新放映计划
                    BulkMerge(newSessions,CinemaCode, StartDate, EndDate);


                    returnData.Status = true;
                    returnData.Info = "获取排期成功,耗时" + QuerySessionUseTime + "毫秒";
                }
                else
                {
                    returnData.Status = false;
                    returnData.Info = "获取排期失败！";
                }
            }
            return returnData;
        }

    }
}
