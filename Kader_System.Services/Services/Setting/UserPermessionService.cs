using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.Auth;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Kader_System.Domain.Constants.SD.ApiRoutes;

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
            var user = _context.Users.AsNoTracking()
                            .Where(x => x.Id == userId)
                            .Select(u => new
                            {
                                u.UserName,
                                u.TitleId,
                                u.CurrentTitleId
                            }).FirstOrDefault();

            var titleIds = user.TitleId?.Split(',', StringSplitOptions.None)
                     .Where(id => int.TryParse(id, out _))
                     .Select(int.Parse)
                     .ToList();

            var titles = _context.Titles
            .Where(title => titleIds.Contains(title.Id))
                            .Select(title => new TitleLookups
                            {
                                Id = title.Id,
                                TitleName = lang == Localization.English ? title.TitleNameEn : title.TitleNameAr
                            }).ToList();

            var finalResult = results.Select(x => new DTOSPGetUserPermissionsBySubScreen
            {

                actions = x.actions,
                cat_id = x.cat_id,
                cat_title = x.cat_title,
                main_id = x.main_id,
                main_img = x.main_image,
                main_title = x.main_title,
                permissions = x.permissions.CreateNewPermission(x.actions, x.permissions, x.permissions),
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
                UserName = user.UserName,
                titles = titles,
                CurrentTitleId = user.CurrentTitleId
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
