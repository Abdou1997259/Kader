using Kader_System.Domain.Customization.Attributes.Kader_System.Domain.Customization.Attributes;
using Kader_System.Domain.Models.Interviews;

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
        public int Gender { get; set; }


        public IFormFile? ImagePath { get; set; }

        public ICollection<Education> Educations { get; set; } = new List<Education>();

        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();



    }

}
