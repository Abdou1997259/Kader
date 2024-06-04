namespace Kader_System.Services.IServices.Setting;

public interface ISubMainScreenService
{
    Task<Response<IEnumerable<StSelectListForSubMainScreenResponse>>> ListOfSubMainScreensAsync(string lang);
    Task<Response<StGetAllSubMainScreensResponse>> GetAllSubMainScreensAsync(string lang, StGetAllFiltrationsForSubMainScreenRequest model);
    Task<Response<StCreateSubMainScreenRequest>> CreateSubMainScreenAsync(StCreateSubMainScreenRequest model);
    Task<Response<StGetSubMainScreenByIdResponse>> GetSubMainScreenByIdAsync(int id);
    Task<Response<StUpdateSubMainScreenRequest>> UpdateSubMainScreenAsync(int id, StUpdateSubMainScreenRequest model);
    Task<Response<string>> UpdateActiveOrNotSubMainScreenAsync(int id);
    Task<Response<string>> DeleteSubMainScreenAsync(int id);
}
