using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.UserCinema;
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
    public class UserCinemaController : RootExraController
    {
        UserCinemaService _userCinemaService;
        CinemaService _cinemaService;
        #region ctor
        public UserCinemaController()
        {
            _userCinemaService = new UserCinemaService();
            _cinemaService = new CinemaService();
        }
        #endregion
        /// <summary>
        /// 渠道对接影院首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int UserId)
        {
            ViewBag.UserId = UserId;
            return View();
        }

        public async Task<ActionResult> List(DynatablePageModel<UserCinemaQueryModel> pageModel)
        {
            var UserCinemas = await _userCinemaService.GetUserCinemasPagedAsync(
                pageModel.Query.UserId,
                pageModel.Query.CinemaCode,
                pageModel.Query.CinemaName,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(UserCinemas.ToDynatableModel(UserCinemas.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 新增接入商影院
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create(int UserId)
        {
            CreateOrUpdateUserCinemaViewModel model = new CreateOrUpdateUserCinemaViewModel();

            model.UserId = UserId;

            await PreparyCreateOrEditViewData();

            return View(nameof(CreateOrUpdate), model);
        }
        /// <summary>
        /// 编辑接入商影院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var userCinema = await _userCinemaService.GetUserCinemaByIdAsync(id);
            if (userCinema == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateUserCinemaViewModel model = new CreateOrUpdateUserCinemaViewModel();
            model.MapFrom(userCinema);

            await PreparyCreateOrEditViewData();

            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 删除接入商影院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var userCinema = await _userCinemaService.GetUserCinemaByIdAsync(id);
            if (userCinema != null && userCinema.Id > 0)
            {
                await _userCinemaService.DeleteAsync(userCinema);
            }
            return Object();
        }

        /// <summary>
        /// 添加或修改接入商影院
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateUserCinemaViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改接入商影院
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateUserCinemaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }
            UserCinemaEntity userCinema = new UserCinemaEntity();
            if (model.Id > 0)
            {
                userCinema = await _userCinemaService.GetUserCinemaByIdAsync(model.Id);
            }
            userCinema.MapFrom(model);
            if (userCinema.Id == 0)
            {
                //判断是否已经存在
                var existedusercinema = await _userCinemaService.GetUserCinemaByUserIdAndCinemaCodeAsync(model.UserId,model.CinemaCode);
                if (existedusercinema != null)
                {
                    return ErrorObject("接入商影院已存在！");
                }
                await _userCinemaService.InsertAsync(userCinema);
            }
            else
            {
                await _userCinemaService.UpdateAsync(userCinema);
            }
            return RedirectObject(Url.Action(nameof(Index))+"?queries[UserId]=" +model.UserId+ "&UserId="+model.UserId);
        }

        private async Task PreparyCreateOrEditViewData()
        {
            var allCinemas = await _cinemaService.GetAllCinemasAsync();
            ViewBag.CinemaCode_dd = allCinemas.Select(x => new SelectListItem { Text = x.Name, Value = x.Code.ToString() });

            ViewBag.OpenSnacks_dd = EnumUtil.GetSelectList<SnackInterfaceEnum>();
        }
    }
}