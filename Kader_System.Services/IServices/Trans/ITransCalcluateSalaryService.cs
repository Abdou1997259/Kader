﻿
namespace Kader_System.Services.IServices.Trans
{
    public interface ITransCalcluateSalaryService
    {
        Task<Response<Tuple<Header, List<GetSalariesEmployeeResponse>>>>
            GetDetailsOfCalculation(EmployeeTransactionDetailsFilters model, string lang);



        Task<Response<GetSalaryCalculatorResponse>> GetAllCalculators(
            GetSalaryCalculatorFilterRequest model, string host, string lang);
        Task<Response<string>> PaySalary(int id);
        Task<Response<string>> DeleteCalculator(int Id);
        Task<Response<string>> CalculateSalary(UpdateCalculateSalaryModelRequest request);
        Task<Response<GetLookupsCalculatedSalaries>> GetLookups(string lang);
        Task<Response<SalaryResponse>> GetById(int id, string lang);


    }
}
