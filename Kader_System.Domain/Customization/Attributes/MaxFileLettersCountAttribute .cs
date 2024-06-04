namespace Kader_System.Domain.Customization.Attributes;

public class MaxFileLettersCountAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileLettersCountAttribute(int maxFileSize) =>
        _maxFileSize = maxFileSize;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file is not null)
            if (file.FileName.Length > _maxFileSize)
                return new ValidationResult($"Maximum allowed size is {_maxFileSize} char");

        return ValidationResult.Success;
    }
}