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
            DateTime? startDate = null, endDate = null;
            if (!string.IsNullOrEmpty(pageModel.Query.filmDateRange))
            {
                var dates = pageModel.Query.filmDateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                startDate = DateTime.Parse(dates[0]);
                endDate = DateTime.Parse(dates[1]);
            }

            var filmInfo = await _filmInfoService.GetFilmInfoPagedAsync(
                pageModel.Query.FilmCode,
                pageModel.Query.FilmName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage,
                startDate,
                endDate
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

        /// <summary>
        /// 获取影片信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GrabFilmData()
        {
            string URLAddress = "http://datacenter.ykse.com.cn:8200/FilmDataDownload.aspx?year=" + DateTime.Now.ToString("yyyy");
            string httpresult = HttpHelper.VisitUrl(URLAddress);
            XElement Film = XElement.Parse(httpresult.Replace("<![CDATA[", "").Replace("]]>", ""));
            IEnumerable<XElement> FilmInfoList = Film.Descendants("FilmInfomation");
            foreach (var film in FilmInfoList)
            {
                YKFilmInfomation ykFilm = JSONHelper.DeserializeObject<YKFilmInfomation>(film);
                FilmInfoEntity entity = _filmInfoService.GetFilmInfoByCode(ykFilm.FilmInfomation.ID);
                if(entity==null)
                {
                    entity = new FilmInfoEntity();
                    entity.MapFrom(ykFilm.FilmInfomation);
                    _filmInfoService.Insert(entity);
                }
            }
            //重新读权限，回到首页
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "FilmInfo").SingleOrDefault();
            CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View(nameof(Index));
        }
    }
}