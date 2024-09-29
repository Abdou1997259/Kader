

using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Domain.DTOs.Response.Interview;

namespace Kader_System.Services.IServices.InterviewServices
{
    public interface IApplicantServices
    {
        Task<Response<IEnumerable<object>>> ListOfContractsAsync(string lang);
        Task<Response<GetAllResponse>> GetAllContractAsync(string lang, GetAllFilteredRequests model, string host);




        Task<Response<CreateContractRequest>> CreateContractAsync(CreateContractRequest model, string moduleName);
        Task<Response<object>> GetContractByIdAsync(int id, string lang);
        Task<Response<object>> GetLookUps(string lang);
        Task<Response<CreateContractRequest>> UpdateContractAsync(int id, CreateContractRequest model, string moduleName);
        Task<Response<CreateContractRequest>> RestoreContractAsync(int id);
        Task<Response<GetContractForUserResponse>> GetContractByUser(int EmpId, string lang);

        Task<Response<string>> UpdateActiveOrNotContractAsync(int id);
        Task<Response<string>> DeleteContractAsync(int id);

    }
}
