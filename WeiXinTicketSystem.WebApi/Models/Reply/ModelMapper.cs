using System;
using System.Linq;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using WeiXinTicketSystem.Entity.Enum;
using NetSaleSvc.Api.Models;

namespace WeiXinTicketSystem.WebApi.Models
{
    public static class ModelMapper
    {
        /// <summary>
        /// map
        /// </summary>
        /// <param name="cinema"></param>
        /// <param name="userCinema"></param>
        /// <returns></returns>
        public static QuerySnackTypesReplyType MapFrom(this QuerySnackTypesReplyType type, SnackTypeEntity snacktype)
        {
            type.TypeId = snacktype.Id;
            type.CinemaCode = snacktype.CinemaCode;
            type.TypeName = snacktype.TypeName;
            type.Remark = snacktype.Remark;
            type.Image = snacktype.Image;
            return type;
        }

        public static QuerySnacksReplySnack MapFrom(this QuerySnacksReplySnack snack, SnackEntity entity)
        {
            snack.SnackId = entity.Id;
            snack.CinemaCode = entity.CinemaCode;
            snack.SnackCode = entity.SnackCode;
            snack.TypeId = entity.TypeId;
            snack.SnackName = entity.SnackName;
            snack.Remark = entity.Remark;
            snack.StandardPrice = entity.StandardPrice;
            snack.SalePrice = entity.SalePrice;
            snack.Status = entity.Status.GetDescription();
            snack.Stock = entity.Stock;
            snack.ExpDate = entity.ExpDate.ToFormatStringWithT();
            snack.IsRecommand = entity.IsRecommand.HasValue ? entity.IsRecommand.Value : false;
            snack.Image = entity.Image;
            return snack;
        }
        public static BookSnacksReplySnacks MapFrom(this BookSnacksReplySnacks data, SnackOrderViewEntity order)
        {
            data.CinemaCode = order.OrderBaseInfo.CinemaCode;
            data.OrderCode = order.OrderBaseInfo.OrderCode;
            data.MobilePhone = order.OrderBaseInfo.MobilePhone;
            data.SnacksCount = order.OrderBaseInfo.SnacksCount;
            data.TotalPrice = order.OrderBaseInfo.TotalPrice;
            data.OrderStatus = order.OrderBaseInfo.OrderStatus.GetDescription();
            data.DeliveryAddress = order.OrderBaseInfo.DeliveryAddress;
            data.SendTime = order.OrderBaseInfo.SendTime;
            data.Created = order.OrderBaseInfo.Created;
            data.AutoUnLockDateTime = order.OrderBaseInfo.AutoUnLockDateTime;

            data.Snacks = order.SnackOrderDetails.Select(
                x => new BookSnacksReplySnack()
                {
                    SnackCode = x.SnackCode,
                    StandardPrice = x.StandardPrice,
                    SalePrice = x.SalePrice,
                    Number = x.Number,
                    SubTotalPrice = x.SubTotalPrice
                }).ToList();
            return data;
        }
        public static PaySnackOrderReplyOrder MapFrom(this PaySnackOrderReplyOrder data, SnackOrderEntity entity)
        {
            data.CinemaCode = entity.CinemaCode;
            data.OrderCode = entity.OrderCode;
            data.OrderStatus = entity.OrderStatus.GetDescription();
            data.OrderPayFlag = entity.OrderPayFlag.HasValue ? entity.OrderPayFlag.Value : false;
            data.OrderTradeNo = entity.OrderTradeNo;
            data.OrderPayTime = entity.OrderPayTime.ToFormatStringWithT();
            return data;
        }

