namespace Kader_System.Domain.Dtos.Request.HR;

public class HrCreateShiftRequest
{
    
    [Display(Name = Annotations.NameInEnglish), Required(ErrorMessage = Annotations.FieldIsRequired)]
    [DefaultValue("Main Shift")]
    public required string Name_en { get; set; }

    [Display(Name = Annotations.NameInArabic), Required(ErrorMessage = Annotations.FieldIsRequired)]
    [DefaultValue("الدوام الرئيسي")]
    public required string Name_ar { get; set; }
    [DefaultValue("09:00:00")]
    public string Start_shift { get; set; }
    [DefaultValue("17:00:00")]
    public string End_shift { get; set; }
}
