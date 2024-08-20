using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests
{
    public class DTOLoanRequest
    {
        [Required(ErrorMessage = "please Insert Employee Id")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "please Insert Installment Count")]
        public int InstallmentsCount { get; set; }
        [Required(ErrorMessage = "please Insert Amount")]
        public double Amount { get; set; }     
        public string? Notes { get; set; }
        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]

        public IFormFile? Attachment { get; set; }
        [Required(ErrorMessage = "please Insert Notes ")]
        [DefaultValue("2024/01/01")]
         public DateOnly  StartDate { get; set; }




    }
}