        public static SnackOrderViewEntity MapFrom(this SnackOrderViewEntity order, BookSnacksQueryJson Queryjson)
        {
            //订单基本信息
            SnackOrderEntity orderBaseInfo = new SnackOrderEntity();
            orderBaseInfo.CinemaCode = Queryjson.CinemaCode;
            orderBaseInfo.OrderCode = RandomHelper.InitializeOrderNumber();//平台生成的16位订单号
            orderBaseInfo.MobilePhone = Queryjson.MobilePhone;
            orderBaseInfo.SnacksCount = Queryjson.Snacks.Count();
            //orderBaseInfo.TotalPrice = Queryjson.Snacks.Sum(x => x.SalePrice);//不是这么算，还有份数
            orderBaseInfo.OrderStatus = SnackOrderStatusEnum.Booked;
            orderBaseInfo.Created = DateTime.Now;
            orderBaseInfo.AutoUnLockDateTime = DateTime.Now.AddMinutes(15);
            orderBaseInfo.DeliveryAddress = Queryjson.DeliveryAddress;
            orderBaseInfo.SendTime = Queryjson.SendTime;
            orderBaseInfo.OpenID = Queryjson.OpenID;
            order.OrderBaseInfo = orderBaseInfo;

            order.SnackOrderDetails = Queryjson.Snacks.Select(
                x => new SnackOrderDetailEntity()
                {
                    SnackCode = x.SnackCode,
                    StandardPrice = x.StandardPrice,
                    SalePrice = x.SalePrice,
                    Number = x.Number,
                    SubTotalPrice = x.SalePrice * x.Number,
                    Created = DateTime.Now,
                    Deleted = false
                }).ToList();

            order.OrderBaseInfo.TotalPrice = order.SnackOrderDetails.Sum(x => x.SubTotalPrice);//小计求和

            return order;
        }

        public static QueryBannersReplyBanner MapFrom(this QueryBannersReplyBanner banner, BannerEntity entity)
        {
            banner.BannerId = entity.Id;
            banner.CinemaCode = entity.CinemaCode;
            banner.Title = entity.Title;
            banner.Image = entity.Image;
            banner.Created = entity.Created.ToFormatStringWithT();
            banner.StartDate = entity.StartDate.ToFormatStringWithT();
            banner.EndDate = entity.EndDate.ToFormatStringWithT();
            banner.Status = entity.Status.GetDescription();
            return banner;
        }


        public static QueryActivityReplyActivity MapFrom(this QueryActivityReplyActivity activity, ActivityEntity entity)
        {
            activity.ActivityId = entity.Id;
            activity.CinemaCode = entity.CinemaCode;
            activity.Title = entity.Title;
            activity.Image = entity.Image;
            activity.ActivityContent = entity.ActivityContent;
            activity.StartDate = entity.StartDate.ToFormatStringWithT();
            activity.EndDate = entity.EndDate.ToFormatStringWithT();
            activity.Status = entity.Status.GetDescription();
            return activity;
        }
        public static QueryUserSnackOrdersReplyOrder MapFrom(this QueryUserSnackOrdersReplyOrder order, SnackOrderEntity entity)
        {
            order.OrderId = entity.Id;
            order.CinemaCode = entity.CinemaCode;
            order.OrderCode = entity.OrderCode;
            order.MobilePhone = entity.MobilePhone;
            order.SnacksCount = entity.SnacksCount;
            order.TotalPrice = entity.TotalPrice;
            order.OrderStatus = entity.OrderStatus.GetDescription();
            order.DeliveryAddress = entity.DeliveryAddress;
            order.SendTime = entity.SendTime;
            order.Created = entity.Created;
            return order;
        }
        public static QuerySnackOrderReplyOrder MapFrom(this QuerySnackOrderReplyOrder data, SnackOrderViewEntity order)
        {
            data.OrderId = order.OrderBaseInfo.Id;
            data.CinemaCode = order.OrderBaseInfo.CinemaCode;
            data.OrderCode = order.OrderBaseInfo.OrderCode;
            data.MobilePhone = order.OrderBaseInfo.MobilePhone;
            data.SnacksCount = order.OrderBaseInfo.SnacksCount;
            data.TotalPrice = order.OrderBaseInfo.TotalPrice;
            data.ReleaseTime = order.OrderBaseInfo.ReleaseTime.ToFormatStringWithT();
            data.SubmitTime = order.OrderBaseInfo.SubmitTime.ToFormatStringWithT();
            data.VoucherCode = order.OrderBaseInfo.VoucherCode;
            data.OrderStatus = order.OrderBaseInfo.OrderStatus.GetDescription();
            data.RefundTime = order.OrderBaseInfo.RefundTime.ToFormatStringWithT();
            data.FetchTime = order.OrderBaseInfo.FetchTime.ToFormatStringWithT();
            data.Created = order.OrderBaseInfo.Created.ToFormatStringWithT();
            data.DeliveryAddress = order.OrderBaseInfo.DeliveryAddress;
            data.SendTime = order.OrderBaseInfo.SendTime.ToFormatStringWithT();
            data.AutoUnLockDateTime = order.OrderBaseInfo.AutoUnLockDateTime.ToFormatStringWithT();
            data.OrderPayFlag = order.OrderBaseInfo.OrderPayFlag.HasValue ? order.OrderBaseInfo.OrderPayFlag.Value : false;
            data.OrderPayType = order.OrderBaseInfo.OrderPayType.HasValue ? order.OrderBaseInfo.OrderPayType.Value.GetDescription() : "";
            data.OrderPayTime = order.OrderBaseInfo.OrderPayTime.ToFormatStringWithT();
            data.OrderTradeNo = order.OrderBaseInfo.OrderTradeNo;
            data.IsUseConpons = order.OrderBaseInfo.IsUseConpons.HasValue ? order.OrderBaseInfo.IsUseConpons.Value : false;
            data.ConponCode = order.OrderBaseInfo.ConponCode;
            data.ConponPrice = order.OrderBaseInfo.ConponPrice.HasValue ? order.OrderBaseInfo.ConponPrice.Value : 0;
            data.OpenID = order.OrderBaseInfo.OpenID;
            data.Snacks = order.SnackOrderDetails.Select(
                x => new QuerySnackOrderReplySnack()
                {
                    SnackCode = x.SnackCode,
                    StandardPrice = x.StandardPrice,
                    SalePrice = x.SalePrice,
                    Number = x.Number,
                    SubTotalPrice = x.SubTotalPrice
                }).ToList();
            return data;
        }

