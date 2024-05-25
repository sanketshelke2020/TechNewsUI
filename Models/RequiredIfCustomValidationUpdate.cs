﻿using System.ComponentModel.DataAnnotations;

namespace TechNewsUI.Models
{
    public class RequiredIfCustomeValidationUpdate : ValidationAttribute
    {
        private string[] _field;
        public RequiredIfCustomeValidationUpdate(params string[] field)
        { // The constructor which we use in modal.
            this._field = field;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var category = (UpdateSectionMasterCommandDto)validationContext.ObjectInstance;

            foreach (var val in _field)
            {
                if (val == "youtubeRequired" && category.CategoryType == "Article")
                {
                    if(category.flexRadioDefault == "link")
                    {
                        if (value == null)
                        {
                            var result = new ValidationResult("Youtube link is Required");
                            return result;
                        }
                    }
                }
                    if (val == "fileRequired" && category.CategoryType == "Article")
                {
                    if (category.flexRadioDefault == "image")
                    {
                        if (value == null && category.StoredFileName==null)
                        {
                            var result = new ValidationResult("Please upload a file");
                            return result;
                        }
                    }
                }
                if (val == category.CategoryType)
                {
                    if (value == null)
                    {
                        var result = new ValidationResult("This Field is Required");
                        return result;
                    }
                }
            }



            return null;
        }
    }
}
