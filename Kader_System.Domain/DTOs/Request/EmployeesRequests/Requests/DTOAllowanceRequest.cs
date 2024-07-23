namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests
{
    public class DTOAllowanceRequest
    {
        [Required(ErrorMessage = "please Insert Employee Id")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "please Insert Amount ")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "please Insert allowance id")]
        public int allowance_id { get; set; }
        [Required(ErrorMessage = "please Insert Vacation allowance type id")]
        public int allowance_type_id { get; set; }
        public string? Notes { get; set; }
        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFile? Attachment { get; set; }
    }

}
