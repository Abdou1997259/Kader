

namespace Kader_System.Domain.DTOs.Request.HR.Loan
{
    public class ReInstallmentRequest
    {
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly StartCalculationDate { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int InstallmentCount { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public decimal RestAmount { get; set; }

    }
}
