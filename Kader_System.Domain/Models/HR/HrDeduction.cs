﻿namespace Kader_System.Domain.Models.HR;

[Table("hr_deductions")]
public class HrDeduction : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Name_ar { get; set; }
    public required string Name_en { get; set; }
    [ForeignKey(nameof(Added_by))]
    public ApplicationUser User { get; set; }
    public long? AccountNo { get; set; }
}
