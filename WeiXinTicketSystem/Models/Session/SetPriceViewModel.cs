using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Models
{
    public class SetPriceViewModel
    {
        /// <summary>
        /// 价格设置ID
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int PriceSettingId { get; set; }
        /// <summary>
        /// 价格设置影院编码
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public string CinemaCode { get; set; }
        /// <summary>
        /// 价格设置UserID
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int? UserID { get; set; }
        /// <summary>
        /// 价格设置排期编码
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public string SessionCode { get; set; }
        /// <summary>
        /// 价格设置影片编码
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public string FilmCode { get; set; }
        /// <summary>
        /// 价格设置价格
        /// </summary>
        [Display(Name = "价格")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        [Range(typeof(decimal), "01.00", "100.00", ErrorMessage = "{0}在{1}和{2}之间")]
        public decimal? Price { get; set; }
        /// <summary>
        /// 价格设置类型
        /// </summary>
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "类型")]
        [Required(ErrorMessage = "{0}不能为空")]
        public int Type { get; set; }
    }
}