        public static QueryConponsReplyConpon MapFrom(this QueryConponsReplyConpon conpon, ConponEntity entity)
        {
            conpon.ConponId = entity.Id;
            conpon.CinemaCode = entity.CinemaCode;
            conpon.ConponType = entity.ConponType.GetDescription();
            conpon.OpenID = entity.OpenID;
            conpon.Price = entity.Price;
            conpon.ConponCode = entity.ConponCode;
            conpon.ValidityDate = entity.ValidityDate.ToFormatStringWithT();
            conpon.Status = entity.Status.GetDescription();
            conpon.UseDate = entity.UseDate.ToFormatStringWithT();
            conpon.Title = entity.Title;
            conpon.Deleted = entity.Deleted;
            conpon.Image = entity.Image;
            return conpon;
        }
        public static ConponEntity MapFrom(this ConponEntity entity, SendConponQueryJson queryJson)
        {
            entity.ConponType = (ConponTypeEnum)queryJson.ConponType;
            entity.CinemaCode = queryJson.CinemaCode;
            entity.OpenID = queryJson.OpenID;
            entity.Price = queryJson.Price;
            entity.ConponCode = RandomHelper.CreateRandomCode();
            entity.ValidityDate = queryJson.ValidityDate;
            entity.Status = ConponStatusEnum.NotUsed;
            entity.Created = DateTime.Now;
            entity.Deleted = false;
            entity.Title = queryJson.Title;
            entity.Image = queryJson.Image;
            return entity;
        }
        public static SendConponReplyConpon MapFrom(this SendConponReplyConpon conpon, ConponEntity entity)
        {
            conpon.CinemaCode = entity.CinemaCode;
            conpon.Title = entity.Title;
            conpon.ConponType = (int)entity.ConponType;
            conpon.ConponCode = entity.ConponCode;
            conpon.Price = entity.Price.HasValue ? entity.Price.Value : 0;
            conpon.ValidityDate = entity.ValidityDate.ToFormatStringWithT();
            conpon.Image = entity.Image;
            conpon.SendTime = entity.Created.ToFormatStringWithT();
            conpon.OpenID = entity.OpenID;
            return conpon;
        }

