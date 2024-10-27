namespace Kader_System.Domain.Models.Trans
{
    [Table("trans_payment_salary")]
    public class PaymentSalary : BaseEntity
    {
        public int id { get; set; }
        public double total_amount { get; set; }
        public int employee_number { get; set; }

        public DateOnly? disposable_date { get; set; }

        public string? disposable_user { get; set; }
        [ForeignKey(nameof(disposable_user))]
        public ApplicationUser? User { get; set; } = default!;
        public int company_id { get; set; }
        [ForeignKey(nameof(company_id))]
        public HrCompany company { get; set; } = default!;

        public int? transSalary_calculator_id { get; set; }
        [ForeignKey(nameof(transSalary_calculator_id))]
        public TransSalaryCalculator? TransSalaryCalculator { get; set; } = default!;
    }
}
