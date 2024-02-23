using System;
using System.ComponentModel.DataAnnotations;

public class DateShouldBeAfterAttribute : ValidationAttribute
{
    private readonly DateTime _earliestDate;

    public DateShouldBeAfterAttribute(string earliestDate)
    {
        _earliestDate = DateTime.Parse(earliestDate);
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            DateTime inputValue = (DateTime)value;
            if (inputValue < _earliestDate)
            {
                return new ValidationResult($"Date should not be older than {_earliestDate}");
            }
        }
        return ValidationResult.Success;
    }
}
