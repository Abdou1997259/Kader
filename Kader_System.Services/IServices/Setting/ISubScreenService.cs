namespace Kader_System.Services.IServices.Setting;

public interface ISubScreenService
{
    Task<Response<IEnumerable<StSelectListForSubMainScreenResponse>>> ListOfSubScreensAsync(string lang);
    Task<Response<StGetAllSubScreensResponse>> GetAllSubScreensAsync(string lang, StGetAllFiltrationsForSubScreenRequest model, string host);
    public Task<Response<StCreateSubScreenRequest>> CreateSubScreenAsync(StCreateSubScreenRequest model, string appPath, string moduleName);
    Task<Response<StGetSubMainScreenByIdResponse>> GetSubScreenByIdAsync(int id, string lang);
    Task<Response<StUpdateSubScreenRequest>> UpdateSubScreenAsync(int id, StUpdateSubScreenRequest model, string appPath, string moduleName);
    Task<Response<string>> UpdateActiveOrNotSubScreenAsync(int id);
    Task<Response<string>> DeleteSubScreenAsync(int id);
    Task<Response<GetAllSubScreenInfo>> GetAllInfo(string lang);
    public Task<Response<string>> OrderByPattern(int[] pattern);
    Task<Response<StScreenSub>> RestoreSubScreenAsync(int id);
    Task<int> DeleteScreenCodeSpace();
    //Task UpdateSubMainScreenAsync(int id, StUpdateSubMainScreenRequest model);
}
