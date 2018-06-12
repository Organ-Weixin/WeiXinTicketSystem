using WeiXinTicketSystem.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.WebApi.Extension
{
    public static class ReplyExtension
    {
        /// <summary>
        /// 检查传入参数
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="CinemaCode"></param>
        /// <returns></returns>
        public static bool RequestInfoGuard(this QuerySnackTypesReply reply, string Username, string Password, string CinemaCode)
        {
            if (string.IsNullOrEmpty(Username))
            {
                reply.SetNecessaryParamMissReply(nameof(Username));
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                reply.SetNecessaryParamMissReply(nameof(Password));
                return false;
            }
            if (string.IsNullOrEmpty(CinemaCode))
            {
                reply.SetNecessaryParamMissReply(nameof(CinemaCode));
                return false;
            }

            return true;
        }

        public static bool RequestInfoGuard(this QuerySnacksReply reply, string Username, string Password, string CinemaCode, string TypeId, string CurrentPage, string PageSize)
        {
            int rint = 0;
            if (string.IsNullOrEmpty(Username))
            {
                reply.SetNecessaryParamMissReply(nameof(Username));
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                reply.SetNecessaryParamMissReply(nameof(Password));
                return false;
            }
            if (string.IsNullOrEmpty(CinemaCode))
            {
                reply.SetNecessaryParamMissReply(nameof(CinemaCode));
                return false;
            }
            if (string.IsNullOrEmpty(TypeId))
            {
                reply.SetNecessaryParamMissReply(nameof(TypeId));
                return false;
            }
            if (!int.TryParse(TypeId, out rint))
            {
                reply.SetSnackTypeInvalidReply();
                return false;
            }
            if (string.IsNullOrEmpty(CurrentPage))
            {
                reply.SetNecessaryParamMissReply(nameof(CurrentPage));
                return false;
            }
            if (!int.TryParse(CurrentPage, out rint))
            {
                reply.SetCurrentPageInvalidReply();
                return false;
            }
            if (string.IsNullOrEmpty(PageSize))
            {
                reply.SetNecessaryParamMissReply(nameof(PageSize));
                return false;
            }
            if (!int.TryParse(PageSize, out rint))
            {
                reply.SetPageSizeInvalidReply();
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this BookSnacksReply reply, string UserName, string Password, string CinemaCode, string MobilePhone, string DeliveryAddress, string SendTime, string OpenID, List<BookSnacksQueryJsonSnack> Snacks)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                reply.SetNecessaryParamMissReply(nameof(UserName));
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                reply.SetNecessaryParamMissReply(nameof(Password));
                return false;
            }
            if (string.IsNullOrEmpty(CinemaCode))
            {
                reply.SetNecessaryParamMissReply(nameof(CinemaCode));
                return false;
            }
            if (string.IsNullOrEmpty(MobilePhone))
            {
                reply.SetNecessaryParamMissReply(nameof(MobilePhone));
                return false;
            }
            if (string.IsNullOrEmpty(DeliveryAddress))
            {
                reply.SetNecessaryParamMissReply(nameof(DeliveryAddress));
                return false;
            }
            if (string.IsNullOrEmpty(SendTime))
            {
                reply.SetNecessaryParamMissReply(nameof(SendTime));
                return false;
            }
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
            if (Snacks == null || Snacks.Count <= 0)
            {
                reply.SetNecessaryParamMissReply(nameof(Snacks));
                return false;
            }
            return true;
        }
        public static bool RequestInfoGuard(this ReleaseSnacksReply reply, string UserName, string Password, string CinemaCode, string OrderCode)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                reply.SetNecessaryParamMissReply(nameof(UserName));
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                reply.SetNecessaryParamMissReply(nameof(Password));
                return false;
            }
            if (string.IsNullOrEmpty(CinemaCode))
            {
                reply.SetNecessaryParamMissReply(nameof(CinemaCode));
                return false;
            }
            if (string.IsNullOrEmpty(OrderCode))
            {
                reply.SetNecessaryParamMissReply(nameof(OrderCode));
                return false;
            }

            return true;
        }
        public static bool RequestInfoGuard(this PayOrderReply reply, string UserName, string Password,string CinemaCode,string OrderCode,string OrderPayType,string OrderPayTime,string OrderTradeNo,string IsUseConpons,string ConponCode,string OpenID)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                reply.SetNecessaryParamMissReply(nameof(UserName));
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                reply.SetNecessaryParamMissReply(nameof(Password));
                return false;
            }
            if (string.IsNullOrEmpty(CinemaCode))
            {
                reply.SetNecessaryParamMissReply(nameof(CinemaCode));
                return false;
            }
            if (string.IsNullOrEmpty(OrderCode))
            {
                reply.SetNecessaryParamMissReply(nameof(OrderCode));
                return false;
            }
            if (string.IsNullOrEmpty(OrderPayType))
            {
                reply.SetNecessaryParamMissReply(nameof(OrderPayType));
                return false;
            }
            if (string.IsNullOrEmpty(OrderPayTime))
            {
                reply.SetNecessaryParamMissReply(nameof(OrderPayTime));
                return false;
            }
            if (string.IsNullOrEmpty(OrderTradeNo))
            {
                reply.SetNecessaryParamMissReply(nameof(OrderTradeNo));
                return false;
            }
            if (string.IsNullOrEmpty(IsUseConpons))
            {
                reply.SetNecessaryParamMissReply(nameof(IsUseConpons));
                return false;
            }
            if (string.IsNullOrEmpty(ConponCode))
            {
                reply.SetNecessaryParamMissReply(nameof(ConponCode));
                return false;
            }
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查传入参数
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="CinemaCode"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static bool RequestInfoGuard(this QueryActivityReply reply, string Username, string Password, string CinemaCode, string CurrentPage, string PageSize)
        {
            int rint = 0;
            if (string.IsNullOrEmpty(Username))
            {
                reply.SetNecessaryParamMissReply(nameof(Username));
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                reply.SetNecessaryParamMissReply(nameof(Password));
                return false;
            }
            if (string.IsNullOrEmpty(CinemaCode))
            {
                reply.SetNecessaryParamMissReply(nameof(CinemaCode));
                return false;
            }
            if (string.IsNullOrEmpty(CurrentPage))
            {
                reply.SetNecessaryParamMissReply(nameof(CurrentPage));
                return false;
            }
            if (!int.TryParse(CurrentPage, out rint))
            {
                reply.SetCurrentPageInvalidReply();
                return false;
            }
            if (string.IsNullOrEmpty(PageSize))
            {
                reply.SetNecessaryParamMissReply(nameof(PageSize));
                return false;
            }
            if (!int.TryParse(PageSize, out rint))
            {
                reply.SetPageSizeInvalidReply();
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this QueryConponsReply reply, string Username, string Password, string CinemaCode, string OpenID, string statusID, string CurrentPage, string PageSize)
        {
            int rint = 0;
            if (string.IsNullOrEmpty(Username))
            {
                reply.SetNecessaryParamMissReply(nameof(Username));
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                reply.SetNecessaryParamMissReply(nameof(Password));
                return false;
            }
            if (string.IsNullOrEmpty(CinemaCode))
            {
                reply.SetNecessaryParamMissReply(nameof(CinemaCode));
                return false;
            }
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
            if (string.IsNullOrEmpty(statusID))
            {
                reply.SetNecessaryParamMissReply(nameof(statusID));
                return false;
            }
            if (string.IsNullOrEmpty(CurrentPage))
            {
                reply.SetNecessaryParamMissReply(nameof(CurrentPage));
                return false;
            }
            if (!int.TryParse(CurrentPage, out rint))
            {
                reply.SetCurrentPageInvalidReply();
                return false;
            }
            if (string.IsNullOrEmpty(PageSize))
            {
                reply.SetNecessaryParamMissReply(nameof(PageSize));
                return false;
            }
            if (!int.TryParse(PageSize, out rint))
            {
                reply.SetPageSizeInvalidReply();
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this QueryFilmsReply reply, string Username, string Password, string CinemaCode,string FilmCode)
        {
            if (string.IsNullOrEmpty(Username))
            {
                reply.SetNecessaryParamMissReply(nameof(Username));
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                reply.SetNecessaryParamMissReply(nameof(Password));
                return false;
            }
            if (string.IsNullOrEmpty(CinemaCode))
            {
                reply.SetNecessaryParamMissReply(nameof(CinemaCode));
                return false;
            }
            if (string.IsNullOrEmpty(FilmCode))
            {
                reply.SetNecessaryParamMissReply(nameof(FilmCode));
                return false;
            }

            return true;
        }

        public static bool RequestInfoGuard(this QuerySessionsReply reply, string Username, string Password, string CinemaCode, string SessionTime)
        {
            if (string.IsNullOrEmpty(Username))
            {
                reply.SetNecessaryParamMissReply(nameof(Username));
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                reply.SetNecessaryParamMissReply(nameof(Password));
                return false;
            }
            if (string.IsNullOrEmpty(CinemaCode))
            {
                reply.SetNecessaryParamMissReply(nameof(CinemaCode));
                return false;
            }
            if (string.IsNullOrEmpty(SessionTime))
            {
                reply.SetNecessaryParamMissReply(nameof(SessionTime));
                return false;
            }
            return true;
        }

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this QueryCinemaListReply reply, string Username, string Password)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }

        //    return true;
        //}



        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="CinemaCode"></param>
        ///// <param name="ScreenCode"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this QuerySeatReply reply, string Username, 
        //    string Password, string CinemaCode, string ScreenCode)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(ScreenCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(ScreenCode));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="CinemaCode"></param>
        ///// <param name="StartDate"></param>
        ///// <param name="EndDate"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this QueryFilmReply reply, string Username, string Password,
        //    string CinemaCode, string StartDate, string EndDate)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(StartDate))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(StartDate));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(EndDate))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(EndDate));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="CinemaCode"></param>
        ///// <param name="StartDate"></param>
        ///// <param name="EndDate"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this QuerySessionReply reply, string Username, string Password,
        //    string CinemaCode, string StartDate, string EndDate)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(StartDate))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(StartDate));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(EndDate))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(EndDate));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="CinemaCode"></param>
        ///// <param name="SessionCode"></param>
        ///// <param name="Status"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this QuerySessionSeatReply reply, string Username, string Password,
        //    string CinemaCode, string SessionCode, string Status)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(SessionCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(SessionCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Status))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Status));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="QueryXml"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this LockSeatReply reply, string Username, string Password,
        //    string QueryXml)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(QueryXml))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(QueryXml));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="QueryXml"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this ReleaseSeatReply reply, string Username, string Password,
        //    string QueryXml)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(QueryXml))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(QueryXml));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="QueryXml"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this SubmitOrderReply reply, string Username, string Password,
        //    string QueryXml)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(QueryXml))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(QueryXml));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="CinemaCode"></param>
        ///// <param name="PrintNo"></param>
        ///// <param name="VerifyCode"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this QueryPrintReply reply, string Username, string Password,
        //    string CinemaCode, string PrintNo, string VerifyCode)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(PrintNo))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(PrintNo));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(VerifyCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(VerifyCode));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="CinemaCode"></param>
        ///// <param name="PrintNo"></param>
        ///// <param name="VerifyCode"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this RefundTicketReply reply, string Username, string Password,
        //    string CinemaCode, string PrintNo, string VerifyCode)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(PrintNo))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(PrintNo));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(VerifyCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(VerifyCode));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="CinemaCode"></param>
        ///// <param name="OrderCode"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this QueryOrderReply reply, string Username, string Password,
        //    string CinemaCode, string OrderCode)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(OrderCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(OrderCode));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="CinemaCode"></param>
        ///// <param name="PrintNo"></param>
        ///// <param name="VerifyCode"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this QueryTicketReply reply, string Username, string Password,
        //    string CinemaCode, string PrintNo, string VerifyCode)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(PrintNo))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(PrintNo));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(VerifyCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(VerifyCode));
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// 检查传入参数
        ///// </summary>
        ///// <param name="reply"></param>
        ///// <param name="Username"></param>
        ///// <param name="Password"></param>
        ///// <param name="CinemaCode"></param>
        ///// <param name="PrintNo"></param>
        ///// <param name="VerifyCode"></param>
        ///// <returns></returns>
        //public static bool RequestInfoGuard(this FetchTicketReply reply, string Username, string Password,
        //    string CinemaCode, string PrintNo, string VerifyCode)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(PrintNo))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(PrintNo));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(VerifyCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(VerifyCode));
        //        return false;
        //    }

        //    return true;
        //}







        //public static bool RequestInfoGuard(this SubmitSnacksReply reply, string Username, string Password,string QueryXml)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }

        //    if (string.IsNullOrEmpty(QueryXml))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(QueryXml));
        //        return false;
        //    }
        //    return true;
        //}

        //public static bool RequestInfoGuard(this QuerySnacksOrderReply reply, string Username, string Password, string CinemaCode, string ChannelOrderCode)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(ChannelOrderCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(ChannelOrderCode));
        //        return false;
        //    }

        //    return true;
        //}

        //public static bool RequestInfoGuard(this RevokeSnacksReply reply, string Username, string Password, string CinemaCode, string OrderCode)
        //{
        //    if (string.IsNullOrEmpty(Username))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Username));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Password))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(Password));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CinemaCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(CinemaCode));
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(OrderCode))
        //    {
        //        reply.SetNecessaryParamMissReply(nameof(OrderCode));
        //        return false;
        //    }

        //    return true;
        //}
    }
}
