namespace Kader_System.Services.IServices.Trans;

public interface ITransBenefitService
{
    Task<Response<IEnumerable<SelectListOfTransBenefitResponse>>> ListOfTransBenefitsAsync(string lang);
    Task<Response<GetAllTransBenefitResponse>> GetAllTransBenefitsAsync(string lang, GetAllFilterationForTransBenefitRequest model, string host);

    Task<Response<BenefitLookUps>> GetBenefitsLookUpsData(string lang);
    Task<Response<CreateTransBenefitRequest>> CreateTransBenefitAsync(CreateTransBenefitRequest model);
    Task<Response<GetTransBenefitById>> GetTransBenefitByIdAsync(int id, string lang);
    Task<Response<GetTransBenefitById>> UpdateTransBenefitAsync(int id, CreateTransBenefitRequest model);
    Task<Response<object>> RestoreTransBenefitAsync(int id);
    Task<Response<string>> UpdateActiveOrNotTransBenefitAsync(int id);
    Task<Response<string>> DeleteTransBenefitAsync(int id);
}
