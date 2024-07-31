using Kader_System.DataAccesss.DbContext;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.Setting
{
    public class PermessionStructureService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper, KaderDbContext context) : IPermessionStructureService
    {
        public async Task<Response<object>> GetAllPermessionStructure(string lang)
        {
            var actions = await unitOfWork.ActionsRepo.GetAllAsync();
            var per = (from q in await unitOfWork.PermessionStructure.GetAllAsync()
                       select new
                       {
                           sub_id = q.ScreenId,
                           sub_title = lang == Localization.Arabic ? q.ScreenSub.Screen_sub_title_ar : q.ScreenSub.Screen_sub_title_en,
                           cat_id = q.ScreenSub.ScreenCatId,
                           cat_title = lang == Localization.Arabic ? q.ScreenSub.ScreenCat.Screen_cat_title_ar : q.ScreenSub.ScreenCat.Screen_cat_title_en,
                           main_id = q.ScreenSub.ScreenCat.MainScreenId,
                           main_title = lang == Localization.Arabic ? q.ScreenSub.ScreenCat.screenCat.Screen_main_title_ar : q.ScreenSub.ScreenCat.screenCat.Screen_main_title_en,
                           screen_code = q.ScreenSub.Screen_Code,
                           actions = actions.Select(x => x.Id).ToArray(),
                           permissions = lang == Localization.Arabic ? actions.Select(x =>x.Name).ToArray() : actions.Select(x => x.NameInEnglish).ToArray()
                       }).ToList();

            return new Response<object>()
            {
                Check = true,
                DynamicData = per,
                Msg = ""
            };
        }
    }
}
