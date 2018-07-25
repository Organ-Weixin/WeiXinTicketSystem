using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateTicketSystemAccountViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "影院编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(8, ErrorMessage = "{0}最多8个字符")]
        public string CinemaCode { get; set; }

        [Display(Name = "影院名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string CinemaName { get; set; }

        [Display(Name = "影院系统对接UrL")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Url { get; set; }

        [Display(Name = "对接账号")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string UserName { get; set; }

        [Display(Name = "对接密码")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Password { get; set; }

        [Display(Name = "支付类型")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string PayType { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "影院系统")]
        public int TicketSystem { get; set; }

    }
}