        public static QuerySessionsReplySession MapFrom(this QuerySessionsReplySession session, SessionInfoEntity entity)
        {
            session.SessionId = entity.Id;
            session.CinemaCode = entity.CinemaCode;
            session.SessionCode = entity.SessionCode;
            session.ScreenCode = entity.ScreenCode;
            session.StartTime = entity.StartTime.ToFormatStringWithT();
            session.FilmCode = entity.FilmCode;
            session.FilmName = entity.FilmName;
            session.Duration = entity.Duration;
            session.Language = entity.Language;
            session.UpdateTime = entity.UpdateTime.ToFormatStringWithT();
            session.StandardPrice = entity.StandardPrice;
            session.LowestPrice = entity.LowestPrice;
            session.SettlePrice = entity.SettlePrice;
            session.TicketFee = entity.TicketFee;
            session.IsAvalible = entity.IsAvalible == true ? "可用" : "不可用";
            session.Dimensional = entity.Dimensional;
            session.ListingPrice = entity.ListingPrice;
            session.SalePrice = entity.SalePrice.HasValue ? entity.SalePrice.Value : 0;
            return session;
        }

        public static QueryMembersReplyMember MapFrom(this QueryMembersReplyMember member, MemberCardEntity entity)
        {
            member.Id = entity.Id;
            member.CinemaCode = entity.CinemaCode;
            member.OpenID = entity.OpenID;
            member.CardNo = entity.CardNo;
            member.CardPassword = entity.CardPassword;
            member.Balance = entity.Balance;
            member.Score = entity.Score;
            member.MemberGrade = entity.MemberGrade.GetDescription();
            member.Status = entity.Status.GetDescription();
            member.Created = entity.Created;

            return member;
        }

        public static QueryGiftsReplyGift MapFrom(this QueryGiftsReplyGift gift, GiftEntity entity)
        {
            gift.GiftId = entity.Id;
            gift.CinemaCode = entity.CinemaCode;
            gift.Title = entity.Title;
            gift.Details = entity.Details;
            gift.OriginalPrice = entity.OriginalPrice.HasValue ? entity.OriginalPrice.Value : 0;
            gift.Price = entity.Price.HasValue ? entity.Price.Value : 0;
            gift.Image = entity.Image;
            gift.Stock = entity.Stock.HasValue ? entity.Stock.Value : 0;
            gift.StartDate = entity.StartDate.ToFormatStringWithT();
            gift.EndDate = entity.EndDate.ToFormatStringWithT();
            gift.Status = entity.Status.GetDescription();
            return gift;
        }

        public static MemberCardEntity MapFrom(this MemberCardEntity member, RegisterMemberQueryJson Queryjson)
        {
            member.CinemaCode = Queryjson.CinemaCode;
            member.OpenID = Queryjson.OpenID;
            member.CardNo = Queryjson.CardNo;
            member.CardPassword = Queryjson.CardPassword;
            member.Balance = Queryjson.Balance;
            member.Score = Queryjson.Score;
            member.MemberGrade = (MemberCardGradeEnum)Queryjson.MemberGrade;
            member.Status = MemberCardStatusEnum.Enable;
            member.Created = DateTime.Now;
            return member;
        }

        public static RegisterMemberReplyMember MapFrom(this RegisterMemberReplyMember data, MemberCardEntity member)
        {
            data.CinemaCode = member.CinemaCode;
            data.OpenID = member.OpenID;
            data.CardNo = member.CardNo;
            data.CardPassword = member.CardPassword;
            data.Balance = member.Balance;
            data.Score = member.Score;
            data.MemberGrade = member.MemberGrade.GetDescription();
            data.Status = member.Status.GetDescription();
            data.Created = member.Created;

            return data;
        }

        public static FilmCommentEntity MapFrom(this FilmCommentEntity comment, SubmitFilmCommentQueryJson Queryjson)
        {
            comment.FilmCode = Queryjson.FilmCode;
            comment.FilmName = Queryjson.FilmName;
            comment.Score = Queryjson.Score;
            comment.CommentContent = Queryjson.CommentContent;
            comment.OpenID = Queryjson.OpenID;
            comment.Created = DateTime.Now;
            return comment;
        }

