namespace Kader_System.Domain.Models.Trans
{
    [Table("Trans_Loan_Details")]
    public class TransLoanDetails : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateOnly DeductionDate { get; set; }

        public decimal Amount { get; set; }

        public DateOnly? PaymentDate { get; set; }
        public int DelayCount { get; set; }
        public int? TransLoanId { get; set; }

        [ForeignKey(nameof(TransLoanId))]
        public TransLoan? TransLoan { get; set; }

    }
}
