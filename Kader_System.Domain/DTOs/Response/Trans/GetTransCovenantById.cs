namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetTransCovenantById
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public DateTime? AddedOn { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = default!;
        public double Amount { get; set; }
        public string? Notes { get; set; }
    }
}
