namespace Kader_System.Domain.Dtos.Request.HR;

public class HrUpdateCompanyRequest 
{
    [Display(Name = Annotations.NameInEnglish), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Name_en { get; set; }

    [Display(Name = Annotations.NameInArabic), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Name_ar { get; set; }

    [Display(Name = Annotations.CompanyOwner), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Company_owner { get; set; }

    [AllowedValues(1, 2), Display(Name = Annotations.CompanyOwner)]
    public int Company_type { get; set; }

    [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]
    public List<IFormFile>? company_licenses { get; set; }


    [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]
    public List<IFormFile>? company_contracts { get; set; } = default!;
}



public class FileData
{
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
}