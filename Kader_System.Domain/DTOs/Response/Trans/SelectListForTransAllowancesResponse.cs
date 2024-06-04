namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class SelectListForTransAllowancesResponse
    {
        public int Id { get; set; }
        public DateTime? AddedOn { get; set; }
        public DateOnly ActionMonth { get; set; }
        public string? Notes { get; set; }
        public double Amount { get; set; }

        public int SalaryEffectId { get; set; }

        public string SalaryEffect { get; set; } = default!;

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = default!;

        public int AllowanceId { get; set; }

        public string AllowanceName { get; set; } = default!;
    }
}
