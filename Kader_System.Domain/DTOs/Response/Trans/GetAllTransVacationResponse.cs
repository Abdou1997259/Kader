namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetAllTransVacationResponse:PaginationData<TransVacationData>
    {
    }

    public class TransVacationData
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
       
        public DateTime? AddedDate { get; set; }
        public DateOnly EndDate { get; set; }
        public double DaysCount { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int VacationId { get; set; }
        public string VacationName { get; set; } 
        public string? Notes { get; set; }
        public string? AddedBy { get; set; }
        public string? VacationType { get; set; }

    }
}
