using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Service
{
    public class SnackTypeService
    {
        #region ctor
        private readonly IRepository<SnackTypeEntity> _snackTypeRepository;
        private readonly IRepository<AdminSnackTypesViewEntity> _snackTypeViewRepository;
        public SnackTypeService()
        {
            _snackTypeRepository = new Repository<SnackTypeEntity>();
            _snackTypeViewRepository = new Repository<AdminSnackTypesViewEntity>();
        }
        #endregion

        public async Task<IPageList<AdminSnackTypesViewEntity>> GetSnacksTypePagedAsync(string cinemaCode,
            int offset, int perPage, string keyword)
        {
            var query = _snackTypeViewRepository.Query
                .OrderByDescending(x => x.Id)
                .Skip(offset)
                .Take(perPage);

            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.CinemaName.Contains(keyword) || x.TypeName.Contains(keyword)
                    || x.Remark.Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        public async Task<IList<SnackTypeEntity>> GetAllSnacksTypesAsync(string cinemaCode)
        {
            var query = _snackTypeRepository.Query.Where(x => !x.IsDel);
            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            return await query.ToListAsync();
        }

        public async Task<SnackTypeEntity> GetAsync(int Id)
        {
            return await _snackTypeRepository.Query.Where(x => x.Id == Id
                && !x.IsDel).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(SnackTypeEntity entity)
        {
            await _snackTypeRepository.UpdateAsync(entity);
        }
        public async Task InsertAsync(SnackTypeEntity entity)
        {
            await _snackTypeRepository.InsertAsync(entity);
        }
    }
}
