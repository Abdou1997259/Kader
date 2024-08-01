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
    public class PermessionStructureService(KaderDbContext _context, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : IPermessionStructureService
    {
        public async Task<Response<DTOUserPermessionsForUser>> GetAllPermessionStructureForUser(string lang)
        {
            var actions = _context.Actions.AsNoTracking();

            var per = await (from q in _context.ScreenActions.AsNoTracking()
                             join s in _context.SubMainScreens on q.ScreenId equals s.Id
                             join sc in _context.MainScreens on s.ScreenCatId equals sc.Id
                             join ms in _context.MainScreenCategories on sc.MainScreenId equals ms.Id
                             select new DTOUserPermessionsForUser
                             {
                                 sub_id = q.ScreenId,
                                 sub_title = lang == Localization.Arabic ? s.Screen_sub_title_ar : s.Screen_sub_title_en,
                                 cat_id = s.ScreenCatId,
                                 cat_title = lang == Localization.Arabic ? sc.Screen_cat_title_ar : sc.Screen_cat_title_en,
                                 main_id = sc.MainScreenId,
                                 main_title = lang == Localization.Arabic ? ms.Screen_main_title_ar : ms.Screen_main_title_en,
                                 screen_code = s.ScreenCode,
                                 url = s.Url,
                                 actions = actions.Select(x => x.Id).ToArray(), // This part remains unchanged
                             }).ToListAsync();
     
    

    

            // Perform the dictionary creation on the client side
            var result = per.Select(x => new DTOUserPermessionsForUser
            {
                sub_id = x.sub_id,
                sub_title = x.sub_title,
                cat_id = x.cat_id,
                cat_title = x.cat_title,
                main_id = x.main_id,
                main_title = x.main_title,
                screen_code = x.screen_code,
                url = x.url,
                actions = x.actions,
                permissions = (lang == Localization.Arabic ?
                               actions.ToDictionary(a => a.Name, a => true) :
                               actions.ToDictionary(a => a.NameInEnglish, a => true))
            }).ToList();
            return new Response<DTOUserPermessionsForUser>()
            {
                Check = true,
                DataList = result,
                Msg = ""
            };
        }

        public async Task<Response<DTOUserPermessionsForPofile>> GetAllPermessionStructureForProfile(string lang)
        {
            var actions = _context.Actions.AsNoTracking();

            var per = await(from q in _context.ScreenActions.AsNoTracking()
                            join s in _context.SubMainScreens on q.ScreenId equals s.Id
                            join sc in _context.MainScreens on s.ScreenCatId equals sc.Id
                            join ms in _context.MainScreenCategories on sc.MainScreenId equals ms.Id
                            select new DTOUserPermessionsForPofile
                            {
                                sub_id = q.ScreenId,
                                sub_title = lang == Localization.Arabic ? s.Screen_sub_title_ar : s.Screen_sub_title_en,
                                cat_id = s.ScreenCatId,
                                cat_title = lang == Localization.Arabic ? sc.Screen_cat_title_ar : sc.Screen_cat_title_en,
                                main_id = sc.MainScreenId,
                                main_title = lang == Localization.Arabic ? ms.Screen_main_title_ar : ms.Screen_main_title_en,
                                screen_code = s.ScreenCode,
                                url = s.Url,
                                actions = string.Join(",",actions.Select(x => x.Id.ToString()).ToList()), // This part remains unchanged
                            }).ToListAsync();

            // Perform the dictionary creation on the client side
            var result = per.Select(x => new DTOUserPermessionsForPofile
            {
                sub_id = x.sub_id,
                sub_title = x.sub_title,
                cat_id = x.cat_id,
                cat_title = x.cat_title,
                main_id = x.main_id,
                main_title = x.main_title,
                screen_code = x.screen_code,
                url = x.url,    
                actions = x.actions,
                permissions = [.. actions.Select(x =>x.Id)]
            }).ToList();
            return new Response<DTOUserPermessionsForPofile>()
            {
                Check = true,
                DataList = result,
                Msg = ""
            };
        }
    }

}
