namespace Kader_System.Domain.Dtos.Request.Setting;

public class StCreateSubScreenRequest
{
    [Display(Name = Annotations.NameInEnglish), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public string screen_sub_title_en { get; set; } = string.Empty;

    [Display(Name = Annotations.NameInArabic), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public string screen_sub_title_ar { get; set; } = string.Empty;

    [Display(Name = Annotations.Name), Required(ErrorMessage = Annotations.FieldIsRequired)]


    public int screen_cat_id { get; set; }
    [Display(Name = Annotations.Name), Required(ErrorMessage = Annotations.FieldIsRequired)]

    public string url { get; set; } = string.Empty;

    public List<int>? actions { get; set; } = [];
}

