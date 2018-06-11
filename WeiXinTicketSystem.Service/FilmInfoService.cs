﻿using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Entity.Repository;
using WeiXinTicketSystem.Entity.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXinTicketSystem.Entity.Models.PageList;
using WeiXinTicketSystem.Entity.Enum;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Web;

namespace WeiXinTicketSystem.Service
{
    public class FilmInfoService
    {
        #region ctor
        private readonly IRepository<FilmInfoEntity> _filmInfoRepository;

        public FilmInfoService()
        {
            //TODO: 移除内部依赖
            _filmInfoRepository = new Repository<FilmInfoEntity>();
        }
        #endregion


        /// <summary>
        /// 根据影片编码获取影片信息
        /// </summary>
        /// <param name="filmCode"></param>
        /// <returns></returns>
        public FilmInfoEntity GetFilmInfoByFilmCode(string filmCode)
        {
            return _filmInfoRepository.Query.Where(x => x.FilmCode == filmCode).SingleOrDefault();
        }

        /// <summary>
        /// 获取所有影片信息列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<FilmInfoEntity>> GetAllFilmInfoAsync()
        {
            return await _filmInfoRepository.Query.Where(x => !x.IsDel).ToListAsync();
        }

        /// <summary>
        /// 后台分页读取影片信息
        /// </summary>
        /// <param name="filmCode"></param>
        /// <param name="filmName"></param>
        /// <param name="keyword"></param>
        /// <param name="offset"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public async Task<IPageList<FilmInfoEntity>> GetFilmInfoPagedAsync(string filmCode, string filmName, string keyword, int offset, int perPage)
        {
            var query = _filmInfoRepository.Query.OrderByDescending(x => x.Id).Skip(offset).Take(perPage);
            //影片编码
            if (!string.IsNullOrEmpty(filmCode))
            {
                query.Where(x => x.FilmCode == filmCode);
            }
            //影片名称
            if (!string.IsNullOrEmpty(filmName))
            {
                query.Where(x => x.FilmName.Contains(filmName));
            }
            //其他数据
            if (!string.IsNullOrEmpty(keyword))
            {
                query.Where(x => x.Publisher.Contains(keyword) || x.Producer.Contains(keyword) || x.Director.Contains(keyword) || x.Cast.Contains(keyword) || x.Introduction.Contains(keyword));
            }
            query.Where(x => !x.IsDel);
            return await query.ToPageListAsync();
        }

        /// <summary>
        /// 获取FilmInfoEntity实体
        /// </summary>
        /// <param name="filmCode"></param>
        /// <returns></returns>
        public async Task<FilmInfoEntity> GetFilmInfoByFilmCodeAsync(string filmCode)
        {
            return await _filmInfoRepository.Query.Where(x => x.FilmCode == filmCode).SingleOrDefaultAsync();
        }

        /// <summary>
        /// 根据影片信息ID获取影片信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<FilmInfoEntity> GetFilmInfoByIdAsync(int Id)
        {
            return await _filmInfoRepository.Query.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(FilmInfoEntity entity)
        {
            _filmInfoRepository.Update(entity);
        }


        /// <summary>
        /// 更新影片信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(FilmInfoEntity entity)
        {
            await _filmInfoRepository.UpdateAsync(entity);
        }


        /// <summary>
        /// 新增影片信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(FilmInfoEntity entity)
        {
            await _filmInfoRepository.InsertAsync(entity);
        }

        /// <summary>
        /// 新增影片信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(FilmInfoEntity entity)
        {
            _filmInfoRepository.Insert(entity);
        }

        /// <summary>
        ///  通过影片编码查找所有影片信息
        /// </summary>
        /// <param name="filmCode"></param>
        /// <returns></returns>
        public IEnumerable<FilmInfoEntity>  GetFilmsByID(string filmCode)
        {
            return _filmInfoRepository.Query.Where(x => x.FilmCode == filmCode).ToList();
        }

        public FilmInfoEntity GetFilmByFilmName(string FilmName)
        {
             _filmInfoRepository.Query.Where(t => t.FilmName == FilmName || t.FilmName.Contains(FilmName)).ToList();
            if (_filmInfoRepository.Query.Where(t => t.FilmName == FilmName || t.FilmName.Contains(FilmName)).ToList().Count > 0)
            {
                return _filmInfoRepository.Query.Where(t => t.FilmName == FilmName || t.FilmName.Contains(FilmName)).ToList()[0];
            }
            else
                return null;
        }

        
    }
}
