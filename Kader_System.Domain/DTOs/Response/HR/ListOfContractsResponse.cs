namespace Kader_System.Domain.DTOs.Response.HR
{
    public class ListOfContractsResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public double TotalSalary { get; set; }
        public double FixedSalary { get; set; }
        public double HousingAllowance { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
