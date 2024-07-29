using DateSimulator.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DateSimulator.ValidationAttributes
{
    public class CustomValidDateValidation : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || !Regex.IsMatch(value.ToString()!, @"^(0[1-9]|1[0-2])\/(19|20)\d{2}$"))
            {
                return new ValidationResult(ErrorMessage ?? "Invalid date");
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            AttributeMergeUtil.MergeAttribute(context.Attributes, "data-val-validity", ErrorMessage ?? "Invalid date");
        }
    }
}
