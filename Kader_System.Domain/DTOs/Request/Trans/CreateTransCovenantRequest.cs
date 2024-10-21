namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CreateTransCovenantRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string name_en { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string name_ar { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly date { get; set; }
        public string? notes { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double amount { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int employee_id { get; set; }
        public IFormFile? attachment_file { get; set; }

    }
}
