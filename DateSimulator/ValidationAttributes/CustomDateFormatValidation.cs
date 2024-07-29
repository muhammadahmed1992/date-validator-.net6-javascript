using DateSimulator.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DateSimulator.ValidationAttributes
{
    public class CustomDateFormatValidation : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || !Regex.IsMatch(value.ToString()!, @"^(\d{2}\/\d{4})$"))
            {
                return new ValidationResult(ErrorMessage ?? "Invalid date format (MM / YYYY expected)");
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            AttributeMergeUtil.MergeAttribute(context.Attributes, "data-val-format", ErrorMessage ?? "Invalid date format (MM / YYYY expected)");
        }
    }
}
