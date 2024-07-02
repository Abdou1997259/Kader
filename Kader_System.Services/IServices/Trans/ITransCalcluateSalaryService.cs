namespace Kader_System.Services.IServices.Trans
{
    public interface ITransCalcluateSalaryService
    {
        Task<Response<object>> CalculateSalary(CalcluateSalaryModelRequest model);
    }
}
