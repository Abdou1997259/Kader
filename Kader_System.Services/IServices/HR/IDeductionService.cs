namespace Kader_System.Services.IServices.HR;

public interface IDeductionService
{
    Task<Response<IEnumerable<SelectListResponse>>> ListOfDeductionsAsync(string lang);
    Task<Response<HrGetAllDeductionsResponse>> GetAllDeductionsAsync(string lang, HrGetAllFiltrationsForDeductionsRequest model, string host);
    Task<Response<HrCreateDeductionRequest>> CreateDeductionAsync(HrCreateDeductionRequest model);
    Task<Response<HrGetDeductionByIdResponse>> GetDeductionByIdAsync(int id);
    Task<Response<HrUpdateDeductionRequest>> UpdateDeductionAsync(int id, HrUpdateDeductionRequest model);
    Task<Response<HrGetDeductionByIdResponse>> RestoreDeductionAsync(int id);
    Task<Response<string>> UpdateActiveOrNotDeductionAsync(int id);
    Task<Response<string>> DeleteDeductionAsync(int id);
}
