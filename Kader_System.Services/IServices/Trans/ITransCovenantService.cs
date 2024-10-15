namespace Kader_System.Services.IServices.Trans;

public interface ITransCovenantService
{
    Task<Response<IEnumerable<SelectListOfCovenantResponse>>> ListOfTransCovenantsAsync(string lang);
    Task<Response<GetAllTransCovenantResponse>> GetAllTransCovenantsAsync(string lang, GetAllFilterationForTransCovenant model, string host);
    Task<Response<CreateTransCovenantRequest>> CreateTransCovenantAsync(CreateTransCovenantRequest model, string lang);
    Task<Response<GetTransCovenantById>> GetTransCovenantByIdAsync(int id, string lang);
    Task<Response<CreateTransCovenantRequest>> UpdateTransCovenantAsync(int id, CreateTransCovenantRequest model, string lang);
    Task<Response<object>> RestoreTransCovenantAsync(int id);
    Task<Response<string>> UpdateActiveOrNotTransCovenantAsync(int id);
    Task<Response<string>> DeleteTransCovenantAsync(int id);
}
