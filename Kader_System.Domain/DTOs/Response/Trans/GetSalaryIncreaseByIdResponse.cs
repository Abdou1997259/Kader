

namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetSalaryIncreaseByIdResponse
    {
        public double Amount { get; set; }
        public string? Notes { get; set; }
        public int Increase_type { get; set; }
        public int Employee_id { get; set; }
        public DateOnly TransactionDate { get; set; }
        public double PerviousSalary { get; set; }
        public string EmployeeName { get; set; }    
    }
}
