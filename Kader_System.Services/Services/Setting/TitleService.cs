using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.Auth;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.Setting
{

    public class TitleService(IUnitOfWork unitOfWork, KaderDbContext _context, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper, ITitlePermessionService permessionService) : ITitleService
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

        public async Task<Response<GetAllTitleResponse>> GetAllTitlesAsync(string lang, GetAllFilterrationForTitleRequest model, string host)
        {
            Expression<Func<Title, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                              && (string.IsNullOrEmpty(model.Word) ||
                                                  x.TitleNameAr.Contains(model.Word)
                                                  || x.TitleNameEn.Contains(model.Word)
                                               );

            var totalRecords = await unitOfWork.Titles.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();

            var result = new GetAllTitleResponse
            {
                TotalRecords = totalRecords,

                Items = (await unitOfWork.Titles.GetSpecificSelectAsync(filter: filter, x => x,
                     take: model.PageSize,
                     skip: (model.PageNumber - 1) * model.PageSize)).Select(x => new TitleData
                     {
                         Id = x.Id,
                         TitleNameAr = x.TitleNameAr,
                         TitleNameEn = x.TitleNameEn,
                         Add_date = x.Add_date.HasValue
                                    ? x.Add_date.Value.ToString("MM/dd/yyyy HH:mm:ss")
                                    : null



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

        public async Task<Response<UpdateTitleRequest>> UpdateTitleAsync(int id, UpdateTitleRequest model, string lang, bool all = false)
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
            foreach (var sub in model.permissions)
            {
                // Get ActionIds for the current SubId
                var actionIdsForSubId = subMainScreenActions
                    .Where(x => x.ScreenSubId == sub.SubId)
                    .Select(x => x.ActionId)
                    .Distinct()
                    .ToList();

                // Get TitlePermssion for the current SubId
                var titlePermissions = sub.title_permission;

                // Check if there is at least one ActionId that is not in the TitlePermssion
                var missingActionsExist = titlePermissions.Except(actionIdsForSubId);

                // Process the result
                if (missingActionsExist.Any() && missingActionsExist.Any(x => x != 0))
                {
                    var permssions = await unitOfWork.ActionsRepo.GetSpecificSelectAsync(x => missingActionsExist.Any(u => u == x.Id), x => x);
                    var subscrren = await unitOfWork.SubScreens.GetByIdAsync(sub.SubId);
                    string name = Localization.Arabic == lang ? subscrren.Screen_sub_title_ar : subscrren.Screen_sub_title_en;
                    string nameofPermissions = "";
                    foreach (var per in permssions)
                    {
                        nameofPermissions += Localization.Arabic == lang ? per.Name + " " : per.NameInEnglish + " ";
                    }
                    var msg = $"{name} {sharLocalizer[Localization.ScreenInAction]} {nameofPermissions}";
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






            var result = await AssginTitlePermssion(id, model.permissions, lang, all);
            if (result.Check == false)
            {
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
        public async Task<Response<string>> RestoreTitleAsync(int id)
        {
            var obj = await unitOfWork.Titles.GetByIdAsync(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }
            obj.IsDeleted = false;
            unitOfWork.Titles.Update(obj);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Check = true,
                Error = string.Empty,
                Msg = sharLocalizer[Localization.Restored],
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

        public async Task<Response<string>> DeleteTitleAsync(int id) // 1
        {


            var isUserTakeThisTitle = await unitOfWork.Users.GetFirstOrDefaultAsync(x => x.CurrentTitleId == id);

            if (isUserTakeThisTitle is not null)
            {
                string resultMsg = sharLocalizer[Localization.UserInTitle];

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }


            var titleResult = await _context.Titles.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.
               SetProperty(p => p.IsDeleted, true).
               SetProperty(p => p.DeleteDate, DateTime.Now));



            if (titleResult > 0)
            {
                //var users = _context.Users.AsNoTracking()
                //                .AsEnumerable()
                //                .Where(u => u.TitleId.Split(",", StringSplitOptions.None)
                //                .Contains(id.ToString()))
                //                .ToList();




                //foreach (var user in users)
                //{
                //    var userTitles = user.TitleId.Splitter(); // [1,2,3]
                //    userTitles.Remove(id); // [2,3]
                //    var removedUserTitles = string.Join(",", userTitles); // "2,3"
                //    user.TitleId = removedUserTitles; // "2,3"
                //    user.CurrentTitleId = id == user.CurrentTitleId ? userTitles.ElementAt(0) : user.CurrentTitleId; // = 2 shifted if current = 1 ,if current = 2 then will be = 2  
                //    _context.Users.Update(user);
                //    await _context.SaveChangesAsync();
                //}
                return new Response<string>()
                {
                    Check = true,
                    Msg = sharLocalizer[Localization.Deleted],
                    Data = "Data deleted sucessfully",
                };
            }
            return new Response<string>()
            {
                Check = false
            };
        }


        private async Task<Response<string>> AssginTitlePermssion(int id, IEnumerable<Permissions> model, string lang, bool all = false)
        {




            foreach (var assignedPermission in model)

            {



                if (await unitOfWork.SubScreens.ExistAsync(x => x.Id == assignedPermission.SubId))
                {

                    if (all)
                    {
                        var userPermissionQuery = await unitOfWork.TitlePermissionRepository
                                .GetSpecificSelectTrackingAsync(x => x.SubScreenId == assignedPermission.SubId, x => x, includeProperties: "ScreenSub,Title");

                        if (userPermissionQuery.Count() > 0)
                        {
                            unitOfWork.TitlePermissionRepository.RemoveRange(userPermissionQuery);
                            await unitOfWork.CompleteAsync();

                        }
                        if (assignedPermission.title_permission.Count == 0 || assignedPermission.title_permission.Any(x => x == 0))
                        {
                            goto LabelExpression0;
                        }
                        else
                        {
                            await unitOfWork.TitlePermissionRepository.AddAsync(new TitlePermission
                            {


                                TitleId = id,


                                SubScreenId = assignedPermission.SubId,
                                Permissions = string.Join(',', assignedPermission.title_permission)
                            });
                            await unitOfWork.CompleteAsync();
                        }
                    LabelExpression0:;
                        var userWithTitle = await unitOfWork.Users.GetSpecificSelectAsync(x => x.TitleId.Contains(id.ToString()), select: x => x.Id);
                        if (userWithTitle.Any())
                        {
                            foreach (var userId in userWithTitle)
                            {
                                if (assignedPermission.title_permission.Count == 0 || assignedPermission.title_permission.Any(x => x == 0))
                                {
                                    var removeduserpermssion = (await unitOfWork.UserPermssionRepositroy.GetSpecificSelectTrackingAsync(x => x.TitleId == id && x.SubScreenId == assignedPermission.SubId, x => x)).ToList();

                                    unitOfWork.UserPermssionRepositroy.RemoveRange(removeduserpermssion);
                                    await unitOfWork.CompleteAsync();
                                    goto LabelExpression2;

                                }
                                var userpermssion = await unitOfWork.UserPermssionRepositroy.GetFirstOrDefaultAsync(x => x.UserId == userId && x.TitleId == id && x.SubScreenId == assignedPermission.SubId);
                                if (userpermssion is not null)
                                    unitOfWork.UserPermssionRepositroy.Remove(userpermssion);





                                await unitOfWork.UserPermssionRepositroy.AddAsync(new UserPermission
                                {
                                    UserId = userId,
                                    TitleId = id,
                                    Permission = string.Join(',', assignedPermission.title_permission),
                                    SubScreenId = assignedPermission.SubId
                                });

                                await unitOfWork.CompleteAsync();

                            LabelExpression2:;


                            }

                        }
                        //var userWithTitle = await unitOfWork.Users.GetSpecificSelectAsync(x => x.CurrentTitleId != null, select: x => new { x.Id, x.CurrentTitleId });
                        //if (userWithTitle.Any())
                        //{
                        //    foreach (var userIdtitle in userWithTitle)
                        //    {
                        //        if (assignedPermission.title_permission.Count == 0 || assignedPermission.title_permission.Any(x => x == 0))
                        //        {
                        //            var removeduserpermssion = (await unitOfWork.UserPermssionRepositroy.GetSpecificSelectTrackingAsync(x => x.UserId == userIdtitle.Id && x.TitleId == userIdtitle.CurrentTitleId && x.SubScreenId == assignedPermission.SubId, x => x)).ToList();

                        //            unitOfWork.UserPermssionRepositroy.RemoveRange(removeduserpermssion);
                        //            await unitOfWork.CompleteAsync();
                        //            goto LabelExpression2;

                        //        }
                        //        var userpermssion = await unitOfWork.UserPermssionRepositroy.GetFirstOrDefaultAsync(x => x.UserId == userIdtitle.Id && x.TitleId == userIdtitle.CurrentTitleId && x.SubScreenId == assignedPermission.SubId);
                        //        if (userpermssion is not null)
                        //            unitOfWork.UserPermssionRepositroy.Remove(userpermssion);

                        //        await unitOfWork.UserPermssionRepositroy.AddAsync(new UserPermission
                        //        {
                        //            UserId = userIdtitle.Id,
                        //            TitleId = userIdtitle.CurrentTitleId,
                        //            Permission = string.Join(',', assignedPermission.title_permission),
                        //            SubScreenId = assignedPermission.SubId
                        //        });

                        //        await unitOfWork.CompleteAsync();


                        //    }
                        //LabelExpression2:;

                        //}

                    }
                    else
                    {
                        var titlePermissionQuery = (await unitOfWork.TitlePermissionRepository
                           .GetSpecificSelectTrackingAsync(x => x.SubScreenId == assignedPermission.SubId, x => x, includeProperties: "ScreenSub,Title")).ToList();

                        if (titlePermissionQuery.Count() > 0)
                        {
                            unitOfWork.TitlePermissionRepository.RemoveRange(titlePermissionQuery);
                            await unitOfWork.CompleteAsync();
                        }
                        if (assignedPermission.title_permission.Count == 0 || assignedPermission.title_permission.Any(x => x == 0))
                        {
                            goto LabelExpression;
                        }
                        else
                        {
                            await unitOfWork.TitlePermissionRepository.AddAsync(new TitlePermission
                            {



                                TitleId = id,

                                SubScreenId = assignedPermission.SubId,
                                Permissions = string.Join(',', assignedPermission.title_permission)
                            });
                            await unitOfWork.CompleteAsync();
                        }
                    LabelExpression:;



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


            return new()
            {
                Check = true,
                Data = "",

            };
        }
    }
}
