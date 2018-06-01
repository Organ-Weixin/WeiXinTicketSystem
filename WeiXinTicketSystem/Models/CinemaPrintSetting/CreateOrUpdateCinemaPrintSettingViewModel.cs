using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateCinemaPrintSettingViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "影院编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(8, ErrorMessage = "{0}最多8个字符")]
        public string CinemaCode { get; set; }

        [Display(Name = "影院名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string CinemaName { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "取票是否打印各数据项名称")]
        public int IsPrintName { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "影院是否已客制化化过票版")]
        public int IsCustomTicketTemplet { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "影院是否已客制化过套餐模板")]
        public int IsCustomPackageTemplet { get; set; }
    }
}