using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateMemberCardViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "影院编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(8, ErrorMessage = "{0}最多8个字符")]
        public string CinemaCode { get; set; }

        [Display(Name = "购票用户Id")]
        public string OpenID { get; set; }


        [Display(Name = "卡号")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string CardNo { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string CardPassword { get; set; }

        [Display(Name = "会员卡余额")]
        public decimal? Balance { get; set; }

        [Display(Name = "会员卡积分")]
        public double? Score { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "会员卡等级")]
        public int MemberGrade { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "状态")]
        public int Status { get; set; }
    }
}