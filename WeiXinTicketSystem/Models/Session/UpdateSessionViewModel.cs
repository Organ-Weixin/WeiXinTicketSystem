using WeiXinTicketSystem.Attributes.UI;
using System.ComponentModel.DataAnnotations;

namespace WeiXinTicketSystem.Models
{
    public class UpdateSessionViewModel
    {
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "所属影院")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(10, ErrorMessage = "{0}最多10个字符")]
        public string CinemaCode { get; set; }

        [Display(Name = "时间范围")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string SessionDateRange { get; set; }
    }
}