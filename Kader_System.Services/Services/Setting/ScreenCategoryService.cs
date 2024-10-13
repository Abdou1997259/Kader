using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Services.IServices.AppServices;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.Setting;

public class ScreenCategoryService(KaderDbContext context, IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper, IFileServer fileServer) :
    IScreenCategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IMapper _mapper = mapper;
    private readonly KaderDbContext _context = context;
    private readonly IFileServer _fileServer = fileServer;


    #region Main screen category

    public async Task<Response<IEnumerable<StSelectListForScreenCategoryResponse>>> ListOfScreensCategoriesAsync(string lang)
    {
        var result =
                await _unitOfWork.ScreenCategories.GetSpecificSelectAsync(null!,
                select: x => new StSelectListForScreenCategoryResponse
                {
                    Ids = x.Id,
                    Screen_cat_title = lang == Localization.Arabic ? x.Screen_cat_title_ar : x.Screen_cat_title_en,
                    Screen_main_image = Path.Combine(SD.GoRootPath.GetSettingImagesPath, x.ScreenMain.Screen_main_image ?? "")
                }, orderBy: x =>
                  x.OrderBy(x => x.Id));

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
            var result = await _context.MainScreens
                 .Where(s => s.Id == id)
                 .ExecuteUpdateAsync(s => s.SetProperty(x => x.Order, x => i + 1));
        }
        return new() { Check = true };
    }
    public async Task<Response<StScreenCat>> RestoreCatScreenAsync(int id)
    {
        var obj = await _unitOfWork.ScreenCategories.GetByIdAsync(id);
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
        await _unitOfWork.ScreenCategories.SoftDeleteAsync(obj, "IsDeleted", false);
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

    public async Task<Response<StGetAllScreensCategoriesResponse>> GetAllScreensCategoriesAsync(string lang, StGetAllFiltrationsForScreenCategoryRequest model, string host)
    {
        Expression<Func<StScreenCat, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                            && (string.IsNullOrEmpty(model.Word) ||
                                                x.Screen_cat_title_ar.Contains(model.Word)
                                                || x.Screen_cat_title_en.Contains(model.Word)
                                             );


        var totalRecords = await unitOfWork.ScreenCategories.CountAsync(filter: filter);
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();

        var result = new StGetAllScreensCategoriesResponse
        {
            TotalRecords = totalRecords,

            Items = (await unitOfWork.ScreenCategories.GetSpecificSelectAsync(filter: filter, x => x,
                 take: model.PageSize,
                 skip: (model.PageNumber - 1) * model.PageSize, includeProperties: "ScreenMain", orderBy: x =>
                  x.OrderByDescending(x => x.Order))).Select(x => new ScreenCategoryData
                  {
                      Id = x.Id,
                      screen_main_id = x.MainScreenId,
                      Screen_cat_Title = lang == Localization.Arabic ? x.Screen_cat_title_ar : x.Screen_cat_title_en,
                      Screen_main_title = lang == Localization.Arabic ? x.ScreenMain.Screen_main_title_ar : x.ScreenMain.Screen_main_title_en,
                      Screen_main_image = Path.Combine(SD.GoRootPath.GetSettingImagesPath, x.ScreenMain.Screen_main_image ?? " ")

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

    public async Task<Response<StCreateScreenCategoryRequest>> CreateScreenCategoryAsync(StCreateScreenCategoryRequest model)
    {
        bool exists = false;
        exists = await _unitOfWork.ScreenCategories.ExistAsync(x => x.Screen_cat_title_ar.Trim() == model.Screen_cat_title_ar.Trim()
        || x.Screen_cat_title_en.Trim() == model.Screen_cat_title_en.Trim());

        if (exists)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.AlreadyExitedWithSameName],
                _sharLocalizer[Localization.MainScreenCategory]);

            return new()
            {
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        if (!await _unitOfWork.MainScreens.ExistAsync(x => x.Id == model.Screen_main_id))
        {
            return new()
            {
                Check = false,
                Msg = _sharLocalizer[Localization.CannotBeFound, _sharLocalizer[Localization.MainScreen]]

            };

        }


        var maxId = await _unitOfWork.ScreenCategories.MaxInCloumn(x => x.Id);

        await _unitOfWork.ScreenCategories.AddAsync(new()
        {
            Screen_cat_title_ar = model.Screen_cat_title_ar,
            Screen_cat_title_en = model.Screen_cat_title_en,
            MainScreenId = model.Screen_main_id,
            Order = maxId + 1
        });
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Msg = _sharLocalizer[Localization.Done],
            Check = true,
            Data = model
        };
    }

    public async Task<Response<StGetMainScreenCategoryByIdResponse>> GetScreenCategoryByIdAsync(int id)
    {
        var obj = (await _unitOfWork.ScreenCategories.GetSpecificSelectAsync(x => x.Id == id, x => x, includeProperties: "ScreenMain")).FirstOrDefault();

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
                ScreenMainId = obj.ScreenMain.Id,
                Screen_cat_title_ar = obj.Screen_cat_title_ar,
                Screen_cat_title_en = obj.Screen_cat_title_en,

            },
            Check = true
        };


    }

    public async Task<Response<StUpdateScreenCategoryRequest>> UpdateScreenCategoryAsync(int id, StUpdateScreenCategoryRequest model, string lang, string appPath, string moduleName)
    {
        var obj = await _unitOfWork.ScreenCategories.GetByIdAsync(id);

        if (obj == null)
        {
            return new()
            {
                Check = false,
                Msg = _sharLocalizer[Localization.CannotBeFound, _sharLocalizer[Localization.MainScreenCategory]]

            };
        }
        if (!await _unitOfWork.MainScreens.ExistAsync(x => x.Id == model.Screen_main_id))
        {
            return new()
            {
                Check = false,
                Msg = _sharLocalizer[Localization.CannotBeFound, _sharLocalizer[Localization.MainScreen]]

            };

        }
        if (await _unitOfWork.ScreenCategories.ExistAsync(x => x.Id != id
        && (x.Screen_cat_title_ar.Trim() == model.Screen_cat_title_ar.Trim()
        || x.Screen_cat_title_en.Trim() == model.Screen_cat_title_en.Trim())))
        {
            return new()
            {
                Check = false,
                Msg = _sharLocalizer[Localization.AlreadyExitedWithSameName, _sharLocalizer[Localization.MainScreenCategory]]

            };
        }

        _mapper.Map(model, obj);


        _unitOfWork.ScreenCategories.Update(obj);


        var result = await _unitOfWork.CompleteAsync();



        return new Response<StUpdateScreenCategoryRequest>
        {
            Check = true,
            Data = model,
            Msg = _sharLocalizer[Localization.Updated],


        };
    }

    public Task<Response<string>> UpdateActiveOrNotScreenCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<string>> DeleteScreenCategoryAsync(int id)
    {
        var obj = await _unitOfWork.ScreenCategories.GetByIdAsync(id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.MainScreenCategory]);

            return new()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        _unitOfWork.ScreenCategories.Remove(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data = string.Empty,
            Msg = _sharLocalizer[Localization.Deleted]
        };
    }



    #endregion
}