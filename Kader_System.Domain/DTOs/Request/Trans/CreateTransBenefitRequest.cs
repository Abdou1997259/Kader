namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CreateTransBenefitRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly ActionMonth { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double Amount { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int increase_type_id { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int SalaryEffectId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int BenefitId { get; set; }
        public string? Notes { get; set; }
        public string? Attachment { get; set; }
        public string? FileName { get; set; }
    }
}
