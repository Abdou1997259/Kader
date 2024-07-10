
namespace Kader_System.Services.IServices.Trans
{
    public interface ITransCalcluateSalaryService
    {
        Task<Response<IEnumerable<GetSalariesEmployeeResponse>>> GetDetailsOfCalculation(CalcluateEmpolyeeFilters model, string lang);

        Task<Response<string>> CalculateSalaryDetailedTrans(CalcluateSalaryModelRequest model);

        Task<Response<IEnumerable<GetSalaryCalculatorResponse>>> GetAllCalculators(GetSalaryCalculatorFilterRequest model, string host, string lang);

        Task<Response<string>> DeleteCalculator(int Id);
        Task<Response<GetLookupsCalculatedSalaries>> GetLookups(string lang);


    }
}
