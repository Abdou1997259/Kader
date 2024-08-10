using Kader_System.DataAccess.Repositories;
using Kader_System.DataAccesss.DbContext;
using Kader_System.Domain.Dtos.Response;
using Kader_System.Domain.Models.EmployeeRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using System.Net;

namespace Kader_System.Services.Services.Setting;

public class SubMainScreenService(KaderDbContext _context, IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper, ILoggingRepository loggingRepository, Domain.Interfaces.IFileServer fileServer) : ISubMainScreenService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggingRepository _loggingRepository = loggingRepository;
    private readonly Domain.Interfaces.IFileServer _fileServer = fileServer;


    #region Sub main screen

    public async Task<Response<IEnumerable<StSelectListForSubMainScreenResponse>>> ListOfSubMainScreensAsync(string lang)
    {
        var result =
                await _unitOfWork.SubMainScreens.GetSpecificSelectAsync(null!,
                select: x => new StSelectListForSubMainScreenResponse
                {
                    Id = x.Id,
                    Screen_sub_title = lang == Localization.Arabic ? x.Screen_sub_title_ar : x.Screen_sub_title_en,
                    Screen_main_cat_id = x.ScreenCatId,
                    Url = x.Url,
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

    public async Task<Response<StGetAllSubMainScreensResponse>> GetAllSubMainScreensAsync(string lang, StGetAllFiltrationsForSubMainScreenRequest model, string host)
    {
        Expression<Func<StScreenSub, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                             && (string.IsNullOrEmpty(model.Word) ||
                                                 x.Screen_sub_title_ar.Contains(model.Word)
                                                 || x.Screen_sub_title_en.Contains(model.Word)
                                              );

        var totalRecords = await unitOfWork.SubMainScreens.CountAsync(filter: filter);
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();

        var result = new StGetAllSubMainScreensResponse
        {
            TotalRecords = totalRecords,

            Items = (await unitOfWork.SubMainScreens.GetSpecificSelectAsync(filter: filter, x => x,
                 take: model.PageSize,
                 skip: (model.PageNumber - 1) * model.PageSize)).Select(x => new SubMainScreenData
                 {
                     Screen_sub_title = lang == Localization.Arabic ? x.Screen_sub_title_ar : x.Screen_sub_title_en,
                     Url = x.Url,
                     Screen_cat_id = x.ScreenCatId,
                     Screen_sub_image = string.Concat(ReadRootPath.SettingImagesPath, x.ScreenCat.StScreenSub.Select(y => y.Screen_sub_image).ToList())
                 }, orderBy: x =>
                   x.OrderByDescending(x => x.Id))).ToList()
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

    public async Task<Response<StCreateSubMainScreenRequest>> CreateSubMainScreenAsync(StCreateSubMainScreenRequest model, string appPath, string moduleName)
    {


        bool exists = false;
        exists = await _unitOfWork.SubMainScreens.ExistAsync(x => x.Screen_sub_title_en.Trim() == model.Screen_sub_title_ar
        && x.Screen_sub_title_en.Trim() == model.Screen_sub_title_ar.Trim());

        if (exists)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.IsExist],
                _sharLocalizer[Localization.MainScreenCategory]);

            return new()
            {
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        string imageName = null!, imageExtension = null!;
        if (model.Screen_sub_image is not null)
        {

            var fileObj = ManageFilesHelper.UploadFile(model.Screen_sub_image, GoRootPath.SettingImagesPath);
            imageName = fileObj.FileName;
            imageExtension = fileObj.FileExtension;
        }

        var lastsubscreen = await _unitOfWork.SubMainScreens.GetLast();
          ;

        var screencode = "01" + $"{lastsubscreen.Id}".PadLeft(4, '0');
        await _unitOfWork.SubMainScreens.AddAsync(new()
        {
            Screen_sub_title_ar = model.Screen_sub_title_ar,
            Screen_sub_title_en = model.Screen_sub_title_en,
            ScreenCatId = model.ScreenCatId,
            ScreenCode = screencode,
        });
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Msg = _sharLocalizer[Localization.Done],
            Check = true,
            Data = model
        };


        //var newRequest = _mapper.Map<StScreenSub>(model);
        //bool exists = false;
        //exists = await _unitOfWork.SubMainScreens.ExistAsync(x => x.Screen_sub_title_ar.Trim() == model.Screen_sub_title_ar
        //|| x.Screen_sub_title_en.Trim() == model.Screen_sub_title_en.Trim());
        //newRequest.Screen_sub_image = (model.Screen_sub_image == null || model.Screen_sub_image.Length == 0) ? null :
        //      await _fileServer.UploadFile(appPath, moduleName, model.Screen_sub_image);
        //await _unitOfWork.SubMainScreens.AddAsync(newRequest);

        //if (exists)
        //{
        //    string resultMsg = string.Format(_sharLocalizer[Localization.IsExist],
        //        _sharLocalizer[Localization.SubMainScreen]);

        //    return new()
        //    {
        //        Error = resultMsg,
        //        Msg = resultMsg
        //    };
        //}

        //var obj = await _unitOfWork.SubMainScreens.AddAsync(new()
        //{
        //    Screen_sub_title_en = model.Screen_sub_title_en,
        //    Screen_sub_title_ar = model.Screen_sub_title_ar,
        //    ScreenCatId = model.ScreenCatId,
        //    Url = model.Url,
        //    ScreenCode = model.ScreenCode,
        //    ListOfActions = model.Actions.Select(ob => new StSubMainScreenAction
        //    {
        //        ActionId = ob,

        //    }).ToList()
        //});
        //await _unitOfWork.CompleteAsync();

        //return new()
        //{
        //    Msg = _sharLocalizer[Localization.Done],
        //    Check = true,
        //    Data = model
        //};
    }

    public async Task<Response<StGetSubMainScreenByIdResponse>> GetSubMainScreenByIdAsync(int id, string lang)
    {
        var obj = await _unitOfWork.SubMainScreens.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "ListOfActions.Action");


        if (obj is null)
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new()
            {
                Data = null!,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        var lookup = await  _context.MainScreens.
            AsQueryable().
            ToDynamicLookUpAsync("Id", lang == "ar" ? "Screen_cat_title_ar" : "Screen_cat_title_en");
        return new()
        {
            Data = new()
            {
                Id = id,
                Screen_cat_id = obj.Id,
                Screen_sub_title_ar = obj.Screen_sub_title_ar,
                Screen_sub_title_en = obj.Screen_sub_title_en,
                Url = obj.Url,
                //Name = obj.Name,
                Actions = obj.ListOfActions.Select(x => new ActionsData
                {
                    Id = x.ActionId,
                    Name = x.Action.Name,
                    NameInEnglish = x.Action.NameInEnglish
                }).ToList()


            },
            Check = true,
            LookUps = lookup
        };
    }
    public async Task<Response<StScreenSub>> RestoreSubScreenAsync(int id)
    {
        var obj = await _unitOfWork.SubMainScreens.GetByIdAsync(id);
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
        await _unitOfWork.SubMainScreens.SoftDeleteAsync(obj, "IsDeleted", false);
        //obj.IsDeleted = false;
        //_unitOfWork.Allowances.Update(obj);
        //await _unitOfWork.CompleteAsync();
        return new()
        {
            Check = true,
            Data =obj,
            Msg = _sharLocalizer[Localization.Restored]
        };


    }

    public async Task<Response<StUpdateSubMainScreenRequest>> UpdateSubMainScreenAsync(int id, StUpdateSubMainScreenRequest model, string appPath, string moduleName)
    {
  

    var obj = await _unitOfWork.SubMainScreens.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "ListOfActions");

        if (obj is null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.SubMainScreen]);

            return new()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        using var transaction = _unitOfWork.BeginTransaction();
        try
        {
            if (obj.ListOfActions.Count > 0)
            {
                _unitOfWork.SubMainScreenActions.RemoveRange(obj.ListOfActions);
                await _unitOfWork.CompleteAsync();
            }


            var mappedsubscreen = _mapper.Map(model, obj);
            _unitOfWork.SubMainScreens.Update(mappedsubscreen);
 
            obj.Screen_sub_image = (model.Screen_sub_image == null || model.Screen_sub_image.Length == 0) ? null :
                await _fileServer.UploadFile(appPath,moduleName, model.Screen_sub_image);


            var full_path = Path.Combine(appPath, moduleName);
            if (model.Screen_sub_image != null)
                _fileServer.RemoveFile(full_path, obj.Screen_sub_image);


              _unitOfWork.SubMainScreens.Update(obj);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
 

          
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            await _loggingRepository.LogExceptionInDb(ex, JsonConvert.SerializeObject(model));
            return new()
            {
                Error = ex.Message,
                Msg = ex.Message + (ex.InnerException == null ? string.Empty : ex.InnerException.Message)
            };
        }
    }

   

    public async Task<Response<string>> DeleteSubMainScreenAsync(int id)
    {
        var obj = await _unitOfWork.SubMainScreens.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "ListOfActions");

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.SubMainScreen]);

            return new()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        string err = _sharLocalizer[Localization.Error];

        _unitOfWork.SubMainScreenActions.RemoveRange(obj.ListOfActions);
        _unitOfWork.SubMainScreens.Remove(obj);

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

    public Task<Response<GetAllSubScreenInfo>> GetAllInfo(string lang)
    {
        throw new NotImplementedException();
    }

   
    public Task<Response<string>> UpdateActiveOrNotSubMainScreenAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<string>> OrderByPattern(int[] pattern)
    {
        int count = 0;
        var allsubs = await _unitOfWork.SubMainScreens.GetAllAsync();
        foreach (var sub in allsubs) {
            sub.Order = pattern[count];
            count++;
        
        };
        _unitOfWork.SubMainScreens.UpdateRange(allsubs);
        await _unitOfWork.CompleteAsync();  
        return new() {Check = true};
    }




    #endregion
}
