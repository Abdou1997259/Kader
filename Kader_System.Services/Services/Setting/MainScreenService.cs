
using Kader_System.DataAccesss.DbContext;
using Kader_System.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.Setting;

public class MainScreenService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper ,KaderDbContext context) : IMainScreenService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IMapper _mapper = mapper;

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
                   x.OrderByDescending(x => x.Id))).ToList()
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

    public async Task<Response<StCreateMainScreenRequest>> CreateMainScreenAsync(StCreateMainScreenRequest model)
    {
        bool exists = false;
        exists = await _unitOfWork.MainScreens.ExistAsync(x => x.Screen_main_title_ar.Trim() == model.Screen_main_title_ar
        && x.Screen_main_title_en.Trim() == model.Screen_main_title_en.Trim());

        if (exists)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.IsExist],
                _sharLocalizer[Localization.MainScreen]);

            return new()
            {
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        await _unitOfWork.MainScreens.AddAsync(new()
        {
            Screen_main_title_ar = model.Screen_main_title_ar,
            Screen_main_title_en = model.Screen_main_title_en,
            Id = model.Screen_main_id
        });
        await _unitOfWork.CompleteAsync();

        return new()
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
        obj.Id = model.Screen_main_id;

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

        var ChildScreens = mainScreens.Select(ms => new GetAllStMainScreen
        {
            Screen_main_title = lang == "en" ?  ms.Screen_main_title_en  :  ms.Screen_main_title_ar,
            Screen_main_image = ms.Screen_main_image,
            CategoryScreen = ms.CategoryScreen.Select(x => new GetAllStMainScreenCat
            {
                Ids = ms.CategoryScreen.Select(x => x.Id).ToList(),
                Screen_cat_title = ms.CategoryScreen.Select(x => lang == "en" ? x.Screen_cat_title_en : x.Screen_cat_title_ar).ToList(),
                Screen_main_cat_image = ms.CategoryScreen.Select(x => x.Screen_main_cat_image).ToList(),
                StScreenSub = x.StScreenSub.Select(k => new GetAllStScreenSub
                {
                    Ids = k.ScreenCat.StScreenSub.Select(x => x.Id).ToList(),
                    Screen_sub_title = k.ScreenCat.StScreenSub.Select(y => lang == "en" ? y.Screen_sub_title_en : y.Screen_sub_title_ar).ToList(),
                    Url = k.ScreenCat.StScreenSub.Select(y => y.Url).ToList(),
                    Screen_sub_image = k.ScreenCat.StScreenSub.Select(y => y.Screen_sub_image).ToList(),
                    ScreenCode = k.ScreenCat.StScreenSub.Select(y => y.ScreenCode).ToList(),

                }).ToList(),
            }).ToList()
        }).ToList();


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
