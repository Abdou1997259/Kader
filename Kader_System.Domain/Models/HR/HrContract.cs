namespace Kader_System.Domain.Models.HR;

[Table("hr_contracts")]
public class HrContract : BaseEntity
{
    [Key]
    public int id { get; set; }
    public double total_salary { get; set; }
    public double fixed_salary { get; set; }
    public double housing_allowance { get; set; }
    public DateOnly start_date { get; set; }
    public DateOnly end_date { get; set; }
    public string file_name { get; set; }


    public int employee_id { get; set; }
    [ForeignKey(nameof(employee_id))]
    public HrEmployee employee { get; set; } = default!;
    public int company_id { get; set; }
    [ForeignKey(nameof(Added_by))]
    public ApplicationUser? User { get; set; }
    public ICollection<HrContractAllowancesDetail> list_of_allowances_details { get; set; } = [];
}
