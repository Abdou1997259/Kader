namespace Kader_System.Services.IServices.Setting
{
    public interface IScreenService
    {
        Task<Response<GetAllScreenResponse>> GetAllScreensAsync(string lang, string host, GetAllFilterationForScreen model);
        Task<Response<CreateScreenRequest>> CreateScreenAsync(CreateScreenRequest model);
        Task<Response<GetScreenDataById>> GetScreenByIdAsync(int id, string lang);
        Task<Response<CreateScreenRequest>> UpdateScreenAsync(int id, CreateScreenRequest model);
        Task<Response<GetScreenDataById>> RestoreScreenAsync(int id);
        Task<Response<string>> DeleteScreenAsync(int id);
    }
}
