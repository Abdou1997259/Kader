
namespace Kader_System.Domain.DTOs.Request.HR.Loan
{
    public class PayForLoanDetailsRequest
    {

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly? PaymentDate { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public decimal Amount { get; set; }

    }
}
