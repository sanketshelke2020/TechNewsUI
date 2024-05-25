namespace TechNewsUI.Models
{
    public class DynamicFormMultipleOption
    {
        public int DynamicFormMultipleOptionId { get; set; }
        public string FieldType { get; set; }
        public string OptionName { get; set; }
        public int DynamicFieldId { get; set; }
        public DynamicField DynamicField { get; set; }
    }
}
