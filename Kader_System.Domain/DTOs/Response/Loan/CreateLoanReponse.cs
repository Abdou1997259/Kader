namespace Kader_System.Domain.DTOs.Response.Loan
{
    public class CreateLoanReponse
    {
        public int Id { get; set; }
        public DateOnly LoanDate { get; set; }
        public DateOnly StartLoanDate { get; set; }
        public DateOnly EndDoDate { get; set; }
        public DateOnly DocumentDate { get; set; }
        public short AdvanceType { get; set; }
        public decimal MonthlyDeducted { get; set; }

        public decimal LoanAmount { get; set; }
        public decimal PrevDedcutedAmount { get; set; }

        public int InstallmentCount { get; set; }


        public short LoanType { get; set; }

        public string? Notes { get; set; }
        public DateOnly StartCalculationDate { get; set; }
        public DateOnly EndCalculationDate { get; set; }

        public string EmployeeName { get; set; }


        public TransLoanslookups TransLoanslookups { get; set; }
    }
    public class TransLoanslookups
    {
        public IEnumerable<EmployeeLookup> HrEmployees { get; set; }

        public IEnumerable<AdvancedType> AdvancedTypes { get; set; }

    }
    public class EmployeeLookup
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }

    }
}
