namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CreateTransVacationRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly start_date { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double days_count { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int employee_id { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int vacation_id { get; set; }

        public string? notes { get; set; }

        public IFormFile? attachment_file { get; set; }

    }
}
