using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class GenerateCouponViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "影院编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(8, ErrorMessage = "{0}最多8个字符")]
        public string CinemaCode { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "上级优惠券类型")]
        public string ConponTypeParentId { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "优惠券类型")]
        public string ConponTypeCode { get; set; }


        [Display(Name = "优惠金额")]
        public decimal? Price { get; set; }


        [Display(Name = "优惠券名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Title { get; set; }

        [File]
        [Display(Name = "优惠券图标")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Image { get; set; }

        [Display(Name = "有效期")]
        public string ValidityDate { get; set; }

        [Display(Name = "张数")]
        public string GenerateNum { get; set; }
    }
}