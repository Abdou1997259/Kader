namespace Kader_System.Services.IServices.HR;

public interface IQualificationService
{
    Task<Response<IEnumerable<SelectListResponse>>> ListOfQualificationsAsync(string lang);
    Task<Response<HrGetAllQualificationsResponse>> GetAllQualificationsAsync(string lang, HrGetAllFiltrationsForQualificationsRequest model, string host);
    Task<Response<HrCreateQualificationRequest>> CreateQualificationAsync(HrCreateQualificationRequest model);
    Task<Response<HrGetQualificationByIdResponse>> GetQualificationByIdAsync(int id);
    Task<Response<HrUpdateQualificationRequest>> RestoreQualificationAsync(int id);
    Task<Response<HrUpdateQualificationRequest>> UpdateQualificationAsync(int id, HrUpdateQualificationRequest model);
    Task<Response<string>> UpdateActiveOrNotBenefitAsync(int id);
    Task<Response<string>> DeleteQualificationAsync(int id);
}
