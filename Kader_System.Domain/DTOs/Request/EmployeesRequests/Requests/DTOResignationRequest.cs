using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests
{
    public class DTOResignationRequest
    {

        [Required(ErrorMessage = "please Insert Employee Id")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "please Insert Employee Id")]
        public string? Notes { get; set; }
        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]
        public string? AttachmentPath { get; set; }
        public IFormFile? Attachment { get; set; }

    }
}
