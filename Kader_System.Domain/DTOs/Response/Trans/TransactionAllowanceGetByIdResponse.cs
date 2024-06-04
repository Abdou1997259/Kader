namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class TransactionAllowanceGetByIdResponse
    {
        public int Id { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateOnly ActionMonth { get; set; }
        public string? Notes { get; set; }
        public string? EmployeeName { get; set; }
        public string? AllowanceName { get; set; }
        public string? SalaryEffectName { get; set; }
        public double Amount { get; set; }
        public int SalaryEffectId { get; set; }
        public int EmployeeId { get; set; }
        public int AllowanceId { get; set; }

    }
}
