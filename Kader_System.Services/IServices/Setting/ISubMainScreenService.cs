namespace Kader_System.Services.IServices.Setting;

public interface ISubMainScreenService
{
    Task<Response<IEnumerable<StSelectListForSubMainScreenResponse>>> ListOfSubMainScreensAsync(string lang);
    Task<Response<StGetAllSubMainScreensResponse>> GetAllSubMainScreensAsync(string lang, StGetAllFiltrationsForSubMainScreenRequest model, string host);
    public Task<Response<StCreateSubMainScreenRequest>> CreateSubMainScreenAsync(StCreateSubMainScreenRequest model, string appPath, string moduleName);
    Task<Response<StGetSubMainScreenByIdResponse>> GetSubMainScreenByIdAsync(int id ,string lang);
    Task<Response<StUpdateSubMainScreenRequest>> UpdateSubMainScreenAsync(int id, StUpdateSubMainScreenRequest model, string appPath, string moduleName);
    Task<Response<string>> UpdateActiveOrNotSubMainScreenAsync(int id);
    Task<Response<string>> DeleteSubMainScreenAsync(int id);
    Task<Response<GetAllSubScreenInfo>> GetAllInfo(string lang);
    public  Task<Response<string>> OrderByPattern(int[] pattern);
   Task<Response<StScreenSub>> RestoreSubScreenAsync(int id);
    Task<int> DeleteScreenCodeSpace();
    //Task UpdateSubMainScreenAsync(int id, StUpdateSubMainScreenRequest model);
}
