

using Kader_System.DataAccess.Repositories;
using Kader_System.Domain.DTOs.Request.Auth;
using Kader_System.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kader_System.Services.Services.Setting
{

    public class TitleService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper, ITitlePermessionService permessionService) : ITitleService
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
                    includeProperties: $"{nameof(_instance.TitlePermissions)}",
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

        public async Task<Response<UpdateTitleRequest>> UpdateTitleAsync(int id, UpdateTitleRequest model,string lang,bool all=false)
        {
            var title = await unitOfWork.Titles.GetByIdAsync(id);

            if (title == null)
            {
                string resultMsg = string.Format(sharLocalizer[Localization.IsNotExisted],
                    sharLocalizer[Localization.Vacation]);

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            var subMainScreenActions = await unitOfWork.SubMainScreenActions.GetAllAsync();
            foreach (var sub in model.Permssions)
            {
                // Get ActionIds for the current SubId
                var actionIdsForSubId = subMainScreenActions
                    .Where(x => x.ScreenSubId == sub.SubId)
                    .Select(x => x.ActionId)
                    .Distinct()
                    .ToList();

                // Get TitlePermssion for the current SubId
                var titlePermissions = sub.TitlePermssion;

                // Check if there is at least one ActionId that is not in the TitlePermssion
                var missingActionsExist = titlePermissions.Except(actionIdsForSubId);

                // Process the result
                if (missingActionsExist.Any() && missingActionsExist.Any(x => x != 0))
                {
                    var permssions = await unitOfWork.ActionsRepo.GetSpecificSelectAsync(x => missingActionsExist.Any(u => u == x.Id), x => x);
                    var subscrren = await unitOfWork.SubMainScreens.GetByIdAsync(sub.SubId);
                    string name = Localization.Arabic == lang ? subscrren.Screen_sub_title_ar : subscrren.Screen_sub_title_en;
                    string nameofpermissions = "";
                    foreach (var per in permssions)
                    {
                        nameofpermissions += Localization.Arabic == lang ? per.Name + " " : per.NameInEnglish + " ";
                    }
                    var msg = $"{name} {sharLocalizer[Localization.ScreenInAction]} {nameofpermissions}";
                    // Handle the case where at least one ActionId is missing
                    // Example: Log or perform some action
                    return new()
                    {
                        Check = false,
                        Msg = msg,
                        Data = null

                    };

                }
            }


            title.TitleNameAr = model.TitleNameAr;
            title.TitleNameEn = model.TitleNameEn;


          



           var result= await AssginTitlePermssion(id, model.Permssions,lang,all);
            if (result.Check == false) {
                return new()
                {
                    Msg = result.Msg,
                    Check = false,
                    Data = null
                };


            }
            unitOfWork.Titles.Update(title);
        


            await unitOfWork.CompleteAsync();



            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        public async Task<Response<GetTitleByIdResponse>> GetTitleByIdAsync(int id, string lang)
        {
            var title = await unitOfWork.Titles.GetByIdAsync(id);
            if (title is null)
            {
                var msg = sharLocalizer[Localization.NotFound];
                return new()
                {
                    Msg = msg,
                    Check = false,
                    Data = null,
                };

            }


            return new Response<GetTitleByIdResponse>()
            {
                Check = true,
                Data = new GetTitleByIdResponse
                {
                   Id = id,
                    Name = Localization.Arabic == lang ? title.TitleNameAr : title.TitleNameEn,
                    all_permissions = (await permessionService.GetTitlePermissionsBySubScreen(id, lang)).DynamicData,
                }

            };
        }


        public Task<Response<string>> UpdateActiveOrNotTitleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> DeleteTitleAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<Response<string>> AssginTitlePermssion(int id, IEnumerable<Permissions> model, string lang, bool all=false)
        {



          
            foreach (var assignedPermission in model)

            {

         

                if (await unitOfWork.SubMainScreens.ExistAsync(x => x.Id == assignedPermission.SubId) ) {

                    if (all)
                    {
                        var userPermissionQuery = await unitOfWork.TitlePermissionRepository
                                .GetSpecificSelectAsync(x => true, x => x);

                        if (userPermissionQuery.Count() > 0)
                        {
                            unitOfWork.TitlePermissionRepository.RemoveRange(userPermissionQuery);
                        }
                        if (assignedPermission.TitlePermssion.Count == 0||assignedPermission.TitlePermssion.Any(x => x == 0))
                        {
                            continue;
                        }
                        else
                        {
                            await unitOfWork.TitlePermissionRepository.AddAsync(new TitlePermission
                            {


                                TitleId=id,


                                SubScreenId = assignedPermission.SubId,
                                Permissions = string.Join(',', assignedPermission.TitlePermssion)
                            });
                        }
                    }
                    else
                    {
                        var userPermissionQuery = await unitOfWork.TitlePermissionRepository
                           .GetSpecificSelectAsync(x => x.SubScreenId == assignedPermission.SubId, x => x);

                        if (userPermissionQuery.Count() > 0)
                        {
                            unitOfWork.TitlePermissionRepository.RemoveRange(userPermissionQuery);
                        }
                        if (assignedPermission.TitlePermssion.Count == 0 ||assignedPermission.TitlePermssion.Any(x=>x==0))
                        {
                            continue;
                        }
                        else
                        {
                            await unitOfWork.TitlePermissionRepository.AddAsync(new TitlePermission
                            {





                                SubScreenId = assignedPermission.SubId,
                                Permissions = string.Join(',', assignedPermission.TitlePermssion)
                            });
                        }


                    }

                }
                else
                {
                    var msg = sharLocalizer[Localization.InvalidSubId];
                    return new Response<string>()
                    {
                        Check = false,
                        Data = null,
                        Msg = msg


                    };
                };



              


            }

            await unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = "",

            };
        }
    }
}
