using Kader_System.Domain.DTOs.Response;

namespace Kader_System.Services.IServices.HR
{
    public interface IFingerPrintDeviceService
    {
        Task<Response<GetAllFingerPrintDevicesResponse>> GetAllFingerPrintDevicesAsync(string lang,GetAllFingerPrintDevicesFilterrationRequest request,string host);
        Task<Response<GetFingerPrintDeviceByIdResponse>> GetFingerPrintDeviceByIdAsync(int id);
        Task<Response<IEnumerable<ListOfFingerPrintDevicesResponse>>> GetFingerPrintDevicesAsync(string lang);
        Task<Response<FingerPrintLookupDataResponse>> GetFingerPrintsLookUpsData(string lang);
        Task<Response<CreateFingerPrintDeviceRequest>> CreateFingerPrintDevicesAsync(CreateFingerPrintDeviceRequest request);
        Task<Response<CreateFingerPrintDeviceRequest>> UpdateFingerPrintDevicesAsync(int id,CreateFingerPrintDeviceRequest request);
        Task<Response<object>> RestoreFingerPrintAsync(int id);
        Task<Response<string>> UpdateActiveOrNotEmployeeAsync(int id);
        Task<Response<string>> DeleteFingerPrintAsync(int id);
    }
}
