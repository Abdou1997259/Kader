using Kader_System.Domain.DTOs.Request.HR.Loan;
using Kader_System.Domain.DTOs.Response.Loan;

namespace Kader_System.Services.IServices.Trans
{
    public interface ITransLoanService
    {
        Task<Response<IEnumerable<ListOfLoansResponse>>> ListLoansAsync(string lang);
        Task<Response<GetAllLoansResponse>> GetAllLoanAsync(string lang, GetAllFilltrationForLoanRequest model, string host);

        Task<Response<CreateLoanReponse>> CreateLoanAsync(CreateLoanRequest model, string lang);
        Task<Response<GetLoanByIdReponse>> GetLoanByIdAsync(int id, string lang);
        Task<Response<TransLoanslookups>> GetDeductionsLookUpsData(string lang);
        Task<Response<UpdateLoanReponse>> UpdateLoanAsync(int id, UpdateLoanRequest model, string lang);
        Task<Response<object>> RestoreLoanAsync(int id, string lang);
        Task<Response<string>> UpdateActiveOrNotLoanAsync(int id);
        Task<Response<string>> PayForLoanDetails(PayForLoanDetailsRequest model);
        Task<Response<string>> DelayForLoanDetails(DelayForTransLoanRequest model);
        Task<Response<string>> DeleteLoanAsync(int id);
    }
}
