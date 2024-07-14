namespace Kader_System.Domain.Models.Trans
{
    [Table("trans_salary_calculators")]
    public class TransSalaryCalculator : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateOnly DocumentDate { get; set; }
        public Status Status { get; set; }
        public string? Description { get; set; }

        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? ManagementId { get; set; }
        public bool IsMigrated { get; set; }
        public ICollection<TransSalaryCalculatorDetail>? TransSalaryCalculatorsDetails { get; set; } = [];


    }
}
