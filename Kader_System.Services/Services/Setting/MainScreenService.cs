using AutoMapper.Internal;
using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using System.Linq;

namespace Kader_System.Services.Services.Setting;

public class MainScreenService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer,IPermessionStructureService permessionStructureService, IMapper mapper, KaderDbContext context, IFileServer fileServer) : IMainScreenService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IMapper _mapper = mapper;
    private readonly IFileServer _fileServer = fileServer;
    private readonly IPermessionStructureService _permessionStructureService= permessionStructureService;
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
                  x.OrderBy(x => x.Order));

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
    public async Task<Response<string>> OrderByPattern(int[] orderedIds)
    {
        for (int i = 0; i < orderedIds.Length; i++)
        {
            var id = orderedIds[i];
            await _dbContext.MainScreenCategories
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.Order, x => i + 1));
        }
        return new() { Check = true };
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
    public async Task<Response<StGetAllMainScreensResponse>> GetAllMainScreensAsync(string lang, StGetAllFiltrationsForMainScreenRequest model, string host,string moduleName)
    {
        Expression<Func<StMainScreen, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                               && (string.IsNullOrEmpty(model.Word) ||
                                                   x.Screen_main_title_ar.Contains(model.Word)
                                                   || x.Screen_main_title_en.Contains(model.Word)
                                                );

        var totalRecords = await unitOfWork.MainScreens.CountAsync(filter: filter);
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();

        var result = new StGetAllMainScreensResponse
        {
            TotalRecords = totalRecords,

            Items = (await unitOfWork.MainScreens.GetSpecificSelectAsync(filter: filter, x => x,
                 take: model.PageSize,
                 skip: (model.PageNumber - 1) * model.PageSize, orderBy: x =>
                  x.OrderBy(x => x.Order))).Select(x => new MainScreenData
                 {
                     Id=x.Id,
                     Screen_main_title = Localization.Arabic ==lang? x.Screen_main_title_ar:x.Screen_main_title_en,
                     Screen_main_image= x.Screen_main_image == null ? string.Empty : _fileServer.GetFilePath(moduleName, x.Screen_main_image)

                  }).ToList(),
            CurrentPage = model.PageNumber,
            FirstPageUrl = host + $"?PageSize={model.PageSize}&PageNumber=1&IsDeleted={model.IsDeleted}",
            From = (page - 1) * model.PageSize + 1,
            To = Math.Min(page * model.PageSize, totalRecords),
            LastPage = totalPages,
            LastPageUrl = host + $"?PageSize={model.PageSize}&PageNumber={totalPages}&IsDeleted={model.IsDeleted}",
            PreviousPage = page > 1 ? host + $"?PageSize={model.PageSize}&PageNumber={page - 1}&IsDeleted={model.IsDeleted}" : null,
            NextPageUrl = page < totalPages ? host + $"?PageSize={model.PageSize}&PageNumber={page + 1}&IsDeleted={model.IsDeleted}" : null,
            Path = host,
            PerPage = model.PageSize,
            Links = pageLinks
        };

        if (result.TotalRecords is 0)
        {
            string resultMsg = sharLocalizer[Localization.NotFoundData];

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

    public async Task<Response<StCreateMainScreenRequest>> CreateMainScreenAsync(StCreateMainScreenRequest model, string appPath, string moduleName)
    {
        var mainScreenmap = _mapper.Map<StMainScreen>(model);
        var maxId = await _unitOfWork.MainScreens.MaxInCloumn(x =>x.Id);
        mainScreenmap.Order = maxId + 1;
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



    public async Task<Response<StGetMainScreenByIdResponse>> GetMainScreenByIdAsync(int id,string moduleName)
    {
        var obj = await _unitOfWork.MainScreens.GetFirstOrDefaultAsync(x => x.Id == id);
        var imagePath = obj.Screen_main_image == null ? string.Empty : _fileServer.GetFilePath(moduleName, obj.Screen_main_image);
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
                Screen_main_image= imagePath,
                Screen_Main_title_ar = obj.Screen_main_title_ar,
                Screen_Main_title_en = obj.Screen_main_title_en
            },
            Check = true
        };
    }

    public async Task<Response<StUpdateMainScreenRequest>> UpdateMainScreenAsync(int id, StUpdateMainScreenRequest model, string appPath, string moduleName)
    {
        var obj = await _unitOfWork.MainScreens.GetFirstOrDefaultAsync(x => x.Id ==id);

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
        if (model.Screen_main_image != null)
        {
            if (_fileServer.FileExist(appPath, moduleName, obj.Screen_main_image))
                _fileServer.RemoveFile(appPath, moduleName, obj.Screen_main_image);


            obj.Screen_main_image = (model.Screen_main_image.Length == 0) ? null
                : await _fileServer.UploadFile(appPath, moduleName, model.Screen_main_image);
        }


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

    public async Task<Response<GetAllStMainScreen>> GetMainScreensWithRelatedDataAsync(string lang)
    {
        var mainScreens = await context.MainScreenCategories.
            Include(ms => ms.CategoryScreen).
            ThenInclude(cs => cs.StScreenSub)
              .Where(ms => ms.CategoryScreen != null)
            .ToListAsync(); 



        var permStruct = (List<Dictionary<string, DTOSPGetPermissionsBySubScreen>>)(await _permessionStructureService.GetPermissionsBySubScreen(lang)).DynamicData;

 
        var subs = await _unitOfWork.SubMainScreenActions.GetAllAsync();
        var permision = await _unitOfWork.TitlePermissionRepository.GetAllAsync();
        var mains = await _unitOfWork.MainScreens.GetAllAsync();


        var ChildScreens = mainScreens
       .Where(ms => ms.CategoryScreen.Any(x => x.MainScreenId == ms.Id)).OrderBy(ms => ms.Order)
       .Select(ms => new GetAllStMainScreen
       {
           main_title = lang == "en" ? ms.Screen_main_title_en : ms.Screen_main_title_ar,
           main_image = ms.Screen_main_image,
           cats = ms.CategoryScreen.Where(x => x.MainScreenId == ms.Id).OrderBy(x => x.Order)
               .Select(x => new GetAllStMainScreenCat
               {
                   Id = x.Id,
                   title = lang == "en" ? x.Screen_cat_title_en : x.Screen_cat_title_ar,
                   main_id = x.MainScreenId,
                   subs = x.StScreenSub.Where(k => permision.Any(ps =>
                   ps.SubScreenId == k.Id && ps.Permissions.Contains("1") && k.ScreenCatId == x.Id)).OrderBy(k => k.Order)
                   .Select(k => new GetAllStScreenSub
                   {
                       Sub_Id = k.Id,
                       Screen_CatId = k.ScreenCatId,
                       cat_Title = lang == "en" ? x.Screen_cat_title_en : x.Screen_cat_title_en,
                       main_title = mains.Where(u => x.Id == k.ScreenCatId).Select(c => c.Name).FirstOrDefault(),
                       main_id = mains.Where(u => x.Id == k.ScreenCatId).Select(c => c.Id).FirstOrDefault(),
                       sub_title = lang == "en" ? k.Screen_sub_title_en : k.Screen_sub_title_ar,
                       url = k.Url,
                       sub_image = Path.Combine(SD.GoRootPath.GetSettingImagesPath, ms.Screen_main_image ?? " "),
                       screen_code = k.ScreenCode,
                       actions = subs.Where(x => x.ScreenSubId == k.Id).Select(x => x.ActionId).ToList().Concater(),
                       permissions = permision.Where(ps =>ps.SubScreenId == k.Id).Select(ps =>ps.Permissions).FirstOrDefault()
 
                   }).ToList()
               }).ToList()
       }).ToList();

        

        foreach (var mainScreen in ChildScreens)
        {
            Console.WriteLine($"Main Screen Title: {mainScreen.main_title}");
            Console.WriteLine($"Main Screen Image: {mainScreen.main_image}");

            foreach (var categoryScreen in mainScreen.cats)
            {
                if (categoryScreen.main_id ==  null ) continue;
                Console.WriteLine($"Category ID: {categoryScreen.Id}");
                Console.WriteLine($"Category Screen Title: {categoryScreen.title}");
                Console.WriteLine($"Main Screen Title: {mainScreen.main_title}");


                foreach (var screenSub in categoryScreen.subs)
                {
                    if (screenSub == null) continue;
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











