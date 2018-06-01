using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateSnackTypeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// 影院编码
        /// </summary>
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "所属影院")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(10, ErrorMessage = "{0}最多10个字符")]
        public string CinemaCode { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Display(Name = "类型名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string TypeName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Remark { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [File]
        [Display(Name = "图片")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Image { get; set; }
    }
}