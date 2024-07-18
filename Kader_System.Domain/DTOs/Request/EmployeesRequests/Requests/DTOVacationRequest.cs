using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests
{
    public class DTOVacationRequest
    {
        [Required(ErrorMessage = "please Insert Employee Id")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "please Insert Vacation TypeId ")]
        public int VacationTypeId { get; set; }
        [Required(ErrorMessage = "please Insert Day Counts")]
        public int DayCounts { get; set; }
        [Required(ErrorMessage = "please Insert Start Date")]
        public DateTime StartDate { get; set; }
        public string? Notes { get; set; }
        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]

        public IFormFile? Attachment { get; set; }
    }
}