        public static SubmitFilmCommentReplyComment MapFrom(this SubmitFilmCommentReplyComment data, FilmCommentEntity comment)
        {
            data.FilmCode = comment.FilmCode;
            data.FilmName = comment.FilmName;
            data.Score = comment.Score;
            data.CommentContent = comment.CommentContent;
            data.OpenID = comment.OpenID;
            data.Created = comment.Created;

            return data;
        }
        public static QueryFilmsReplyFilm MapFrom(this QueryFilmsReplyFilm data, FilmInfoEntity entity)
        {
            data.FilmId = entity.Id;
            data.FilmCode = entity.FilmCode;
            data.FilmName = entity.FilmName;
            data.Version = entity.Version;
            data.Duration = entity.Duration;
            data.PublishDate = entity.PublishDate.ToFormatStringWithT();
            data.Publisher = entity.Publisher;
            data.Producer = entity.Producer;
            data.Director = entity.Director;
            data.Cast = entity.Cast;
            data.Introduction = entity.Introduction;
            data.Score = entity.Score;
            data.Area = entity.Area;
            data.Type = entity.Type;
            data.Language = entity.Language;
            data.Status = entity.Status.GetDescription();
            data.Image = entity.Image;
            data.Trailer = entity.Trailer;
            return data;
        }
        public static QueryTicketReplyTickets MapFrom(this QueryTicketReplyTickets data,OrderViewEntity order,CinemaEntity cinema,ScreenInfoEntity screen)
        {
            data.TicketsCount = order.orderSeatDetails.Count;
            var Tickets = order.orderSeatDetails.Select(x => new QueryTicketReplyTicket
            {
                PrintNo = order.orderBaseInfo.PrintNo,
                TicketInfoCode=x.TicketInfoCode,
                CinemaCode= cinema.CinemaCode,
                CinemaName=cinema.CinemaName,
                ScreenCode=screen.ScreenCode,
                ScreenName=screen.ScreenName,
                FilmCode=order.orderBaseInfo.FilmCode,
                FilmName=order.orderBaseInfo.FilmName,
                SessionCode=order.orderBaseInfo.SessionCode,
                SessionDateTime= order.orderBaseInfo.SessionTime.ToFormatStringWithT() ?? string.Empty,
                TicketCode=x.FilmTicketCode,
                SeatCode=x.SeatCode,
                SeatName= $"{x.RowNum}排{x.ColumnNum}座",
                Price= x.Price.ToString("0.##"),
                Service = x.Fee.ToString("0.##"),
                PrintFlag = x.PrintFlag.GetValueOrDefault(false).ToString()
            }).ToList();
            data.Tickets = Tickets;
            return data;
        }

        public static QueryOrderReplyOrder MapFrom(this QueryOrderReplyOrder data,OrderViewEntity order,CinemaEntity cinema,ScreenInfoEntity screen,SessionInfoEntity session)
        {
            data.OrderCode = order.orderBaseInfo.SubmitOrderCode;
            data.CinemaCode = order.orderBaseInfo.CinemaCode;
            data.CinemaType = cinema.TicketSystem;
            data.CinemaName = cinema.CinemaName;
            data.ScreenCode = screen.ScreenCode;
            data.ScreenName = screen.ScreenName;
            data.SessionCode = order.orderBaseInfo.SessionCode;
            data.StartTime = order.orderBaseInfo.SessionTime.ToFormatStringWithT();
            data.PlaythroughFlag = session?.PlaythroughFlag ?? "No";
            data.PrintNo = order.orderBaseInfo.PrintNo;
            data.VerifyCode = order.orderBaseInfo.VerifyCode;

            QueryOrderReplyFilm film = new QueryOrderReplyFilm
            {
                Code = order.orderBaseInfo.FilmCode,
                Name = order.orderBaseInfo.FilmName,
                Duration = (session?.Duration ?? 0).ToString(),
                Sequence = (session?.Sequence ?? 1).ToString()
            };
            data.Film = film;

            var Seats = order.orderSeatDetails.Select(x => new QueryOrderReplySeat
            {
                SeatCode = x.SeatCode,
                RowNum = x.RowNum ?? string.Empty,
                ColumnNum = x.ColumnNum ?? string.Empty,
                FilmTicketCode = x.FilmTicketCode,
                PrintStatus = order.orderBaseInfo.PrintStatus.GetValueOrDefault(false) ? YesOrNoEnum.Yes : YesOrNoEnum.No,
                PrintTime = order.orderBaseInfo.PrintTime?.ToFormatStringWithT() ?? string.Empty,
                RefundStatus = order.orderBaseInfo.OrderStatus == OrderStatusEnum.Refund ? YesOrNoEnum.Yes : YesOrNoEnum.No,
                RefundTime = order.orderBaseInfo.RefundTime?.ToFormatStringWithT() ?? string.Empty
            }).ToList();
            data.Seats = Seats;
            return data;
        }
        public static QuerySessionSeatReplySeat MapFrom(this QuerySessionSeatReplySeat seat,NetSaleSvc.Api.Models.QuerySessionSeatReplySeat netSeat)
        {
            seat.SeatCode = netSeat.Code;
            seat.RowNum = netSeat.RowNum;
            seat.ColumnNum = netSeat.ColumnNum;
            seat.Status = netSeat.Status;
            return seat;
        }

