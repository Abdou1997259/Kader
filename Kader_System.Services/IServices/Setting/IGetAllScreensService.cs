namespace Kader_System.Services.IServices.Setting
{
    public interface IGetAllScreensService
    {
        //public Task<Response<Dictionary<string, List<Dictionary<string, object>>>>> GetAllScreens(string lang);

        Task<Response<ScreenLookups>> GetAllScreensAsync(string lang);

    }
}
