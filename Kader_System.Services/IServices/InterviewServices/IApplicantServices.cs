using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Domain.DTOs.Response.Interview;

namespace Kader_System.Services.IServices.InterviewServices
{
    public interface IApplicantServices
    {
        Task<Response<IEnumerable<object>>> ListOfAsync(string lang);
        //Task<Response<GetAllResponse>> GetAllAsync(string lang, GetAllFilteredRequests model, string host);




        Task<Response<CreateApplicantRequest>> CreateAsync(CreateApplicantRequest model, string moduleName, string lang);


        Task<Response<CreateApplicantRequest>> UpdateAsync(int id, CreateApplicantRequest model, string lang);

        Task<Response<object>> GetByIdAsync(int id, string lang);
        Task<Response<object>> GetDetails(int id, string lang);
        Task<Response<string>> Accept(int id, AcceptApplicantRequest model);
        Task<Response<string>> Reject(int id);
        Task<Response<string>> RateMe(int id, RateApplicantRequest request);
        Task<Response<GetAllApplicantsResponse>> GetPaginatedApplicants
            (GetApplicantsFilterationRequest model, string lang, string host);
        Task<Response<string>> RestoreAsync(int id);
        //Task<Response<string>> UpdateActiveOrNotContractAsync(int id);
        Task<Response<string>> DeleteAsync(int id);


    }
}
