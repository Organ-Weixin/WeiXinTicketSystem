using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using WeiXinTicketSystem.Util;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WeiXinTicketSystem.Service
{
    public class SeatInfoService
    {
        #region ctor
        private readonly IRepository<ScreenSeatInfoEntity> _screenSeatInfoRepository;

        public SeatInfoService()
        {
            //TODO: 移除内部依赖
            _screenSeatInfoRepository = new Repository<ScreenSeatInfoEntity>();
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
