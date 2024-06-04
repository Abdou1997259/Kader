namespace Kader_System.Services.IServices.Auth;

public interface IDbInitSeedsService
{
    Task SeedClaimsForSuperAdmin();
}
