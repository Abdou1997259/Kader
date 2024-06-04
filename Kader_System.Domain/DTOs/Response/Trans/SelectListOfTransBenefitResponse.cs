namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class SelectListOfTransBenefitResponse
    {
        public int Id { get; set; }
        public DateOnly ActionMonth { get; set; }
        public DateTime? AddedOn { get; set; }
        public int AmountTypeId { get; set; }
        public string ValueTypeName { get; set; }
        public double Amount { get; set; }
        public int SalaryEffectId { get; set; }
        public string SalaryEffect { get; set; } = default!;
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = default!;
        public int BenefitId { get; set; }
        public string BenefitName { get; set; } = default!;
        public string? Notes { get; set; }
    }
}
