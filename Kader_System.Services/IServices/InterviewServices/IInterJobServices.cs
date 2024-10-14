using Kader_System.Domain.DTOs.Request.Interview;

namespace Kader_System.Services.IServices.InterviewServices
{
    public interface IInterJobServices
    {
        Task<Response<IEnumerable<object>>> ListOfAsync(string lang);





        Task<Response<CreateInterJobRequest>> CreateAsync(CreateInterJobRequest model, string moduleName, string lang);


        Task<Response<CreateInterJobRequest>> UpdateAsync(int id, CreateInterJobRequest model, string lang);

        Task<Response<object>> GetByIdAsync(int id, string lang);
        Task<Response<string>> RestoreAsync(int id);

        Task<Response<string>> DeleteAsync(int id);
    }
}
