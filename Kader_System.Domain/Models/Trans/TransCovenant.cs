namespace Kader_System.Domain.Models.Trans;

[Table("trans_covenants")]
public class TransCovenant : BaseEntity
{
    [Key]
    public int id { get; set; }
    public required string name_en { get; set; }
    public required string name_ar { get; set; }
    public DateOnly date { get; set; }
    public string? notes { get; set; }
    public double amount { get; set; }

    public int employee_id { get; set; }
    [ForeignKey(nameof(employee_id))]
    public HrEmployee employee { get; set; } = default!;
    public int company_id { get; set; }
    public string? attachment { get; set; }

}
