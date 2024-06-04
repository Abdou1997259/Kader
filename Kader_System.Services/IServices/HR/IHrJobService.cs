
namespace Kader_System.Services.IServices.HR
{
    public interface IHrJobService
    {
        Task<Response<IEnumerable<SelectListResponse>>> ListOfJobsAsync(string lang);
        Task<Response<HrGetAllJobsResponse>> GetAllJobsAsync(string lang, HrGetAllFilterationForJobRequest model,string host);
        Task<Response<HrCreateJobRequest>> CreateJobAsync(HrCreateJobRequest model);
        Task<Response<HrGetJobByIdResponse>> GetJobByIdAsync(int id);
        Task<Response<HrUpdateJobRequest>> UpdateJobAsync(int id, HrUpdateJobRequest model);
        Task<Response<HrGetJobByIdResponse>> RestoreJobAsync(int id);
        Task<Response<string>> UpdateActiveOrNotBenefitAsync(int id);
        Task<Response<string>> DeleteJobAsync(int id);
    }
}
