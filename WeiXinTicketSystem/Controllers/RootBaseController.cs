using WeiXinTicketSystem.Models;
using WeiXinTicketSystem.Utils;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.Mvc;
using System.IO;

namespace WeiXinTicketSystem.Controllers
{
    /// <summary>
    /// 所有控制器（包括登陆控制器）的基类
    /// </summary>
    public class RootBaseController : Controller
    {
        #region Error Log

        protected void LogError(string message)
        {
            LogHelper.LogMessage(message);
        }

        protected void LogError(string format, params string[] args)
        {
            LogHelper.LogMessage(format, args);
        }

        protected void LogError(Exception ex, string format, params string[] args)
        {
            LogHelper.LogException(ex, format, args);
        }

        protected void LogError(Exception ex, string contextualMessage = null)
        {
            LogHelper.LogException(ex, contextualMessage);
        }

        #endregion

        #region Helper ActionResult

        public virtual ActionResult HttpBadRequest(string description = default(string))
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, description);
        }

        /// <summary>
        /// 无数据成功返回
        /// </summary>
        /// <returns></returns>
        protected ActionResult Object()
        {
            return Object(data: null);
        }

        /// <summary>
        /// 成功时的返回
        /// </summary>
        /// <param name="data">返回的数据, 单个： {"category": {"id": 1}} 列表: {"categories": [{"id": 1}]} </param>
        /// <returns></returns>
        protected ActionResult Object(object data)
        {
            return Object(code: 0, message: "success", data: data);
        }

        /// <summary>
        /// 找不到对象(⊙﹏⊙)
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="data">可选的错误数据</param>
        /// <returns></returns>
        protected ActionResult ObjectNotFound(string message = "found nothing", object data = null)
        {
            return Object(code: (int)HttpStatusCode.NotFound, message: message, data: data);
        }

        /// <summary>
        /// 失败时的返回
        /// </summary>
        /// <param name="code">详见 xTripEnum.ApiCode</param>
        /// <param name="message">错误消息</param>
        /// <param name="data">可选的错误数据 {"title": "标题不能为空"}</param>
        /// <returns></returns>
        protected ActionResult Object(int code, string message, object data = null)
        {
            return Json(new { code, message, data }, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult RedirectObject(string url)
        {
            return Object(new { url });
        }

        protected ActionResult ErrorObject(string userFriendlyMessage = "发生错误", int code = 1001)
        {
            return Object(code, userFriendlyMessage);
        }

        protected ActionResult ModalErrorView(string userFriendlymessage = null)
        {
            return ErrorView(userFriendlymessage: userFriendlymessage, viewName: "ModalError");
        }

        protected ActionResult ErrorView(string userFriendlymessage = "发生错误", string viewName = "Error")
        {
            ViewBag.Error = userFriendlymessage;

            return View(viewName);
        }

        protected ActionResult DynatableResult(DynatableResultModel model)
        {
            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        protected FileResult ExcelResult(FileStream fs,string fileName)
        {
            return File(fs, "application/vnd.ms-excel", fileName);
        }
        #endregion
    }
}