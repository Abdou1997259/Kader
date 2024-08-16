using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests
{
    public class DTODelayPermissionRequest
    {
        [Required(ErrorMessage = "please Insert EmployeeId")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "please Insert HoursDelay")]
        public int HoursDelay { get; set; }
        public string? Notes { get; set; }
        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFile? Attachment { get; set; }
    }
 
}
