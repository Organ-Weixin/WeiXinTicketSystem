using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateRoleViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "角色名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(50, ErrorMessage = "{0}最多50个字符")]
        public string Name { get; set; }

        [Display(Name = "角色描述")]
        [StringLength(200, ErrorMessage = "{0}最多200个字符")]
        public string Description { get; set; }
    }
}