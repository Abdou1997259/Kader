using Kader_System.Domain.Customization.Attributes.Kader_System.Domain.Customization.Attributes;

namespace Kader_System.Domain.DTOs.Request.Interview
{
    public class CreateApplicantRequest
    {

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [EmailPhoneUnique(Annotations.IsPhoneEmailUnique)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = Annotations.FieldIsRequired)]

        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int YearOfExperiences { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]

        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public decimal CurrentSalary { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public decimal ExpectedSalary { get; set; }

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [AllowedValues(1, 2, ErrorMessage = Annotations.OneTwoValueAllowed)]
        public int Gender { get; set; }

        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFile? ImageFile { get; set; }

        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length),
       FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFile? CVFile { get; set; }

        public List<EducationDto> Educations { get; set; } = new List<EducationDto>();

        public List<ExperienceDto> Experiences { get; set; } = new List<ExperienceDto>();




    }
    public class EducationDto
    {

        public int UniversityId { get; set; }
        public int FacultyId { get; set; }
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
    public class ExperienceDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
    }
}
