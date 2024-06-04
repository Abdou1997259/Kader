namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CreateTransVacationRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double DaysCount { get; set; }
        
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int VacationId { get; set; }
       
        public string? Notes { get; set; }
        public string? Attachment { get; set; }
        public string? FileName { get; set; }

    }
}
