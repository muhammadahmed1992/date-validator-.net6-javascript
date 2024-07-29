using DateSimulator.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DateSimulator.ValidationAttributes
{
    public class CustomEndDateValidation : ValidationAttribute, IClientModelValidator
    {
        private readonly string _startDateProperty;
        public CustomEndDateValidation(string startDateProperty)
        {
            _startDateProperty = startDateProperty;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDateProperty);

            if (startDateProperty == null)
            {
                return new ValidationResult($"Unknown property: {_startDateProperty}");
            }

            var customDate = (CustomDate)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(customDate.EmployementEndDate))
            {
                return ValidationResult.Success;
            }

            var startMonthYear = customDate.EmployementStartDate!.Split('/');
            var endMonthYear = customDate.EmployementEndDate!.Split('/');

            var startYear = int.Parse(startMonthYear[1]);
            var endYear = int.Parse(endMonthYear[1]);
            var startMonth = int.Parse(startMonthYear[0]);
            var endMonth = int.Parse(endMonthYear[0]);

            if (endYear < startYear || (endYear == startYear && endMonth <= startMonth))
            {
                return new ValidationResult(ErrorMessage ?? "End Date must be greater than Start Date");
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            AttributeMergeUtil.MergeAttribute(context.Attributes, "data-val-comparer", ErrorMessage ?? "End Date must be greater than Start Date");
        }
    }
}
