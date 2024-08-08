namespace Kader_System.Services.IServices.Setting;

public interface IMainScreenService
{
    Task<Response<IEnumerable<StSelectListForMainScreenResponse>>> ListOfMainScreensAsync(string lang);
    public Task<Response<GetAllStMainScreen>> GetMainScreensWithRelatedDataAsync(string lang);

    Task<Response<StGetAllMainScreensResponse>> GetAllMainScreensAsync(string lang, StGetAllFiltrationsForMainScreenRequest model,string host);
    Task<Response<StCreateMainScreenRequest>> CreateMainScreenAsync(StCreateMainScreenRequest model, string serverPath, string moduleName);
    Task<Response<StGetMainScreenByIdResponse>> GetMainScreenByIdAsync(int id);
    Task<Response<StUpdateMainScreenRequest>> UpdateMainScreenAsync(int id, StUpdateMainScreenRequest model);
    //Task<Response<string>> UpdateActiveOrNotSubMainScreenAsync(int id);
    Task<Response<string>> DeleteMainScreenAsync(int id);
    Task<Response<StMainScreen>> RestoreMainScreenAsync(int id);
    Task<Response<string>> OrderByPattern(int[] pattern);
}
