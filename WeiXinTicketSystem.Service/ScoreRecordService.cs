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
    public class ScoreRecordService
    {
        #region ctor
        private readonly IRepository<ScoreRecordEntity> _scoreRecordRepository;
        public ScoreRecordService()
        {
            //TODO: 移除内部依赖
            _scoreRecordRepository = new Repository<ScoreRecordEntity>();
        }
        #endregion


        /// <summary>
        /// 根据影院编码获取积分记录信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<ScoreRecordEntity> GetScoreRecordByCinemaCode(string CinemaCode)
        {
            return _scoreRecordRepository.Query.Where(x => x.CinemaCode == CinemaCode).ToList();
        }


        /// <summary>
        /// 获取所有积分记录列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ScoreRecordEntity>> GetAllScoreRecordAsync()
        {
            return await _scoreRecordRepository.Query.ToListAsync();
        }

        /// <summary>
        /// 后台分页读取积分记录信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<ScoreRecordEntity>> GetScoreRecordPagedAsync(string cinemaCode, string keyword, int offset, int perPage)
        {
            var query = _scoreRecordRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Description.Contains(keyword));
            }
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取ScoreRecordEntity实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<IList<ScoreRecordEntity>> GetScoreRecordByCinemaCodeAsync(string CinemaCode)
        {
            return await _scoreRecordRepository.Query.Where(x => x.CinemaCode == CinemaCode).ToListAsync();
        }


        /// <summary>
        /// 根据积分记录ID获取积分记录信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ScoreRecordEntity> GetScoreRecordByIdAsync(int Id)
        {
            return await _scoreRecordRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(ScoreRecordEntity entity)
        {
            _scoreRecordRepository.Update(entity);
        }


        /// <summary>
        /// 更新积分记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ScoreRecordEntity entity)
        {
            await _scoreRecordRepository.UpdateAsync(entity);
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(ScoreRecordEntity entity)
        {
            _scoreRecordRepository.Insert(entity);
        }

        /// <summary>
        /// 新增积分记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(ScoreRecordEntity entity)
        {
            await _scoreRecordRepository.InsertAsync(entity);
        }
    }
}
