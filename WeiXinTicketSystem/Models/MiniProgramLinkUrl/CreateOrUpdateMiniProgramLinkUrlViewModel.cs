using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateMiniProgramLinkUrlViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "链接名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string LinkName { get; set; }

        [Display(Name = "链接Url")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string LinkUrl { get; set; }
    }
}