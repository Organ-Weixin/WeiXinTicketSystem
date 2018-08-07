using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;


namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateRecommendGradeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "等级编号")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(10, ErrorMessage = "{0}最多10个字符")]
        public string GradeCode { get; set; }

        [Display(Name = "等级名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string GradeName { get; set; }

        [Display(Name = "备注")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Remark { get; set; }
    }
}