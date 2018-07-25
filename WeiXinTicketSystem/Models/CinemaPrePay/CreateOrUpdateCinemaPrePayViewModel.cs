using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateCinemaPrePayViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "影院编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(8, ErrorMessage = "{0}最多8个字符")]
        public string CinemaCode { get; set; }

        [Display(Name = "影院名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string CinemaName { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否支付预付款")]
        public int IsPrePay { get; set; }

        [Display(Name = "剩余预付款")]
        public decimal? SurplusPayment { get; set; }

        [Display(Name = "预付款最低金额")]
        public decimal? LowestPrePayment { get; set; }
    }
}