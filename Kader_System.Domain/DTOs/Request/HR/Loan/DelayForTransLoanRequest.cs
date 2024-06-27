namespace Kader_System.Domain.DTOs.Request.HR.Loan
{
    public class DelayForTransLoanRequest
    {

        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly DeductionDate { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public decimal Amount { get; set; }


    }
}
