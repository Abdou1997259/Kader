using Kader_System.Domain.Dtos.Request.HR;
using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.Domain.Interfaces.HR;

public interface ISalaryIncreaseTypeRepository : IBaseRepository<HrValueType>
{
    public Task<List<SelectListLookupResponse>> GetAllSalaryIncreaseTypes();
    public Task<SelectListLookupResponse> GetSalaryIncreaseTypesById(int id);
    public Task<int> AddSalaryIncreaseType(HrCreateSalaryIncreaseTypesRequest selectList);
    public Task<int> UpdateSalaryIncreaseType(SelectListLookupResponse selectList);
    public Task<int> DeleteSalaryIncreaseType(int id);
    public Task<object> GetSalaryIncreaseType(string lang);

}
