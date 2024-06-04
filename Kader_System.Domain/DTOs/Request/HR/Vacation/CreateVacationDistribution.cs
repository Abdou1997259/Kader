namespace Kader_System.Domain.DTOs.Request.HR.Vacation
{
    public class CreateVacationDistribution
    {
        public required string NameEn { get; set; }
        public required string NameAr { get; set; }
        public int DaysCount { get; set; }
        public int SalaryCalculatorId { get; set; }
    }
}
