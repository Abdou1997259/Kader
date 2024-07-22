using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests
{
    public class DTODelayPermissionRequest
    {
        [Required(ErrorMessage = "please Insert Id")]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "please Insert EmployeeId")]

        public double HoursDelay { get; set; }
        [Required(ErrorMessage = "please Insert HoursDelay")]

        public string? Notes { get; set; }
        [Required(ErrorMessage = "please Insert Notes")]

        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]

        public IFormFile? Attachment { get; set; }
    }
}
