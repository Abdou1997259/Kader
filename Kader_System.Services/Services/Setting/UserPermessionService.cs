using Kader_System.DataAccesss.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.Setting
{
    public class UserPermessionService(KaderDbContext _context, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper, IPermessionStructureService permession) : IUserPermessionService
    {
        public async Task<Response<DTOSPGetUserPermissionsBySubScreen>> GetUserPermissionsBySubScreen(string userId, string lang)
        {
            var userIdParameter = new SqlParameter("@UserId", userId);
            var langParameter = new SqlParameter("@Lang", lang);

            var results = await _context.SPUserPermissionsBySubScreens
                .FromSqlRaw("EXEC SP_GetUserPermissionsBySubScreen @UserId, @Lang", userIdParameter, langParameter)
                .AsNoTracking()
                .ToListAsync();

            var userName = await _context.Users.AsNoTracking().Where(x => x.Id == userId).Select(x =>x.UserName).FirstOrDefaultAsync();
            var finalResult = results.Select(x => new DTOSPGetUserPermissionsBySubScreen
            {

                actions = x.actions,
                cat_id = x.cat_id,
                cat_title = x.cat_title,
                main_id = x.main_id,
                main_img = x.main_image,
                main_title = x.main_title,
                permissions = x.permissions.Split(',').Distinct().ToDictionary(
                            perm => perm,
                            perm => true
                      ),
                screen_code = x.screen_code,
                sub_id = x.sub_id,
                sub_title = x.sub_title,
                url = x.url
            });

            return new ResponseWithUser<DTOSPGetUserPermissionsBySubScreen>

            {
                Check = true,
                DynamicData = finalResult,
                Error = "",
                UserName = userName
            };
        }

        private async Task<Dictionary<int, string>> GetActionNamesAsync(string lang)
        {
            var actions = await _context.Actions
                                        .AsNoTracking()
                                        .ToListAsync();
            return actions.ToDictionary(a => a.Id, a => lang == "ar" ? a.Name : a.NameInEnglish);
        }

    }
}
