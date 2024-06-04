using Kader_System.Domain.DTOs;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.HR
{
    public class DepartmentService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> shareLocalizer, IMapper mapper) :IDepartmentService
    {
        private HrDepartment _instanceDepartment;

        #region Retrieve

        public async Task<Response<IEnumerable<ListOfDepartmentsResponse>>> ListOfDepartmentsAsync(string lang)
        {
            var result =
                await unitOfWork.Departments.GetSpecificSelectAsync(null!,
                    select: x => new ListOfDepartmentsResponse()
                    {
                        Id = x.Id,
                        NameAr = x.NameAr,
                        NameEn = x.NameEn,
                        ManagementId = x.ManagementId,
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

        public async Task<Response<GetAllDepartmentsResponse>> GetAllDepartmentsAsync(string lang
            , GetAllFiltrationsForDepartmentsRequest model,string host)
        {
            Expression<Func<HrDepartment, bool>> filter = x => x.IsDeleted == model.IsDeleted &&
                                                               (string.IsNullOrEmpty(model.Word) || x.NameAr.Contains(model.Word) || x.NameEn.Contains(model.Word));
            var totalRecords = await unitOfWork.Departments.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllDepartmentsResponse
            {
                TotalRecords = totalRecords,

                Items = (await unitOfWork.Departments.GetSpecificSelectAsync(filter: filter, includeProperties: $"{nameof(_instanceDepartment.Management)},{nameof(_instanceDepartment.Manager)}",
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    select: x => new DepartmentData
                    {
                        Id = x.Id,
                        NameAr = x.NameAr,
                        NameEn = x.NameEn,
                        ManagementId = x.ManagementId,
                        ManagerId = x.ManagerId,
                        ManagementName = lang == Localization.Arabic ? x.Management.NameAr : x.Management.NameEn,
                        ManagerName = lang == Localization.Arabic ? x.Manager.FirstNameAr : x.Manager.FirstNameEn
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


        public async Task<Response<GetDepartmentByIdResponse>> GetDepartmentByIdAsync(int id, string lang)
        {
            Expression<Func<HrDepartment, bool>> filter = x => x.Id == id;
            var obj = await unitOfWork.Departments.GetFirstOrDefaultAsync(filter, includeProperties: $"{nameof(_instanceDepartment.Management)},{nameof(_instanceDepartment.Manager)}");

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
                    ManagementId = obj.ManagementId,
                    ManagerId = obj.ManagerId,
                    ManagementName = lang == Localization.Arabic ? obj.Management.NameAr : obj.Management.NameEn,
                    ManagerName = lang == Localization.Arabic ? obj.Manager?.FullNameAr : obj.Manager?.FullNameEn
                },
                Check = true
            };
        }


        #endregion


        #region Insert
        public async Task<Response<CreateDepartmentRequest>> CreateDepartmentAsync(CreateDepartmentRequest model)
        {
            var exists = await unitOfWork.Departments.ExistAsync(x => x.NameAr.Trim() == model.NameAr.Trim()
                                                                      && x.NameEn.Trim() == model.NameEn.Trim());

            if (exists)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.IsExist],
                    shareLocalizer[Localization.Department]);

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }
            var department = mapper.Map<HrDepartment>(model);
            await unitOfWork.Departments.AddAsync(department);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Msg = shareLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        #endregion

        #region Update
        public async Task<Response<CreateDepartmentRequest>> UpdateDepartmentAsync(int id, CreateDepartmentRequest model)
        {
            var obj = await unitOfWork.Departments.GetByIdAsync(id);
            if (obj == null)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                    shareLocalizer[Localization.Department]);

                return new()
                {
                    Data = model,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            obj.NameAr = model.NameAr;
            obj.NameEn = model.NameEn;
            obj.ManagementId = model.ManagementId;
            obj.ManagerId = model.ManagerId;

            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = shareLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }


        #endregion


        public Task<Response<string>> UpdateActiveOrNotDepartmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        #region Delete

        public async Task<Response<string>> DeleteDepartmentAsync(int id)
        {
            var obj = await unitOfWork.Departments.GetByIdAsync(id);
            if (obj == null)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                    shareLocalizer[Localization.Department]);

                return new()
                {
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            unitOfWork.Departments.Remove(obj);
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
