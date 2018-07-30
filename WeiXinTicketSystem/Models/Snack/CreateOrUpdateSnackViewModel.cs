using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateSnackViewModel
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
        /// 类型
        /// </summary>
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "类型")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string TypeCode { get; set; }
        /// <summary>
        /// 套餐名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string SnackName { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [File]
        [Display(Name = "图片")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Image { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Remark { get; set; }

        /// <summary>
        /// 标准价
        /// </summary>
        [Display(Name = "标准价")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "01.00", "100.00", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        [Display(Name = "销售价")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "01.00", "100.00", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        [Display(Name = "库存")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N0}")]
        [Range(1,9999, ErrorMessage = "您输入的数量不符合规范，应该在{1}-{2}之间")]
        public decimal Stock { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "是否推荐")]
        public int IsRecommand { get; set; }
    }
}