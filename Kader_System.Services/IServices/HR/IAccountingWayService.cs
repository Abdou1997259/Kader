namespace Kader_System.Services.IServices.HR;

public interface IAccountingWayService
{
    Task<Response<IEnumerable<StSelectListForMainScreenCategoryResponse>>> ListOfMainScreensCategoriesAsync(string lang);
    Task<Response<StGetAllMainScreensCategoriesResponse>> GetAllMainScreensCategoriesAsync(string lang, StGetAllFiltrationsForMainScreenCategoryRequest model);
    Task<Response<StCreateMainScreenCategoryRequest>> CreateMainScreenCategoryAsync(StCreateMainScreenCategoryRequest model);
    Task<Response<StGetMainScreenCategoryByIdResponse>> GetMainScreenCategoryByIdAsync(int id);
    Task<Response<StUpdateMainScreenCategoryRequest>> UpdateMainScreenCategoryAsync(int id, StUpdateMainScreenCategoryRequest model);
    Task<Response<string>> UpdateActiveOrNotMainScreenCategoryAsync(int id);
    Task<Response<string>> DeleteMainScreenCategoryAsync(int id);
}
