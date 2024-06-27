namespace Kader_System.Domain.DTOs.Response.Loan
{
    public class PayForLoanDetailsResponse
    {
        public int Id { get; set; }
        public DateOnly DeductionDate { get; set; }

        public decimal Amount { get; set; }

        public DateOnly? PaymentDate { get; set; }
        public int DelayCount { get; set; }
        public bool IsPaid => PaymentDate != null;

    }
}
