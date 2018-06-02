using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.SnackOrder;
using WeiXinTicketSystem.Properties;
using WeiXinTicketSystem.Utils;
using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;
using System.IO;

namespace WeiXinTicketSystem.Controllers
{
    public class SnackOrderController : RootExraController
    {
        private SnackOrderService _snackOrderService;

        #region ctor
        public SnackOrderController()
        {
            _snackOrderService = new SnackOrderService();
        }
        #endregion

        public async Task<ActionResult> Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "SnackOrder").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            ViewBag.CurrentPermissions = CurrentPermissions;
            PrepareIndexViewData();
            return View();
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<SnacksOrderPageQueryModel> pageModel)
        {
            DateTime? startDate = null, endDate = null;
            if (!string.IsNullOrEmpty(pageModel.Query.OrderDateRange))
            {
                var dates = pageModel.Query.OrderDateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                startDate = DateTime.Parse(dates[0]);
                endDate = DateTime.Parse(dates[1]);
            }


            var orders = await _snackOrderService.GetOrdersPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? null : CurrentUser.CinemaCode,
                 pageModel.Offset,
                 pageModel.PerPage,
                 pageModel.Query.Search,
                 pageModel.Query.OrderStatus,
                 startDate,
                 endDate);

            return DynatableResult(orders.ToDynatableModel(
                orders.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        public async Task<FileResult> ExportExcelDetail(int? OrderStatus, string OrderDateRange, string Search)
        {
            DateTime? startDate = null, endDate = null;
            if (!string.IsNullOrEmpty(OrderDateRange))
            {
                var dates = OrderDateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                startDate = DateTime.Parse(dates[0]);
                endDate = DateTime.Parse(dates[1]);
            }
            if (OrderStatus == null)
            {
                OrderStatus = 5;
            }
            var orders = await _snackOrderService.GetOrdersAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? null : CurrentUser.CinemaCode,
                 Search,
                 (SnackOrderStatusEnum)OrderStatus,
                 startDate,
                 endDate);

            string[] propertyName = new string[] { "订单ID", "影院编码", "订单号", "手机号", "套餐数量", "总金额", "预定时间", "凭证号", "订单状态", "退订时间", "取货时间", "更新时间", "创建时间", "送货地址","送货时间","订单支付标志","订单支付类型","订单支付时间","支付交易号","是否使用优惠券","优惠券编号","影院名称"};
            //将参数写入到一个临时集合中
            List<string> propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);

            //创建Excel文件名称
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            string strPath = Server.MapPath("~/download/");
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            string filePath = strPath + fileName;

            FileStream fs = ExcelUtil.WriteFile(filePath, orders, propertyNameList);

            return ExcelResult(fs, fileName);
        }


        /// <summary>
        /// 准备订单管理首页下拉框数据
        /// </summary>
        /// <returns></returns>
        private void PrepareIndexViewData()
        {
            ViewBag.OrderStatus = EnumUtil.GetSelectList<SnackOrderStatusEnum>(SnackOrderStatusEnum.Complete);
        }
    }
}