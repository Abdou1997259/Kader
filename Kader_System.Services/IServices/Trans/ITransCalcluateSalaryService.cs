namespace Kader_System.Services.IServices.Trans
{
    public interface ITransCalcluateSalaryService
    {
        Task<Response<IEnumerable<GetSalariesEmployeeResponse>>> GetDetailsOfCalculation(CalcluateEmpolyeeFilters model, string lang);

        public Task<Response<string>> CalculateSalaryDetailedTrans(CalcluateSalaryModelRequest model);


    }
}
