using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateBannerViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "影院编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(8, ErrorMessage = "{0}最多8个字符")]
        public string CinemaCode { get; set; }

        [Display(Name = "标题")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Title { get; set; }

        [File]
        [Display(Name = "图片")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Image { get; set; }

        [Display(Name = "开始时间")]
        public string StartDate { get; set; }

        [Display(Name = "结束时间")]
        public string EndDate { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否启用")]
        public int Status { get; set; }
    }
}