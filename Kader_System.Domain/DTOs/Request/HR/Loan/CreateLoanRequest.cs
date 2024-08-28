

namespace Kader_System.Domain.DTOs.Request.HR.Loan
{
    public class CreateLoanRequest
    {
      
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly StartLoanDate { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]


        public short AdvanceType { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public decimal MonthlyDeducted { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public decimal LoanAmount { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public decimal PrevDedcutedAmount { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public int InstallmentCount { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly StartCalculationDate { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        public DateOnly EndCalculationDate { get; set; }
        [Required(ErrorMessage = Annotations.FieldIsRequired)]
        [Range(1, 2, ErrorMessage = "The Value must be 1 or 2")]
        public short LoanType { get; set; }


        public string? Notes { get; set; }


    }



}
