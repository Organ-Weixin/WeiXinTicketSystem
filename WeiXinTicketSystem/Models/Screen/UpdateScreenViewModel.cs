using WeiXinTicketSystem.Attributes.UI;
using System.ComponentModel.DataAnnotations;

namespace WeiXinTicketSystem.Models
{
    public class UpdateScreenViewModel
    {
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "所属影院")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(10, ErrorMessage = "{0}最多10个字符")]
        public string CinemaCode { get; set; }
    }
}