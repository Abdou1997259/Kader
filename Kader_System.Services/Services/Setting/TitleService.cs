

namespace Kader_System.Services.Services.Setting
{
    
    public class TitleService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITitleService
    {
        private Title _instance;
        public async Task<Response<IEnumerable<SelectListOfTitleResponse>>> ListOfTitlesAsync(string lang)
        {
            var result =
                await unitOfWork.Titles.GetSpecificSelectAsync(null!,
                    select: x => new SelectListOfTitleResponse
                    {
                        Id = x.Id,
                        TitleNameAr = x.TitleNameAr,
                        TitleNameEn = x.TitleNameEn
                    }, orderBy: x =>
                        x.OrderByDescending(x => x.Id));

            if (!result.Any())
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

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

        public async Task<Response<GetAllTitleResponse>> GetAllTitlesAsync(string lang, GetAllFilterrationForTitleRequest model)
        {
            Expression<Func<Title, bool>> filter = x => x.IsDeleted == model.IsDeleted;

            var result = new GetAllTitleResponse
            {
                TotalRecords = await unitOfWork.Titles.CountAsync(filter: filter),

                Items = (await unitOfWork.Titles.GetSpecificSelectAsync(filter: filter,
                    includeProperties:$"{nameof(_instance.TitlePermissions)}",
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    select: x => new TitleData()
                    {
                        Id = x.Id,
                        TitleNameAr = x.TitleNameAr,
                        TitleNameEn = x.TitleNameEn,
                        Permissions = x.TitlePermissions.Select(p=>new GetAllTitlePermissionResponse()
                        {
                            Id = p.Id,
                            SubScreenId = p.SubScreenId,
                            sub_title = p.ScreenSub!.Screen_sub_title_ar,
                            actions = "",
                            url = p.ScreenSub!.Url,
                            title_permission = new List<int>()
                            {
                               1,2, 3, 4,
                            }
                            
                        }).ToList()
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

        public async Task<Response<CreateTitleRequest>> CreateTitleAsync(CreateTitleRequest model)
        {
            bool exists = false;
            exists = await unitOfWork.Titles.ExistAsync(x => x.TitleNameAr.Trim() == model.TitleNameAr
                                                                && x.TitleNameEn.Trim() == model.TitleNameEn.Trim());

            if (exists)
            {
                string resultMsg = string.Format(sharLocalizer[Localization.IsExist],
                    sharLocalizer[Localization.Vacation]);

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            var newTitle = new Title()
            {

               TitleNameAr = model.TitleNameAr,
               TitleNameEn = model.TitleNameEn,
               
            };
            foreach (var titlePermission in model.Permissions)
            {
                newTitle.TitlePermissions.Add(new TitlePermission()
                {
                 SubScreenId = titlePermission.SubScreenId,
                 Permissions = titlePermission.Permissions

                });
            }



            await unitOfWork.Titles.AddAsync(newTitle);

            await unitOfWork.CompleteAsync();

            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        public async Task<Response<GetTitleByIdResponse>> GetTitleByIdAsync(int id,string lang)
        {
            return await unitOfWork.Titles.GetTitleByIdAsync(id, lang);
        }

        public Task<Response<CreateTitleRequest>> UpdateTitleAsync(int id, CreateTitleRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> UpdateActiveOrNotTitleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> DeleteTitleAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
