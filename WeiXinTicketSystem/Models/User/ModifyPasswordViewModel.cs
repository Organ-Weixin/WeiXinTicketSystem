using WeiXinTicketSystem.Attributes.UI;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Models
{
    public class ModifyPasswordViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Password]
        [Display(Name = "新密码")]
        [Required(ErrorMessage = "{0}不可为空")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0}长度必须在{2}到{1}个字符之间")]
        public string NewPassword { get; set; }

        [Password]
        [Display(Name = "确认新密码")]
        [Required(ErrorMessage = "{0}不可为空")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0}长度必须在{2}到{1}个字符之间")]
        [System.ComponentModel.DataAnnotations.Compare(nameof(NewPassword), ErrorMessage = "两次密码输入不一致")]
        public string ConfirmNewPassword { get; set; }
    }
}