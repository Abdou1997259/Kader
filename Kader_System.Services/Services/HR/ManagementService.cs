using Kader_System.Domain.DTOs;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.HR
{
    public class ManagementService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> shareLocalizer, IMapper mapper) : IManagementService
    {
        private HrManagement _instanceManagement;

        #region Retrieve

    
        public async Task<Response<IEnumerable<HrListOfManagementsResponse>>> ListOfManagementsAsync(string lang)
        {
            var result =
                await unitOfWork.Managements.GetSpecificSelectAsync(null!,
                    select: x => new HrListOfManagementsResponse
                    {
                        Id = x.Id,
                        NameAr =x.NameAr,
                        NameEn = x.NameEn,
                        CompanyId = x.CompanyId,
                        ManagerId = x.ManagerId
                    }, orderBy: x =>
                        x.OrderByDescending(x => x.Id));

            if (!result.Any())
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

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

        public async Task<Response<GetAllManagementsResponse>> GetAllManagementsAsync(string lang, 
            HrGetAllFiltrationsFoManagementsRequest model,string host)
        {
            Expression<Func<HrManagement, bool>> filter = x => x.IsDeleted == model.IsDeleted;
            var totalRecords = await unitOfWork.Managements.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllManagementsResponse
            {
                TotalRecords = totalRecords,

                Items = (await unitOfWork.Managements.GetSpecificSelectAsync(filter: filter,includeProperties:$"{nameof(_instanceManagement.Company)},{nameof(_instanceManagement.Manager)}",
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    select: x => new ManagementData
                    {
                        Id = x.Id,
                        NameAr = x.NameAr,
                        NameEn = x.NameEn,
                        CompanyId = x.CompanyId,
                        ManagerId = x.ManagerId,
                        CompanyName = lang == Localization.Arabic ? x.Company.NameAr : x.Company.NameEn,
                        ManagerName = lang == Localization.Arabic ? x.Manager.FullNameAr : x.Manager.FullNameEn
                    }, orderBy: x =>
                        x.OrderByDescending(x => x.Id))).ToList(),
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
                string resultMsg = shareLocalizer[Localization.NotFoundData];

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

      

        public async Task<Response<HrGetManagementByIdResponse>> GetManagementByIdAsync(int id,string lang)
        {
            Expression<Func<HrManagement, bool>> filter = x => x.Id == id;
            var obj = await unitOfWork.Managements.GetFirstOrDefaultAsync(filter, includeProperties: $"{nameof(_instanceManagement.Company)},{nameof(_instanceManagement.Manager)}");

            if (obj is null)
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

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
                    NameAr = obj.NameAr,
                    NameEn = obj.NameEn,
                    CompanyId = obj.CompanyId,
                    ManagerId = obj.ManagerId,
                    CompanyName = lang == Localization.Arabic ? obj.Company.NameAr : obj.Company.NameEn,
                    ManagerName = lang == Localization.Arabic ? obj.Manager?.FullNameAr : obj.Manager?.FullNameEn
                },
                Check = true
            };
        }

        #endregion

        #region Insert

        public async Task<Response<CreateManagementRequest>> CreateManagementAsync(CreateManagementRequest model)
        {
            var exists = await unitOfWork.Managements.ExistAsync(x => x.NameAr.Trim() == model.NameAr.Trim()
                                                                    && x.NameEn.Trim() == model.NameEn.Trim());

            if (exists)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.IsExist],
                    shareLocalizer[Localization.Management]);

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }
            await unitOfWork.Managements.AddAsync(new()
            {
                NameEn = model.NameEn,
                NameAr = model.NameAr,
                CompanyId = model.CompanyId,
                ManagerId = model.ManagerId,
                
            });
            await unitOfWork.CompleteAsync();

            return new()
            {
                Msg = shareLocalizer[Localization.Done],
                Check = true,
                Data = model
            };

        }
        #endregion

        #region Updates

        public async Task<Response<CreateManagementRequest>> UpdateManagementAsync(int id, CreateManagementRequest model)
        {
            var obj = await unitOfWork.Managements.GetByIdAsync(id);
            if (obj == null)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                    shareLocalizer[Localization.Management]);

                return new()
                {
                    Data = model,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            obj.NameAr = model.NameAr;
            obj.NameEn=model.NameEn;
            obj.CompanyId=model.CompanyId;
            obj.ManagerId=model.ManagerId;

           await unitOfWork.CompleteAsync();
           return new()
           {
               Msg = shareLocalizer[Localization.Done],
               Check = true,
               Data = model
           };

        }
        #endregion

        public Task<Response<string>> UpdateActiveOrNotManagementAsync(int id)
        {
            throw new NotImplementedException();
        }

        #region Delete

        public async Task<Response<string>> DeleteManagementAsync(int id)
        {
            var obj=await unitOfWork.Managements.GetByIdAsync(id);
            if (obj == null)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                    shareLocalizer[Localization.Management]);

                return new()
                {
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            unitOfWork.Managements.Remove(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = string.Empty,
                Msg = shareLocalizer[Localization.Deleted]
            };
        }

        #endregion

    }
}
