using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Kader_System.Domain.Customization.Attributes
{
    public class UniqueNationalIdAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value,
           ValidationContext validationContext)
        {
            if (value is not string nationalId)
            {
                return ValidationResult.Success; // No validation required for non-string values.
            }

            // Resolve the required services from the DI container.
            var unitOfWork = validationContext.GetService<IUnitOfWork>();
            var localizer = validationContext.GetService<IStringLocalizer<SharedResource>>();

            if (unitOfWork == null)
            {
                throw new InvalidOperationException("IUnitOfWork is not registered in the dependency injection container.");
            }

            if (localizer == null)
            {
                throw new InvalidOperationException("IStringLocalizer<SharedResource> is not registered in the dependency injection container.");
            }

            // Perform the asynchronous validation.
            return IsUniqueNationalId(nationalId, unitOfWork, localizer).GetAwaiter().GetResult();
        }

        private async Task<ValidationResult?> IsUniqueNationalId(string nationalId,
            IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            bool exists = await unitOfWork.Employees.ExistAsync(x => x.NationalId == nationalId);
            if (exists)
            {
                // Use localized error message
                string errorMessage = localizer[Localization.UniqueNationalId];
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success; // Phone number is unique.
        }
    }
}
