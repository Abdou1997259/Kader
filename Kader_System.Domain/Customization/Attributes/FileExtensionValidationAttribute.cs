namespace Kader_System.Domain.Customization.Attributes
{
    public class FileExtensionValidationAttribute : ValidationAttribute
    {
        private readonly string _allowedExtension;

        public FileExtensionValidationAttribute(string allowedExtension)
        {
            _allowedExtension = allowedExtension;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isAllowed = _allowedExtension.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    return new ValidationResult($"Extension {extension} is not allowed");
                }

               
            }
            return ValidationResult.Success;
        }
    }
}
