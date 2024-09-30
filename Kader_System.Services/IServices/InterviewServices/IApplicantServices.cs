using Kader_System.Domain.DTOs.Request.Interview;

namespace Kader_System.Services.IServices.InterviewServices
{
    public interface IApplicantServices
    {
        Task<Response<IEnumerable<object>>> ListOfAsync(string lang);
        //Task<Response<GetAllResponse>> GetAllAsync(string lang, GetAllFilteredRequests model, string host);




        Task<Response<CreateApplicantRequest>> CreateAsync(CreateApplicantRequest model, string moduleName, string lang);


        //Task<Response<CreateApplicantRequest>> UpdateAsync(int id, CreateApplicantRequest model, string moduleName);

        //Task<Response<object>> GetById(int id , string lang);
        //Task<Response<string>> RestoreContractAsync(int id);
        //Task<Response<string>> UpdateActiveOrNotContractAsync(int id);
        //Task<Response<string>> DeleteContractAsync(int id);

    }
}
