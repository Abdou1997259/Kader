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
        public short DocumentType { get; set; }
        public decimal MonthlyDeducted { get; set; }

        public decimal LoanAmount { get; set; }
        public decimal PrevDedcutedAmount { get; set; }

        public int InstallmentCount { get; set; }
        public bool MakePaymentJournal { get; set; }
        public bool IsDeductedFromSalary { get; set; }
        public string? Notes { get; set; }
        public int EmpolyeeId { get; set; }
        [ForeignKey(nameof(EmpolyeeId))]
        public HrEmployee HrEmployee { get; set; } = default!;


    }
}
