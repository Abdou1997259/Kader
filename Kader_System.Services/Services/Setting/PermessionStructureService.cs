using Kader_System.DataAccesss.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.Setting
{
    public class PermessionStructureService(KaderDbContext _context, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper, KaderDbContext context) : IPermessionStructureService
    {
        public async Task<Response<object>> GetAllPermessionStructure(string lang)
        {
            var actions = context.Actions.AsNoTracking().ToList(); // Load actions in memory first

            var per = await (from q in _context.ScreenActions.AsNoTracking()
                             join s in _context.SubMainScreens on q.ScreenId equals s.Id
                             join sc in _context.MainScreens on s.ScreenCatId equals sc.Id
                             join ms in _context.MainScreenCategories on sc.MainScreenId equals ms.Id
                             join up in _context.UserPermissions on s.Id equals up.SubScreenId
                             select new
                             {
                                 sub_id = q.ScreenId,
                                 sub_title = lang == Localization.Arabic ? s.Screen_sub_title_ar : s.Screen_sub_title_en,
                                 cat_id = s.ScreenCatId,
                                 cat_title = lang == Localization.Arabic ? sc.Screen_cat_title_ar : sc.Screen_cat_title_en,
                                 main_id = sc.MainScreenId,
                                 main_title = lang == Localization.Arabic ? ms.Screen_main_title_ar : ms.Screen_main_title_en,
                                 screen_code = s.ScreenCode,
                                 actions = actions.Select(x => x.Id).ToArray(), // This part remains unchanged
                             })
                             .ToListAsync();

            // Perform the dictionary creation on the client side
            var result = per.Select(x => new
            {
                x.sub_id,
                x.sub_title,
                x.cat_id,
                x.cat_title,
                x.main_id,
                x.main_title,
                x.screen_code,
                x.actions,
                permissions = (lang == Localization.Arabic ?
                               actions.ToDictionary(a => a.Name, a => true) :
                               actions.ToDictionary(a => a.NameInEnglish, a => true))
            }).ToList();
            return new Response<object>()
            {
                Check = true,
                DynamicData = result,
                Msg = ""
            };
        }
    }
}
