using Kader_System.DataAccess.Repositories;
using Kader_System.DataAccesss.DbContext;
using Kader_System.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
﻿using Kader_System.DataAccesss.Context;

namespace Kader_System.Services.Services.Setting;

public class MainScreenCategoryService(KaderDbContext context, IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper, Domain.Interfaces.IFileServer fileServer) : IMainScreenCategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IMapper _mapper = mapper;
    private readonly KaderDbContext _context = context;
    private readonly Domain.Interfaces.IFileServer _fileServer = fileServer;


    #region Main screen category

    public async Task<Response<IEnumerable<StSelectListForMainScreenCategoryResponse>>> ListOfMainScreensCategoriesAsync(string lang)
    {
        var result =
                await _unitOfWork.MainScreenCategories.GetSpecificSelectAsync(null!,
                select: x => new StSelectListForMainScreenCategoryResponse
                {
                    Ids = x.Id,
                    Screen_cat_title = lang == Localization.Arabic ? x.Screen_cat_title_ar : x.Screen_cat_title_en,
                    Screen_main_cat_image = x.Screen_main_cat_image != null ? string.Concat(ReadRootPath.SettingImagesPath, x.Screen_main_cat_image) : string.Empty
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
    public async Task<Response<StMainScreenCat>> RestoreCatScreenAsync(int id)
    {
        var obj = await _unitOfWork.MainScreenCategories.GetByIdAsync(id);
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
        await _unitOfWork.MainScreenCategories.SoftDeleteAsync(obj, "IsDeleted", false);
        //obj.IsDeleted = false;
        //_unitOfWork.Allowances.Update(obj);
        //await _unitOfWork.CompleteAsync();
        return new()
        {
            Check = true,
            Data = obj,
            Msg = _sharLocalizer[Localization.Restored]
        };

    public async Task<Response<StGetAllMainScreensCategoriesResponse>> GetAllMainScreensCategoriesAsync(string lang, StGetAllFiltrationsForMainScreenCategoryRequest model, string host)

    }
    public async Task<Response<StGetAllMainScreensCategoriesResponse>> GetAllMainScreensCategoriesAsync(string lang, StGetAllFiltrationsForMainScreenCategoryRequest model)
    {
        Expression<Func<StMainScreenCat, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                            && (string.IsNullOrEmpty(model.Word) ||
                                                x.Screen_cat_title_ar.Contains(model.Word)
                                                || x.Screen_cat_title_en.Contains(model.Word)
                                             );


        var totalRecords = await unitOfWork.MainScreenCategories.CountAsync(filter: filter);
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();

        var result = new StGetAllMainScreensCategoriesResponse
        {
            TotalRecords = totalRecords,

            Items = ( await unitOfWork.MainScreenCategories.GetSpecificSelectAsync(filter: filter, x => x,
                 take: model.PageSize,
                 skip: (model.PageNumber - 1) * model.PageSize, includeProperties: "screenCat")).Select(x => new MainScreenCategoryData
                 {
                     Id = x.Id,
                     Screen_cat_Title = lang == Localization.Arabic ? x.Screen_cat_title_ar : x.Screen_cat_title_en,
                     Screen_main_title = lang == Localization.Arabic ? x.screenCat.Screen_main_title_ar : x.screenCat.Screen_main_title_en,
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
                    
                     Screen_main_title = lang == Localization.Arabic ? x.Screen_cat_title_ar : x.Screen_cat_title_en,
                     ScrennCatTitle = lang == Localization.Arabic ? x.Screen_cat_title_ar : x.Screen_cat_title_en
                     //Screen_main_image = x.Screen_main_image != null ? string.Concat(ReadRootPath.SettingImagesPath, x.Screen_main_image) : string.Empty
                 }, orderBy: x =>
                   x.OrderBy(x => x.Order))).ToList()
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

    public async Task<Response<StCreateMainScreenCategoryRequest>> CreateMainScreenCategoryAsync(StCreateMainScreenCategoryRequest model)
    {
        bool exists = false;
        exists = await _unitOfWork.MainScreenCategories.ExistAsync(x => x.Screen_cat_title_ar.Trim() == model.Screen_cat_title_ar
        && x.Screen_cat_title_en.Trim() == model.Screen_cat_title_ar.Trim());

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
        if (model.Screen_main_cat_image is not null)
        {

            var fileObj = ManageFilesHelper.UploadFile(model.Screen_main_cat_image, GoRootPath.SettingImagesPath);
            imageName = fileObj.FileName;
            imageExtension = fileObj.FileExtension;
        }


        await _unitOfWork.MainScreenCategories.AddAsync(new()
        {
            Screen_cat_title_ar = model.Screen_cat_title_ar,
            Screen_cat_title_en = model.Screen_cat_title_en,
            //Screen_main_cat_image = imageName,
            ImageExtension = imageExtension,
            MainScreenId= model.Screen_main_id,
        });
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Msg = _sharLocalizer[Localization.Done],
            Check = true,
            Data = model
        };
    }

    public async Task<Response<StGetMainScreenCategoryByIdResponse>> GetMainScreenCategoryByIdAsync(int id)
    {
        var obj = await _unitOfWork.MainScreenCategories.GetByIdAsync(id);

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
                Screen_main_title_ar = obj.Screen_cat_title_ar,
                Screen_main_title_en = obj.Screen_cat_title_en,
                Screen_main_cat_image = string.Concat(ReadRootPath.SettingImagesPath, obj.Screen_main_cat_image)
            },
            Check = true
        };

        
    }

    public async Task<Response<StUpdateMainScreenCategoryRequest>> UpdateMainScreenCategoryAsync(int id, StUpdateMainScreenCategoryRequest model, string lang, string appPath, string moduleName)
    {
        var obj = await _unitOfWork.MainScreenCategories.GetByIdAsync(id);


        var mappedcatscreen = _mapper.Map(model, obj);
        _unitOfWork.MainScreenCategories.Update(mappedcatscreen);

        obj.Screen_main_cat_image = (model.Screen_main_cat_image == null || model.Screen_main_cat_image.Length == 0) ? null :
            await _fileServer.UploadFile(appPath, moduleName, model.Screen_main_cat_image);


        var full_path = Path.Combine(appPath, moduleName);
        if (model.Screen_main_cat_image != null)
            _fileServer.RemoveFile(full_path, obj.Screen_main_cat_image);


        _unitOfWork.MainScreenCategories.Update(obj);
        var result = await _unitOfWork.CompleteAsync();
       
        var lookupActionScreen = await _context.Actions.AsQueryable()
            .ToDynamicLookUpAsync("Id", lang == "ar" ? "Name" : "NameInEnglish");

        var lookupMainScreen = await _context.MainScreenCategories.AsQueryable()
            .ToDynamicLookUpAsync("Id", lang == "ar" ? "Screen_main_title_ar" : "Screen_main_title_en");

        var lookupCatScreen = await _context.MainScreens.AsQueryable()
           .ToDynamicLookUpAsync("Id", lang == "ar" ? "Screen_cat_title_en" : "Screen_cat_title_en", "Screen_main_cat_image");




        if (model.Screen_main_cat_image is not null)
        {
            string path = GoRootPath.SettingImagesPath;

            //ManageFilesHelper.RemoveFile(path + "/" + obj.Screen_main_image);

            var fileObj = ManageFilesHelper.UploadFile(model.Screen_main_cat_image, path);
            //obj.Screen_main_image = fileObj.FileName;
            //obj.ImageExtension= fileObj.FileExtension;
            obj.Screen_main_cat_image = path;
        }

        obj.Screen_cat_title_ar = model.Screen_cat_title_ar;
        obj.Screen_cat_title_en = model.Screen_cat_title_ar;

        _unitOfWork.MainScreenCategories.Update(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data = model,
            Msg = _sharLocalizer[Localization.Updated],
            LookUps = lookupActionScreen,
            LookUpsScreen = lookupMainScreen.Concat(lookupCatScreen).ToList()
        };
    }

    public Task<Response<string>> UpdateActiveOrNotMainScreenCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<string>> DeleteMainScreenCategoryAsync(int id)
    {
        var obj = await _unitOfWork.MainScreenCategories.GetByIdAsync(id);

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

        _unitOfWork.MainScreenCategories.Remove(obj);
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
