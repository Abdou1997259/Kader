namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetTransBenefitById
    {
        public int Id { get; set; }
        public DateOnly ActionMonth { get; set; }
        public DateTime? AddedOn { get; set; }
        public int increase_type_id { get; set; }
        public string increase_type { get; set; }
        public int SalaryEffectId { get; set; }
        public string benefits_type { get; set; } = default!;
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = default!;
        public int BenefitId { get; set; }
        public string BenefitName { get; set; } = default!;
        public string? Notes { get; set; }
        public string? AttachmentFile { get; set; }

        public double Amount { get; set; }

    }
}
