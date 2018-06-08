using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateConponViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "影院编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(8, ErrorMessage = "{0}最多8个字符")]
        public string CinemaCode { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "优惠券类型")]
        public int ConponType { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "用户")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string OpenID { get; set; }

        [Display(Name = "价格")]
        public decimal? Price { get; set; }


        [Display(Name = "优惠券编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string ConponCode { get; set; }

        [Display(Name = "有效期")]
        public string ValidityDate { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否使用")]
        public int IfUse { get; set; }

        [Display(Name = "使用时间")]
        public string UseDate { get; set; }
    }
}