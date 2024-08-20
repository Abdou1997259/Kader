using AutoMapper;
using Kader_System.DataAccess.Repositories;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.HR.Vacation;

namespace Kader_System.Services.IServices.HR;

public interface IEmployeeNotesServices
{
    public  Task<Response<CreateEmployeeNotes>> CreateEmployeeNotesAsync(CreateEmployeeNotes model);
    public  Task<Response<GetAllEmployeeNotesResponse>> GetAllEmployeeNotesAsync(string lang, GetAllEmployeeNotesRequest model, string host);

    public  Task<Response<EmployeeNotesData>> GetEmployeeNotesByIdAsync(int id);
    public  Task<Response<CreateEmployeeNotes>> UpdateEmployeeNotesAsync(int id, CreateEmployeeNotes model);

    public  Task<Response<string>> DeleteEmployeeNotesAsync(int id);
}
