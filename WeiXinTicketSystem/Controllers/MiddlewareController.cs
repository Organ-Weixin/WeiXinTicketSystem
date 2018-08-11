using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Middleware;
using WeiXinTicketSystem.Models.User;
using WeiXinTicketSystem.Properties;
using WeiXinTicketSystem.Utils;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Controllers
{
    public class MiddlewareController : RootExraController
    {
        private MiddlewareService _middlewareService;

        #region ctor
        public MiddlewareController()
        {
            _middlewareService = new MiddlewareService();
        }
        #endregion
        /// <summary>
        /// 中间件首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> List(DynatablePageModel<MiddlewareQueryModel> pageModel)
        {
            var Middlewares = await _middlewareService.GetMiddlewaresPagedAsync(
                pageModel.Query.Title,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(Middlewares.ToDynatableModel(Middlewares.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 新增中间件
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            CreateOrUpdateMiddlewareViewModel model = new CreateOrUpdateMiddlewareViewModel();

            PreparyCreateOrEditViewData();

            return View(nameof(CreateOrUpdate), model);
        }
        /// <summary>
        /// 编辑中件间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var middleware = await _middlewareService.GetMiddlewareByIdAsync(id);
            if (middleware == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateMiddlewareViewModel model = new CreateOrUpdateMiddlewareViewModel();
            model.MapFrom(middleware);
            PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 删除中间件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var middleware = await _middlewareService.GetMiddlewareByIdAsync(id);
            if (middleware != null && middleware.Id > 0)
            {
                middleware.IsDel = 1;
                await _middlewareService.UpdateAsync(middleware);
            }
            return Object();
        }

        /// <summary>
        /// 添加或修改中间件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateMiddlewareViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改中间件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateMiddlewareViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }
            MiddlewareEntity middleware = new MiddlewareEntity();
            if (model.Id > 0)
            {
                middleware = await _middlewareService.GetMiddlewareByIdAsync(model.Id);
            }
            middleware.MapFrom(model);
            if (middleware.Id == 0)
            {
                //判断是否已经存在
                var existedmiddleware = await _middlewareService.GettMiddlewareByUrlAsync(model.Url);
                if (existedmiddleware != null)
                {
                    return ErrorObject("中间件已存在！");
                }
                middleware.CinemaCode = "";
                middleware.CinemaCount = 0;
                middleware.IsDel = 0;
                await _middlewareService.InsertAsync(middleware);
            }
            else
            {
                await _middlewareService.UpdateAsync(middleware);
            }
            return RedirectObject(Url.Action(nameof(Index)));
        }

        private void PreparyCreateOrEditViewData()
        {
            ViewBag.Type_dd = EnumUtil.GetSelectList<CinemaTypeEnum>();
        }
    }
}