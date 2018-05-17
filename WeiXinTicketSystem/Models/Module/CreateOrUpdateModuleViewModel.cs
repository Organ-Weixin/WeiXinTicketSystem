using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateModuleViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "模块名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string ModuleName { get; set; }

        [Display(Name = "模块图标")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string ModuleIcon { get; set; }
        /// <summary>
        /// 上级模块编号
        /// </summary>
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "上级模块编号")]
        public int ModuleParentId { get; set; }

        [Display(Name = "模块排序号")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "0", "100", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal ModuleSequence { get; set; }

        [Display(Name = "模块标签")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string ModuleFlag { get; set; }
    }
}