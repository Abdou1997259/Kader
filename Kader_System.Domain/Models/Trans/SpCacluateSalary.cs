using Microsoft.EntityFrameworkCore;

namespace Kader_System.Domain.Models.Trans;

[Keyless]
public class SpCacluateSalary
{

    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string JournalType { get; set; }
    public double CalculatedSalary { get; set; }

    public DateOnly JournalDate { get; set; }
}

