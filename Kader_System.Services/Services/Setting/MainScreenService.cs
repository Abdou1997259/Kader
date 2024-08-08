
using Kader_System.DataAccesss.DbContext;
using Kader_System.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.Setting;

public class MainScreenService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper ,KaderDbContext context,IFileServer fileServer) : IMainScreenService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IMapper _mapper = mapper;
    private readonly IFileServer _fileServer = fileServer;

    private readonly KaderDbContext _dbContext = context;

  


    #region Main screen

    public async Task<Response<IEnumerable<StSelectListForMainScreenResponse>>> ListOfMainScreensAsync(string lang)
    {
        var result =
                await _unitOfWork.MainScreens.GetSpecificSelectAsync(null!,
                select: x => new StSelectListForMainScreenResponse
                {
                    Id = x.Id,
                    Title = lang == Localization.Arabic ? x.Screen_main_title_ar : x.Screen_main_title_en,
                    //Main_id = x.ma,
                    Main_title = lang == Localization.Arabic ? x.Screen_main_title_ar : x.Screen_main_title_en,
                    Main_image = x.Screen_main_image != null ? string.Concat(ReadRootPath.SettingImagesPath, x.Screen_main_image) : string.Empty
                }, orderBy: x =>
                  x.OrderByDescending(x => x.Id));

        if (!result.Any())
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new()
            {
                Data = [],
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        return new()
        {
            Data = result,
            Check = true
        };
    }

    public async Task<Response<StGetAllMainScreensResponse>> GetAllMainScreensAsync(string lang, StGetAllFiltrationsForMainScreenRequest model)
    {
        Expression<Func<StMainScreen, bool>> filter = x => x.IsDeleted == model.IsDeleted;

        var result = new StGetAllMainScreensResponse
        {
            TotalRecords = await _unitOfWork.MainScreens.CountAsync(filter: filter),

            Items = (await _unitOfWork.MainScreens.GetSpecificSelectAsync(filter: filter,
                 take: model.PageSize,
                 skip: (model.PageNumber - 1) * model.PageSize,
                 select: x => new MainScreenData
                 {
                     Id = x.Id,
                     Screen_main_title = lang == Localization.Arabic ? x.Screen_main_title_ar : x.Screen_main_title_en,
                     Screen_main_image = x.Screen_main_image != null ? string.Concat(ReadRootPath.SettingImagesPath, x.Screen_main_image) : string.Empty,
                     //Title = lang == Localization.Arabic ? x.Screen_main_title_ar : x.Screen_main_title_en
                 }, orderBy: x =>
                   x.OrderBy(x => x.Order))).ToList()
        };
        if (result.TotalRecords is 0)
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new()
            {
                Data = new()
                {
                    Items = []
                },
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        return new()
        {
            Data = result,
            Check = true
        };
    }
    public async Task<Response<StMainScreen>> RestoreMainScreenAsync(int id)
    {
        var obj = await _unitOfWork.MainScreens.GetByIdAsync(id);
        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Allowance]);

            return new()
            {
                Data = null,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        await _unitOfWork.MainScreens.SoftDeleteAsync(obj, "IsDeleted", false);
        //obj.IsDeleted = false;
        //_unitOfWork.Allowances.Update(obj);
        //await _unitOfWork.CompleteAsync();
        return new()
        {
            Check = true,
            Data = obj,
            Msg = _sharLocalizer[Localization.Restored]
        };


    }
    public async Task<Response<StCreateMainScreenRequest>> CreateMainScreenAsync(StCreateMainScreenRequest model, string appPath, string moduleName)
    {
         var mainScreenmap = _mapper.Map<StMainScreen>(model);

         bool exists = await _unitOfWork.MainScreens.ExistAsync(x => x.Screen_main_title_en.Trim() == model.Screen_main_title_ar);

        if (exists)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.IsExist], _sharLocalizer[Localization.MainScreen]);
            return new Response<StCreateMainScreenRequest>
            {
                Error = resultMsg,
                Msg = resultMsg
            };
        }

         mainScreenmap.Screen_main_image = (model.Screen_main_image == null || model.Screen_main_image.Length == 0) ? null
            : await _fileServer.UploadFile(appPath, moduleName, model.Screen_main_image);

         await _unitOfWork.MainScreens.AddAsync(mainScreenmap);

         await _unitOfWork.CompleteAsync();

         return new Response<StCreateMainScreenRequest>
        {
            Msg = _sharLocalizer[Localization.Done],
            Check = true,
            Data = model
        };

    }



    public async Task<Response<StGetMainScreenByIdResponse>> GetMainScreenByIdAsync(int id)
    {
     
        var obj = await _unitOfWork.MainScreens.GetFirstOrDefaultAsync(x => x.Id == id);

        if (obj is null)
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new()
            {
                Data = new(),
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        return new()
        {
            Data = new()
            {
                Id = id,
                Screen_cat_title_ar = obj.Screen_main_title_ar,
                Screen_cat_title_en = obj.Screen_main_title_en,
                Main_title_ar = obj.Screen_main_title_ar,
                Main_title_en = obj.Screen_main_title_en
            },
            Check = true
        };
    }

    public async Task<Response<StUpdateMainScreenRequest>> UpdateMainScreenAsync(int id, StUpdateMainScreenRequest model)
    {
        var obj = await _unitOfWork.MainScreens.GetFirstOrDefaultAsync(x => x.Id == model.id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.MainScreen]);

            return new()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        obj.Screen_main_title_ar = model.Screen_main_title_ar;
        obj.Screen_main_title_en = model.Screen_main_title_en;
 
        _unitOfWork.MainScreens.Update(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data = model,
            Msg = _sharLocalizer[Localization.Updated]
        };
    }

    public Task<Response<string>> UpdateActiveOrNotMainScreenAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<string>> DeleteMainScreenAsync(int id)
    {
        var obj = await _unitOfWork.MainScreens.GetByIdAsync(id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.MainScreen]);

            return new()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        string err = _sharLocalizer[Localization.Error];

        _unitOfWork.MainScreens.Remove(obj);

        bool result = await _unitOfWork.CompleteAsync() > 0;

        if (!result)
            return new()
            {
                Check = false,
                Data = string.Empty,
                Error = err,
                Msg = err
            };
        return new()
        {
            Check = true,
            Data = string.Empty,
            Msg = _sharLocalizer[Localization.Deleted]
        };
    }

    public async Task<Response<GetAllStMainScreen>> GetMainScreensWithRelatedDataAsync(string lang
        )
    {
        var mainScreens = await context.MainScreenCategories.
            Include(ms => ms.CategoryScreen).
            ThenInclude(cs => cs.StScreenSub).
            ToListAsync();

        var subs =await _unitOfWork.SubMainScreenActions.GetAllAsync();
        var permision = await _unitOfWork.TitlePermissionRepository.GetAllAsync();
        var mains = await _unitOfWork.MainScreens.GetAllAsync() ;



        var ChildScreens = mainScreens.Select(ms => new GetAllStMainScreen
        {

            main_title = lang == "en" ? ms.Screen_main_title_en : ms.Screen_main_title_ar,
            main_image = ms.Screen_main_image,
            cats = ms.CategoryScreen.Select(x => new GetAllStMainScreenCat
            {
                Id = x.Id,
                title = lang == "en" ? x.Screen_cat_title_en : x.Screen_cat_title_ar,
                main_id = x.MainScreenId ,
                subs = x.StScreenSub.Select(k => new GetAllStScreenSub
                {
                    Sub_Id = k.Id,
                    Screen_CatId = k.ScreenCatId,
                    cat_Title = lang == "en" ? x.Screen_cat_title_en : x.Screen_cat_title_en,
                    main_title = mains.Where(u => x.Id == k.ScreenCatId).Select(c => c.Name).FirstOrDefault(),
                    main_id = mains.Where(u => x.Id == k.ScreenCatId).Select(c => c.Id).FirstOrDefault(),
                    sub_title = lang == "en" ? k.Screen_sub_title_en : k.Screen_sub_title_ar,
                    url = k.Url,
                    sub_image = k.Screen_sub_image,
                    screen_code = k.ScreenCode,
                    actions = subs.Where(x => x.ScreenSubId == k.Id).Select(x => x.ActionId).ToList().Concater(),
                    permissions = permision.Where(x => x.SubScreenId == k.Id).Select(p => p.Id).ToList().Concater()
                }).ToList()
            }).ToList()
        }).ToList();

        foreach (var mainScreen in ChildScreens)
        {
            Console.WriteLine($"Main Screen Title: {mainScreen.main_title}");
            Console.WriteLine($"Main Screen Image: {mainScreen.main_image}");

            foreach (var categoryScreen in mainScreen.cats)
            {
                Console.WriteLine($"Category ID: {categoryScreen.Id}");
                Console.WriteLine($"Category Screen Title: {categoryScreen.title}");
                Console.WriteLine($"Category Screen Title: {categoryScreen.title}");
 
                foreach (var screenSub in categoryScreen.subs)
                {
                    Console.WriteLine($"Screen Sub ID: {screenSub.Sub_Id}");
                    Console.WriteLine($"Screen Sub Title: {screenSub.sub_title}");
                    Console.WriteLine($"Screen Screen_CatId {screenSub.Screen_CatId}");
                    Console.WriteLine($"Screen Main Title: {screenSub.main_title}");
                    Console.WriteLine($"Category Screen Title: {screenSub.cat_Title}");
                    Console.WriteLine($"Actions: {screenSub.actions}");
                    Console.WriteLine($"URL: {screenSub.url}");
                    Console.WriteLine($"Screen Sub Image: {screenSub.sub_image}");
                    Console.WriteLine($"Screen Code: {screenSub.screen_code}");
                    Console.WriteLine();
                }
            }
        }


      



        return new Response<GetAllStMainScreen>()
        {
            Check = true,
            DataList = ChildScreens,
            Error = "",
            Msg = "",
        };
    }

   







    #endregion
}
