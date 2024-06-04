namespace Kader_System.Domain.Models;

public abstract class BaseEntity : IBaseEntity
{
    public bool IsDeleted { get; set; } 
    public DateTime? Add_date { get; set; } 
    public DateTime? UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }

    public string? Added_by { get; set; }
    public string? UpdateBy { get; set; }
    public string? DeleteBy { get; set; }
    public bool IsActive { get; set; } = true;
}
