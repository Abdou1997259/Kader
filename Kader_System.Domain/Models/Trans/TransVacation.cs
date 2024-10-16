﻿namespace Kader_System.Domain.Models.Trans;

[Table("trans_vacations")]
public class TransVacation : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public double? SalaryAmount { get; set; }

    public double DaysCount { get; set; }
    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public HrEmployee Employee { get; set; } = default!;

    public int VacationId { get; set; }
    [ForeignKey(nameof(VacationId))]
    public HrVacationDistribution Vacation { get; set; } = default!;
    public string? Notes { get; set; }
    public string? Attachment { get; set; }

    public int? CalculateSalaryId { get; set; }
    public int? CalculateSalaryDetailsId { get; set; }
    public int CompanyId { get; set; }
}
