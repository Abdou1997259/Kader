namespace Kader_System.Services.IServices.Setting;

public interface IMainScreenService
{
    Task<Response<IEnumerable<StSelectListForMainScreenResponse>>> ListOfMainScreensAsync(string lang);
    public Task<Response<GetAllStMainScreen>> GetMainScreensWithRelatedDataAsync(string lang);

    public Task<Response<StGetAllMainScreensResponse>> GetAllMainScreensAsync(string lang, StGetAllFiltrationsForMainScreenRequest model, string host, string moduleName);
    Task<Response<StCreateMainScreenRequest>> CreateMainScreenAsync(StCreateMainScreenRequest model, string serverPath, string moduleName);
    Task<Response<StGetMainScreenByIdResponse>> GetMainScreenByIdAsync(int id, string moduleName);
    Task<Response<StUpdateMainScreenRequest>> UpdateMainScreenAsync(int id, StUpdateMainScreenRequest model, string appPath, string moduleName);
    //Task<Response<string>> UpdateActiveOrNotSubMainScreenAsync(int id);
    Task<Response<string>> OrderByPattern(int[] pattern);
    Task<Response<string>> DeleteMainScreenAsync(int id);
    Task<Response<StMainScreen>> RestoreMainScreenAsync(int id);
}