
namespace Kader_System.Services.IServices.Trans
{
    public interface ITransCalcluateSalaryService
    {
        Task<Response<IEnumerable<GetSalariesEmployeeResponse>>> GetDetailsOfCalculation(CalcluateEmpolyeeFilters model, string lang);

        Task<Response<string>> CalculateSalaryDetailedTrans(CalcluateSalaryModelRequest model);

        Task<Response<GetSalaryCalculatorResponse>> GetAllCalculators();

    }
}
