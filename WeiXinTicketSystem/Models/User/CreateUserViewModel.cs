using WeiXinTicketSystem.Attributes.UI;
using System.ComponentModel.DataAnnotations;

namespace WeiXinTicketSystem.Models
{
    public class CreateUserViewModel : UserBaseViewModel
    {
        [Password]
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}不可为空")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0}长度必须在{2}到{1}个字符之间")]
        public string Password { get; set; }

        [Password]
        [Display(Name = "确认密码")]
        [Required(ErrorMessage = "{0}不可为空")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0}长度必须在{2}到{1}个字符之间")]
        [Compare(nameof(Password), ErrorMessage = "两次密码输入不一致")]
        public string ConfirmPassword { get; set; }
    }
}