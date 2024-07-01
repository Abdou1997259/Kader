namespace Kader_System.Services.IServices.Trans
{
    public interface ITransCalcluateSalary
    {
        Task<Response<string>> CalculateSalary(CalcluateSalaryModelRequest model);
    }
}
