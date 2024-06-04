namespace Kader_System.Domain.Dtos.Request.Setting;

public class StCreateMainScreenCategoryRequest
{
    [Display(Name = Annotations.NameInEnglish), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Screen_main_title_en { get; set; } 

    [Display(Name = Annotations.NameInArabic), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Screen_main_title_ar { get; set; } 

    [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length)]
    public IFormFile? Screen_main_image { get; set; }
}
