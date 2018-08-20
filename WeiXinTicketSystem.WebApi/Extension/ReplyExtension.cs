using WeiXinTicketSystem.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetSaleSvc.Api.Models;

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

        public static bool RequestInfoGuard(this Models.QuerySnacksReply reply, string Username, string Password, string CinemaCode, string TypeCode, string CurrentPage, string PageSize)
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
            if (string.IsNullOrEmpty(TypeCode))
            {
                reply.SetNecessaryParamMissReply(nameof(TypeCode));
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

        public static bool RequestInfoGuard(this Models.QuerySnacksReply reply, string Username, string Password, string CinemaCode, string TypeCode)
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
            if (string.IsNullOrEmpty(TypeCode))
            {
                reply.SetNecessaryParamMissReply(nameof(TypeCode));
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this Models.BookSnacksReply reply, string UserName, string Password, string CinemaCode, string MobilePhone, string DeliveryAddress, string SendTime, string OpenID, List<BookSnacksQueryJsonSnack> Snacks)
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
        public static bool RequestInfoGuard(this Models.ReleaseSnacksReply reply, string UserName, string Password, string CinemaCode, string OrderCode)
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
        public static bool RequestInfoGuard(this PaySnackOrderReply reply, string UserName, string Password,string CinemaCode,string OrderCode,string OrderPayType,string OrderPayTime,string OrderTradeNo,string IsUseConpons,string ConponCode,string OpenID)
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
            //if (string.IsNullOrEmpty(ConponCode))
            //{
            //    reply.SetNecessaryParamMissReply(nameof(ConponCode));
            //    return false;
            //}
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
            return true;
        }
        public static bool RequestInfoGuard(this Models.SubmitSnacksReply reply, string Username, string Password, string CinemaCode,string OrderCode,string OrderTradeNo,string MobilePhone,string OpenID, List<SubmitSnacksQueryJsonSnack> Snacks)
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
            if (string.IsNullOrEmpty(OrderCode))
            {
                reply.SetNecessaryParamMissReply(nameof(OrderCode));
                return false;
            }
            if (string.IsNullOrEmpty(OrderTradeNo))
            {
                reply.SetNecessaryParamMissReply(nameof(OrderTradeNo));
                return false;
            }
            if (string.IsNullOrEmpty(MobilePhone))
            {
                reply.SetNecessaryParamMissReply(nameof(MobilePhone));
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
        public static bool RequestInfoGuard(this FetchSnacksReply reply, string UserName, string Password, string CinemaCode, string OrderCode,string VoucherCode)
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
            if (string.IsNullOrEmpty(VoucherCode))
            {
                reply.SetNecessaryParamMissReply(nameof(VoucherCode));
                return false;
            }

            return true;
        }
        public static bool RequestInfoGuard(this RefundSnacksReply reply, string UserName, string Password, string CinemaCode, string OrderCode)
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
        public static bool RequestInfoGuard(this QueryBannersReply reply, string UserName, string Password, string CinemaCode)
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
            return true;
        }

        public static bool RequestInfoGuard(this QueryUserSnackOrdersReply reply, string Username, string Password, string CinemaCode, string OpenID, string CurrentPage, string PageSize)
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
        public static bool RequestInfoGuard(this QuerySnackOrderReply reply, string Username, string Password, string CinemaCode, string OrderCode)
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
            if (string.IsNullOrEmpty(OrderCode))
            {
                reply.SetNecessaryParamMissReply(nameof(OrderCode));
                return false;
            }
            return true;
        }
        public static bool RequestInfoGuard(this SendConponReply reply, string UserName, string Password, string CinemaCode, string GroupCode, string Number,string OpenID)
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
            if (string.IsNullOrEmpty(GroupCode))
            {
                reply.SetNecessaryParamMissReply(nameof(GroupCode));
                return false;
            }
            if (string.IsNullOrEmpty(Number))
            {
                reply.SetNecessaryParamMissReply(nameof(Number));
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

        public static bool RequestInfoGuard(this QueryActivityReply reply, string Username, string Password, string CinemaCode, string GradeCode)
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
            if (string.IsNullOrEmpty(GradeCode))
            {
                reply.SetNecessaryParamMissReply(nameof(GradeCode));
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this QueryActivitySequenceReply reply, string Username, string Password, string CinemaCode, string GradeCode,string ActivitySequence)
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
            if (string.IsNullOrEmpty(GradeCode))
            {
                reply.SetNecessaryParamMissReply(nameof(GradeCode));
                return false;
            }
            if (string.IsNullOrEmpty(ActivitySequence))
            {
                reply.SetNecessaryParamMissReply(nameof(ActivitySequence));
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this QueryConponsReply reply, string Username, string Password, string CinemaCode, string OpenID, string status, string CurrentPage, string PageSize)
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
            if (string.IsNullOrEmpty(status))
            {
                reply.SetNecessaryParamMissReply(nameof(status));
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
        public static bool RequestInfoGuard(this QueryGiftsReply reply, string Username, string Password, string CinemaCode,string Status, string CurrentPage, string PageSize)
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
            if (string.IsNullOrEmpty(Status))
            {
                reply.SetNecessaryParamMissReply(nameof(Status));
                return false;
            }
            if (string.IsNullOrEmpty(CurrentPage))
            {
                reply.SetNecessaryParamMissReply(nameof(CurrentPage));
                return false;
            }
            if (string.IsNullOrEmpty(PageSize))
            {
                reply.SetNecessaryParamMissReply(nameof(PageSize));
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

        public static bool RequestInfoGuard(this QuerySessionReply reply, string Username, string Password, string CinemaCode, string StartDate,string EndDate)
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
            if (string.IsNullOrEmpty(StartDate))
            {
                reply.SetNecessaryParamMissReply(nameof(StartDate));
                return false;
            }
            if (string.IsNullOrEmpty(EndDate))
            {
                reply.SetNecessaryParamMissReply(nameof(EndDate));
                return false;
            }
            return true;
        }
        public static bool RequestInfoGuard(this QuerySessionSeatReply reply, string Username, string Password, string CinemaCode, string SessionCode, string Status)
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
            if (string.IsNullOrEmpty(SessionCode))
            {
                reply.SetNecessaryParamMissReply(nameof(SessionCode));
                return false;
            }
            if (string.IsNullOrEmpty(Status))
            {
                reply.SetNecessaryParamMissReply(nameof(Status));
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this QueryMembersReply reply, string Username, string Password, string CinemaCode, string OpenID)
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
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this RegisterMemberReply reply, string UserName, string Password, string CinemaCode, string OpenID, string CardNo, string CardPassword, string Balance, string Score,string MemberGrade)
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
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
            if (string.IsNullOrEmpty(CardNo))
            {
                reply.SetNecessaryParamMissReply(nameof(CardNo));
                return false;
            }
            if (string.IsNullOrEmpty(CardPassword))
            {
                reply.SetNecessaryParamMissReply(nameof(CardPassword));
                return false;
            }
            if (string.IsNullOrEmpty(Balance))
            {
                reply.SetNecessaryParamMissReply(nameof(Balance));
                return false;
            }
            if (string.IsNullOrEmpty(Score))
            {
                reply.SetNecessaryParamMissReply(nameof(Score));
                return false;
            }
            if (string.IsNullOrEmpty(MemberGrade))
            {
                reply.SetNecessaryParamMissReply(nameof(MemberGrade));
                return false;
            }
            return true;
        }


        public static bool RequestInfoGuard(this SubmitFilmCommentReply reply, string UserName, string Password, string FilmCode, string FilmName, string Score, string CommentContent, string OpenID)
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
            if (string.IsNullOrEmpty(FilmCode))
            {
                reply.SetNecessaryParamMissReply(nameof(FilmCode));
                return false;
            }
            if (string.IsNullOrEmpty(FilmName))
            {
                reply.SetNecessaryParamMissReply(nameof(FilmName));
                return false;
            }
            if (string.IsNullOrEmpty(Score))
            {
                reply.SetNecessaryParamMissReply(nameof(Score));
                return false;
            }
            if (string.IsNullOrEmpty(CommentContent))
            {
                reply.SetNecessaryParamMissReply(nameof(CommentContent));
                return false;
            }
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
           
            return true;
        }

        public static bool RequestInfoGuard(this QueryFilmCommentsReply reply, string Username, string Password, string CinemaCode, string FilmCode, string CurrentPage, string PageSize)
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
            if (string.IsNullOrEmpty(FilmCode))
            {
                reply.SetNecessaryParamMissReply(nameof(FilmCode));
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

        public static bool RequestInfoGuard(this QueryCinemasReply reply, string Username, string Password, string AppId, string CurrentPage, string PageSize)
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
            if (string.IsNullOrEmpty(AppId))
            {
                reply.SetNecessaryParamMissReply(nameof(AppId));
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

        public static bool RequestInfoGuard(this SignInReply reply, string UserName, string Password, string CinemaCode, string OpenID, string Type, string Score, string Description, string Direction)
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
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
            if (string.IsNullOrEmpty(Type))
            {
                reply.SetNecessaryParamMissReply(nameof(Type));
                return false;
            }
            if (string.IsNullOrEmpty(Score))
            {
                reply.SetNecessaryParamMissReply(nameof(Score));
                return false;
            }
            if (string.IsNullOrEmpty(Description))
            {
                reply.SetNecessaryParamMissReply(nameof(Description));
                return false;
            }
            if (string.IsNullOrEmpty(Direction))
            {
                reply.SetNecessaryParamMissReply(nameof(Direction));
                return false;
            }

            return true;
        }

        public static bool RequestInfoGuard(this CollectStampReply reply, string UserName, string Password, string CinemaCode, string OpenID, string StampCode, string CollectType, string Status)
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
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
            if (string.IsNullOrEmpty(StampCode))
            {
                reply.SetNecessaryParamMissReply(nameof(StampCode));
                return false;
            }
            if (string.IsNullOrEmpty(CollectType))
            {
                reply.SetNecessaryParamMissReply(nameof(CollectType));
                return false;
            }
            if (string.IsNullOrEmpty(Status))
            {
                reply.SetNecessaryParamMissReply(nameof(Status));
                return false;
            }

            return true;
        }

        public static bool RequestInfoGuard(this QueryUserStampsReply reply, string Username, string Password, string CinemaCode, string OpenID)
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
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this QueryTicketUserReply reply, string Username, string Password, string CinemaCode, string OpenID)
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
            if (string.IsNullOrEmpty(OpenID))
            {
                reply.SetNecessaryParamMissReply(nameof(OpenID));
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this TicketUserLoginReply reply, string Username, string Password,string CinemaCode, string Code, string EncryptedData, string Iv)
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
            if (string.IsNullOrEmpty(Code))
            {
                reply.SetNecessaryParamMissReply(nameof(Code));
                return false;
            }
            if (string.IsNullOrEmpty(EncryptedData))
            {
                reply.SetNecessaryParamMissReply(nameof(EncryptedData));
                return false;
            }
            if (string.IsNullOrEmpty(Iv))
            {
                reply.SetNecessaryParamMissReply(nameof(Iv));
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this QueryMobilePhoneReply reply, string Username, string Password, string CinemaCode, string Code, string EncryptedData, string Iv)
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
            if (string.IsNullOrEmpty(Code))
            {
                reply.SetNecessaryParamMissReply(nameof(Code));
                return false;
            }
            if (string.IsNullOrEmpty(EncryptedData))
            {
                reply.SetNecessaryParamMissReply(nameof(EncryptedData));
                return false;
            }
            if (string.IsNullOrEmpty(Iv))
            {
                reply.SetNecessaryParamMissReply(nameof(Iv));
                return false;
            }
            return true;
        }

        public static bool RequestInfoGuard(this QueryGivingConditionsReply reply, string Username, string Password, string CinemaCode)
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
            
            return true;
        }


        public static bool RequestInfoGuard(this QueryOrdersReply reply, string Username, string Password, string CinemaCode, string OpenID, string startDate, string endDate, string CurrentPage, string PageSize)
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
            if (string.IsNullOrEmpty(startDate))
            {
                reply.SetNecessaryParamMissReply(nameof(startDate));
                return false;
            }
            if (string.IsNullOrEmpty(endDate))
            {
                reply.SetNecessaryParamMissReply(nameof(endDate));
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

    }
}
