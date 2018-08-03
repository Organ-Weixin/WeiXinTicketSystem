using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateGivingConditionsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "影院编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(8, ErrorMessage = "{0}最多8个字符")]
        public string CinemaCode { get; set; }

        [Display(Name = "赠送条件")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(200, ErrorMessage = "{0}最多200个字符")]
        public string Conditions { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "优惠券类型")]
        public int ConponType { get; set; }

        [Display(Name = "价格")]
        public decimal? Price { get; set; }


        [Display(Name = "开始时间")]
        public string StartDate { get; set; }

        [Display(Name = "结束时间")]
        public string EndDate { get; set; }
    }
}