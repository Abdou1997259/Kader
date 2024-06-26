namespace Kader_System.Domain.DTOs.Response.Loan
{
    public class GetLoanByIdReponse
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime StartLoanDate { get; set; }
        public DateTime EndDoDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public short AdvanceType { get; set; }
        public decimal MonthlyDeducted { get; set; }

        public decimal LoanAmount { get; set; }
        public decimal PrevDedcutedAmount { get; set; }

        public int InstallmentCount { get; set; }


        public short LoanType { get; set; }

        public string? Notes { get; set; }
        public DateTime StartCalculationDate { get; set; }
        public DateTime EndCalculationDate { get; set; }
        public string? EmpolyeeName { get; set; }
        public TransLoanslookups TransLoanslookups { get; set; }
        public IEnumerable<TransLoanDetailsReponse> TransLoanDetails { get; set; }
    }
    public class TransLoanDetailsReponse
    {
        public int Id { get; set; }
        public DateTime DeductionDate { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
        public int DelayCount { get; set; }

    }

}
