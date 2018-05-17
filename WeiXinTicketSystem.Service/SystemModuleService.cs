using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System;
using System.Collections.Generic;
using WeiXinTicketSystem.Entity.Models.PageList;
using System.Threading.Tasks;


namespace WeiXinTicketSystem.Service
{
    public class SystemModuleService
    {
        #region ctor
        private readonly IRepository<SystemModuleEntity> _moduleRepository;
        public SystemModuleService()
        {
            _moduleRepository = new Repository<SystemModuleEntity>();
        }
        #endregion

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<SystemModuleEntity>> GetModulesPagedAsync(int UserId,int offset, int perPage, string keyword)
        {
            var query = _moduleRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            query.Where(x => x.CreateUserId==UserId);
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.ModuleName.Contains(keyword) || x.ModuleFlag.Contains(keyword));
            }
            query.Where(x => !x.Deleted);
            return await query.ToPageListAsync();
        }
        /// <summary>
        /// 读出所有模块
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SystemModuleEntity>> GetAllModulesAsync()
        {
            return await _moduleRepository.Query.Where(x => !x.Deleted).ToListAsync();
        }
        /// <summary>
        /// 读出所有的根模块，两级模块设计
        /// </summary>
        /// <returns></returns>
        public async Task<IList<SystemModuleEntity>> GetRootModulesAsync()
        {
            return await _moduleRepository.Query.Where(x => x.ModuleParentId == 0 && !x.Deleted).ToListAsync();
        }
        /// <summary>
        /// 根据Id读取模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<SystemModuleEntity> GetAsync(int Id)
        {
            return await _moduleRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 更新模块
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SystemModuleEntity entity)
        {
            await _moduleRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增模块
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(SystemModuleEntity entity)
        {
            await _moduleRepository.InsertAsync(entity);
        }
        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(SystemModuleEntity entity)
        {
            await _moduleRepository.DeleteAsync(entity);
        }
    }
}
