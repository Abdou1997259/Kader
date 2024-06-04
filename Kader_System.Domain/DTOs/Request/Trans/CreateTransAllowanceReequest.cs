namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CreateTransAllowanceRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required DateOnly ActionMonth { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required double Amount { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required int SalaryEffectId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required int EmployeeId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public required int AllowanceId { get; set; }
        
        public string? Notes { get; set; }
    }
}
