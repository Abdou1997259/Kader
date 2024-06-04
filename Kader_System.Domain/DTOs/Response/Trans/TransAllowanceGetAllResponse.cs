namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class TransAllowanceGetAllResponse : PaginationData<TransAllowanceData>
    {

    }

    public class TransAllowanceData
    {
        public int Id { get; set; }
        public DateOnly ActionMonth { get; set; }
        public DateTime? AddedOn { get; set; }
        public string? Notes { get; set; }
        public double Amount { get; set; }

        public int SalaryEffectId { get; set; }

        public string SalaryEffect { get; set; } 

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } 

        public int AllowanceId { get; set; }

        public string AllowanceName { get; set; } 
        public string AddedBy { get; set; } 
    }
}
