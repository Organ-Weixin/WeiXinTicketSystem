using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Attributes.UI
{
    public class ListBoxAttribute : UIHintAttribute, IMetadataAware
    {
        public string Suffix { get; set; }
        public bool Multiple { get; set; }
        public bool CheckBoxListStyle { get; set; }

        public ListBoxAttribute(string suffix) : this(suffix, multiple: true, checkBoxListStyle: false)
        {
        }

        protected ListBoxAttribute(string suffix, bool multiple, bool checkBoxListStyle) : base("ListBox")
        {
            Suffix = suffix;
            Multiple = multiple;
            CheckBoxListStyle = checkBoxListStyle;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["Suffix"] = Suffix;
            metadata.AdditionalValues["Multiple"] = Multiple;
            metadata.AdditionalValues["CheckBoxListStyle"] = CheckBoxListStyle;
        }
    }
}