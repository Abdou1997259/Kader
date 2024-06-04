namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CreateTransDeductionRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly ActionMonth { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double Amount { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int AmountTypeId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int SalaryEffectId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int DeductionId { get; set; }
        public string? Notes { get; set; }
        public string? Attachment { get; set; }
        public string? FileName { get; set; }
    }
}
