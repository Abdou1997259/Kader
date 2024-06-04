namespace Kader_System.Domain.DTOs.Request.HR
{
    public class UpdateVacationRequest
    {
        public int ApplyAfterMonth { get; set; }
        public int TotalBalance { get; set; }
        public bool CanTransfer { get; set; }
        public required string NameEn { get; set; }
        public required string NameAr { get; set; }
        public int VacationTypeId { get; set; }
        public ICollection<UpdateVacationDistribution> VacationDistributions { get; set; }
    }

    public class UpdateVacationDistribution
    {
        public int Id { get; set; }
        public required string NameEn { get; set; }
        public required string NameAr { get; set; }
        public int DaysCount { get; set; }
        public int SalaryCalculatorId { get; set; }
        public int VacationId { get; set; }
    }
}
