

namespace Kader_System.DataAccess.DesginPatterns
{
    public static class UserPermissionFactory
    {
        public static async Task<userPerrmissionLoginContext> CreatePermissionsUserStrategy(KaderDbContext db, string userid, int title, string lang)
        {
            userPerrmissionLoginContext userLoginContext = new userPerrmissionLoginContext();

            // ممكن هنا تعمل اليوزر سوبر ادمن عن طريق انك تضيف id في ال  condition 

            if (await db.Users.AnyAsync(x => x.Id == userid && x.IsAdmin))
            {
                // this Super user get general permissions
                userLoginContext.SetPermissionStrategy(new SuperAdminUserStrategy(db, lang));
                return userLoginContext;

            }
            else
            {
                // this ordinary user get specific permissions
                userLoginContext.SetPermissionStrategy(new UserStrategy(db, userid, title, lang));
                return userLoginContext;
            }


        }
    }
}
