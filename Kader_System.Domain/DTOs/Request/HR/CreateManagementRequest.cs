namespace Kader_System.Domain.DTOs.Request.HR
{
    public class CreateManagementRequest
    {
        [Display(Name = Annotations.NameInEnglish), Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required string NameEn { get; set; }

        [Display(Name = Annotations.NameInArabic), Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required string NameAr { get; set; }
        public int? ManagerId { get; set; }
        [Display(Name = Annotations.Company), Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required int CompanyId { get; set; }
    }
}
