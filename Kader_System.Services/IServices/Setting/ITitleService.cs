
namespace Kader_System.Services.IServices.Setting
{
    public interface ITitleService
    {
        Task<Response<IEnumerable<SelectListOfTitleResponse>>> ListOfTitlesAsync(string lang);
        Task<Response<GetAllTitleResponse>> GetAllTitlesAsync(string lang, GetAllFilterrationForTitleRequest model);
        Task<Response<CreateTitleRequest>> CreateTitleAsync(CreateTitleRequest model);
        Task<Response<GetTitleByIdResponse>> GetTitleByIdAsync(int id,string lang);
        Task<Response<CreateTitleRequest>> UpdateTitleAsync(int id, CreateTitleRequest model);
        Task<Response<string>> UpdateActiveOrNotTitleAsync(int id);
        Task<Response<string>> DeleteTitleAsync(int id);
    }
}
