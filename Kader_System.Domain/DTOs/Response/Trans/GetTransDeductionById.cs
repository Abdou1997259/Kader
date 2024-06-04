namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetTransDeductionById
    {
        public int Id { get; set; }
        public DateOnly ActionMonth { get; set; }
        public DateTime? AddedOn { get; set; }
        public int AmountTypeId { get; set; }
        public int SalaryEffectId { get; set; }
        public string SalaryEffect { get; set; } = default!;
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = default!;
        public int DeductionId { get; set; }
        public string DeductionName { get; set; } = default!;
        public string? Notes { get; set; }
        public string? AttachmentFile { get; set; }
        public double Amount { get; set; }
        public string discount_type { get; set; }
    }
}
