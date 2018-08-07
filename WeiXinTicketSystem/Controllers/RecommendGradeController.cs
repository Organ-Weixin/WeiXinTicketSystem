using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.RecommendGrade;
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
using WeiXinTicketSystem.Properties;

namespace WeiXinTicketSystem.Controllers
{
    public class RecommendGradeController : RootExraController
    {
        private RecommendGradeService _recommendGradeService;
        private CinemaService _cinemaService;

        #region ctor
        public RecommendGradeController()
        {
            _recommendGradeService = new RecommendGradeService();
            _cinemaService = new CinemaService();
        }
        #endregion


        /// <summary>
        /// 推荐等级管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "RecommendGrade").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        /// <summary>
        /// 推荐等级列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<DynatablePageQueryModel> pageModel)
        {
            var recommendGrade = await _recommendGradeService.GetRecommendGradePagedAsync(
                 pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage);

            return DynatableResult(recommendGrade.ToDynatableModel(
                recommendGrade.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加推荐等级
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            CreateOrUpdateRecommendGradeViewModel model = new CreateOrUpdateRecommendGradeViewModel();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改推荐等级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var recommendGrade = await _recommendGradeService.GetRecommendGradeByIdAsync(id);
            if (recommendGrade == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateRecommendGradeViewModel model = new CreateOrUpdateRecommendGradeViewModel();
            model.MapFrom(recommendGrade);
            return CreateOrUpdate(model);
        }


        /// <summary>
        /// 添加或修改推荐等级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateRecommendGradeViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改推荐等级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateRecommendGradeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            RecommendGradeEntity recommendGrade = new RecommendGradeEntity();
            if (model.Id > 0)
            {
                recommendGrade = await _recommendGradeService.GetRecommendGradeByIdAsync(model.Id);
            }

            recommendGrade.MapFrom(model);

            if (recommendGrade.Id == 0)
            {
                recommendGrade.IsDel = false;
                await _recommendGradeService.InsertAsync(recommendGrade);
            }
            else
            {
                await _recommendGradeService.UpdateAsync(recommendGrade);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除推荐等级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var recommendGrade = await _recommendGradeService.GetRecommendGradeByIdAsync(id);

            if (recommendGrade != null)
            {
                recommendGrade.IsDel = true;
                await _recommendGradeService.UpdateAsync(recommendGrade);
            }
            return Object();
        }


        ///// <summary>
        ///// 绑定下拉框
        ///// </summary>
        ///// <returns></returns>
        //private async Task PreparyCreateOrEditViewData()
        //{
        //    if (CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE)
        //    {
        //        List<CinemaEntity> cinemas = new List<CinemaEntity>();
        //        cinemas.AddRange(await _cinemaService.GetAllCinemasAsync());
        //        ViewBag.CinemaCode_dd = cinemas.Select(x => new SelectListItem { Text = x.Name, Value = x.Code });
        //    }
        //    else
        //    {
        //        ViewBag.CinemaCode_dd = new List<SelectListItem>
        //        {
        //            new SelectListItem { Text = CurrentUser.CinemaName, Value = CurrentUser.CinemaCode }
        //        };
        //    }

        //}

    }
}