        public static OrderViewEntity MapFrom(this OrderViewEntity order,CinemaEntity cinema,
            LockSeatQueryJson Queryjson, SessionInfoEntity sessionInfo)
        {
            //订单基本信息
            OrderEntity orderBaseInfo = new OrderEntity();
            orderBaseInfo.CinemaCode = Queryjson.CinemaCode;
            orderBaseInfo.SessionCode = sessionInfo.SessionCode;
            orderBaseInfo.ScreenCode = sessionInfo.ScreenCode;
            orderBaseInfo.SessionTime = sessionInfo.StartTime;
            orderBaseInfo.FilmCode = sessionInfo.FilmCode;
            orderBaseInfo.FilmName = sessionInfo.FilmName;
            orderBaseInfo.TicketCount = Queryjson.Order.SeatsCount;
            orderBaseInfo.TotalPrice = Queryjson.Order.Seats.Sum(x => x.Price);
            orderBaseInfo.TotalFee = Queryjson.Order.Seats.Sum(x => x.Fee);
            orderBaseInfo.OrderStatus = OrderStatusEnum.Created;
            orderBaseInfo.Created = DateTime.Now;
            orderBaseInfo.OpenID = Queryjson.OpenID;
            //orderBaseInfo.PayType = Queryjson.Order.PayType;//payType默认为0，1不更新到数据库，满天星系统除外，满天星系统的PayType取值为I0/N0等

            if (cinema.TicketSystem == CinemaTypeEnum.ManTianXing)
            {
                //数据库中会员及非会员支付类型以逗号分隔存于PayType字段中，会员在前
                if (Queryjson.Order.PayType == "1")
                {
                    orderBaseInfo.IsMemberPay = true;
                    orderBaseInfo.PayType = cinema.PayType.Split(',')?.First();
                }
                else
                {
                    orderBaseInfo.IsMemberPay = false;
                    orderBaseInfo.PayType = cinema.PayType.Split(',')?.Last();
                }
            }
            order.orderBaseInfo = orderBaseInfo;

            order.orderSeatDetails = Queryjson.Order.Seats.Select(
                x => new OrderSeatDetailEntity()
                {
                    SeatCode = x.SeatCode,
                    Price = x.Price,
                    Fee = x.Fee,
                    Created = DateTime.Now
                }).ToList();

            return order;
        }

        public static OrderViewEntity MapFrom(this OrderViewEntity order, SubmitOrderQueryJson QueryJson)
        {
            order.orderBaseInfo.TotalPrice = QueryJson.Order.Seats.Sum(x => x.Price);
            order.orderBaseInfo.TotalSalePrice = QueryJson.Order.Seats.Sum(x => x.RealPrice);
            order.orderBaseInfo.TotalFee = QueryJson.Order.Seats.Sum(x => x.Fee);
            order.orderBaseInfo.MobilePhone = QueryJson.Order.MobilePhone;
            if (order.orderBaseInfo.IsMemberPay)
            {
                order.orderBaseInfo.PaySeqNo = QueryJson.Order.PaySeqNo;
            }
            order.orderSeatDetails.ForEach(x =>
            {
                var newInfo = QueryJson.Order.Seats.Where(y => y.SeatCode == x.SeatCode).SingleOrDefault();
                if (newInfo != null)
                {
                    x.Price = newInfo.Price;
                    x.SalePrice = newInfo.RealPrice;
                    x.Fee = newInfo.Fee;
                }
            });

            return order;
        }
    }
}