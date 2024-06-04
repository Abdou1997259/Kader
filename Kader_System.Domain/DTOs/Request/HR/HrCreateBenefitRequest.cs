namespace Kader_System.Domain.Dtos.Request.HR;

public class HrCreateBenefitRequest
{
    public int Id { get; set; }

    [Display(Name = Annotations.NameInEnglish), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Name_en { get; set; }

    [Display(Name = Annotations.NameInArabic), Required(ErrorMessage = Annotations.FieldIsRequired)]
    public required string Name_ar { get; set; }
}
