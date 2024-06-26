namespace Kader_System.Domain.DTOs.Response.Loan
{
    public class ListOfLoansResponse
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime StartLoanDate { get; set; }
        public DateTime EndDoDate { get; set; }
        public DateTime DocumentDate { get; set; }

        public short LoanType { get; set; }
        public short AdvanceType { get; set; }
        public decimal MonthlyDeducted { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal PrevDedcutedAmount { get; set; }
        public string EmployeeName { get; set; }
        public int InstallmentCount { get; set; }
        public bool MakePaymentJournal { get; set; }
        public bool IsDeductedFromSalary { get; set; }
        public DateTime StartCalculationDate { get; set; }
        public DateTime EndCalculationDate { get; set; }


        public string? Notes { get; set; }
    }
}
