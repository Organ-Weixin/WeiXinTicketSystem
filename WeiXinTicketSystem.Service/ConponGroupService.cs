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
    public class ConponGroupService
    {
        #region ctor
        private readonly IRepository<ConponEntity> _conponRepository;
        private readonly IRepository<ConponGroupEntity> _conponGroupRepository;
        private readonly IRepository<ConponGroupViewEntity> _conponGroupViewRepository;

        public ConponGroupService()
        {
            //TODO: 移除内部依赖
            _conponRepository= new Repository<ConponEntity>();
            _conponGroupRepository = new Repository<ConponGroupEntity>();
            _conponGroupViewRepository=new Repository<ConponGroupViewEntity>();
        }
        #endregion


        /// <summary>
        /// 根据影院编码获取优惠券组信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<ConponGroupEntity> GetConponGroupByCinemaCode(string CinemaCode)
        {
            return _conponGroupRepository.Query.Where(x => x.CinemaCode == CinemaCode).ToList();
        }

        /// <summary>
        /// 根据影院编码和优惠券类型编号获取优惠券组信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public IList<ConponGroupEntity> GetConponGroupByCinemaCodeAndTypeCode(string CinemaCode,string typeCode)
        {
            if (string.IsNullOrEmpty(CinemaCode) || string.IsNullOrEmpty(typeCode))
                return new List<ConponGroupEntity>();
            return _conponGroupRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.TypeCode ==typeCode).ToList();
        }

        /// <summary>
        /// 根据影院编码和优惠券组编号获取优惠券组信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public ConponGroupEntity GetConponGroupByCinemaCodeAndGroupCode(string CinemaCode, string groupCode)
        {
            if (string.IsNullOrEmpty(CinemaCode) || string.IsNullOrEmpty(groupCode))
                return new ConponGroupEntity();
            return _conponGroupRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.GroupCode == groupCode).SingleOrDefault();
        }

        /// <summary>
        /// 根据影院编码和优惠券组编号获取优惠券组信息(视图)
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public ConponGroupViewEntity GetConponGroupViewByCinemaCodeAndGroupCode(string CinemaCode, string groupCode)
        {
            if (string.IsNullOrEmpty(CinemaCode) || string.IsNullOrEmpty(groupCode))
                return new ConponGroupViewEntity();
            return _conponGroupViewRepository.Query.Where(x => x.CinemaCode == CinemaCode && x.GroupCode == groupCode).SingleOrDefault();
        }

        /// <summary>
        /// 后台分页读取优惠券组信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="ConponCode"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<ConponGroupViewEntity>> GetConponGroupPagedAsync(string cinemaCode, string keyword, int offset, int perPage)
        {
            var query = _conponGroupViewRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
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
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 根据优惠券组ID获取优惠券组信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ConponGroupEntity> GetConponGroupByIdAsync(int Id)
        {
            return await _conponGroupRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(ConponGroupEntity entity)
        {
            _conponGroupRepository.Update(entity);
        }

        /// <summary>
        /// 更新优惠券组
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ConponGroupEntity entity)
        {
            await _conponGroupRepository.UpdateAsync(entity);
        }
        /// <summary>
        /// 新增优惠券组
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(ConponGroupEntity entity)
        {
            _conponGroupRepository.Insert(entity);
        }
        /// <summary>
        /// 新增优惠券组
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(ConponGroupEntity entity)
        {
            await _conponGroupRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 删除优惠券组
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(ConponGroupEntity entity)
        {
            await _conponGroupRepository.DeleteAsync(entity);
        }
        /// <summary>
        /// 添加优惠券组和优惠券
        /// </summary>
        /// <param name="ConponGroupView"></param>
        public void Insert(ConponViewEntity ConponGroupView)
        {
            using (var connection = DbConnectionFactory.OpenConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _conponGroupRepository.InsertWithTransaction(ConponGroupView.ConponGroupInfo,
                            connection, transaction);
                        ConponGroupView.ConponGroupConpons.ForEach(x =>
                        {
                            _conponRepository.InsertWithTransaction(x, connection, transaction);
                        });
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
