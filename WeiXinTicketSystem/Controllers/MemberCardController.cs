using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.MemberCard;
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
using System.Web.UI.WebControls;

namespace WeiXinTicketSystem.Controllers
{
    public class MemberCardController : RootExraController
    {
        private MemberCardService _memberCardService;
        private TicketUsersService _ticketUsersService;
        private CinemaService _cinemaService;
        #region ctor
        public MemberCardController()
        {
            _memberCardService = new MemberCardService();
            _ticketUsersService = new TicketUsersService();
            _cinemaService = new CinemaService();
        }
        #endregion


        /// <summary>
        /// 会员卡管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "MemberCard").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }


        ///// <summary>
        ///// 会员卡列表
        ///// </summary>
        ///// <param name="pageModel"></param>
        ///// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<MemberCardQueryModel> pageModel)
        {
            var memberCard = await _memberCardService.GetMemberCardPagedAsync(
                 CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? pageModel.Query.CinemaCode : CurrentUser.CinemaCode,
                pageModel.Query.CardNo,
                pageModel.Query.Search,
                pageModel.Offset,
                pageModel.PerPage
            );
            return DynatableResult(memberCard.ToDynatableModel(memberCard.TotalCount, pageModel.Offset, x => x.ToDynatableItem()));
        }


        /// <summary>
        /// 添加会员卡
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateMemberCardViewModel model = new CreateOrUpdateMemberCardViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改会员卡
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var memberCard = await _memberCardService.GetMemberCardByIdAsync(id);
            if (memberCard == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateMemberCardViewModel model = new CreateOrUpdateMemberCardViewModel();
            model.MapFrom(memberCard);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改会员卡
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateMemberCardViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改会员卡
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateMemberCardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            MemberCardEntity memberCard = new MemberCardEntity();
            if (model.Id > 0)
            {
                memberCard = await _memberCardService.GetMemberCardByIdAsync(model.Id);
            }

            memberCard.MapFrom(model);

            if (memberCard.Id == 0)
            {
                memberCard.Created = DateTime.Now;
                await _memberCardService.InsertAsync(memberCard);
            }
            else
            {
                memberCard.Updated = DateTime.Now;
                await _memberCardService.UpdateAsync(memberCard);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }


        /// <summary>
        /// 删除会员卡
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var memberCard = await _memberCardService.GetMemberCardByIdAsync(id);

            if (memberCard != null)
            {
                await _memberCardService.DeleteAsync(memberCard);
            }
            return Object();
        }

        private async Task PreparyCreateOrEditViewData()
        {
            //绑定会员卡等级枚举
            ViewBag.MemberGrade_dd = EnumUtil.GetSelectList<MemberCardGradeEnum>();

            //绑定状态枚举
            ViewBag.Status_dd = EnumUtil.GetSelectList<MemberCardStatusEnum>();


            //绑定购票用户
            List<TicketUserEntity> ticketUsers = new List<TicketUserEntity>();
            ticketUsers.AddRange(await _ticketUsersService.GetAllTicketUserAsync());
            ViewBag.OpenID_dd = ticketUsers.Select(x => new SelectListItem { Text = x.NickName, Value = x.OpenID });

            //影院下拉
            if (CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE)
            {
                List<CinemaEntity> cinemas = new List<CinemaEntity>();
                cinemas.AddRange(await _cinemaService.GetAllCinemasAsync());
                ViewBag.CinemaCode_dd = cinemas.Select(x => new SelectListItem { Text = x.Name, Value = x.Code });
            }
            else
            {
                ViewBag.CinemaCode_dd = new List<SelectListItem>
                {
                    new SelectListItem { Text = CurrentUser.CinemaName, Value = CurrentUser.CinemaCode }
                };
            }

        }


    }
}