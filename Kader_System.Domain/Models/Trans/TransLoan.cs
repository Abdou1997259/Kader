namespace Kader_System.Domain.Models.Trans
{
    [Table("trans_loan")]
    public class TransLoan : BaseEntity
    {
        [Key]
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

        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public HrEmployee HrEmployee { get; set; } = default!;
        public int? CalculateSalaryId { get; set; }
        public int? CalculateSalaryDetailsId { get; set; }

        public ICollection<TransLoanDetails> TransLoanDetails { get; set; }



    }
}
