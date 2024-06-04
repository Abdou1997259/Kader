namespace Kader_System.Domain.Dtos.Request.Setting;

public class StCreateMainScreenRequest
{
    [Display(Name = Annotations.NameInEnglish), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Screen_cat_title_en { get; set; }

    [Display(Name = Annotations.NameInArabic), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Screen_cat_title_ar { get; set; } 

    public int Screen_main_id { get; set; }
}
