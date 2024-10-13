using Kader_System.Domain.DTOs.Request.Interview;

namespace Kader_System.Services.IServices.InterviewServices
{
    public interface IApplicantServices
    {
        Task<Response<IEnumerable<object>>> ListOfAsync(string lang);
        //Task<Response<GetAllResponse>> GetAllAsync(string lang, GetAllFilteredRequests model, string host);
        Task<Response<object>> GetByIdAsync(int id, string lang);



        Task<Response<CreateApplicantRequest>> CreateAsync(CreateApplicantRequest model, string moduleName, string lang);


        Task<Response<CreateApplicantRequest>> UpdateAsync(int id, CreateApplicantRequest model, string lang);

        Task<Response<object>> GetById(int id, string lang);
        Task<Response<string>> RestoreAsync(int id);
        //Task<Response<string>> UpdateActiveOrNotContractAsync(int id);
        Task<Response<string>> DeleteAsync(int id);

    }
}
