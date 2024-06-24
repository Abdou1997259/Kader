using Kader_System.Domain.DTOs.Request.HR.Loan;
using Kader_System.Domain.DTOs.Response.Loan;

namespace Kader_System.Services.IServices.HR
{
    public interface ILoanService
    {
        Task<Response<IEnumerable<ListOfLoansResponse>>> ListLoansAsync();
        Task<Response<GetAllLoansResponse>> GetAllLoanAsync(string lang, GetAllFilltrationForLoanRequest model, string host);
        Task<Response<CreateLoanRequest>> CreateLoanAsync(CreateLoanRequest model);
        Task<Response<GetLoanByIdReponse>> GetLoanByIdAsync(int id);
        Task<Response<UpdateLoanRequest>> UpdateLoanAsync(int id, UpdateLoanRequest model);
        Task<Response<GetLoanByIdReponse>> RestoreLoanAsync(int id);
        Task<Response<string>> UpdateActiveOrNotLoanAsync(int id);
        Task<Response<string>> DeleteLoanAsync(int id);
    }
}

