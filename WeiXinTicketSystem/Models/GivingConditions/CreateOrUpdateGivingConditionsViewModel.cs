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

        [Display(Name = "赠送条件: 满")]
        [Required(ErrorMessage = "{0}不能为空")]
        public decimal? Price { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "上级优惠券类型")]
        public string TypeCode { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "优惠券类型")]
        public string GroupCode { get; set; }

        [Display(Name = "赠送张数")]
        public string Number { get; set; }

        [Display(Name = "开始时间")]
        public string StartDate { get; set; }

        [Display(Name = "结束时间")]
        public string EndDate { get; set; }
    }
}