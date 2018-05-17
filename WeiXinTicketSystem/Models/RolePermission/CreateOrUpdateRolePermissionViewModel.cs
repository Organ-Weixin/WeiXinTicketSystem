using WeiXinTicketSystem.Attributes.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Models
{
    public class CreateOrUpdateRolePermissionViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int RoleId { get; set; }

        /// <summary>
        /// 模块编号
        /// </summary>
        [ListBox("_dd", Multiple = false)]
        [Display(Name = "模块编号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string ModuleId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [ListBox("_dd", Multiple = true)]
        [Display(Name = "权限")]
        [Required(ErrorMessage = "{0}不能为空")]
        public IList<int> Permissions { get; set; }
    }
}