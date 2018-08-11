using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateUserCinemaViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// 接入商ID
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }

        /// <summary>
        /// 影院编码
        /// </summary>
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "影院编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string CinemaCode { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Password { get; set; }

        /// <summary>
        /// 费率
        /// </summary>
        [Display(Name = "费率")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "0.00", "100.00", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal? Fee { get; set; }

        /// <summary>
        /// 影院提成
        /// </summary>
        [Display(Name = "影院提成")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "0.00", "100.00", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal? CinemaFee { get; set; }

        /// <summary>
        /// 会员支付类型
        /// </summary>
        [Display(Name = "会员支付类型")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string MemberPayType { get; set; }

        /// <summary>
        /// 非会员支付类型
        /// </summary>
        [Display(Name = "非会员支付类型")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string NonMemberPayType { get; set; }

        /// <summary>
        /// 最低实际价格
        /// </summary>
        [Display(Name = "最低实际价格")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "0.00", "100.00", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal? RealPrice { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        [Display(Name = "有效期")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string ExpDateRange { get; set; }

        /// <summary>
        /// 套餐接口
        /// </summary>
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "套餐接口")]
        [Required(ErrorMessage = "{0}不能为空")]
        public int OpenSnacks { get; set; }
    }
}