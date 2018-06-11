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
    public class BannerService
    {
        #region ctor
        private readonly IRepository<BannerEntity> _BannerRepository;

        public BannerService()
        {
            //TODO: 移除内部依赖
            _BannerRepository = new Repository<BannerEntity>();
        }
        #endregion

        /// <summary>
        /// 根据影院编码获取图片上传信息
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public BannerEntity GetBannerByCinemaCode(string CinemaCode)
        {
            return _BannerRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefault();
        }


        /// <summary>
        /// 获取所有图片上传列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<BannerEntity>> GetAllBannerAsync()
        {
            return await _BannerRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }


        /// <summary>
        /// 后台分页读取图片上传信息
        /// </summary>
        /// <param name="cinemaCode"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<BannerEntity>> GetBannerPagedAsync(string cinemaCode, string keyword, int offset, int perPage)
        {
            var query = _BannerRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影院编码
            if (!string.IsNullOrEmpty(cinemaCode))
            {
                query.Where(x => x.CinemaCode == cinemaCode);
            }
            
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Title.Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }


        /// <summary>
        /// 获取PictureUploadEntity实体
        /// </summary>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public async Task<BannerEntity> GetBannerByCinemaCodeAsync(string CinemaCode)
        {
            return await _BannerRepository.Query.Where(x => x.CinemaCode == CinemaCode).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 根据图片上传ID获取图片上传信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<BannerEntity> GetBannerByIdAsync(int Id)
        {
            return await _BannerRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(BannerEntity entity)
        {
            _BannerRepository.Update(entity);
        }

        /// <summary>
        /// 更新图片上传
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(BannerEntity entity)
        {
            await _BannerRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 新增图片上传
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(BannerEntity entity)
        {
            await _BannerRepository.InsertAsync(entity);
        }

    }
}
