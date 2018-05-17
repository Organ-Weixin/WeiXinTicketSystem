using WeiXinTicketSystem.Attributes.UI;
using System.ComponentModel.DataAnnotations;

namespace WeiXinTicketSystem.Models
{
    public class ChangePasswordViewModel
    {
        [Password]
        [Display(Name = "旧密码")]
        [Required(ErrorMessage = "{0}不可为空")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0}长度必须在{2}到{1}个字符之间")]
        public string OldPassword { get; set; }

        [Password]
        [Display(Name = "新密码")]
        [Required(ErrorMessage = "{0}不可为空")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0}长度必须在{2}到{1}个字符之间")]
        public string NewPassword { get; set; }

        [Password]
        [Display(Name = "确认新密码")]
        [Required(ErrorMessage = "{0}不可为空")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0}长度必须在{2}到{1}个字符之间")]
        [Compare(nameof(NewPassword), ErrorMessage = "两次密码输入不一致")]
        public string ConfirmNewPassword { get; set; }
    }
}