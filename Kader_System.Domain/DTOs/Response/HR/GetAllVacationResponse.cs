namespace Kader_System.Domain.DTOs.Response.HR
{
    public class GetAllVacationResponse : PaginationData<VacationData>
    {

    }

    public class VacationData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? AddedBy { get; set; }=string.Empty;
        public int ApplyAfterMonth { get; set; }
        public int TotalBalance { get; set; }
        public bool CanTransfer { get; set; }
        public string VacationType { get; set; } = string.Empty;
        public int? EmployeesCount { get; set; }
    }
}
