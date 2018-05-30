using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateCinemaMemberCardSettingViewModel
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
        [Display(Name = "是否使用会员卡")]
        public int IsCardUse { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否使用会员卡注册")]
        public int IsCardRegister { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否使用会员卡充值")]
        public int IsCardReCharge { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "会员卡类型")]
        public int CardType { get; set; }

        [Display(Name = "第三方会员卡URL地址")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string ThirdMemberUrl { get; set; }

        [Display(Name = "会员卡初始密码")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string InitialCardPassword { get; set; }

        [Display(Name = "奥斯卡会员卡折扣")]
        public double OscarDiscount { get; set; }

    }
}