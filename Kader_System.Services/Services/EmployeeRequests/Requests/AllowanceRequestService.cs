using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class AllowanceRequestService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : IAllowanceRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;



        #region ListOfAllwanceRequest

        public async Task<Response<IEnumerable<DTOAllowanceRequest>>> ListOfAllowanceRequest()
        {
            var result = await unitOfWork.AllowanceRequests.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id));
            var msg = sharLocalizer[Localization.NotFound];
            if (result == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var mappingResult = mapper.Map<IEnumerable<DTOAllowanceRequest>>(result);

            return new()
            {
                Data = mappingResult,
                Msg = msg,
            };
        }
        #endregion


        #region PaginatedAllwanceRequest
        public async Task<Response<GetAllowanceRequestResponse>> GetAllowanceRequest(GetAllFilterationAllowanceRequest model, string host)
        {
            Expression<Func<AllowanceRequest, bool>> filter = model.ApporvalStatus == RequestStatusTypes.All ?
                     x => x.IsDeleted == false :
                     x => x.IsDeleted == false && x.StatuesOfRequest.ApporvalStatus == (int)model.ApporvalStatus; var totalRecords = await unitOfWork.AllowanceRequests.CountAsync(filter: filter);
            var data = await unitOfWork.AllowanceRequests.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id));
            var msg = sharLocalizer[Localization.NotFound];
            if (data == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var mappingResult = mapper.Map<List<DTOAllowanceRequestResponse>>(data);


            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllowanceRequestResponse()
            {
                TotalRecords = totalRecords,

                Items = mappingResult,
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
                        Items = [],
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
        #endregion


        #region AllwanceRequestGetById
        public async Task<Response<DTOAllowanceRequestResponse>> GetById(int id)

        {
            var result = await unitOfWork.AllowanceRequests.GetByIdAsync(id);
            if (result == null)
            {
                var msg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };

            }

            var mappingResult = mapper.Map<DTOAllowanceRequestResponse>(result);

            return new()
            {
                Data = mappingResult,
                Check = true,

            };

        }
        #endregion

        #region AddAllowanceRequest
        public async Task<Response<GetAllowanceRequestResponse>> AddNewAllowanceRequest(DTOAllowanceRequest model,string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.AllowanceRequest)
        {

            var IsEmpolyeeExisted = await unitOfWork.Employees.ExistAsync(model.EmployeeId);
            if (!IsEmpolyeeExisted)
            {

                var msg = $"{sharLocalizer[Localization.Employee]} {sharLocalizer[Localization.NotFound]}";
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var newRequest = mapper.Map<AllowanceRequest>(model);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.attachment_file_name = (model.Attachment== null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(moduleNameWithType, model.Attachment);
            await unitOfWork.AllowanceRequests.AddAsync(newRequest);
            var result = await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }
        #endregion


        #region UpdateAllowanceRequest
        public async Task<Response<AllowanceRequest>> UpdateAllowanceRequest(int id, DTOAllowanceRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.AllowanceRequest)
        {
            var result = await unitOfWork.AllowanceRequests.GetByIdAsync(id);

            if (result == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = sharLocalizer[Localization.NotFound]
                };
            }
            var updatingModel = mapper.Map(model, result);
            if (model.Attachment is not null)
            {
                var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
                updatingModel.attachment_file_name = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                    await _fileServer.UploadFile(moduleNameWithType, model.Attachment);
            }
            unitOfWork.AllowanceRequests.Update(result);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Data = updatingModel,
                Check = true
            };


        }

        #endregion

        #region DeleteAllowance
        public async Task<Response<AllowanceRequest>> DeleteAllowanceRequest(int id)
        {
            var allowanceRequest = await unitOfWork.AllowanceRequests.GetByIdAsync(id);
            var msg = $"{sharLocalizer[Localization.Employee]} {sharLocalizer[Localization.NotFound]}";
            if (allowanceRequest == null)
            {

                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };
            }
            unitOfWork.AllowanceRequests.Remove(allowanceRequest);
            await unitOfWork.CompleteAsync();
            msg = sharLocalizer[Localization.Deleted];

            return new()
            {
                Data = allowanceRequest,
                Msg = msg,
                Check = true,
            };

        }

    

      

      





        #endregion







    }
}
