namespace Kader_System.Services.IServices.Setting;

public interface IScreenCategoryService
{
    Task<Response<IEnumerable<StSelectListForScreenCategoryResponse>>> ListOfScreensCategoriesAsync(string lang);
    Task<Response<StGetAllScreensCategoriesResponse>> GetAllScreensCategoriesAsync(string lang, StGetAllFiltrationsForScreenCategoryRequest model, string host);
    Task<Response<StCreateScreenCategoryRequest>> CreateScreenCategoryAsync(StCreateScreenCategoryRequest model);
    Task<Response<StGetMainScreenCategoryByIdResponse>> GetScreenCategoryByIdAsync(int id);
    public Task<Response<StUpdateScreenCategoryRequest>> UpdateScreenCategoryAsync(int id, StUpdateScreenCategoryRequest model, string lang, string appPath, string moduleName);
    Task<Response<string>> UpdateActiveOrNotScreenCategoryAsync(int id);
    Task<Response<string>> DeleteScreenCategoryAsync(int id);
    Task<Response<string>> OrderByPattern(int[] pattern);
    Task<Response<StScreenCat>> RestoreCatScreenAsync(int id);
}