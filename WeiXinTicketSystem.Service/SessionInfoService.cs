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

namespace WeiXinTicketSystem.Service
{
    public class SessionInfoService
    {
        #region ctor
        private readonly IRepository<SessionInfoEntity> _sessionInfoRepository;
        private readonly IRepository<AdminSessionViewEntity> _adminSessionRepository;

        public SessionInfoService()
        {
            //TODO: 移除内部依赖
            _sessionInfoRepository = new Repository<SessionInfoEntity>();
            _adminSessionRepository = new Repository<AdminSessionViewEntity>();
        }
        #endregion

        /// <summary>
        /// 获取影院在指定时间段内的排期
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public IList<SessionInfoEntity> GetSessions(string CinemaCode, int UserId, DateTime StartDate, DateTime EndDate)
        {
            EndDate = EndDate.AddDays(1);
            return _sessionInfoRepository.Query.Where(x => x.CCode == CinemaCode && x.UserID == UserId
                && x.StartTime > StartDate && x.StartTime < EndDate).OrderBy(x => x.StartTime).ToList();
        }

        /// <summary>
        /// 获取影院在指定时间段内的排期的影片信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public IList<SessionInfoEntity> GetSessionsFilm(string CinemaCode, int UserId, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                EndDate = EndDate.AddDays(1);
                return _sessionInfoRepository.Query.GroupBy(a => a.FilmName ).Select(x => new { x.FilmName }).Where(x => x.CCode == CinemaCode && x.UserID == UserId
                      && x.StartTime > StartDate && x.StartTime < EndDate).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 根据排期编码获取排期信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="SessionCode"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public SessionInfoEntity GetSessionInfo(string CinemaCode, string SessionCode, int UserId)
        {
            return _sessionInfoRepository.Query.Where(x => x.CCode == CinemaCode && x.UserID == UserId
            && x.SCode == SessionCode).SingleOrDefault();
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
        /// <param name="thirdUserId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IPageList<AdminSessionViewEntity>> GetSessionsPagedAsync(string cinemaCode,
            int offset, int perPage, string keyword, int? thirdUserId,DateTime? startDate, DateTime? endDate)
        {
            var query = _adminSessionRepository.Query
                .OrderByDescending(x => x.FilmCode)
                .Skip(offset)
                .Take(perPage);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CCode == cinemaCode);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaName.Contains(keyword) || x.ScreenName.Contains(keyword) || x.FilmName.Contains(keyword));
            }

            if (thirdUserId.HasValue)
            {
                query.Where(x => x.UserID == thirdUserId.Value);
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
        public IList<SessionInfoWithCustomPrice> GetSessionWithUserPrice(string CinemaCode, int UserId,
            DateTime StartDate, DateTime EndDate)
        {
            const string sql = @"SELECT SessionInfo.*, PricePlan.* 
                FROM SessionInfo 
                LEFT JOIN PricePlan ON PricePlan.[CinemaCode] = SessionInfo.[CCode] and (PricePlan.Code = SessionInfo.SCode or PricePlan.Code = SessionInfo.FilmCode) and (PricePlan.UserID = @UserID or PricePlan.UserID = @CinemaCode)
                WHERE SessionInfo.CCode = @CinemaCode AND SessionInfo.UserId = @UserId AND SessionInfo.StartTime >= @StartDate AND SessionInfo.StartTime < @EndDate
                ORDER BY StartTime";

            Dictionary<int, SessionInfoWithCustomPrice> resultDic = new Dictionary<int, SessionInfoWithCustomPrice>();

            _sessionInfoRepository.QueryDouble<SessionInfoEntity, PricePlanEntity, SessionInfoWithCustomPrice>(sql,
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
                param: new { CinemaCode = CinemaCode, UserId = UserId, StartDate = StartDate, EndDate = EndDate.AddDays(1) },
                commandType: CommandType.Text);

            return resultDic.Values.ToList();
        }

        /// <summary>
        /// 批量合并
        /// </summary>
        /// <param name="entities"></param>
        public void BulkMerge(IEnumerable<SessionInfoEntity> Entities, string CinemaCode,
            DateTime StartDate, DateTime EndDate, int UserId)
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
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
