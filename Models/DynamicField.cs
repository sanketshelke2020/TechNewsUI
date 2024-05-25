using TechNewsUI.Attributes;

namespace TechNewsUI.Models
{
    public class DynamicField
    {
        public int DynamicFieldId { get; set; }
      
        public string CategoryType { get; set; }
        public string LabelText { get; set; }
       [DynamicFieldOptionValidator]
        public string? Options { get; set; }
        public string? PlaceHolder { get; set; }
        [DynamicFieldMaxValidator]
        public int? MaxLength { get; set; }
        [DynamicFieldMinMaxValidator]

        public int? MinLength { get; set; }
        public string FieldType { get; set; }
        public bool? IsNumber { get; set; }
        public string FieldCode { get; set; }
        


    }
}
