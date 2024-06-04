namespace Kader_System.Domain.Dtos.Request.Setting;

public class StCreateSubMainScreenRequest
{
    [Display(Name = Annotations.NameInEnglish), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Screen_sub_title_en { get; set; } 

    [Display(Name = Annotations.NameInArabic), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Screen_sub_title_ar { get; set; }

    [Display(Name = Annotations.Name), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Name { get; set; }
    public int Screen_main_id { get; set; }
    public required string Url { get; set; } 
    public List<int> Actions { get; set; } = [];
}
