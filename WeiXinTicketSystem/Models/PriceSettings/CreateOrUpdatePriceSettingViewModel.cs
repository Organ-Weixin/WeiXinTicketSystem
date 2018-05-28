using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdatePriceSettingViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "所属影院")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(10, ErrorMessage = "{0}最多10个字符")]
        public string CinemaCode { get; set; }


        [Display(Name = "影院名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string CinemaName { get; set; }

        [Display(Name = "微信折扣")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "0", "100", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal WeChatDiscount { get; set; }

        [Display(Name = "活动价上限")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "0.00", "100.00", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal FackPriceUpperLimit { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否按标准价出票")]
        public int IsSellByStandardPrice { get; set; }

        [Display(Name = "情侣座差价")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "0.00", "100.00", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal LoveSeatPriceDifferences { get; set; }

        [Display(Name = "服务费")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "0.00", "100.00", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal Fee { get; set; }

        [Display(Name = "会员服务费")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "0.00", "100.00", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal MemberFee { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "服务费支付方式")]
        public int FeePayType { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "服务费收取方式")]
        public int FeeGatherType { get; set; }


    }
}