using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class ContractTerminationRequestService(IUnitOfWork unitOfWork, IUserContextService userContextService, IRequestService requestService, IHttpContextAccessor httpContextAccessor, KaderDbContext context, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper)
        : IContractTerminationRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly KaderDbContext _context = context;
        private readonly IRequestService _requestService = requestService;
        private readonly IUserContextService _userContextService = userContextService;
        #region ListOfContractTerminationRequest
        public async Task<Response<IEnumerable<ListOfContractTerminationRequestResponse>>> ListOfContractTerminationRequest()
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var result = unitOfWork.ContractTerminationRequest
                .GetSpecificSelectAsync(x => x.IsDeleted == false && x.CompanyId == currentCompany, x => x, orderBy: x => x.OrderBy(x => x.Id));
            var msg = _sharLocalizer[Localization.NotFound];
            if (result == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var mappingResult = mapper.Map<IEnumerable<ListOfContractTerminationRequestResponse>>(result);

            return new()
            {
                Data = mappingResult,
                Msg = msg,
            };
        }
        #endregion

        #region PaginatedContractTerminationRequest
        public async Task<Response<GetAllContractTermiantionRequestResponse>> GetAllContractTerminationRequest(GetFilterationContractTerminationRequest model, string host)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();

            #region ApprovalExpression
            Expression<Func<Domain.Models.EmployeeRequests.Requests.ContractTerminationRequest, bool>> filter = x =>
                x.IsDeleted == false && x.CompanyId == currentCompany &&
                (model.ApporvalStatus == RequestStatusTypes.All ||
                (model.ApporvalStatus == RequestStatusTypes.Approved && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved) ||
                (model.ApporvalStatus == RequestStatusTypes.ApprovedRejected &&
                    (x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved ||
                     x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected)) ||
                (model.ApporvalStatus == RequestStatusTypes.Rejected && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected) ||
                (model.ApporvalStatus == RequestStatusTypes.Pending && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Pending));

            #endregion

            var totalRecords = await _unitOfWork.ContractTerminationRequest.CountAsync(filter: filter);
            var items = (await _unitOfWork.ContractTerminationRequest.GetSpecificSelectAsync(filter, x => new ListOfContractTerminationRequestResponse
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                request_date = x.Add_date.Value.ToString("yyyy-MM-dd"),
                EmployeeName = _requestService.GetRequestHeaderLanguage == Localization.English ? x.Employee.FullNameEn : x.Employee.FullNameAr,
                ApporvalStatus = x.StatuesOfRequest.ApporvalStatus,
                reason = x.StatuesOfRequest.StatusMessage,
                Notes = x.Notes,
                AttachmentPath = x.AttachmentPath != null ? _fileServer.CombinePath(Modules.EmployeeRequest, HrEmployeeRequestTypesEnums.TerminateContract.ToString(), x.AttachmentPath) : null
            },
            orderBy: x => x.OrderByDescending(x => x.Id),
                skip: (model.PageNumber - 1) * model.PageSize, take: model.PageSize, includeProperties: "Employee,StatuesOfRequest")).ToList();
            #region Pagination

            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            #endregion

            var result = new GetAllContractTermiantionRequestResponse
            {
                TotalRecords = totalRecords,
                Items = items,
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
                Links = pageLinks,
            };

            if (result.TotalRecords is 0)
            {
                string resultMsg = _sharLocalizer[Localization.NotFoundData];

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
        #endregion

        #region GetContractTerminationRequestById
        public async Task<Response<
            ListOfContractTerminationRequestResponse>> GetById(int id)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var result = await unitOfWork.ContractTerminationRequest
                .GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompanyId);

            if (result == null)
            {
                var msg = _sharLocalizer[Localization.NotFoundData];
                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };

            }

            var mappingResult = mapper.Map<ListOfContractTerminationRequestResponse>(result);
            return new()
            {
                Data = mappingResult,
                Check = true,

            };

        }
        #endregion

        #region AddContractTerminationRequest
        public async Task<Response<
            Domain.Models.EmployeeRequests.Requests.ContractTerminationRequest>>
            AddNewContractTerminationRequest(DTOContractTerminationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.TerminateContract)
        {

            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            if (!await _unitOfWork.Employees.ExistAsync(x =>
            x.Id == model.EmployeeId &&
            x.CompanyId == currentCompanyId))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted,
                    _sharLocalizer[Localization.Employee]]
                };
            }
            if (await _unitOfWork.ContractTerminationRequest.ExistAsync(
                x => x.EmployeeId == model.EmployeeId && x.CompanyId == currentCompanyId))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.RequestedBefore]
                };
            }
            var newRequest =
                _mapper.Map<Domain.Models.EmployeeRequests.Requests
                .ContractTerminationRequest>(model);
            newRequest.CompanyId = currentCompanyId;
            StatuesOfRequest statues = new()
            {
                ApporvalStatus = (int)RequestStatusTypes.Pending
            };
            newRequest.StatuesOfRequest = statues;
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            await _unitOfWork.ContractTerminationRequest.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }
        #endregion

        #region DeleteContractTerminationRequest
        public async Task<Response<string>> DeleteContracTermniationRequest(int id, string fullPath)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();

            var msg = $"{_sharLocalizer[Localization.Employee]} {_sharLocalizer[Localization.NotFound]}";
            var _contractTerminationRequest = await
                _unitOfWork.ContractTerminationRequest
                .GetEntityWithIncludeAsync(x => x.Id == id && x.CompanyId == currentCompanyId, "StatuesOfRequest");
            if (_contractTerminationRequest != null)
            {
                if (_contractTerminationRequest.StatuesOfRequest.ApporvalStatus != 1)
                {
                    msg = _sharLocalizer[Localization.ApproveRejectDelte];
                    return new()
                    {
                        Msg = msg,
                        Check = false,
                    };
                }
                var result = await _unitOfWork.ContractTerminationRequest.SoftDeleteAsync(_contractTerminationRequest, DeletedBy: userId);
                if (result > 0)
                {
                    if (!string.IsNullOrWhiteSpace(_contractTerminationRequest.AttachmentPath))
                    {
                        _fileServer.RemoveFile(fullPath, HrEmployeeRequestTypesEnums.TerminateContract.ToString(), _contractTerminationRequest.AttachmentPath);
                    }
                    msg = _sharLocalizer[Localization.Deleted];
                    return new()
                    {
                        Msg = msg,
                        Check = true,
                    };
                }
            }
            return new()
            {
                Check = false,
                Data = null,
                Msg = msg
            };

        }
        #endregion

        #region UpdateContractTerminationRequest
        public async
            Task<Response<Domain.Models.EmployeeRequests.Requests
                .ContractTerminationRequest>>
            UpdateContractTerminationRequest(int id, DTOContractTerminationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.TerminateContract)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            if (!await _unitOfWork.Employees.ExistAsync(x =>
            x.Id == model.EmployeeId && x.CompanyId == currentCompanyId))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted,
                    _sharLocalizer[Localization.Employee]]
                };
            }
            var _contract = await _unitOfWork.ContractTerminationRequest.GetFirstOrDefaultAsync
                (x => x.CompanyId == currentCompanyId && x.Id == id);
            if (_contract == null || _contract.StatuesOfRequest.ApporvalStatus != (int)RequestStatusTypes.Pending)
            {
                var msg = _sharLocalizer[Localization.NotFound] + " or " + _sharLocalizer[Localization.NotPending];
                return new()
                {
                    Check = false,
                    Msg = msg,
                    Data = null
                };
            }
            var mappedleave = _mapper.Map(model, _contract);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            #region UpdateFile
            if (model.Attachment is not null)
            {
                if (_contract.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, _contract.AttachmentPath);
                _contract.AttachmentPath = await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            }
            else
            {
                if (_contract.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, _contract.AttachmentPath);
                _contract.AttachmentPath = null;
            }

            #endregion

            _unitOfWork.ContractTerminationRequest.Update(_contract);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };


        }
        #endregion

        #region Status
        public async Task<Response<string>> ApproveRequest(int requestId)
        {
            var transaction = _unitOfWork.BeginTransaction();
            try
            {
                var contractTermination = await _unitOfWork.ContractTerminationRequest.GetByIdAsync(requestId);
                var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
                var userId = _httpContextAccessor.HttpContext.User.GetUserId();
                if (contractTermination == null)
                {
                    return new Response<string>()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.CannotBeFound]
                    };

                }
                var empId = contractTermination.EmployeeId;
                var contract = await _unitOfWork.Contracts.GetFirstOrDefaultAsync(x => x.employee_id == empId);
                if (contract == null)
                {
                    return new Response<string>()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.ContractNotFound]
                    };
                }



                var result = await
                  _unitOfWork.ContractTerminationRequest.
                  UpdateApporvalStatus(x => x.Id == requestId && x.CompanyId == currentCompanyId,
                  RequestStatusTypes.Approved, userId);

                contract.IsDeleted = true;

                if (result > 0)
                {
                    transaction.Commit();
                    return new Response<string>()
                    {
                        Check = true,
                        Msg = "Approved sucessfully"
                    };
                }
                return new Response<string>()
                {
                    Check = false,
                    Msg = "Cannot approve , request is not pending or is deleted"
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new Response<string>()
                {
                    Check = false,
                    Msg = ex.Message
                };
            }

        }
        public async Task<Response<string>> RejectRequest(int requestId, string resoan)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _unitOfWork.ContractTerminationRequest.UpdateApporvalStatus(x => x.Id == requestId, RequestStatusTypes.Rejected, userId, resoan);
            if (result > 0)
            {
                return new Response<string>()
                {
                    Check = true,
                    Msg = "Rejected sucessfully"
                };
            }
            return new Response<string>()
            {
                Check = false,
                Msg = "Cannot approve"
            };
        }
        #endregion
    }
}
