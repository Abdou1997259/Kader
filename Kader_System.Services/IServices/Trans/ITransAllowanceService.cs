
namespace Kader_System.Services.IServices.Trans;

public interface ITransAllowanceService
{
    Task<Response<IEnumerable<SelectListForTransAllowancesResponse>>> ListOfTransAllowancesAsync(string lang);
    Task<Response<TransAllowanceGetAllResponse>> GetAllTransAllowancesAsync(string lang, GetAllFilterationAllowanceRequest model, string host);
    Task<Response<CreateTransAllowanceRequest>> CreateTransAllowanceAsync(CreateTransAllowanceRequest model);
    Task<Response<TransactionAllowanceGetByIdResponse>> GetTransAllowanceByIdAsync(int id,string lang);
    Task<Response<TransAllowanceLookUpsData>> GetAllowancesLookUpsData(string lang);
    Task<Response<TransactionAllowanceGetByIdResponse>> UpdateTransAllowanceAsync(int id, CreateTransAllowanceRequest model);
    Task<Response<string>> UpdateActiveOrNotTransAllowanceAsync(int id);
    Task<Response<object>> RestoreTransAllowanceAsync(int id);
    Task<Response<string>> DeleteTransAllowanceAsync(int id);
}
