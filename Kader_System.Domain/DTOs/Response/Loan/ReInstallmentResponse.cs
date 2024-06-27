
namespace Kader_System.Domain.DTOs.Response.Loan
{
    public class ReInstallmentResponse
    {

        public int Id { get; set; }
        public DateOnly DeductionDate { get; set; }

        public decimal Amount { get; set; }

        public DateOnly? PaymentDate { get; set; }
        public int DelayCount { get; set; }
        public int TransLoanId { get; set; }


    }
}
