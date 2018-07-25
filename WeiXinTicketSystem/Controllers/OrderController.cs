using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Models.Order;
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
    public class OrderController : RootExraController
    {
        private OrderService _orderService;

        #region ctor
        public OrderController()
        {
            _orderService = new OrderService();
        }
        #endregion

        public ActionResult Index()
        {
            var menu = CurrentSystemMenu.Where(x => x.ModuleFlag == "Order").SingleOrDefault();
            List<int> CurrentPermissions = menu.Permissions.Split(',').Select(x => int.Parse(x)).ToList();
            PrepareIndexViewData();
            ViewBag.CurrentPermissions = CurrentPermissions;
            return View();
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public async Task<ActionResult> List(DynatablePageModel<OrderPageQueryModel> pageModel)
        {
            DateTime? startDate = null, endDate = null;
            if (!string.IsNullOrEmpty(pageModel.Query.OrderDateRange))
            {
                var dates = pageModel.Query.OrderDateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                startDate = DateTime.Parse(dates[0]);
                endDate = DateTime.Parse(dates[1]);
            }


            var orders = await _orderService.GetOrdersPagedAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? null : CurrentUser.CinemaCode,
                 pageModel.Offset,
                 pageModel.PerPage,
                 pageModel.Query.Search,
                 12,//小程序渠道12
                 pageModel.Query.OrderStatus,
                 startDate,
                 endDate);

            return DynatableResult(orders.ToDynatableModel(
                orders.TotalCount,
                pageModel.Offset,
                x => x.ToDynatableItem()));
        }

        public async Task<FileResult> ExportExcelDetail(int? ThirdUserId,int? OrderStatus,string OrderDateRange,string Search)
        {
            DateTime? startDate = null, endDate = null;
            if (!string.IsNullOrEmpty(OrderDateRange))
            {
                var dates = OrderDateRange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                startDate = DateTime.Parse(dates[0]);
                endDate = DateTime.Parse(dates[1]);
            }
            if(OrderStatus==null)
            {
                OrderStatus = 8;
            }
            var orders = await _orderService.GetOrdersAsync(
                CurrentUser.CinemaCode == Resources.DEFAULT_CINEMACODE ? null : CurrentUser.CinemaCode,
                 Search,
                 12,//小程序渠道12
                 (OrderStatusEnum)OrderStatus,
                 startDate,
                 endDate);

            string[] propertyName = new string[] { "订单ID", "影院编码", "排期编号", "影厅编号", "排期时间", "影片编码", "影片名称", "票数", "上报总金额", "总服务费", "总金额", "总情侣座差价","订单状态","服务费支付方式","客人支付总服务费", "手机号", "锁座时间", "自动解锁时间", "锁座订单号", "提交时间", "提交订单号", "取票码", "验证码", "订印状态", "订印时间", "退单时间","退单批次号", "创建时间", "更新时间", "是否删除", "错误信息", "订单流水号", "满天星支付类型","订单支付标志","订单支付类型","订单支付时间", "满天星取票密码", "会员卡交易流水号","订单交易流水号","会员卡卡号","购票用户编号", "影院名称" };
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

            FileStream fs = ExcelUtil.WriteFile(filePath,orders,propertyNameList);

            return ExcelResult(fs, fileName);
        }


        /// <summary>
        /// 准备订单管理首页下拉框数据
        /// </summary>
        /// <returns></returns>
        private void PrepareIndexViewData()
        {
            ViewBag.OrderStatus = EnumUtil.GetSelectList<OrderStatusEnum>(OrderStatusEnum.Complete);
        }
    }
}