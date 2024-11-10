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



    public class ResignationRequestService(IUnitOfWork unitOfWork, IUserContextService userContextService, ILogger<ResignationRequestService> logger, KaderDbContext context, IRequestService requestService, IHttpContextAccessor httpContextAccessor, IHttpContextService contextService, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : IResignationRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        private readonly IHttpContextService _contextService = contextService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly KaderDbContext _context = context;
        private readonly IRequestService _requestService = requestService;
        private readonly ILogger<ResignationRequestService> _logger = logger;
        private readonly IUserContextService _userContextService = userContextService;
        #region ListOfResignationRequest
        public async Task<Response<IEnumerable<ListOfResignationRequestResponse>>> ListOfResignationRequest()
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var result = await unitOfWork.ResignationRepository
                .GetSpecificSelectAsync(x => x.IsDeleted == false && x.CompanyId == currentCompanyId, x => x, orderBy: x => x.OrderBy(x => x.Id));
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
            var mappingResult = mapper.Map<IEnumerable<ListOfResignationRequestResponse>>(result);

            return new()
            {
                Data = mappingResult,
                Msg = msg,
            };
        }
        #endregion

        #region PaginatedResignationRequest
        public async Task<Response<GetAllResignationRequestResponse>> GetAllResignationRequest(GetFillterationResignationRequest model, string host)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            #region ApprovalExpression
            Expression<Func<ResignationRequest, bool>> filter = x =>
                x.IsDeleted == false && x.CompanyId == currentCompanyId &&
                (model.ApporvalStatus == RequestStatusTypes.All ||
                (model.ApporvalStatus == RequestStatusTypes.Approved && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved) ||
                (model.ApporvalStatus == RequestStatusTypes.ApprovedRejected &&
                    (x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved ||
                     x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected)) ||
                (model.ApporvalStatus == RequestStatusTypes.Rejected && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected) ||
                (model.ApporvalStatus == RequestStatusTypes.Pending && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Pending));

            #endregion

            var totalRecords = await _unitOfWork.ResignationRepository.CountAsync(filter: filter);
            var items = (await _unitOfWork.ResignationRepository.GetSpecificSelectAsync(filter, x => new ListOfResignationRequestResponse
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                request_date = x.Add_date.Value.ToString("yyyy-MM-dd"),
                EmployeeName = _requestService.GetRequestHeaderLanguage == Localization.English ? x.Employee.FullNameEn : x.Employee.FullNameAr,
                ApporvalStatus = x.StatuesOfRequest.ApporvalStatus,
                reason = x.StatuesOfRequest.StatusMessage,
                Notes = x.Notes,
                AttachmentPath = x.AttachmentPath != null ? _fileServer.CombinePath(Modules.EmployeeRequest, HrEmployeeRequestTypesEnums.ResignationRequest.ToString(), x.AttachmentPath) : null
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

            var result = new GetAllResignationRequestResponse
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

        #region GetResignationRequetById
        public async Task<Response<DtoListOfResignationResposne>> GetById(int id)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var result = await unitOfWork.ResignationRepository.GetFirstOrDefaultAsync(
                x => x.CompanyId == currentCompanyId && x.Id == id);
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

            var mappingResult = mapper.Map<DtoListOfResignationResposne>(result);
            return new()
            {
                Data = mappingResult,
                Check = true,

            };

        }
        #endregion

        #region AddResignationRequest
        public async Task<Response<ResignationRequest>> AddNewResignationRequest(DTOResignationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.ResignationRequest)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            if (!await _unitOfWork.Employees.ExistAsync(x => x.Id ==
            model.EmployeeId && x.CompanyId == currentCompanyId))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Employee]]
                };

            }
            if (await _unitOfWork.ResignationRepository.ExistAsync(x =>
            x.EmployeeId == model.EmployeeId && x.CompanyId == currentCompanyId &&
            x.StatuesOfRequest.StatusTypes == RequestStatusTypes.Pending))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsPending]
                };
            }
            var newRequest = _mapper.Map<ResignationRequest>(model);
            newRequest.CompanyId = currentCompanyId;
            StatuesOfRequest statues = new()
            {
                StatusTypes = RequestStatusTypes.Pending
            };
            newRequest.StatuesOfRequest = statues;
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            await _unitOfWork.ResignationRepository.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }
        #endregion

        #region DeleteResignationRequest
        public async Task<Response<ResignationRequest>> DeleteResignationRequest(int id, string ModuleName)
        {
            try
            {
                var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
                var userId = _httpContextAccessor.HttpContext.User.GetUserId();

                var resignationRequest = await _unitOfWork.ResignationRepository
                    .GetFirstOrDefaultAsync(x => x.CompanyId == currentCompanyId && x.Id == id);
                if (resignationRequest == null)
                {
                    return new()
                    {
                        Msg = _sharLocalizer[Localization.CannotBeFound, _sharLocalizer[Localization.Resignation]],
                        Check = true,
                    };

                }
                if (resignationRequest?.StatuesOfRequest?.ApporvalStatus != 1)
                {

                    return new()
                    {
                        Msg = _sharLocalizer[Localization.ApproveRejectDelte],
                        Check = false,
                    };
                }


                if (!string.IsNullOrWhiteSpace(resignationRequest.AttachmentPath))
                {
                    _fileServer.RemoveFile(ModuleName, HrEmployeeRequestTypesEnums.ResignationRequest.ToString(), resignationRequest.AttachmentPath);
                }
                resignationRequest.IsDeleted = true;
                resignationRequest.DeleteDate = DateTime.Now;
                resignationRequest.DeleteBy = userId;
                _unitOfWork.ResignationRepository.Update(resignationRequest);
                await _unitOfWork.CompleteAsync();


                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = _sharLocalizer[Localization.Deleted]
                };
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.InnerException.Message);
                return new()
                {
                    Check = false,
                    Msg = ex?.InnerException?.Message
                };

            }

        }
        #endregion

        #region UpdateResignationRequest
        public async Task<Response<ResignationRequest>> UpdateResignationRequest(int id, DTOResignationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.ResignationRequest)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            if (!await _unitOfWork.Employees.ExistAsync(x => x.Id == model.EmployeeId && x.CompanyId == currentCompanyId))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Employee]]
                };
            }

            var resignation = await _unitOfWork
                .ResignationRepository.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompanyId);
            if (resignation == null || resignation.StatuesOfRequest.ApporvalStatus != (int)RequestStatusTypes.Pending)
            {
                var msg = _sharLocalizer[Localization.NotFound] + " or " + _sharLocalizer[Localization.NotPending];
                return new()
                {
                    Check = false,
                    Msg = msg,
                    Data = null
                };
            }
            var mappedresignation = _mapper.Map(model, resignation);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);


            #region UpdateFile
            if (model.Attachment is not null)
            {
                if (resignation.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, resignation.AttachmentPath);
                resignation.AttachmentPath = await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            }
            else
            {
                if (resignation.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, resignation.AttachmentPath);
                resignation.AttachmentPath = null;
            }

            #endregion

            _unitOfWork.ResignationRepository.Update(resignation);
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
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();

            var userId = _httpContextAccessor.HttpContext
                .User.GetUserId();
            var result = await _unitOfWork.ResignationRepository
                .UpdateApporvalStatus(x =>
                x.Id == requestId &&
                x.CompanyId == currentCompanyId, RequestStatusTypes.Approved, userId);


            if (result > 0)
            {


                var request = await _unitOfWork.ResignationRepository.GetFirstOrDefaultAsync(x => x.Id == requestId && x.CompanyId == currentCompanyId);
                var employee = await _unitOfWork.Employees.GetByIdAsync(request.Id);
                employee.IsActive = false;
                await _unitOfWork.CompleteAsync();

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
        public async Task<Response<string>> RejectRequest(int requestId, string resoan)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _unitOfWork.ResignationRepository
                .UpdateApporvalStatus(x => x.Id == requestId && x.CompanyId == currentCompanyId, RequestStatusTypes.Rejected, userId, resoan);
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
