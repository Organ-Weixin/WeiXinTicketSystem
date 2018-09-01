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
        private readonly IRepository<AdminMemberCardViewEntity> _memberCardViewRepository;
        public MemberCardService()
        {
            //TODO: 移除内部依赖
            _memberCardRepository = new Repository<MemberCardEntity>();
            _memberCardViewRepository = new Repository<AdminMemberCardViewEntity>();
        }
        #endregion


        /// <summary>
        /// 获取所有会员卡列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<MemberCardEntity>> GetAllMemberCardAsync()
        {
            return await _memberCardRepository.Query.ToListAsync();
        }

        /// <summary>
        /// 根据卡号获取会员卡
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public MemberCardEntity GetMemberCardByCardNo(string CinemaCode,string CardNo)
        {
            return _memberCardRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.CardNo == CardNo).SingleOrDefault();
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
        public async Task<IPageList<AdminMemberCardViewEntity>> GetMemberCardPagedAsync(string cinemaCode, string cardNo, string keyword, int offset, int perPage)
        {
            var query = _memberCardViewRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
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
                query.Where(x => x.CardNo.Contains(keyword) || x.NickName.Contains(keyword) || x.MobilePhone.Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
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
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(MemberCardEntity entity)
        {
            _memberCardRepository.Insert(entity);
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

        /// <summary>
        /// 删除会员卡
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(MemberCardEntity entity)
        {
            await _memberCardRepository.DeleteAsync(entity);
        }

        /// <summary>
        /// 根据影院编码和OpenID获取会员卡信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public async Task<IList<MemberCardEntity>> GetMemberCardByOpenIDAsync(string CinemaCode,string OpenID)
        {
            return await _memberCardRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.OpenID == OpenID).ToListAsync();
        }

        /// <summary>
        /// 根据影院编码和手机号码获取会员卡信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public IList<MemberCardEntity> GetMemberCardByMobilePhoneAsync(string CinemaCode, string MobilePhone)
        {
            return _memberCardRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.MobilePhone == MobilePhone).ToList();
        }
    }
}
