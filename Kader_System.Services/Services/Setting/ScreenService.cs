using Kader_System.Domain.DTOs;

namespace Kader_System.Services.Services.Setting
{
    public class ScreenService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer) : IScreenService
    {
        #region Retreive

        public async Task<Response<GetAllScreenResponse>> GetAllScreensAsync(string lang, string host, GetAllFilterationForScreen model)
        {
            Expression<Func<Screen, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                                          && (string.IsNullOrEmpty(model.Word)
                                                             || x.Code.ToString().Contains(model.Word)
                                                             || x.NameAr.Contains(model.Word)
                                                             || x.NameEn.Contains(model.Word));
            var totalRecords = await unitOfWork.Screens.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllScreenResponse
            {
                TotalRecords = totalRecords,

                Items = (await unitOfWork.Screens.GetScreenInfoData(filter: filter,
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize))
                 ,
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

        public async Task<Response<GetScreenDataById>> GetScreenByIdAsync(int id, string lang)
        {
            var obj = await unitOfWork.Screens.GetScreenDataById(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Error = string.Empty,
                Check = true,
                Data = new()
                {
                    Actions = obj.Actions.Select(a => a.Action.Id).ToList(),
                    ActiveIcon = $"{ReadRootPath.SettingImagesPath}{obj.ActiveIcon}",
                    Code = obj.Code,
                    EndPoint = obj.EndPoint,
                    Icon = $"{ReadRootPath.SettingImagesPath}{obj.Icon}",
                    Id = obj.Id,
                    NameAr = obj.NameAr,
                    NameEn = obj.NameEn,
                    Url = obj.Url,
                    ScreenType = obj.ScreenType,
                    Sort = obj.Sort,
                    ParentId = obj.ParentId,
                    ParentName = lang == Localization.Arabic ? obj.ParentScreen!.NameAr : obj.ParentScreen!.NameEn
                }
            };
        }

        #endregion

        #region Insert
        public async Task<Response<CreateScreenRequest>> CreateScreenAsync(CreateScreenRequest model)
        {
            var exists = await unitOfWork.Screens.ExistAsync(x => x.NameAr.Trim() == model.NameAr.Trim()
                                                                  && x.NameEn.Trim() == model.NameEn.Trim());

            if (exists)
            {
                string resultMsg = string.Format(sharLocalizer[Localization.IsExist],
                    sharLocalizer[Localization.Screen]);

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }
            GetFileNameAndExtension? activeIcon = null;
            if (model.ActiveIcon != null)
            {
                activeIcon = ManageFilesHelper.UploadFile(model.ActiveIcon, GoRootPath.SettingImagesPath);
            }
            GetFileNameAndExtension? icon = null;
            if (model.Icon != null)
            {
                icon = ManageFilesHelper.UploadFile(model.Icon, GoRootPath.SettingImagesPath);
            }


            await unitOfWork.Screens.AddAsync(new()
            {
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                Actions = model.Actions.Select(a => new StScreenAction()
                {
                    ActionId = a
                }).ToList(),
                ActiveIcon = activeIcon?.FileName,
                Code = await unitOfWork.Screens.GenerateNewCode_Async(model.ParentId),
                EndPoint = model.EndPoint,
                Icon = icon?.FileName,
                ParentId = model.ParentId,
                ScreenType = model.ScreenType,
                Sort = model.Sort,
                Url = model.Url


            });
            await unitOfWork.CompleteAsync();

            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }
        #endregion


        #region Update
        public async Task<Response<CreateScreenRequest>> UpdateScreenAsync(int id, CreateScreenRequest model)
        {
            var obj = await unitOfWork.Screens.GetScreenDataById(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            if (obj.ActiveIcon != null)
            {
                ManageFilesHelper.RemoveFile(Path.Combine(GoRootPath.SettingImagesPath, obj.ActiveIcon));
            }
            if (obj.Icon != null)
            {
                ManageFilesHelper.RemoveFile(Path.Combine(GoRootPath.SettingImagesPath, obj.Icon));
            }

            if (obj.Actions.Any())
            {
                unitOfWork.ScreenActions.RemoveRange(obj.Actions);
            }

            GetFileNameAndExtension? activeIcon = null;
            if (model.ActiveIcon != null)
            {
                activeIcon = ManageFilesHelper.UploadFile(model.ActiveIcon, GoRootPath.SettingImagesPath);
            }
            GetFileNameAndExtension? icon = null;
            if (model.Icon != null)
            {
                icon = ManageFilesHelper.UploadFile(model.Icon, GoRootPath.SettingImagesPath);
            }

            obj.ActiveIcon = activeIcon!.FileName;
            obj.Icon=icon!.FileName;
            obj.NameAr = model.NameAr;
            obj.NameEn=model.NameEn;
           
            obj.Sort=model.Sort;
            if (obj.ParentId != model.ParentId)
            {
                obj.ParentId = model.ParentId;
                obj.Code = await unitOfWork.Screens.GenerateNewCode_Async(model.ParentId);
            }
            obj.EndPoint=model.EndPoint;
            obj.Url=model.Url;
            obj.ScreenType=model.ScreenType;
            unitOfWork.Screens.Update(obj);

            await unitOfWork.ScreenActions.AddRangeAsync(model.Actions.Select(a => new StScreenAction()
            {
                ActionId = a,
                ScreenId = id

            }));


            await unitOfWork.CompleteAsync();

            return new()
            {
                Error = string.Empty,
                Msg = sharLocalizer[Localization.Updated],
                Check = true,
                Data = model,
            };


        }
        public async Task<Response<GetScreenDataById>> RestoreScreenAsync(int id)
        {
            var obj = await unitOfWork.Screens.GetScreenDataById(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            obj.IsDeleted = false;
            foreach (var action in obj.Actions)
            {
                action.IsDeleted = false;
            }

            unitOfWork.Screens.Update(obj);
            unitOfWork.ScreenActions.UpdateRange(obj.Actions);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Check = true,
                Error = string.Empty,
                Msg = sharLocalizer[Localization.Restored],
                Data = new()
                {
                    Actions = obj.Actions.Select(a => a.Action.Id).ToList(),
                    ActiveIcon = $"{ReadRootPath.SettingImagesPath}{obj.ActiveIcon}",
                    Code = obj.Code,
                    EndPoint = obj.EndPoint,
                    Icon = $"{ReadRootPath.SettingImagesPath}{obj.Icon}",
                    Id = obj.Id,
                    NameAr = obj.NameAr,
                    NameEn = obj.NameEn,
                    Url = obj.Url,
                    ScreenType = obj.ScreenType,
                    Sort = obj.Sort,
                    ParentId = obj.ParentId,
                    ParentName = obj.ParentScreen!.NameAr
                }
            };
        }
        #endregion

        #region Delete
        public async Task<Response<string>> DeleteScreenAsync(int id)
        {
            var obj = await unitOfWork.Screens.GetScreenDataById(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            if (obj.IsDeleted)
            {
                if (obj.ActiveIcon != null)
                {
                    ManageFilesHelper.RemoveFile(Path.Combine(GoRootPath.SettingImagesPath, obj.ActiveIcon));
                }
                if (obj.Icon != null)
                {
                    ManageFilesHelper.RemoveFile(Path.Combine(GoRootPath.SettingImagesPath, obj.Icon));
                }
            }
            unitOfWork.Screens.Remove(obj);
            unitOfWork.ScreenActions.RemoveRange(obj.Actions);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = string.Empty,
                Msg = sharLocalizer[Localization.Deleted]
            };
        }

        #endregion




    }
}
