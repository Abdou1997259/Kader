

using Kader_System.Domain.DTOs.Request.Auth;

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
                        Add_date = x.Add_date,
                        //Permissions = x.TitlePermissions.Select(p=>new GetAllTitlePermissionResponse()
                        //{
                        //    Id = p.Id,
                        //    SubScreenId = p.SubScreenId,
                        //    sub_title = p.ScreenSub!.Screen_sub_title_ar,
                        //    actions = "",
                        //    url = p.ScreenSub!.Url,
                        //    title_permission = new List<int>()
                        //    {
                        //       1,2, 3, 4,
                        //    }
                            
                        //}).ToList()
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

            await unitOfWork.Titles.AddAsync(newTitle);


            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }
        public async Task<Response<CreateTitleRequest>> UpdateTitleAsync(int id,CreateTitleRequest model, IEnumerable<AssginTitlePermissionRequest> pers)
        {
            var title = await unitOfWork.Titles.GetByIdAsync(id);

            if (title==null)
            {
                string resultMsg = string.Format(sharLocalizer[Localization.IsNotExisted],
                    sharLocalizer[Localization.Vacation]);

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }



            title.TitleNameAr = model.TitleNameAr;
            title.TitleNameEn = model.TitleNameEn;
               
            
            //foreach (var titlePermission in model.Permissions)
            //{
            //    newTitle.TitlePermissions.Add(new TitlePermission()
            //    {
            //     SubScreenId = titlePermission.SubScreenId,
            //     Permissions = titlePermission.Permissions

            //    });
            //}



             unitOfWork.Titles.Update(title);
            var listOfTitlePermssion = pers.Select(x => new TitlePermission
            {
                SubScreenId = x.SubScreenId,
                TitleId = title.Id,
                Permissions = string.Join(',', x.Permission)

            });
            await unitOfWork.TitlePermissionRepository.AddRangeAsync(listOfTitlePermssion);


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

   
        public Task<Response<string>> UpdateActiveOrNotTitleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> DeleteTitleAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<Response<string>> AssginTitlePermssion(int id, IEnumerable<AssginTitlePermissionRequest> model)
        {

            List<TitlePermission> AddedPer = null;
            foreach (var AssginedPermssion in model)
            {
                var titlePermission = await unitOfWork.TitlePermissionRepository
                    .GetSpecificSelectAsync(x => x.TitleId == id && x.SubScreenId == AssginedPermssion.SubScreenId, x => x);
                IEnumerable<TitlePermission> listUpdatedper = null;

                if (titlePermission != null)
                {

                    listUpdatedper = titlePermission.Select(x => new TitlePermission
                    {
                        Permissions = string.Join(',', AssginedPermssion.Permission),
                        ScreenSub = x.ScreenSub,
                        TitleId = x.TitleId
                    });
                    unitOfWork.TitlePermissionRepository.UpdateRange(listUpdatedper);



                }
                else
                {
                    AddedPer.Add(new TitlePermission
                    {
                        TitleId = id,
                        SubScreenId = AssginedPermssion.SubScreenId,
                        Permissions = string.Join(',', AssginedPermssion.Permission)
                    });



                }



            }
            await unitOfWork.TitlePermissionRepository.AddRangeAsync(AddedPer);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = "",

            };
        }
    }
}
