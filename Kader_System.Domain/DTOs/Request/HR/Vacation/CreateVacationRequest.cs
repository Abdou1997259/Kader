namespace Kader_System.Domain.DTOs.Request.HR.Vacation
{
    public class CreateVacationRequest
    {
        public int ApplyAfterMonth { get; set; }
        public int TotalBalance { get; set; }
        public bool CanTransfer { get; set; }
        public required string NameEn { get; set; }
        public required string NameAr { get; set; }
        public int VacationTypeId { get; set; }
        public ICollection<CreateVacationDistribution> VacationDistributions { get; set;}
    }
}
