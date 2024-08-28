namespace Kader_System.Domain.DTOs.Response.Loan
{
    public class GetLoanByIdReponse
    {
        public int Id { get; set; }
 
        public DateOnly StartLoanDate { get; set; }
     
        public short AdvanceType { get; set; }
        public decimal MonthlyDeducted { get; set; }

        public decimal LoanAmount { get; set; }
        public decimal PrevDedcutedAmount { get; set; }

        public int InstallmentCount { get; set; }
        public int EmployeeId { get; set; }

        public short LoanType { get; set; }

        public string? Notes { get; set; }
        public DateOnly StartCalculationDate { get; set; }
        public DateOnly EndCalculationDate { get; set; }
        public string? EmployeeName { get; set; }
        public TransLoanslookups TransLoanslookups { get; set; }
        public IEnumerable<TransLoanDetailsReponse> TransLoanDetails { get; set; }
    }
    public class TransLoanDetailsReponse
    {
        public int Id { get; set; }
        public DateOnly DeductionDate { get; set; }

        public decimal Amount { get; set; }

        public DateOnly? PaymentDate { get; set; }
        public int DelayCount { get; set; }
        public bool IsPaid => PaymentDate != null;

    }

}
