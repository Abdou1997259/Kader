namespace Kader_System.Domain.DTOs.Request.Interview
{
    public class AcceptApplicantRequest
    {


        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string details_message { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateTime interview_date { get; set; }

        [AllowedLetters(FileSettings.SpecialChar),
        FileExtensionValidation(FileSettings.AllowedFileExtension)]

        public IFormFile? file_path { get; set; }


    }
}
