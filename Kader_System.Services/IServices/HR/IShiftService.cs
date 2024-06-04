namespace Kader_System.Services.IServices.HR;

public interface IShiftService
{
    Task<Response<IEnumerable<SelectListResponse>>> ListOfShiftsAsync(string lang);
    Task<Response<HrGetAllShiftsResponse>> GetAllShiftsAsync(string lang, HrGetAllFiltrationsForShiftsRequest model, string host);
    Task<Response<HrCreateShiftRequest>> CreateShiftAsync(HrCreateShiftRequest model);
    Task<Response<HrGetShiftByIdResponse>> GetShiftByIdAsync(int id);
    Task<Response<HrUpdateShiftRequest>> UpdateShiftAsync(int id, HrUpdateShiftRequest model);
    Task<Response<HrUpdateShiftRequest>> RestoreShiftAsync(int id);
    Task<Response<string>> ChangeShift(int from, int to);
    Task<Response<string>> UpdateActiveOrNotShiftAsync(int id);
    Task<Response<string>> DeleteShiftAsync(int id);
}
