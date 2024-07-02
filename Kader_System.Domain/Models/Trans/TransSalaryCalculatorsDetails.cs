namespace Kader_System.Domain.Models.Trans
{
    [Table("trans_salary_calculators_details")]
    public class TransSalaryCalculatorDetail : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public double Salary { get; set; }
        public int TransSalaryCalculatorsId { get; set; }

        [ForeignKey(nameof(TransSalaryCalculatorsId))]
        public TransSalaryCalculator TransSalaryCalculators { get; set; } = default!;

    }
}
