using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateCinemaViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "影院编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(8, ErrorMessage = "{0}最多8个字符")]
        public string CinemaCode { get; set; }

        [Display(Name = "影院名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string CinemaName { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "售票系统")]
        public int TicketSystem { get; set; }

        [Display(Name = "联系人")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string ContactName { get; set; }

        [Display(Name = "联系人电话")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string ContactMobile { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "所属院线")]
        public int TheaterChain { get; set; }

        [Display(Name = "地址")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Address { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "状态")]
        public int Status { get; set; }

        [Display(Name = "影院所在位置纬度")]
        public double? Latitude { get; set; }

        [Display(Name = "影院所在位置经度")]
        public double? Longitude { get; set; }

       
    }
}