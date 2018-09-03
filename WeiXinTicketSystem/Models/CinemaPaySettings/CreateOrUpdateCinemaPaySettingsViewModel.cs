using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateCinemaPaySettingsViewModel
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

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否使用支付宝")]
        public int IsUseAlipay { get; set; }


        [Display(Name = "支付宝账号")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string AlipaySellerEmail { get; set; }

        [Display(Name = "支付宝 Partner")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string AlipayPartner { get; set; }

        [Display(Name = "支付宝 Key")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string AlipayKey { get; set; }

        [Display(Name = "支付宝 APPID")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string AlipayAPPID { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否使用百度钱包")]
        public int IsUseBfbpay { get; set; }

        [Display(Name = "百度钱包商户号")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string BfbpaySpno { get; set; }

        [Display(Name = "百度钱包合作密钥")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string BfbpayKey { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否使用微信支付")]
        public int IsUseWxpay { get; set; }

        [Display(Name = "微信APPID")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string WxpayAppId { get; set; }

        [Display(Name = "微信支付商户号")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string WxpayMchId { get; set; }
        
        [Display(Name = "微信支付秘钥")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string WxpayKey { get; set; }

        [Display(Name = "退款用到的证书")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string WxpayRefundCert { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否使用会员卡")]
        public int IsUserMemberCard { get; set; }
    }
}