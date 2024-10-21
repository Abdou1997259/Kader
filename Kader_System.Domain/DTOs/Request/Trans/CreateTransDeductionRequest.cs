namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CreateTransDeductionRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly action_month { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double amount { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int amount_type_id { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int salary_effect_id { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int employee_id { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int deduction_id { get; set; }
        public string? notes { get; set; }
        public IFormFile? attachment_file { get; set; }

    }
}
