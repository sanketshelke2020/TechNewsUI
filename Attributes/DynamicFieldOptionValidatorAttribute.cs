using System.ComponentModel.DataAnnotations;
using TechNewsUI.Models;

namespace TechNewsUI.Attributes
{
    public class DynamicFieldOptionValidatorAttribute : ValidationAttribute
    {
        public DynamicFieldOptionValidatorAttribute()
        {

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DynamicField dynamicField = validationContext.ObjectInstance as DynamicField;

            if (dynamicField is null)
            {
               return new ValidationResult("This Options field is required.");
            }
            
            if (
                dynamicField.FieldType == "DropDown" ||
                dynamicField.FieldType == "RadioButton" ||
                dynamicField.FieldType == "CheckBox"
            )
            {
                if (dynamicField.Options is null)
                {
                    return new ValidationResult("This Options field is required.");
                }
                List<string> options = dynamicField.Options.Split(",").ToList();
                if (options.Count <= 1)
                {
                    return new ValidationResult("Options should be more than One.");
                }
            }

            return ValidationResult.Success;
        }
    }
}