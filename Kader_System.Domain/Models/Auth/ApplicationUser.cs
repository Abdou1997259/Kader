namespace Kader_System.Domain.Models.Auth;

public class ApplicationUser : IdentityUser, IBaseEntity
{
   
    public string FullName { get; set; }
   
    public string  TitleId { get; set; }
    public int JobId { get; set; }
    public string CompanyId { get; set; }
    public int CurrentCompanyId { get; set; }
    public int CurrentTitleId { get; set; }
    public int FinancialYear { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? Add_date { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public string? Added_by { get; set; }
    public string? UpdateBy { get; set; }
    public string? DeleteBy { get; set; }
    public bool IsActive { get; set; } = true;

    public string? ImagePath { get; set; }

    public required string VisiblePassword { get; set; }

    public ICollection<ApplicationUserDevice> ListOfDevices { get; set; } = new HashSet<ApplicationUserDevice>();
    public ICollection<AuthRefreshToken> RefreshTokens { get; set; } = new HashSet<AuthRefreshToken>();
}
