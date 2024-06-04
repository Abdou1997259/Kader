namespace Kader_System.Domain.DTOs.Request.HR
{
    public class CreateEmployeeUploadFileRequest
    {
        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFile? EmployeeImageFile { get; set; } = default!;

        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length),
         FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFileCollection? EmployeeAttachments { get; set; } = default!;
    }
}
