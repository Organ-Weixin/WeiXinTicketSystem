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
        public string TypeCode { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "套餐/影片")]
        public string SnackCode { get; set; }

        [Display(Name = "优惠券名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Title { get; set; }

        [Display(Name = "优惠金额")]
        public decimal? Price { get; set; }

        [Display(Name = "有效期")]
        public string ValidityDate { get; set; }

        [Display(Name = "张数")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Range(1,99999, ErrorMessage = "张数应该在{1}-{2}之间")]
        public decimal GenerateNum { get; set; }

        [Display(Name = "备注")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Remark { get; set; }
    }
}