

using Kader_System.DataAccess.Repositories;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.Auth;
using Kader_System.Domain.Interfaces;
using Microsoft.Extensions.Hosting;
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

        public async Task<Response<GetAllTitleResponse>> GetAllTitlesAsync(string lang, GetAllFilterrationForTitleRequest model, string host)
        {
            Expression<Func<Title, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                              &&(string.IsNullOrEmpty(model.Word) ||
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

                Items = (await unitOfWork.Titles.GetSpecificSelectAsync(filter: filter,x =>x,
                     take: model.PageSize,
                     skip: (model.PageNumber - 1) * model.PageSize)).Select(x=>new TitleData
                     {
                         TitleNameAr = x.TitleNameAr,
                         TitleNameEn = x.TitleNameEn,   
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
            //var listOfTitlePermssion = pers.Select(x => new TitlePermission
            //{
            //    SubScreenId = x.SubScreenId,
            //    TitleId = title.Id,
            //    Permissions = string.Join(',', x.Permission)

            //});
            //await unitOfWork.TitlePermissionRepository.AddRangeAsync(listOfTitlePermssion);


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



            int userCounter = 0;
            foreach (var assignedPermission in model)

            {
                var userPermissionQuery = await unitOfWork.TitlePermissionRepository
                    .GetSpecificSelectAsync(x=> x.SubScreenId == assignedPermission.SubId, x => x);

                var userPermission = all ? userPermissionQuery : userPermissionQuery.Where(x => x.Id == id);


                if (userPermission.Any())
                {
                    var listUpdatedPer = userPermission.Select(x => new TitlePermission
                    {
                        Id = x.Id,
                       
                        Permissions = string.Join(',', assignedPermission.TitlePermssion),
                        SubScreenId = x.SubScreenId,
                        TitleId = x.TitleId
                    });
                    unitOfWork.TitlePermissionRepository.UpdateRange(listUpdatedPer);
                }
                else
                {
                    // Check if the SubScreenId exists in the st_screens_subs table
                    var subScreenExists = await unitOfWork.SubMainScreens.AnyAsync(x => x.Id == assignedPermission.SubId);

                    if (subScreenExists)
                    {

                        await unitOfWork.TitlePermissionRepository.AddAsync(new TitlePermission
                        {




                           
                            SubScreenId = assignedPermission.SubId,
                            Permissions = string.Join(',', assignedPermission.TitlePermssion)
                        });
                    }
                    else
                    {
                        // Log the invalid SubScreenId or handle accordingly
                        Console.WriteLine("Invalid SubScreenId: " + assignedPermission.SubId);
                        continue;
                    }
                }
              


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
