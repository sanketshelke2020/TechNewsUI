using System.ComponentModel.DataAnnotations;
using TechNewsUI.Models;

namespace TechNewsUI.Attributes
{
    public class DynamicFieldMaxValidatorAttribute : ValidationAttribute
    {
        public DynamicFieldMaxValidatorAttribute()
        {

        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DynamicField dynamicField = (DynamicField)validationContext.ObjectInstance;

            if (dynamicField is null)
            {
                return new ValidationResult("This Field is required");
            }

            if (
                dynamicField.FieldType == "TextBox" ||
                dynamicField.FieldType == "Text-Area"

            )
            {
                int minLength = 0;
                if (dynamicField.MinLength != null)
                {
                    minLength = dynamicField.MinLength.Value;
                }

                if (dynamicField.MaxLength == null)
                {
                    return new ValidationResult("The MaxLength field is required.");
                }
                int maxLength = dynamicField.MaxLength.Value;
                if (maxLength <= minLength)
                {
                    return new ValidationResult("MaxLength should be greater than minlength.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
