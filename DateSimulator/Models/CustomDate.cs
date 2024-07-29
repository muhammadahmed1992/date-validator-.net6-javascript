using DateSimulator.Utilities;
using DateSimulator.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class CustomDate
{
    [Required(ErrorMessage = "Start Date is required")]
    [CustomDateFormatValidation]
    [CustomValidDateValidation]
    public string? EmployementStartDate { get; set; }

    [Required(ErrorMessage = "End Date is required")]
    [CustomDateFormatValidation]
    [CustomValidDateValidation]
    [CustomEndDateValidation("EmployementStartDate")]
    public string? EmployementEndDate { get; set; }
}



