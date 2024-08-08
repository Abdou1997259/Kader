namespace Kader_System.Services.IServices.Setting;

public interface IMainScreenCategoryService
{
    Task<Response<IEnumerable<StSelectListForMainScreenCategoryResponse>>> ListOfMainScreensCategoriesAsync(string lang);
    Task<Response<StGetAllMainScreensCategoriesResponse>> GetAllMainScreensCategoriesAsync(string lang, StGetAllFiltrationsForMainScreenCategoryRequest model,string host);
    Task<Response<StCreateMainScreenCategoryRequest>> CreateMainScreenCategoryAsync(StCreateMainScreenCategoryRequest model);
    Task<Response<StGetMainScreenCategoryByIdResponse>> GetMainScreenCategoryByIdAsync(int id );
    public  Task<Response<StUpdateMainScreenCategoryRequest>> UpdateMainScreenCategoryAsync(int id, StUpdateMainScreenCategoryRequest model, string lang, string appPath, string moduleName);  
    Task<Response<string>> UpdateActiveOrNotMainScreenCategoryAsync(int id);
    Task<Response<string>> DeleteMainScreenCategoryAsync(int id);
    Task<Response<StMainScreenCat>> RestoreCatScreenAsync(int id);
    Task<Response<string>> OrderByPattern(int[] pattern);
}
