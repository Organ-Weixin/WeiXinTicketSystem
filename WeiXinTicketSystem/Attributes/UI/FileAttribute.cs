using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Attributes.UI
{
    public class FileAttribute : UIHintAttribute
    {
        public FileAttribute() : base("file")
        {}
    }
}