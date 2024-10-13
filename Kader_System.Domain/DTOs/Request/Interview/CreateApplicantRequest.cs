using Kader_System.Domain.Customization.Attributes.Kader_System.Domain.Customization.Attributes;

namespace Kader_System.Domain.DTOs.Request.Interview
{
    public class CreateApplicantRequest
    {

        [Required(ErrorMessage = Annotations.FieldIsRequired)]

        public string full_name { get; set; } = null!;

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [EmailPhoneUnique(Annotations.IsPhoneEmailUnique)]
        public string email { get; set; } = null!;

        [Required(ErrorMessage = Annotations.FieldIsRequired)]

        public string phone { get; set; } = null!;

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int year_of_experiences { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]

        public DateOnly date_of_birth { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public decimal current_salary { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public decimal expected_salary { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [AllowedValues(1, 2, ErrorMessage = Annotations.OneTwoValueAllowed)]
        public int gender { get; set; }

        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFile? image_file { get; set; }

        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length),
       FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFile? cv_file { get; set; }

        public List<EducationDto> educations { get; set; } = new List<EducationDto>();

        public List<ExperienceDto> experiences { get; set; } = new List<ExperienceDto>();




    }
    public class EducationDto
    {

        public int university_id { get; set; }
        public int faculty_id { get; set; }
        public DateOnly from { get; set; }
        public DateOnly to { get; set; }
    }
    public class ExperienceDto
    {
        public string company_name { get; set; } = string.Empty;
        public string job_title { get; set; } = string.Empty;
        public DateOnly from { get; set; }
        public DateOnly to { get; set; }
    }
}
