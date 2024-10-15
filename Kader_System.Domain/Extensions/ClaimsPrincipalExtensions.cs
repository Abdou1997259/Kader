namespace Kader_System.Domain.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal) =>
        principal.FindFirstValue(RequestClaims.UserId)!;
    public static int GetCurrentCompany(this ClaimsPrincipal principal)
    {
        var currentCompany = principal.FindFirstValue(RequestClaims.CurrentCompany);

        if (int.TryParse(currentCompany, out var convertedId))
        {
            return convertedId;
        }
        return 0;
    }


    public static string GetCompaines(this ClaimsPrincipal principal) =>
  principal.FindFirstValue(RequestClaims.Company)!;
    public static string GetEmalil(this ClaimsPrincipal principal) =>
principal.FindFirstValue(RequestClaims.Email)!;
    public static string GetMobile(this ClaimsPrincipal principal) =>
principal.FindFirstValue(RequestClaims.Mobile)!;
    public static string GetTitles(this ClaimsPrincipal principal) =>
principal.FindFirstValue(RequestClaims.Titles)!;
    public static string GetImage(this ClaimsPrincipal principal) =>
principal.FindFirstValue(RequestClaims.Image)!;
    public static string GetFullName(this ClaimsPrincipal principal) =>
principal.FindFirstValue(RequestClaims.FullName)!;
    public static string GetCurrentTitle(this ClaimsPrincipal principal) =>
principal.FindFirstValue(RequestClaims.CurrentTitle)!;
    public static string GetRoleId(this ClaimsPrincipal principal) =>
        principal.FindFirstValue(ClaimTypes.Role)!;

    public static string GetRoleName(this ClaimsPrincipal principal) =>
        principal.Claims.Where(a => a.Type == ClaimTypes.Role).FirstOrDefault()!.Value;

    public static List<string> GetRolesNames(this ClaimsPrincipal principal) =>
        principal.Claims.Where(a => a.Type == ClaimTypes.Role).Select(x => x.Value).ToList();

    //public static string GetRoleName(this ClaimsPrincipal principal) =>
    //    principal.Claims.Where(a => a.Type == ClaimTypes.Role).FirstOrDefault()!.Value;
}
