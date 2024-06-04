namespace Kader_System.Domain.DTOs.Request.HR
{
    public class CreateDepartmentRequest
    {
        [Display(Name = Annotations.NameInEnglish), Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required string NameAr { get; set; }
        [Display(Name = Annotations.NameInArabic), Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required string NameEn { get; set; }

        [Display(Name = Annotations.ManagementId), Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required int ManagementId { get; set; } = 1;

        public int? ManagerId { get; set; } = null!;


    }
}
