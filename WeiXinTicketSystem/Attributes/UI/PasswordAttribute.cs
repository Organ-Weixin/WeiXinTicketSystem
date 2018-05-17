using System.ComponentModel.DataAnnotations;

namespace WeiXinTicketSystem.Attributes.UI
{
    public class PasswordAttribute : UIHintAttribute
    {
        public PasswordAttribute() : base("password")
        { }
    }
}