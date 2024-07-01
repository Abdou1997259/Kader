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
        Task<Response<TransLoanslookups>> GetLookUpsData(string lang);
        Task<Response<UpdateLoanReponse>> UpdateLoanAsync(int id, UpdateLoanRequest model, string lang);
        Task<Response<IEnumerable<ReInstallmentResponse>>> ReInstallmentAsync(ReInstallmentRequest request, int id);
        Task<Response<object>> RestoreLoanAsync(int id, string lang);
        Task<Response<string>> UpdateActiveOrNotLoanAsync(int id);
        Task<Response<PayForLoanDetailsResponse>> PayForLoanDetails(PayForLoanDetailsRequest model, int id);
        Task<Response<DelayForTransLoanResponse>> DelayForLoanDetails(DelayForTransLoanRequest model, int id);
        Task<Response<string>> DeleteLoanAsync(int id);
    }
}
