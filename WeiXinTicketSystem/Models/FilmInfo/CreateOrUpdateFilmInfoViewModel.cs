using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;
using System.Linq;
using System.Web;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateFilmInfoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "影片编码")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string FilmCode { get; set; }

        [Display(Name = "影片名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string FilmName { get; set; }

        [Display(Name = "影片版本")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Version { get; set; }

        [Display(Name = "影片时长")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Duration { get; set; }

        [Display(Name = "发布时间")]
        public string PublishDate { get; set; }

        [Display(Name = "发行方")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Publisher { get; set; }

        [Display(Name = "制片人")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Producer { get; set; }

        [Display(Name = "导演")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Director { get; set; }

        [Display(Name = "演员")]
        [StringLength(500, ErrorMessage = "{0}最多500个字符")]
        public string Cast { get; set; }

        [Display(Name = "影片介绍")]
        public string Introduction { get; set; }

        [Display(Name = "评分")]
        public decimal? Score { get; set; }

        [Display(Name = "地区")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Area { get; set; }


        [Display(Name = "类型")]
        [StringLength(200, ErrorMessage = "{0}最多200个字符")]
        public string Type { get; set; }

        [Display(Name = "语言")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Language { get; set; }

        [ListBox("_dd", Multiple = false)]
        [Display(Name = "状态")]
        public int Status { get; set; }

        [File]
        [Display(Name = "图片")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Image { get; set; }

        [Display(Name = "预告片")]
        [StringLength(300, ErrorMessage = "{0}最多300个字符")]
        public string Trailer { get; set; }
    }
}