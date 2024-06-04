namespace Kader_System.Domain.Customization.Attributes;

public class AllowedLettersAttribute : ValidationAttribute
{
    private readonly string _allowedExtensions;

    public AllowedLettersAttribute(string allowedExtensions) =>
        _allowedExtensions = allowedExtensions;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file is not null)
        {
            string specialChar = _allowedExtensions;
            foreach (var item in specialChar)
                if (file.FileName.Contains(item))
                    return new ValidationResult($"File name is '{file.FileName}' contains special characters like [{_allowedExtensions}]");
        }
        return ValidationResult.Success;
    }

}