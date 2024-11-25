using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Kader_System.Domain.Customization.Attributes
{
    public class BirthDateAttribute : ValidationAttribute
    {
        public string ValueCompareTo { get; set; }
        public int MinAge { get; set; }


        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {

            var _stringLocalizer = validationContext.GetService<IStringLocalizer<SharedResource>>();
            if (value == null)
            {

                return ValidationResult.Success;
            }
            value = value.ToString();
            if (value is string result)
            {
                if (DateOnly.TryParse(result, out var dateOnly))
                {
                    var currentDate = DateOnly.FromDateTime(DateTime.Now);
                    var age = currentDate.Year - dateOnly.Year;
                    if (currentDate < dateOnly.AddYears(age))
                    {
                        age--;
                    }

                    if (age < MinAge)
                    {
                        return new ValidationResult(_stringLocalizer[Localization.LessThanAllowedAge]);
                    }

                    if (!string.IsNullOrEmpty(ValueCompareTo))
                    {
                        var currentAge = validationContext.
                             ObjectType.
                             GetProperty(ValueCompareTo)?
                             .GetValue(validationContext.ObjectInstance);

                        if (currentAge is int expectedAge && expectedAge != age)
                        {
                            new ValidationResult(_stringLocalizer[Localization.BirthDateEqualAge]);
                        }





                    }
                    return ValidationResult.Success;

                }


            }
            return new ValidationResult(Localization.InvalidDateFormat);
        }
    }
}
