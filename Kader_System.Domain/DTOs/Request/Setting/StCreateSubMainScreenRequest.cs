namespace Kader_System.Domain.Dtos.Request.Setting;

public class StCreateSubMainScreenRequest
{
    [Display(Name = Annotations.NameInEnglish), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string screen_sub_title_en { get; set; } 

    [Display(Name = Annotations.NameInArabic), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string screen_sub_title_ar { get; set; }

    [Display(Name = Annotations.Name), Required(ErrorMessage = Annotations.FieldIsRequired)]


    public int screen_cat_id { get; set; }
    public required string url { get; set; }

    public List<int>? actions { get; set; } = [];
}

