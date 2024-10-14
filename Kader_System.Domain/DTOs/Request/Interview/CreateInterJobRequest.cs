namespace Kader_System.Domain.DTOs.Request.Interview
{
    public class CreateInterJobRequest
    {

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly from { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly to { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int applicant_count { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string name_ar { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string name_en { get; set; }

        public string? description { get; set; }

    }
}
