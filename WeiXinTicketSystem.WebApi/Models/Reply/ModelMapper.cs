using System;
using System.Linq;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using WeiXinTicketSystem.Entity.Enum;

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
            type.Id = snacktype.Id;
            type.CinemaCode = snacktype.CinemaCode;
            type.TypeName = snacktype.TypeName;
            type.Remark = snacktype.Remark;
            type.Image = snacktype.Image;
            return type;
        }

        public static QuerySnacksReplySnack MapFrom(this QuerySnacksReplySnack snack,SnackEntity entity)
        {
            snack.Id = entity.Id;
            snack.CinemaCode = entity.CinemaCode;
            snack.SnackCode = entity.SnackCode;
            snack.TypeId = entity.TypeId;
            snack.SnackName = entity.SnackName;
            snack.Remark = entity.Remark;
            snack.StandardPrice = entity.StandardPrice;
            snack.SalePrice = entity.SalePrice;
            snack.Status = entity.Status.GetDescription();
            snack.Stock = entity.Stock;
            snack.ExpDate = entity.ExpDate.HasValue? entity.ExpDate.Value:DateTime.Parse("2099-12-31");
            snack.IsDel = entity.IsDel;
            snack.IsRecommand = entity.IsRecommand.HasValue? entity.IsRecommand.Value:false;
            snack.Image = entity.Image;
            return snack;
        }
        public static BookSnacksReplySnacks MapFrom(this BookSnacksReplySnacks data,SnackOrderViewEntity order)
        {
            data.CinemaCode = order.OrderBaseInfo.CinemaCode;
            data.OrderCode = order.OrderBaseInfo.OrderCode;
            data.MobilePhone = order.OrderBaseInfo.MobilePhone;
            data.SnacksCount = order.OrderBaseInfo.SnacksCount;
            data.TotalPrice = order.OrderBaseInfo.TotalPrice;
            data.OrderStatus = order.OrderBaseInfo.OrderStatus;
            data.DeliveryAddress = order.OrderBaseInfo.DeliveryAddress;
            data.SendTime = order.OrderBaseInfo.SendTime;
            data.Created = order.OrderBaseInfo.Created;
            data.AutoUnLockDateTime = order.OrderBaseInfo.AutoUnLockDateTime;

            data.Snacks = order.SnackOrderDetails.Select(
                x => new BookSnacksReplySnack()
                {
                    SnackCode = x.SnackCode,
                    StandardPrice=x.StandardPrice,
                    SalePrice=x.SalePrice,
                    Number=x.Number,
                    SubTotalPrice=x.SubTotalPrice
                }).ToList();
            return data;
        }
        public static PayOrderReplyOrder MapFrom(this PayOrderReplyOrder data,SnackOrderEntity entity)
        {
            data.CinemaCode = entity.CinemaCode;
            data.OrderCode = entity.OrderCode;
            data.OrderStatus = entity.OrderStatus;
            data.OrderPayFlag = entity.OrderPayFlag.HasValue?entity.OrderPayFlag.Value:false;
            data.OrderTradeNo = entity.OrderTradeNo;
            data.OrderPayTime = entity.OrderPayTime.HasValue?entity.OrderPayTime.Value:DateTime.Now;
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
            banner.Id = entity.Id;
            banner.CinemaCode = entity.CinemaCode;
            banner.Title = entity.Title;
            banner.Image = entity.Image;
            banner.Created = entity.Created;
            banner.StartDate = entity.StartDate.HasValue? entity.StartDate.Value:DateTime.Now;
            banner.EndDate = entity.EndDate.HasValue ? entity.EndDate.Value : DateTime.Now;
            banner.Status = (int)entity.Status;
            return banner;
        }


        public static QueryActivityReplyActivity MapFrom(this QueryActivityReplyActivity activity, ActivityEntity entity)
        {
            activity.Id = entity.Id;
            activity.CinemaCode = entity.CinemaCode;
            activity.Title = entity.Title;
            activity.Image = entity.Image;
            activity.ActivityContent = entity.ActivityContent;
            activity.StartDate = entity.StartDate.Value;
            activity.EndDate = entity.EndDate.Value;
            activity.Status = entity.Status.GetDescription();
            activity.IsDel = entity.IsDel;
            return activity;
        }
        public static QueryUserOrdersReplyOrder MapFrom(this QueryUserOrdersReplyOrder order, SnackOrderEntity entity)
        {
            order.CinemaCode = entity.CinemaCode;
            order.OrderCode = entity.OrderCode;
            order.MobilePhone = entity.MobilePhone;
            order.SnacksCount = entity.SnacksCount;
            order.TotalPrice = entity.TotalPrice;
            order.OrderStatus = entity.OrderStatus;
            order.DeliveryAddress = entity.DeliveryAddress;
            order.SendTime = entity.SendTime;
            order.Created = entity.Created;
            return order;
        }
        public static QueryOrderReplyOrder MapFrom(this QueryOrderReplyOrder data, SnackOrderViewEntity order)
        {
            data.Id = order.OrderBaseInfo.Id;
            data.CinemaCode = order.OrderBaseInfo.CinemaCode;
            data.OrderCode = order.OrderBaseInfo.OrderCode;
            data.MobilePhone = order.OrderBaseInfo.MobilePhone;
            data.SnacksCount = order.OrderBaseInfo.SnacksCount;
            data.TotalPrice = order.OrderBaseInfo.TotalPrice;
            data.ReleaseTime = order.OrderBaseInfo.ReleaseTime.Value;
            data.SubmitTime = order.OrderBaseInfo.SubmitTime.Value;
            data.VoucherCode = order.OrderBaseInfo.VoucherCode;
            data.OrderStatus = order.OrderBaseInfo.OrderStatus;
            data.RefundTime = order.OrderBaseInfo.RefundTime.Value;
            data.FetchTime = order.OrderBaseInfo.FetchTime.Value;
            data.Created = order.OrderBaseInfo.Created;
            data.DeliveryAddress = order.OrderBaseInfo.DeliveryAddress;
            data.SendTime = order.OrderBaseInfo.SendTime;
            data.Created = order.OrderBaseInfo.Created;
            data.AutoUnLockDateTime = order.OrderBaseInfo.AutoUnLockDateTime;
            data.DeliveryAddress = order.OrderBaseInfo.DeliveryAddress;
            data.SendTime = order.OrderBaseInfo.SendTime;
            data.OrderPayFlag = order.OrderBaseInfo.OrderPayFlag.Value;
            data.OrderPayType = order.OrderBaseInfo.OrderPayType.Value;
            data.OrderPayTime = order.OrderBaseInfo.OrderPayTime.Value;
            data.OrderTradeNo = order.OrderBaseInfo.OrderTradeNo;
            data.IsUseConpons = order.OrderBaseInfo.IsUseConpons.Value;
            data.ConponCode = order.OrderBaseInfo.ConponCode;
            data.ConponPrice = order.OrderBaseInfo.ConponPrice.Value;
            data.OpenID = order.OrderBaseInfo.OpenID;
            data.Snacks = order.SnackOrderDetails.Select(
                x => new QueryOrderReplySnack()
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
            conpon.Id = entity.Id;
            conpon.CinemaCode = entity.CinemaCode;
            conpon.ConponType = entity.ConponType.GetDescription();
            conpon.OpenID = entity.OpenID;
            conpon.Price = entity.Price;
            conpon.ConponCode = entity.ConponCode;
            conpon.ValidityDate = entity.ValidityDate;
            conpon.IfUse = entity.IfUse.GetDescription();
            conpon.UseDate = entity.UseDate;
            conpon.Title = entity.Title;
            conpon.Deleted = entity.Deleted;
            conpon.Image = entity.Image;
            return conpon;
        }

        public static QuerySessionsReplySession MapFrom(this QuerySessionsReplySession session, SessionInfoEntity entity)
        {
            session.Id = entity.Id;
            session.CinemaCode = entity.CinemaCode;
            session.SessionCode = entity.SessionCode;
            session.ScreenCode = entity.ScreenCode;
            session.StartTime = entity.StartTime;
            session.FilmCode = entity.FilmCode;
            session.FilmName = entity.FilmName;
            session.Duration = entity.Duration;
            session.Language = entity.Language;
            session.UpdateTime = entity.UpdateTime;
            session.StandardPrice = entity.StandardPrice;
            session.LowestPrice = entity.LowestPrice;
            session.SettlePrice = entity.SettlePrice;
            session.TicketFee = entity.TicketFee;
            session.IsAvalible = entity.IsAvalible.GetDescription();
            session.Dimensional = entity.Dimensional;
            session.ListingPrice = entity.ListingPrice;
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
    }
}