using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateConponTypeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "优惠券类型编号")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(20, ErrorMessage = "{0}最多20个字符")]
        public string TypeCode { get; set; }

        [Display(Name = "优惠券类型名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(100, ErrorMessage = "{0}最多100个字符")]
        public string TypeName { get; set; }

        /// <summary>
        /// 上级模块编号
        /// </summary>
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "上级模块编号")]
        public int? TypeParentId { get; set; }

        [Display(Name = "备注")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Remark { get; set; }

    }
}