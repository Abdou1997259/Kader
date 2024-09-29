namespace Kader_System.Domain.Customization.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    namespace Kader_System.Domain.Customization.Attributes
    {
        public class EmailPhoneUnique : ValidationAttribute
        {
            private readonly string _errorMessage;

            public EmailPhoneUnique(string errorMessage)
            {
                _errorMessage = errorMessage;
            }

            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                // Resolve IUnitOfWork from the service provider
                var unitOfWork = validationContext.GetService(typeof(IUnitOfWork)) as IUnitOfWork;

                if (unitOfWork == null)
                {
                    throw new InvalidOperationException("IUnitOfWork service is not available.");
                }

                if (value == null)
                {
                    return new ValidationResult($"The value cannot be null.", new[] { validationContext.MemberName });
                }

                var email = value as string;
                var phoneProperty = validationContext.ObjectType.GetProperty("Phone");
                var phoneValue = phoneProperty?.GetValue(validationContext.ObjectInstance, null) as string;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneValue))
                {
                    return new ValidationResult($"Invalid email or phone number.");
                }

                // Perform the check asynchronously
                var exists = CheckIfExistsAsync(unitOfWork, email, phoneValue).GetAwaiter().GetResult();

                if (exists)
                {
                    return new ValidationResult(_errorMessage);
                }

                return ValidationResult.Success;
            }

            // Helper method to perform the asynchronous check
            private async Task<bool> CheckIfExistsAsync(IUnitOfWork unitOfWork, string email, string phone)
            {
                return await unitOfWork.Applicant.ExistAsync(x => x.Email == email && x.Phone == phone);
            }
        }
    }

}
