using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Cinema;
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

namespace WeiXinTicketSystem.Controllers
{
    public class CinemaController : RootExraController
    {
        private CinemaService _cinemaService;


        #region ctor
        public CinemaController()
        {
            _cinemaService = new CinemaService();
        }
        #endregion


        /// <summary>
        /// 影院管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Cinema").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        /// <summary>
        /// 影院列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<DynatablePageQueryModel> pageModel)
        {
            var cinema = await _cinemaService.GetCinemasPagedAsync(CurrentUser.CinemaCode, CurrentUser.CinemaName, CinemaStatusEnum.On, pageModel.Query.Search,
                 pageModel.Offset,
                 pageModel.PerPage
                 );

            return DynatableResult(cinema.ToDynatableModel(
                cinema.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        /// <summary>
        /// 添加影院
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            CreateOrUpdateCinemaViewModel model = new CreateOrUpdateCinemaViewModel();
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 修改影院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(int id)
        {
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);
            if (cinema == null)
            {
                return HttpBadRequest();
            }
            CreateOrUpdateCinemaViewModel model = new CreateOrUpdateCinemaViewModel();
            model.MapFrom(cinema);
            await PreparyCreateOrEditViewData();
            return CreateOrUpdate(model);
        }

        /// <summary>
        /// 添加或修改影院
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateOrUpdate(CreateOrUpdateCinemaViewModel model)
        {
            return View(nameof(CreateOrUpdate), model);
        }

        /// <summary>
        /// 添加或修改影院
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> _CreateOrUpdate(CreateOrUpdateCinemaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return ErrorObject(string.Join("/n", errorMessages));
            }

            CinemaEntity cinema = new CinemaEntity();
            if (model.Id > 0)
            {
                cinema = await _cinemaService.GetCinemaByIdAsync(model.Id);
            }

            cinema.MapFrom(model);

            if (cinema.Id == 0)
            {
                cinema.Created = DateTime.Now;
                await _cinemaService.InsertAsync(cinema);
            }
            else
            {
                cinema.Updated = DateTime.Now;
                await _cinemaService.UpdateAsync(cinema);
            }

            return RedirectObject(Url.Action(nameof(Index)));
        }

        /// <summary>
        /// 删除影院
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);

            if (cinema != null)
            {
                cinema.IsDel = true;
                cinema.Updated = DateTime.Now;
                await _cinemaService.UpdateAsync(cinema);
            }
            return Object();
        }

        private async Task PreparyCreateOrEditViewData()
        {
            //绑定售票系统枚举
            List<SelectListItem> cinemaTypeList = new List<SelectListItem>();
            Type enumType = typeof(CinemaTypeEnum); // 获取类型对象  
            FieldInfo[] enumFields = enumType.GetFields();    //获取字段信息对象集合  
                                                              //遍历集合  
            foreach (FieldInfo field in enumFields)
            {
                if (!field.IsSpecialName)
                {
                    
                    //row[1] = (int)Enum.Parse(enumType, field.Name); 也可以这样  
                    cinemaTypeList.Add(new SelectListItem { Text=field.Name,Value=field.GetRawConstantValue().ToString() });
                }
            }

            ViewBag.TicketSystem_dd = cinemaTypeList;

            //绑定所属院线枚举
            List<SelectListItem> theaterChainList = new List<SelectListItem>();
            Type enumType1 = typeof(TheaterChainEnum); // 获取类型对象  
            FieldInfo[] enumFields1 = enumType1.GetFields();    //获取字段信息对象集合  
                                                              //遍历集合  
            foreach (FieldInfo field1 in enumFields1)
            {
                if (!field1.IsSpecialName)
                {

                    //row[1] = (int)Enum.Parse(enumType, field.Name); 也可以这样  
                    theaterChainList.Add(new SelectListItem { Text = field1.Name, Value = field1.GetRawConstantValue().ToString() });
                }
            }

            ViewBag.TheaterChain_dd = theaterChainList;

            //绑定状态(0-未开通，1-已开通)枚举
            List<SelectListItem> StatusList = new List<SelectListItem>();
            Type enumType2 = typeof(CinemaStatusEnum); // 获取类型对象  
            FieldInfo[] enumFields2 = enumType2.GetFields();    //获取字段信息对象集合  
                                                                //遍历集合  
            foreach (FieldInfo field2 in enumFields2)
            {
                if (!field2.IsSpecialName)
                {

                    //row[1] = (int)Enum.Parse(enumType, field.Name); 也可以这样  
                    StatusList.Add(new SelectListItem { Text = field2.Name, Value = field2.GetRawConstantValue().ToString() });
                }
            }

            ViewBag.Status_dd = StatusList;

        }
    }
}