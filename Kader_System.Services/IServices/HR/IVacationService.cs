using Kader_System.Domain.DTOs.Request.HR.Vacation;

namespace Kader_System.Services.IServices.HR;

public interface IVacationService
{
    Task<Response<IEnumerable<SelectListResponse>>> ListOfVacationsAsync(string lang);
    Task<Response<GetAllVacationResponse>> GetAllVacationsWithJoinAsync(string lang, GetAllFilterationFoVacationRequest model, string host);
    Task<Response<CreateVacationRequest>> CreateVacationAsync(CreateVacationRequest model);
    Task<Response<GetVacationDetailsByIdResponse>> GetVacationByIdAsync(int id);
    Task<Response<UpdateVacationRequest>> UpdateVacationAsync(int id, UpdateVacationRequest model);
    Task<Response<UpdateVacationRequest>> RestoreVacationAsync(int id);
    Task<Response<string>> UpdateActiveOrNotVacationAsync(int id);
    Task<Response<string>> DeleteVacationAsync(int id);
}
