using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Domain.DTOs.Response.Interview;

namespace Kader_System.Services.IServices.InterviewServices
{
    public interface IInterJobServices
    {
        Task<Response<IEnumerable<object>>> ListOfAsync(string lang);

        Task<Response<GetAllResponse>> GetPaginatedJobs
                (GetAllFilteredJobRequests model, string lang, string host);



        Task<Response<CreateInterJobRequest>> CreateAsync(CreateInterJobRequest model, string moduleName, string lang);


        Task<Response<CreateInterJobRequest>> UpdateAsync(int id, CreateInterJobRequest model, string lang);

        Task<Response<object>> GetByIdAsync(int id, string lang);
        Task<Response<string>> RestoreAsync(int id);

        Task<Response<string>> DeleteAsync(int id);
        Task<Response<string>> SuspendJob(int id);
        Task<Response<string>> ResumeJob(int id);
        Task<Response<string>> FinishJob(int id);
        Task<Response<string>> ReplayJob(int id, ReplayJobRequest model);


    }
}
