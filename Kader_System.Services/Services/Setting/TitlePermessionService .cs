using Kader_System.DataAccesss.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.Setting
{
    public class TitlePermessionService(KaderDbContext _context, IPermessionStructureService permession) : ITitlePermessionService
    {
        public async Task<ResponseForPermession<DTOSPGetTitlePermissionsBySubScreen>> GetTitlePermissionsBySubScreen(int titleId, string lang)
        {
            var userIdParameter = new SqlParameter("@TitleId", titleId);
            var langParameter = new SqlParameter("@Lang", lang);

            var results = await _context.SPUserPermissionsBySubScreens
                .FromSqlRaw("EXEC SP_GetTitlePermissionsBySubScreen @TitleId, @Lang", userIdParameter, langParameter)
                .AsNoTracking()
                .ToListAsync();
            var titles = await _context.Titles.FirstOrDefaultAsync(x=>x.Id == titleId);
            var finalResult = results.Select(x => new DTOSPGetTitlePermissionsBySubScreen
            {
                actions = x.actions,
                cat_id = x.cat_id,
                cat_title = x.cat_title,
                main_id = x.main_id,
                main_img = x.main_image,
                main_title = x.main_title,
                permissions = x.permissions.CreateNewPermission(x.actions, x.permissions, x.PermissionNames),
                screen_code = x.screen_code,
                sub_id = x.sub_id,
                sub_title = x.sub_title,
                url = x.url
            });


            return new ResponseForPermession<DTOSPGetTitlePermissionsBySubScreen>()
            {
                Check = true,
                DynamicData = finalResult,
                Error = "",
                title_name_ar = titles.TitleNameAr,
                title_name_en = titles.TitleNameEn,
            };
        }
    }
}
