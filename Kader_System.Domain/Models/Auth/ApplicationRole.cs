namespace Kader_System.Domain.Models.Auth;

public class ApplicationRole : IdentityRole, IBaseEntity
{
    public string Title_name_ar { get; set; } = string.Empty;        
    public bool IsDeleted { get; set; }
    public DateTime? Add_date { get; set; } 
    public DateTime? UpdateDate { get; set; } 
    public DateTime? DeleteDate { get; set; }
    public string? Added_by { get; set; }
    public string? UpdateBy { get; set; }
    public string? DeleteBy { get; set; }
    public bool IsActive { get; set; } = true;
}
