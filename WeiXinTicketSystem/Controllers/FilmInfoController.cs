using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.FilmInfo;
using WeiXinTicketSystem.Utils;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Service;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Reflection;
using System.Web;
using System.IO;
using System.Drawing.Imaging;
using System.Net;
using System.Xml.Linq;
using WeiXin.Tools;
using System.Configuration;

namespace WeiXinTicketSystem.Controllers
{
    public class FilmInfoController : RootExraController
    {
        private FilmInfoService _filmInfoService;
        List<int> CurrentPermissions;

        #region ctor
        public FilmInfoController()
        {
            _filmInfoService = new FilmInfoService();
        }
        #endregion


        /// <summary>
        /// 影片信息管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "FilmInfo").SingleOrDefault();
            CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }


        ///// <summary>
        ///// 影片信息列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<FilmInfoQueryModel> pageModel)
        {
            var filmInfo = await _filmInfoService.GetFilmInfoPagedAsync(
                pageModel.Query.FilmCode,
                pageModel.Query.FilmName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(filmInfo.ToDynatableModel(filmInfo.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加影片信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            CreateOrUpdateFilmInfoViewModel model = new CreateOrUpdateFilmInfoViewModel();
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改影片信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var filmInfo = await _filmInfoService.GetFilmInfoByIdAsync(id);
            if (filmInfo == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateFilmInfoViewModel model = new CreateOrUpdateFilmInfoViewModel();
            model.MapFrom(filmInfo);
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 添加或修改影院支付方式配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateFilmInfoViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改影院支付方式配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateFilmInfoViewModel model, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

           FilmInfoEntity filmInfo = new FilmInfoEntity();
            if (model.Id > 0)
            {
                filmInfo = await _filmInfoService.GetFilmInfoByIdAsync(model.Id);
            }
            filmInfo.MapFrom(model);

            //图片处理
            if (Image != null)
            {
                string rootPath = HttpRuntime.AppDomainAppPath.ToString();
                string basePath = ConfigurationManager.AppSettings["ImageBasePath"].ToString();
                string savePath = @"upload\FilmImg\" + DateTime.Now.ToString("yyyyMM") + @"\";
                string accessPath = "upload/FilmImg/" + DateTime.Now.ToString("yyyyMM") + "/";
                System.Drawing.Image image = System.Drawing.Image.FromStream(Image.InputStream);
                //判断原图片是否存在
                if (!string.IsNullOrEmpty(filmInfo.Image))
                {
                    string file = filmInfo.Image.Replace(basePath, rootPath).Replace(accessPath, savePath);
                    if (System.IO.File.Exists(file))
                    {
                        //如果存在则删除
                        System.IO.File.Delete(file);
                    }
                }
                string fileName = ImageHelper.SaveImageToDisk(rootPath + savePath, DateTime.Now.ToString("yyyyMMddHHmmss"), image);
                filmInfo.Image = basePath + accessPath + fileName;
            }

            if (filmInfo.Id == 0)
            {
                await _filmInfoService.InsertAsync(filmInfo);
            }
            else
            {
                await _filmInfoService.UpdateAsync(filmInfo);
            }

            //return RedirectObject(Url.Action(nameof(Index)));
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "FilmInfo").SingleOrDefault();
            CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View(nameof(Index));
        }

        /// <summary>
        /// 删除影院支付方式配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var cinema = await _filmInfoService.GetFilmInfoByIdAsync(id);

            if (cinema != null)
            {
                //cinema.IsDel = true;
                await _filmInfoService.UpdateAsync(cinema);
            }
            return Object();
        }


        private void PreparyCreateOrEditViewData()
        {
            //绑定状态枚举
            ViewBag.Status_dd = EnumUtil.GetSelectList<YesOrNoEnum>();

        }

        public ActionResult UpdateFilm()
        {
             UpdateMovie();
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "FilmInfo").SingleOrDefault();
            CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View(nameof(Index));
        }

        /// <summary>
        /// 更新影片信息
        /// </summary>
        /// <returns></returns>
        public void UpdateMovie()
        {
            WebClient client = new WebClient();
            DateTime date = DateTime.Now.Date.AddDays(1);
            string strDate = date.ToString("yyyy");
            string URLAddress = "http://datacenter.ykse.com.cn:8200/FilmDataDownload.aspx?year=" + strDate;
            string strPath =System.Web.HttpContext.Current.Server.MapPath("/download");
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            string fileName = strPath + "\\MovieInfo.xml";
            client.DownloadFile(URLAddress, fileName);
            XDocument doc = XDocument.Load(fileName);
            XElement Film = XElement.Parse(doc.ToString().Replace("<![CDATA[", "").Replace("]]>", ""));
            IEnumerable<XElement> FilmInfoList = Film.Descendants("FilmInfomation");

            foreach (var film in FilmInfoList)
            {
                QryDownloadMovieInfo qryFilm = JSONHelper.DeserializeObject<QryDownloadMovieInfo>(film);

                FilmInfoEntity entity = ConvertFilmToEntity(qryFilm.FilmInfomation);
                string filmCode = entity.FilmCode;
                if (_filmInfoService.GetFilmsByCode(filmCode).Count() == 0)
                {
                    _filmInfoService.Insert(entity);

                }
            }
            //获取豆瓣上的影片信息更新movie
            updatefromdouban();
        }

        private FilmInfoEntity ConvertFilmToEntity(DownloadMovieInfo film)
        {
            FilmInfoEntity entity = new FilmInfoEntity();
            entity.FilmCode = film.ID;
            entity.FilmName = film.Name;
            entity.Duration = "0";
            entity.PublishDate = film.PublishDate;
            entity.Director = film.Director;
            entity.Cast = film.Cast;
            entity.Introduction = film.Brief;
            entity.Score = 0;
            entity.Area = " ";
            entity.Type = " ";
            entity.Language = " ";
            entity.Status = 0;
            entity.Image = " ";

            return entity;

        }

        public bool updatefromdouban()
        {
            string Url = "https://api.douban.com/v2/movie/coming_soon";//豆瓣即将上映
            string coming_soon = HttpHelper.VisitUrl(Url);
            DoubanComingSoonReply DoubanComingSoonReply = JSONHelper.FromJson<DoubanComingSoonReply>(coming_soon);
            foreach (var subject in DoubanComingSoonReply.subjects)
            {
                string UrlFilm = "https://api.douban.com/v2/movie/subject/" + subject.id;
                string Film = HttpHelper.VisitUrl(UrlFilm);
                DoubanFilmReply DoubanFilmReply = JSONHelper.FromJson<DoubanFilmReply>(Film);
                FilmInfoEntity entity = ConvertDoubanFilmToEntity(DoubanFilmReply);
                //string filmCode = entity.FilmCode;
                //if (dao.GetFilmsByID(filmCode).Count() == 0)
                //{
                //Insert(entity);
                //}
                //更新movie
                FilmInfoEntity filmInfo = _filmInfoService.GetFilmByFilmName(entity.FilmName);
                if (filmInfo != null)
                {
                    filmInfo.Director = entity.Director;
                    filmInfo.Cast = entity.Cast;
                    filmInfo.Introduction = entity.Introduction;
                    filmInfo.Score = entity.Score;
                    filmInfo.Area = entity.Area;
                    filmInfo.Type = entity.Type;

                    filmInfo.Image = entity.Image;
                    _filmInfoService.Update(filmInfo);
                }
            }
            return true;
        }

        private FilmInfoEntity ConvertDoubanFilmToEntity(DoubanFilmReply film)
        {
            FilmInfoEntity entity = new FilmInfoEntity();
            entity.FilmCode = film.id;
            entity.FilmName = film.title;
            entity.Duration = "0";
            //entity.FilmPublishDate = ;
            string directors = string.Empty;
            foreach (var director in film.directors)
            {
                directors += director.name + "/";
            }
            entity.Director = directors.IndexOf('/') > 0 ? directors.TrimEnd('/') : "";
            string casts = string.Empty;
            foreach (var cast in film.casts)
            {
                casts += cast.name + "/";
            }
            entity.Cast = casts.IndexOf('/') > 0 ? casts.TrimEnd('/') : "";
            entity.Introduction = film.summary;
            //entity.FilmDate = film.PublishDate;
            entity.Score = Convert.ToDecimal(film.rating.average);
            string countries = string.Empty;
            foreach (var countrie in film.countries)
            {
                countries += countrie + "/";
            }
            entity.Area = countries.IndexOf('/') > 0 ? countries.TrimEnd('/') : "";
            string genres = string.Empty;
            foreach (var genre in film.genres)
            {
                genres += genre + "/";
            }
            entity.Type = genres.IndexOf('/') > 0 ? genres.TrimEnd('/') : "";
            entity.Language = " ";
            entity.Status = 0;
            entity.Image = DownloadImg(film.images.small);

            return entity;
        }

        /// <summary>
        /// 下载电影图片到本地
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        private string DownloadImg(string Url)
        {
            try
            {
                DateTime nowdate = DateTime.Now;
                WebClient mywebclient = new WebClient();
                string savePath = "/upload/MovieImg/" + nowdate.ToString("yyyyMM") + "/" + Url.Split('/')[Url.Split('/').Length - 1];
                string PhysicalPath = HttpRuntime.AppDomainAppPath.ToString() + "/upload/MovieImg/" + nowdate.ToString("yyyyMM") + "/";
                if (!Directory.Exists(PhysicalPath))
                {
                    Directory.CreateDirectory(PhysicalPath);
                }
                PhysicalPath += Url.Split('/')[Url.Split('/').Length - 1];
                mywebclient.DownloadFile(Url, PhysicalPath);
                return savePath;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}