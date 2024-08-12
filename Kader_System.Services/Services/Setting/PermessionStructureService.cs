using Kader_System.DataAccesss.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.Setting
{
    public class PermessionStructureService(KaderDbContext _context, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : IPermessionStructureService
    {
        public async Task<Response<Dictionary<string, DTOSPGetPermissionsBySubScreen>>> GetPermissionsBySubScreen(string lang)
        {
            var langParameter = new SqlParameter("@Lang", lang);

            var results = await _context.SPPermissionsBySubScreen
                .FromSqlRaw("EXEC SP_GetPermissionsBySubScreen  @Lang", langParameter)
                .AsNoTracking()
                .ToListAsync();



            var finalResult = results.Select(x => new Dictionary<string, DTOSPGetPermissionsBySubScreen>
            {
                {
                    x.screen_code,
                    new DTOSPGetPermissionsBySubScreen
                    {
                        actions = x.actions,
                        cat_id = x.cat_id,
                        cat_title = x.cat_title,
                        main_id = x.main_id,
                        main_img = x.main_image,
                        main_title = x.main_title,
                        permissions = x.permissions?.Splitter(),
                        screen_code = x.screen_code,
                        sub_id = x.sub_id,
                        sub_title = x.sub_title,
                        url = x.url
                    }}}).ToList();

            return new()
            {
                Check = true,
                DynamicData = finalResult,
                Error = "",
            };
        }
    }

}
