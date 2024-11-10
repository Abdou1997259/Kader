

namespace Kader_System.Domain.DTOs.Request.Interview
{
    public class RateApplicantRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public float rate { get; set; }
        public float? hygiene_rate { get; set; }
        public float? character_rate { get; set; }
        public float? hr_rate { get; set; }
        public float? technical_rate { get; set; }

    }
}
