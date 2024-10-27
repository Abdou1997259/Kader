namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests
{
    public class DTOSalaryIncreaseRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]

        public int EmployeeId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]

        public double Amount { get; set; }

        public string? Notes { get; set; }
        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]

        public IFormFile? Attachment { get; set; }
    }
}
