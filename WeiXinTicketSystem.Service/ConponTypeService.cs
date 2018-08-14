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
    public class ConponTypeService
    {
        #region ctor
        private readonly IRepository<ConponTypeEntity> _conponTypeRepository;

        public ConponTypeService()
        {
            //TODO: 移除内部依赖
            _conponTypeRepository = new Repository<ConponTypeEntity>();
        }
        #endregion

        /// <summary>
        /// 获取所有优惠券类型列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ConponTypeEntity>> GetAllConponTypeAsync()
        {
            return await _conponTypeRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }


        /// <summary>
        /// 读出所有的根模块，两级模块设计
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ConponTypeEntity>> GetRootConponTypeAsync()
        {
            return await _conponTypeRepository.Query.Where(x => x.TypeParentId == 0 && !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 根据父节点读取优惠券类型列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ConponTypeEntity>> GetConponTypeByParentIdAsync(int TypeParentId)
        {
            return await _conponTypeRepository.Query.Where(x => x.TypeParentId == TypeParentId && !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 根据优惠券类型编号读取优惠券类型信息(异步)
        /// </summary>
        /// <returns></returns>
        public async Task<ConponTypeEntity> GetConponTypeByTypeCodeAsync(string typeCode)
        {
            return await _conponTypeRepository.Query.Where(x => x.TypeCode == typeCode && !x.IsDel).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据优惠券类型编号读取优惠券类型信息
        /// </summary>
        /// <returns></returns>
        public ConponTypeEntity GetConponTypeByTypeCode(string typeCode)
        {
            return _conponTypeRepository.Query.Where(x => x.TypeCode == typeCode && !x.IsDel).SingleOrDefault();
        }

        /// <summary>
        /// 后台分页读取优惠券类型信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<ConponTypeEntity>> GetConponTypePagedAsync(string keyword, int offset, int perPage)
        {
            var query = _conponTypeRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.TypeName.Contains(keyword) || x.Remark.Contains(keyword));
            }
            query.Where(x => !x.IsDel && x.TypeParentId == 0);
            return await query.ToPageListAsync();
        }


        /// <summary>
        /// 根据优惠券类型ID获取优惠券类型信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ConponTypeEntity> GetConponTypeByIdAsync(int Id)
        {
            return await _conponTypeRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(ConponTypeEntity entity)
        {
            _conponTypeRepository.Update(entity);
        }

        /// <summary>
        /// 更新优惠券类型
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ConponTypeEntity entity)
        {
            await _conponTypeRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增优惠券类型
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(ConponTypeEntity entity)
        {
            await _conponTypeRepository.InsertAsync(entity);
        }

    }
}
