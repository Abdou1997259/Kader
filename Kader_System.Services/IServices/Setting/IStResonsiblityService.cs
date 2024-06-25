namespace Kader_System.Services.IServices.Setting
{
    public interface IStResonsiblityService
    {
        Task<Response<GetAllPaginatedStResonsiblityResponse>> GetAllStResonsiblitysAsync(string lang, string host, GetAllFilterationStResonsiblity model);
        Task<Response<CreateStResonsiblityRequest>> CreateStResonsiblityAsync(CreateStResonsiblityRequest model);
        Task<Response<GetStResonsiblityByIdReponse>> GetStResonsiblityByIdAsync(int id, string lang);
        Task<Response<UpdateStResonsiblityRequest>> UpdateStResonsiblityAsync(int id, UpdateStResonsiblityRequest model);
        Task<Response<GetStResonsiblityByIdReponse>> RestoreStResonsiblityAsync(int id, string lang);
        Task<Response<string>> DeleteStResonsiblityAsync(int id);
    }
}
