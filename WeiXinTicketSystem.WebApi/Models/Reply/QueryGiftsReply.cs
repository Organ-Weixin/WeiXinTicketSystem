using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.WebApi.Models
{
    public class QueryGiftsReply: BaseReply
    {
        public QueryGiftsReplyGifts data { get; set; }
    }
    public class QueryGiftsReplyGifts
    {
        public int GiftsCount { get; set; }
        public List<QueryGiftsReplyGift> Gifts { get; set; }
    }
    public class QueryGiftsReplyGift
    {
        public int GiftId { get; set; }
        public string CinemaCode { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }

    }
}