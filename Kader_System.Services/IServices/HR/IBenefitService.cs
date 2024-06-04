namespace Kader_System.Services.IServices.HR;

public interface IBenefitService
{
    Task<Response<IEnumerable<SelectListResponse>>> ListOfBenefitsAsync(string lang);
    Task<Response<HrGetAllBenefitsResponse>> GetAllBenefitsAsync(string lang, HrGetAllFiltrationsForBenefitsRequest model, string host);
    Task<Response<HrCreateBenefitRequest>> CreateBenefitAsync(HrCreateBenefitRequest model);
    Task<Response<HrGetBenefitByIdResponse>> GetBenefitByIdAsync(int id);
    Task<Response<HrUpdateBenefitRequest>> UpdateBenefitAsync(int id, HrUpdateBenefitRequest model);
    Task<Response<string>> UpdateActiveOrNotBenefitAsync(int id);
    Task<Response<HrGetBenefitByIdResponse>> RestoreBenefitAsync(int id);
    Task<Response<string>> DeleteBenefitAsync(int id);
}
