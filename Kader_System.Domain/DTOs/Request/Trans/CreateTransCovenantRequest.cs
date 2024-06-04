namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class CreateTransCovenantRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public  string NameEn { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public  string NameAr { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public double Amount { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int EmployeeId { get; set; }
        public string? Attachment { get; set; }
        public string? FileName { get; set; }
    }
}
