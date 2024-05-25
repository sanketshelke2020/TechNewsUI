using System.ComponentModel.DataAnnotations;
using TechNewsUI.Models;

namespace TechNewsUI.Attributes
{
    public class DynamicFieldMinMaxValidatorAttribute : ValidationAttribute
    {
        public DynamicFieldMinMaxValidatorAttribute()
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
                if (dynamicField.MinLength == null)
                {
                    return new ValidationResult("The MinLength field is required.");
                }
                int minLength = dynamicField.MinLength.Value;
                if (minLength <= 0)
                {
                    return new ValidationResult("MinLength should be 1 or more than 1");
                }

                if (dynamicField.MaxLength == null)
                {
                    return new ValidationResult("REquired");
                }
                int maxLength = dynamicField.MaxLength.Value;
                if (maxLength <= minLength)
                {
                    return new ValidationResult("MaxLength should be greater than minlength");
                }
            }

            return ValidationResult.Success;
        }
    }
}
