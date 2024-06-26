namespace Kader_System.Domain.Models.Trans
{
    [Table("Trans_Loan")]
    public class TransLoan : BaseEntity
    {
        [Key]
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

        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public HrEmployee HrEmployee { get; set; } = default!;
        public ICollection<TransLoanDetails> TransLoanDetails { get; set; }



    }
}
