namespace Kader_System.Services.IServices.HR;

public interface IDepartmentService
{
    Task<Response<IEnumerable<ListOfDepartmentsResponse>>> ListOfDepartmentsAsync(string lang);
    Task<Response<GetAllDepartmentsResponse>> GetAllDepartmentsAsync(string lang, GetAllFiltrationsForDepartmentsRequest model, string host);
    Task<Response<CreateDepartmentRequest>> CreateDepartmentAsync(CreateDepartmentRequest model);
    Task<Response<GetDepartmentByIdResponse>> GetDepartmentByIdAsync(int id, string lang);
    Task<Response<CreateDepartmentRequest>> UpdateDepartmentAsync(int id, CreateDepartmentRequest model);
    Task<Response<string>> UpdateActiveOrNotDepartmentAsync(int id);
    Task<Response<string>> DeleteDepartmentAsync(int id);
}
