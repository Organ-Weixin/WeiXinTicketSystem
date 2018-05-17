using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WeiXinTicketSystem.Attributes.UI
{
    public class TextAreaAttribute : UIHintAttribute, IMetadataAware
    {
        public int? Rows { get; set; }
        public int? Cols { get; set; }
        public bool IsEditor { get; set; }

        public TextAreaAttribute() : this(rows: 2, cols: -1)
        {
        }

        public TextAreaAttribute(int rows) : this(rows: rows, cols: -1)
        {
        }

        public TextAreaAttribute(int rows, int cols) : base("TextArea")
        {
            Rows = rows > 0 ? (int?)rows : null;
            Cols = cols > 0 ? (int?)cols : null;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["Rows"] = Rows;
            metadata.AdditionalValues["Cols"] = Cols;
            metadata.AdditionalValues["IsEditor"] = IsEditor;
        }
    }
}