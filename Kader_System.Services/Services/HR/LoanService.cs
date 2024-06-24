

using Kader_System.Domain.DTOs.Request.HR.Loan;
using Kader_System.Domain.DTOs.Response.Loan;

namespace Kader_System.Services.Services.HR
{
    public class LoanService : ILoanService
    {
        public async Task<Response<CreateLoanRequest>> CreateLoanAsync(CreateLoanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> DeleteLoanAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetAllLoansResponse>> GetAllLoanAsync(string lang, GetAllFilltrationForLoanRequest model, string host)
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetLoanByIdReponse>> GetLoanByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ListOfLoansResponse>> ListLoansAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<GetLoanByIdReponse>> RestoreLoanAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> UpdateActiveOrNotLoanAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UpdateLoanRequest>> UpdateLoanAsync(int id, UpdateLoanRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
