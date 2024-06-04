namespace Kader_System.Services.IServices.HR
{
    public interface IManagementService
    {
        Task<Response<IEnumerable<HrListOfManagementsResponse>>> ListOfManagementsAsync(string lang);
        Task<Response<GetAllManagementsResponse>> GetAllManagementsAsync(string lang, HrGetAllFiltrationsFoManagementsRequest model, string host);
        Task<Response<CreateManagementRequest>> CreateManagementAsync(CreateManagementRequest model);
        Task<Response<HrGetManagementByIdResponse>> GetManagementByIdAsync(int id, string lang);
        Task<Response<CreateManagementRequest>> UpdateManagementAsync(int id, CreateManagementRequest model);
        Task<Response<string>> UpdateActiveOrNotManagementAsync(int id);
        Task<Response<string>> DeleteManagementAsync(int id);
    }
}
