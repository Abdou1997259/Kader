namespace Kader_System.Domain.Models.Auth;

public class ApplicationUser : IdentityUser, IBaseEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? Add_date { get; set; }    
    public DateTime? UpdateDate { get; set; }    
    public DateTime? DeleteDate { get; set; }
    public string? Added_by { get; set; }
    public string? UpdateBy { get; set; }
    public string? DeleteBy { get; set; }
    public bool IsActive { get; set; } = true;

    public required string VisiblePassword { get; set; }

    public ICollection<ApplicationUserDevice> ListOfDevices { get; set; } = new HashSet<ApplicationUserDevice>();
    public ICollection<AuthRefreshToken> RefreshTokens { get; set; } = new HashSet<AuthRefreshToken>();
}
