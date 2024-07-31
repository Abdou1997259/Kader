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
            var test = await unitOfWork.PermessionStructure.GetAllAsync();
            var per = (from q in await unitOfWork.PermessionStructure.GetAllAsync()
                       select new
                       {
                           sub_id = q.ScreenId,
                           sub_title = lang == Localization.Arabic ? q.Screen.Screen_sub_title_ar : q.Screen.Screen_sub_title_en,
                           cat_id = q.Screen.ScreenCatId,
                           cat_title = lang == Localization.Arabic ? q.Screen.ScreenCat.Screen_cat_title_ar : q.Screen.ScreenCat.Screen_cat_title_en,
                           main_id = q.Screen.ScreenCat.MainScreenId,
                           main_title = lang == Localization.Arabic ? q.Screen.ScreenCat.screenCat.Screen_main_title_ar : q.Screen.ScreenCat.screenCat.Screen_main_title_en,
                           screen_code = q.Screen.ScreenCode,
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
