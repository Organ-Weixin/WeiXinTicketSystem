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
    public class MemberChargeSettingService
    {
        #region ctor
        private readonly IRepository<MemberChargeSettingEntity> _memberChargeSettingRepository;
        private readonly IRepository<AdminMemberChargeSettingViewEntity> _adminMemberChargeSettingViewRepository;

        public MemberChargeSettingService()
        {
            //TODO: 移除内部依赖
            _memberChargeSettingRepository = new Repository<MemberChargeSettingEntity>();
            _adminMemberChargeSettingViewRepository = new Repository<AdminMemberChargeSettingViewEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影院编码获取该影院下所有会员卡充值赠送条件信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<MemberChargeSettingEntity> GetMemberChargeSettingByCinemaCode(string CinemaCode)
        {
            return _memberChargeSettingRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.Deleted).ToList();
        }

        /// <summary>
        /// 获取所有会员卡充值赠送条件列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<MemberChargeSettingEntity>> GetAllMemberChargeSettingAsync()
        {
            return await _memberChargeSettingRepository.Query.Where(x => !x.Deleted).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取会员卡充值赠送条件信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="CinemaName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<AdminMemberChargeSettingViewEntity>> GetMemberChargeSettingPagedAsync(string cinemaCode, string keyword, int offset, int perPage)
        {
            try
            {
                var query = _adminMemberChargeSettingViewRepository.Query.OrderBy(x => x.Price).Skip(offset).Take(perPage);
                //影院编码
                if (!string.IsNullOrEmpty(cinemaCode))
                {
                    query.Where(x => x.CinemaCode == cinemaCode);
                }
                //其他数据
                if (!string.IsNullOrEmpty(keyword))
                {
                    query.Where(x => x.GroupName.Contains(keyword));
                }
                query.Where(x => !x.Deleted);
                return await query.ToPageListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 根据影院编码获取该影院下所有会员卡充值赠送条件信息(异步)
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<IList<MemberChargeSettingEntity>> GetMemberChargeSettingByCinemaCodeAsync(string CinemaCode)
        {
            return await _memberChargeSettingRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.Deleted).ToListAsync();
        }

        /// <summary>
        /// 根据影院编码获取该影院下所有会员卡充值赠送条件信息(视图异步)
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<IList<AdminMemberChargeSettingViewEntity>> GetMemberChargeSettingViewByCinemaCodeAsync(string CinemaCode)
        {
            return await _adminMemberChargeSettingViewRepository.Query.Where(x => x.CinemaCode == CinemaCode && !x.Deleted).ToListAsync();
        }

        /// <summary>
        /// 根据会员卡充值赠送条件ID获取会员卡充值赠送条件信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<MemberChargeSettingEntity> GetMemberChargeSettingByIdAsync(int Id)
        {
            return await _memberChargeSettingRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(MemberChargeSettingEntity entity)
        {
            _memberChargeSettingRepository.Update(entity);
        }

        /// <summary>
        /// 更新会员卡充值赠送条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(MemberChargeSettingEntity entity)
        {
            await _memberChargeSettingRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增会员卡充值赠送条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(MemberChargeSettingEntity entity)
        {
            await _memberChargeSettingRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 根据影院编码和充值金额获取会员卡充值赠送条件信息(异步)
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<IList<MemberChargeSettingEntity>> GetMemberChargeSettingByCinemaCodeAndPriceAsync(string CinemaCode, decimal? price)
        {
            return await _memberChargeSettingRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.Price == price && !x.Deleted).ToListAsync();
        }
    }
}
