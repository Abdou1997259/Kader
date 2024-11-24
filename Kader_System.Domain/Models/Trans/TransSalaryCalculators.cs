namespace Kader_System.Domain.Models.Trans
{
    [Table("trans_salary_calculators")]
    public class TransSalaryCalculator : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateOnly CalculationDate { get; set; }

        public bool IsMigrated { get; set; }
        public int CompanyId { get; set; }
        public ICollection<TransSalaryCalculatorDetail>? TransSalaryCalculatorsDetails { get; set; } = [];


    }
}
