namespace Kader_System.Domain.DTOs.Response.Loan
{
    public class ListOfLoansResponse
    {
        public int Id { get; set; }
        public DateOnly LoanDate { get; set; }
        public DateOnly StartLoanDate { get; set; }
       
        public DateOnly DocumentDate { get; set; }
        public DateTime AddedOn { get; set; }

        public string LoanType { get; set; }
        public short AdvanceType { get; set; }
        public decimal MonthlyDeducted { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal PrevDedcutedAmount { get; set; }
        public string EmployeeName { get; set; }
        public int InstallmentCount { get; set; }
        public bool MakePaymentJournal { get; set; }
        public bool IsDeductedFromSalary { get; set; }
        public DateOnly StartCalculationDate { get; set; }
        public DateOnly EndCalculationDate { get; set; }
        public int PaidInstallmentCount { get; set; }
        public decimal PaidTotalBalance { get; set; }    
        public decimal UnPaidTotalBalance { get; set; }

        public string? Notes { get; set; }
    }
}
