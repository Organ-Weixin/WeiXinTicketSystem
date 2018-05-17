using System.Web.Mvc;

namespace WeiXinTicketSystem.Models
{
    public class UpdateUserViewModel : UserBaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}