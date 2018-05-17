using WeiXinTicketSystem.Attributes.UI;
using System.ComponentModel.DataAnnotations;

namespace WeiXinTicketSystem.Models
{
    public class UserBaseViewModel
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(20, ErrorMessage = "{0}最多20个字符")]
        public string Username { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "所属影院")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(10, ErrorMessage = "{0}最多10个字符")]
        public string CinemaCode { get; set; }

        [Display(Name = "真实姓名")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string RealName { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "所属角色")]
        [Required(ErrorMessage = "{0}不能为空")]
        public int RoleId { get; set; }
    }
}