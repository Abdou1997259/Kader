using Kader_System.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace Kader_System.Services.IServices.HR;

public interface IEmployeeService
{
    Task<Response<IEnumerable<ListOfEmployeesResponse>>> ListOfEmployeesAsync(string lang);
    Task<Response<GetAllEmployeesResponse>> GetAllEmployeesAsync(string lang, GetAllEmployeesFilterRequest model, string host);

    Task<Response<GetAllEmployeesResponse>> GetAllEmployeesByCompanyIdAsync(string lang,
        GetAllEmployeesFilterRequest model, string host, int companyId);
    Task<Response<CreateEmployeeRequest>> CreateEmployeeAsync(CreateEmployeeRequest model);
    Task<Response<GetEmployeeByIdResponse>> GetEmployeeByIdAsync(int id, string lang);
    Response<GetEmployeeByIdResponse> GetEmployeeById(int id, string lang);
    Task<Response<UpdateEmployeeRequest>> UpdateEmployeeAsync(int id, UpdateEmployeeRequest model);
    Task<Response<CreateEmployeeRequest>> RestoreEmployeeAsync(int id);
    Task<Response<string>> UpdateEmployeeAttachemnt(UpdateEmployeeAttachemnt model, int id);
    Task<Response<string>> UpdateActiveOrNotEmployeeAsync(int id);
    Task<Response<string>> DeleteEmployeeAsync(int id);
    Task<Response<EmployeesLookUps>> GetEmployeesLookUpsData(string lang);
    Task<Response<object>> GetEmployeesDataNameAndIdAsLookUp(string lang);
    Task<Response<object>> GetDocuments(int empId);
    Task<Response<string>> RemoveEmployeeAttachement(int attachementId);
    Task<Response<FileContentResult>> DownloadEmployeeAttachement(int id);
    Task<Response<string>> RemoveEmployeeProfile(int empId);





}
