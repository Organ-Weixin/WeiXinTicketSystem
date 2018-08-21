using System;
using System.Linq;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using WeiXinTicketSystem.Entity.Enum;
using NetSaleSvc.Api.Models;
using WeiXinTicketSystem.Service;

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
            type.TypeCode = snacktype.TypeCode;
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
            snack.TypeCode = entity.TypeCode;
            snack.SnackName = entity.SnackName;
            snack.Remark = entity.Remark;
            snack.StandardPrice = entity.StandardPrice;
            snack.SalePrice = entity.SalePrice;
            snack.Status = entity.Status.GetDescription();
            snack.Stock = entity.Stock;
            snack.ExpDate = entity.ExpDate.ToFormatStringWithT();
            snack.IsRecommand = entity.IsRecommand.GetDescription();
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
            activity.LinkUrl = entity.LinkUrl;
            activity.GradeCode = entity.GradeCode;
            if(entity.ActivitySequence !=null)
            {
                activity.ActivitySequence = entity.ActivitySequence.ToString();
            }
            
            activity.Status = entity.Status.GetDescription();
            return activity;
        }

        public static QueryActivitySequenceReplyActivity MapFrom(this QueryActivitySequenceReplyActivity activity, ActivityEntity entity)
        {
            activity.ActivityId = entity.Id;
            activity.CinemaCode = entity.CinemaCode;
            activity.Title = entity.Title;
            activity.Image = entity.Image;
            activity.ActivityContent = entity.ActivityContent;
            activity.StartDate = entity.StartDate.ToFormatStringWithT();
            activity.EndDate = entity.EndDate.ToFormatStringWithT();
            activity.LinkUrl = entity.LinkUrl;
            activity.GradeCode = entity.GradeCode;
            if (entity.ActivitySequence != null)
            {
                activity.ActivitySequence = entity.ActivitySequence.ToString();
            }

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
            //data.ConponCode = order.OrderBaseInfo.ConponCode;
            //data.ConponPrice = order.OrderBaseInfo.ConponPrice.HasValue ? order.OrderBaseInfo.ConponPrice.Value : 0;
            data.OpenID = order.OrderBaseInfo.OpenID;
            data.Snacks = order.SnackOrderDetails.Select(
                x => new QuerySnackOrderReplySnack()
                {
                    SnackCode = x.SnackCode,
                    StandardPrice = x.StandardPrice,
                    SalePrice = x.SalePrice,
                    Number = x.Number,
                    SubTotalPrice = x.SubTotalPrice,
                    ConponCode = x.ConponCode,
                    ConponPrice = x.ConponPrice,
                    ActualPrice = x.ActualPrice

                }).ToList();
            return data;
        }

        public static QueryConponsReplyConpon MapFrom(this QueryConponsReplyConpon conpon, ConponEntity entity)
        {
            conpon.ConponId = entity.Id;
            conpon.CinemaCode = entity.CinemaCode;
            conpon.TypeCode = entity.TypeCode;
            conpon.GroupCode = entity.GroupCode;
            conpon.OpenID = entity.OpenID;
            conpon.Price = entity.Price;
            conpon.ConponCode = entity.ConponCode;
            conpon.SnackCode = entity.SnackCode;
            conpon.ValidityDate = entity.ValidityDate.ToFormatStringWithT();
            conpon.Status = entity.Status.GetDescription();
            conpon.UseDate = entity.UseDate.ToFormatStringWithT();
            conpon.ReceivedDate = entity.ReceivedDate.ToFormatStringWithT();
            conpon.Title = entity.Title;
            conpon.Deleted = entity.Deleted;
            conpon.Remark = entity.Remark;
            return conpon;
        }
        //public static ConponEntity MapFrom(this ConponEntity entity, SendConponQueryJson queryJson)
        //{
        //    entity.ConponTypeCode = queryJson.ConponTypeCode;
        //    entity.CinemaCode = queryJson.CinemaCode;
        //    entity.OpenID = queryJson.OpenID;
        //    entity.Price = queryJson.Price;
        //    entity.ConponCode = RandomHelper.CreateRandomCode();
        //    entity.ValidityDate = queryJson.ValidityDate;
        //    entity.Status = ConponStatusEnum.NotUsed;
        //    entity.Created = DateTime.Now;
        //    entity.Deleted = false;
        //    entity.Title = queryJson.Title;
        //    entity.Image = queryJson.Image;
        //    return entity;
        //}
        public static SendConponReplyConpon MapFrom(this SendConponReplyConpon conpon, ConponEntity entity)
        {
            conpon.CinemaCode = entity.CinemaCode;
            conpon.Title = entity.Title;
            conpon.TypeCode = entity.TypeCode;
            conpon.GroupCode = entity.GroupCode;
            conpon.ConponCode = entity.ConponCode;
            conpon.SnackCode = entity.SnackCode;
            conpon.Price = entity.Price.HasValue ? entity.Price.Value : 0;
            conpon.ValidityDate = entity.ValidityDate.ToFormatStringWithT();
            conpon.Remark = entity.Remark;
            
            return conpon;
        }

        public static QueryMembersReplyMember MapFrom(this QueryMembersReplyMember member, MemberCardEntity entity)
        {
            member.MemberId = entity.Id;
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

        //public static QueryGiftsReplyGift MapFrom(this QueryGiftsReplyGift gift, GiftEntity entity)
        //{
        //    gift.GiftId = entity.Id;
        //    gift.CinemaCode = entity.CinemaCode;
        //    gift.Title = entity.Title;
        //    gift.Details = entity.Details;
        //    gift.OriginalPrice = entity.OriginalPrice.HasValue ? entity.OriginalPrice.Value : 0;
        //    gift.Price = entity.Price.HasValue ? entity.Price.Value : 0;
        //    gift.Image = entity.Image;
        //    gift.Stock = entity.Stock.HasValue ? entity.Stock.Value : 0;
        //    gift.StartDate = entity.StartDate.ToFormatStringWithT();
        //    gift.EndDate = entity.EndDate.ToFormatStringWithT();
        //    gift.Status = entity.Status.GetDescription();
        //    return gift;
        //}

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
        
        public static QueryFilmCommentsReplyComment MapFrom(this QueryFilmCommentsReplyComment filmComment, AdminFilmCommentViewEntity entity)
        {
            filmComment.CommentId = entity.Id;
            filmComment.FilmCode = entity.FilmCode;
            filmComment.FilmName = entity.FilmName;
            filmComment.Score = entity.Score;
            filmComment.CommentContent = entity.CommentContent;
            filmComment.OpenID = entity.OpenID;
            filmComment.Created = entity.Created;
            filmComment.NickName = entity.NickName;
            filmComment.HeadImgUrl = entity.HeadImgUrl;
            return filmComment;
        }

        public static QueryCinemasReplyCinema MapFrom(this QueryCinemasReplyCinema cinema, CinemaViewEntity entity)
        {
            cinema.CinemaId = entity.Id;
            cinema.CinemaCode = entity.Code;
            cinema.CinemaName = entity.Name;
            cinema.CinemaType = entity.CinemaType.GetDescription();
            cinema.ContactName = entity.ContactName;
            cinema.ContactMobile = entity.ContactMobile;
            cinema.TheaterChain = entity.TheaterChain.GetDescription();
            cinema.Address = entity.Address;
            cinema.IsOpen = entity.IsOpen.GetDescription();
            cinema.Latitude = entity.Latitude;
            cinema.Longitude = entity.Longitude;
            cinema.OpenSnacks = entity.IsOpenSnacks.GetDescription();
            cinema.TicketHint = entity.TicketHint;
            cinema.CinemaLabel = entity.CinemaLabel;
            cinema.CinemaPhone = entity.CinemaPhone;
            if(entity.IsSnackDistribution !=null)
            {
                if (entity.IsSnackDistribution == YesOrNoEnum.No)
                {
                    cinema.IsSnackDistribution = "否";
                }
                else if (entity.IsSnackDistribution == YesOrNoEnum.Yes)
                {
                    cinema.IsSnackDistribution = "是";
                }
            }
            
            return cinema;
        }

        public static ScoreRecordEntity MapFrom(this ScoreRecordEntity scoreRecord, SignInQueryJson Queryjson)
        {
            scoreRecord.CinemaCode = Queryjson.CinemaCode;
            scoreRecord.OpenID = Queryjson.OpenID;
            scoreRecord.Type = (ScoreRecordTypeEnum)Queryjson.Type;
            scoreRecord.Score = Queryjson.Score;
            scoreRecord.Description = Queryjson.Description;
            scoreRecord.Direction = (ScoreRecordDirectionEnum)Queryjson.Direction;
            scoreRecord.Created = DateTime.Now;
            return scoreRecord;
        }

        public static SignInReplySignIn MapFrom(this SignInReplySignIn data, ScoreRecordEntity scoreRecord,TicketUserEntity ticketUser)
        {
            data.CinemaCode = scoreRecord.CinemaCode;
            data.OpenID = scoreRecord.OpenID;
            data.Type = scoreRecord.Type.GetDescription();
            data.Score = scoreRecord.Score;
            data.Description = scoreRecord.Description;
            data.Direction = scoreRecord.Direction.GetDescription();
            data.Created = scoreRecord.Created;

            data.TotalScore = ticketUser.TotalScore;

            return data;
        }

        public static UserStampEntity MapFrom(this UserStampEntity userStamp, CollectStampQueryJson Queryjson)
        {
            userStamp.CinemaCode = Queryjson.CinemaCode;
            userStamp.OpenID = Queryjson.OpenID;
            userStamp.StampCode = Queryjson.StampCode;
            userStamp.CollectType = (UserStampCollectTypeEnum)Queryjson.CollectType;
            userStamp.Status = (UserStampStatusEnum)Queryjson.Status;
            userStamp.Created = DateTime.Now;
            return userStamp;
        }

        public static CollectStampReplyStamp MapFrom(this CollectStampReplyStamp data, UserStampEntity userStamp, StampEntity stamp)
        {
            data.CinemaCode = userStamp.CinemaCode;
            data.OpenID = userStamp.OpenID;
            data.CollectType = userStamp.CollectType.GetDescription();
            data.Status = userStamp.Status.GetDescription();
            data.Created = userStamp.Created;
            data.StampCode = userStamp.StampCode;

            data.StampTitle = stamp.Title;
            data.StampImage = stamp.Image;
            data.StampValidityDate = stamp.ValidityDate;

            return data;
        }

        public static QueryUserStampsReplyStamp MapFrom(this QueryUserStampsReplyStamp userStamp, ApiUserStampViewEntity entity)
        {
            userStamp.UserStampId = entity.Id;
            userStamp.CinemaCode = entity.CinemaCode;
            userStamp.OpenID = entity.OpenID;
            userStamp.CollectType = entity.CollectType.GetDescription();
            userStamp.Status = entity.Status.GetDescription();
            userStamp.Created = entity.Created;
            userStamp.StampCode = entity.StampCode;
            userStamp.StampTitle = entity.Title;
            userStamp.StampImage = entity.Image;
            userStamp.StampValidityDate = entity.ValidityDate;

            return userStamp;
        }

        public static TicketUserEntity MapFrom(this TicketUserEntity entity,WxUserInfo user)
        {
            entity.OpenID = user.openId;
            entity.NickName = user.nickName;
            entity.Sex = (UserSexEnum)user.gender;
            entity.Country = user.country;
            entity.Province = user.province;
            entity.City = user.city;
            entity.HeadImgUrl = user.avatarUrl;
            entity.Language = user.language;
            return entity;
        }
        public static QueryTicketUserReplyTicketUser MapFrom(this QueryTicketUserReplyTicketUser ticketUser,TicketUserEntity entity)
        {
            ticketUser.TicketUserId = entity.Id;
            ticketUser.CinemaCode = entity.CinemaCode;
            ticketUser.OpenID = entity.OpenID;
            ticketUser.NickName = entity.NickName;
            ticketUser.Sex = entity.Sex.GetDescription();
            ticketUser.Country = entity.Country;
            ticketUser.Province = entity.Province;
            ticketUser.City = entity.City;
            ticketUser.HeadImgUrl = entity.HeadImgUrl;
            ticketUser.Language = entity.Language;
            ticketUser.TotalScore = entity.TotalScore.HasValue? entity.TotalScore.Value:0;
            ticketUser.IsActive = entity.IsActive.GetDescription();
            ticketUser.Created = entity.Created.ToFormatStringWithT();
            return ticketUser;
        }

        public static TicketUserLoginTicketUser MapFrom(this TicketUserLoginTicketUser ticketUser,TicketUserEntity entity)
        {
            ticketUser.TicketUserId = entity.Id;
            ticketUser.CinemaCode = entity.CinemaCode;
            ticketUser.OpenID = entity.OpenID;
            ticketUser.NickName = entity.NickName;
            ticketUser.Sex = entity.Sex.GetDescription();
            ticketUser.Country = entity.Country;
            ticketUser.Province = entity.Province;
            ticketUser.City = entity.City;
            ticketUser.HeadImgUrl = entity.HeadImgUrl;
            ticketUser.Language = entity.Language;
            ticketUser.TotalScore = entity.TotalScore.HasValue ? entity.TotalScore.Value : 0;
            ticketUser.IsActive = entity.IsActive.GetDescription();
            ticketUser.Created = entity.Created.ToFormatStringWithT();
            return ticketUser;
        }

        public static QueryGivingConditionsReplyCondition MapFrom(this QueryGivingConditionsReplyCondition condition, AdminGivingConditionsViewEntity entity)
        {
            condition.ConditionId = entity.Id;
            condition.CinemaCode = entity.CinemaCode;
            condition.Price = entity.Price;
            condition.TypeCode = entity.TypeCode;
            condition.GroupCode = entity.GroupCode;
            condition.GroupName = entity.GroupName;
            condition.Number = entity.Number;
            condition.StartDate = entity.StartDate.ToFormatStringWithT();
            condition.EndDate = entity.EndDate.ToFormatStringWithT();
            condition.Remark = entity.Remark;
            condition.NotUsedNumber = GetNotUsedNumber(entity.CinemaCode, entity.GroupCode);
            return condition;
        }

        private static int? GetNotUsedNumber(string cinemaCode,string groupCode)
        {
            ConponGroupService _conponGroupService = new ConponGroupService();
            ConponGroupViewEntity conpon = _conponGroupService.GetConponGroupViewByCinemaCodeAndGroupCode(cinemaCode,groupCode);
            return conpon.NotUsedNumber;
        }

        public static QueryOrdersReplyOrder MapFrom(this QueryOrdersReplyOrder conpon, OrderEntity entity)
        {
            conpon.OrderId = entity.Id;
            //影院编码
            conpon.CinemaCode = entity.CinemaCode;
            //放映计划编码
            conpon.SessionCode = entity.SessionCode;
            //影厅编码
            conpon.ScreenCode = entity.ScreenCode;
            //放映计划时间
            conpon.SessionTime = entity.SessionTime;
            //影片编码
            conpon.FilmCode = entity.FilmCode;
            //影片名称
            conpon.FilmName = entity.FilmName;
            //座位数量
            conpon.TicketCount = entity.TicketCount;
            //总的上报价格
            conpon.TotalPrice = entity.TotalPrice;
            //总服务费
            conpon.TotalFee = entity.TotalFee;
            //总实际销售价格
            conpon.TotalSalePrice = entity.TotalSalePrice;
            //订单状态(New : 新建订单，SeatLocked: 已锁座， Payed: 已支付， Complete: 订单完成，TicketRefund 退票， Refund：退款)
            conpon.OrderStatus = (int)entity.OrderStatus;
            //手机号码
            conpon.MobilePhone = entity.MobilePhone;
            //锁座时间
            conpon.LockTime = entity.LockTime;
            //自动解锁时间
            conpon.AutoUnlockDatetime = entity.AutoUnlockDatetime;
            //锁座订单号
            conpon.LockOrderCode = entity.LockOrderCode;
            //提交时间
            conpon.SubmitTime = entity.SubmitTime;
            //提交订单号
            conpon.SubmitOrderCode = entity.SubmitOrderCode;
            //取票码
            conpon.PrintNo = entity.PrintNo;
            //验证码
            conpon.VerifyCode = entity.VerifyCode;
            //打印时间
            conpon.PrintTime = entity.PrintTime;
            //退单时间
            conpon.RefundTime = entity.RefundTime;
            return conpon;
        }
    }
}