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
    public class MemberCardService
    {
        #region ctor
        private readonly IRepository<MemberCardEntity> _memberCardRepository;

        public MemberCardService()
        {
            //TODO: 移除内部依赖
            _memberCardRepository = new Repository<MemberCardEntity>();
        }
        #endregion


        /// <summary>
        /// 根据影院编码获取会员卡信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public MemberCardEntity GetMemberCardByCinemaCode(string CinemaCode)
        {
            return _memberCardRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有会员卡列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<MemberCardEntity>> GetAllMemberCardAsync()
        {
            return await _memberCardRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取会员卡信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<MemberCardEntity>> GetMemberCardPagedAsync(string cinemaCode, string cardNo, string keyword, int offset, int perPage)
        {
            var query = _memberCardRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            //卡号
            if (!string.IsNullOrEmpty(cardNo))
            {
                query.Where(x => x.CardNo.Contains(cardNo));
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CardNo.Contains(keyword) );
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取MemberCardEntity实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<MemberCardEntity> GetMemberCardByCinemaCodeAsync(string CinemaCode)
        {
            return await _memberCardRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据会员卡ID获取会员卡信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<MemberCardEntity> GetMemberCardByIdAsync(int Id)
        {
            return await _memberCardRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(MemberCardEntity entity)
        {
            _memberCardRepository.Update(entity);
        }


        /// <summary>
        /// 更新会员卡
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(MemberCardEntity entity)
        {
            await _memberCardRepository.UpdateAsync(entity);
        }


        /// <summary>
        /// 新增会员卡
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(MemberCardEntity entity)
        {
            await _memberCardRepository.InsertAsync(entity);
        }
    }
}
