namespace Kader_System.Services.IServices.Setting;

public interface IMainScreenService
{
    Task<Response<IEnumerable<StSelectListForMainScreenResponse>>> ListOfMainScreensAsync(string lang);
    Task<Response<StGetAllMainScreensResponse>> GetAllMainScreensAsync(string lang, StGetAllFiltrationsForMainScreenRequest model);
    Task<Response<StCreateMainScreenRequest>> CreateMainScreenAsync(StCreateMainScreenRequest model);
    Task<Response<StGetMainScreenByIdResponse>> GetMainScreenByIdAsync(int id);
    Task<Response<StUpdateMainScreenRequest>> UpdateMainScreenAsync(int id, StUpdateMainScreenRequest model);
    Task<Response<string>> UpdateActiveOrNotMainScreenAsync(int id);
    Task<Response<string>> DeleteMainScreenAsync(int id);
}
