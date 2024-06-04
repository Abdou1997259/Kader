namespace Kader_System.Domain.DTOs.Request.HR
{
    public class ChangeShiftRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int from_shift { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int to_shift { get; set; }
    }
}
