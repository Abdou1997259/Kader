using Kader_System.Services.IServices.AppServices;

namespace Kader_System.Services.Services.Setting
{
    public class GetAllScreensService(IUnitOfWork unitOfWork, IFileServer _fileServer, IStringLocalizer<SharedResource>
        sharLocalizer) : IGetAllScreensService
    {

        public async Task<Response<ScreenLookups>> GetAllScreensAsync(string lang)
        {
            var mains = await unitOfWork.MainScreens.GetAllAsync();
            List<ScreenMainLookup> lookupsForMain = mains.Where(x => !x.IsDeleted).OrderBy(x => x.Order).Select(x => new ScreenMainLookup
            {
                Id = x.Id,
                main_image = _fileServer.GetFilePath(Modules.Setting, x.Screen_main_image),
                main_title = Localization.Arabic == lang ? x.Screen_main_title_ar : x.Screen_main_title_en
            }).ToList();

            List<ScreenCatLookup> lookupsForCat = (await unitOfWork.ScreenCategories.GetSpecificSelectAsync(x => !x.IsDeleted, select: x => new ScreenCatLookup
            {
                Id = x.Id,
                cat_title = Localization.Arabic == lang ? x.Screen_cat_title_ar : x.Screen_cat_title_en,
                main_id = x.ScreenMain.Id,
                Main_Title = Localization.Arabic == lang ? x.ScreenMain.Screen_main_title_ar : x.ScreenMain.Screen_main_title_en,
                main_image = _fileServer.GetFilePath(Modules.Setting, x.ScreenMain.Screen_main_image)

            }, includeProperties: "ScreenMain", orderBy: x => x.OrderBy(s => s.Order))).ToList();


            List<ScreenSubLookup> lookupsForsub = (await unitOfWork.SubScreens.GetSpecificSelectAsync(x => !x.IsDeleted, select: x => new ScreenSubLookup
            {
                sub_id = x.Id,
                cat_id = x.ScreenCat.Id,
                Cat_title = Localization.Arabic == lang ? x.ScreenCat.Screen_cat_title_ar : x.ScreenCat.Screen_cat_title_en,
                sub_title = Localization.Arabic == lang ? x.Screen_sub_title_ar : x.Screen_sub_title_en,
                screen_code = x.ScreenCode,
                url = x.Url

            }, includeProperties: "ScreenCat", orderBy: x => x.OrderBy(s => s.Order))).ToList();

            return new()
            {
                Data = new ScreenLookups
                {
                    cat = lookupsForCat,
                    main = lookupsForMain,
                    sub = lookupsForsub
                },
                Check = true,


            };
        }
    }
}
