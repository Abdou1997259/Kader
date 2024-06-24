namespace Kader_System.Domain.DTOs.Request.HR.Loan
{
    public class GetAllFilltrationForLoanRequest : PaginationRequest
    {
        public DateTime LoanDate { get; set; }
    }
}
