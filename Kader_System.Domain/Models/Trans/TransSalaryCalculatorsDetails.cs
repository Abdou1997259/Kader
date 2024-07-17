namespace Kader_System.Domain.Models.Trans
{
    [Table("trans_salary_calculators_details")]
    public class TransSalaryCalculatorDetail : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; set; }
      
        public double NetSalary { get; set; }

        public double BasicSalary { get; set; }
        public double TotalDeductions {  get; set; } 
        public double TotalBenefits { get; set; }
        public double TotalLoans { get; set; }  
        public double TotalAllownces { get; set; }
        public double Total { get; set; }
        public int TransferId { get; set; } 
        public int TransSalaryCalculatorsId { get; set; }

        [ForeignKey(nameof(TransSalaryCalculatorsId))]
        public TransSalaryCalculator TransSalaryCalculators { get; set; } = default!;


    }
}
