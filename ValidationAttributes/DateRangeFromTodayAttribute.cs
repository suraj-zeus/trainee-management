using System;
using System.ComponentModel.DataAnnotations;

namespace Trainee.api.ValidationAttributes;

public class DateRangeFromTodayAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if(value is DateTime dateValue)
        {
            if(dateValue.Date < DateTime.Today)
            {
                return new ValidationResult("DueDate cannot be in the past.");
            }

            if (dateValue.Date > DateTime.Today.AddYears(100))
            {
                return new ValidationResult("DueDate cannot be more than 100 years in the future.");
            }
        }

        return ValidationResult.Success;
    }
}