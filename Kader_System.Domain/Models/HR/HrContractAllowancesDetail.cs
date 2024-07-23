namespace Kader_System.Domain.Models.HR;
/// <summary>
/// بدلات العقد
/// </summary>
[Table("hr_contract_allowances_details")]
public class HrContractAllowancesDetail : BaseEntity
{
    [Key]
    public int Id { get; set; }
   

    public int AllowanceId { get; set; }
    [ForeignKey(nameof(AllowanceId))]
    public HrAllowance Allowance { get; set; } = default!;
    public double Value { get; set; }
    public bool IsPercent { get; set; }
    
    public int ContractId { get; set; }
    [ForeignKey(nameof(ContractId))]
    public HrContract Contract { get; set; } = default!;
}
