using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class AllowanceRequestService(IUnitOfWork unitOfWork, IUserContextService userContextService, KaderDbContext context, ITransAllowanceService allowanceService, IRequestService requestService, IStringLocalizer<SharedResource> sharLocalizer, IHttpContextAccessor httpContextAccessor, IFileServer fileServer, IMapper mapper) : IAllowanceRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITransAllowanceService _allowanceService = allowanceService;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly KaderDbContext _context = context;
        private readonly IRequestService _requestService = requestService;
        private readonly IUserContextService _userContextService = userContextService;

        #region ListOfAllwanceRequest

        public async Task<Response<IEnumerable<DTOAllowanceRequest>>> ListOfAllowanceRequest()
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();

            var result = await unitOfWork.AllowanceRequests.
                GetSpecificSelectAsync(x => x.IsDeleted == false && x.company_id == currentCompanyId, x => x, orderBy: x => x.OrderByDescending(x => x.Id));
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
        public async Task<Response<GetAllowanceRequestRequestResponse>> GetAllowanceRequest(GetAllFilterationAllowanceRequest model, string host)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            #region ApprovalExpression
            Expression<Func<AllowanceRequest, bool>> filter = x =>
                x.IsDeleted == false && x.company_id == currentCompanyId &&
                (model.ApporvalStatus == RequestStatusTypes.All ||
                (model.ApporvalStatus == RequestStatusTypes.Approved && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved) ||
                (model.ApporvalStatus == RequestStatusTypes.ApprovedRejected &&
                    (x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved ||
                     x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected)) ||
                (model.ApporvalStatus == RequestStatusTypes.Rejected && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected) ||
                (model.ApporvalStatus == RequestStatusTypes.Pending && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Pending));

            #endregion

            var totalRecords = await _unitOfWork.AllowanceRequests.CountAsync(filter);

            var items = await (from x in _context.AllowanceRequests.AsNoTracking().
                                        Include(x => x.StatuesOfRequest).Where(filter)
                               join emp in _context.Employees on x.EmployeeId equals emp.Id
                               join allowance in _context.Allowances on x.allowance_id equals allowance.Id
                               join allowance_type in _context.TransSalaryEffects on x.allowance_type_id equals allowance_type.Id
                               select new ListOfAllowanceRequestResponse
                               {
                                   Id = x.Id,
                                   EmployeeId = x.EmployeeId,
                                   request_date = x.Add_date.Value.ToString("yyyy-mm-dd"),
                                   EmployeeName = _requestService.GetRequestHeaderLanguage == Localization.English ? x.Employee.FullNameEn : x.Employee.FullNameAr,
                                   allowance_id = x.allowance_id,
                                   allowance_type_id = x.allowance_type_id,
                                   amount = x.amount,
                                   allowance_name = _requestService.GetRequestHeaderLanguage == Localization.English ? allowance.Name_en : allowance.Name_ar,
                                   allowance_type_name = _requestService.GetRequestHeaderLanguage == Localization.English ? allowance_type.NameInEnglish : allowance_type.Name,
                                   ApporvalStatus = x.StatuesOfRequest.ApporvalStatus,
                                   reason = x.StatuesOfRequest.StatusMessage,
                                   Notes = x.notes,
                                   AttachmentPath = x.AttachmentPath != null ? _fileServer.CombinePath(Modules.EmployeeRequest, HrEmployeeRequestTypesEnums.AllowanceRequest.ToString(), x.AttachmentPath) : null
                               }).OrderByDescending(x => x.Id).Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize).ToListAsync();
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

            var result = new GetAllowanceRequestRequestResponse
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


        #region AllwanceRequestGetById
        public async Task<Response<ListOfAllowanceRequestResponse>> GetById(int id)

        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var result = await unitOfWork.AllowanceRequests.GetFirstOrDefaultAsync(x => x.Id == id && x.company_id == currentCompanyId);
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

            var mappingResult = mapper.Map<ListOfAllowanceRequestResponse>(result);

            return new()
            {
                Data = mappingResult,
                Check = true,

            };

        }
        #endregion

        #region AddAllowanceRequest
        public async Task<Response<GetAllowanceRequestRequestResponse>> AddNewAllowanceRequest(DTOAllowanceRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.AllowanceRequest)
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

            var newRequest = _mapper.Map<AllowanceRequest>(model);
            newRequest.company_id = currentCompanyId;
            StatuesOfRequest statues = new()
            {
                ApporvalStatus = (int)RequestStatusTypes.Pending
            };
            newRequest.StatuesOfRequest = statues;
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            newRequest.company_id = currentCompanyId;
            await _unitOfWork.AllowanceRequests.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }
        #endregion


        #region UpdateAllowanceRequest
        public async Task<Response<AllowanceRequest>> UpdateAllowanceRequest(int id, DTOAllowanceRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.AllowanceRequest)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var allowance = await _unitOfWork.AllowanceRequests.GetFirstOrDefaultAsync(x => x.company_id == currentCompanyId && x.Id == id);
            if (allowance == null || allowance.StatuesOfRequest.ApporvalStatus != (int)RequestStatusTypes.Pending)
            {
                var msg = _sharLocalizer[Localization.NotFound] + " or " + _sharLocalizer[Localization.NotPending];
                return new()
                {
                    Check = false,
                    Msg = msg,
                    Data = null
                };
            }
            if (!await _unitOfWork.Employees.ExistAsync(x => x.Id == model.EmployeeId && x.CompanyId == currentCompanyId))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Employee]]
                };
            }
            var mappedleave = _mapper.Map(model, allowance);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);

            #region UpdateFile
            if (model.Attachment is not null)
            {
                if (allowance.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, allowance.AttachmentPath);
                allowance.AttachmentPath = await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            }
            else
            {
                if (allowance.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, allowance.AttachmentPath);
                allowance.AttachmentPath = null;
            }

            #endregion

            _unitOfWork.AllowanceRequests.Update(allowance);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }

        #endregion

        #region DeleteAllowance
        public async Task<Response<AllowanceRequest>> DeleteAllowanceRequest(int id, string ModuleName)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var msg = $"{_sharLocalizer[Localization.Employee]} {_sharLocalizer[Localization.NotFound]}";
            var _AllowanceRequests = await
                _unitOfWork.AllowanceRequests.GetEntityWithIncludeAsync(x => x.Id == id && x.company_id == currentCompanyId, "StatuesOfRequest");
            if (_AllowanceRequests != null)
            {
                if (_AllowanceRequests.StatuesOfRequest.ApporvalStatus != 1)
                {
                    msg = _sharLocalizer[Localization.ApproveRejectDelte];
                    return new()
                    {
                        Msg = msg,
                        Check = false,
                    };
                }
                var result = await _unitOfWork.AllowanceRequests.SoftDeleteAsync(_AllowanceRequests, DeletedBy: userId);
                if (result > 0)
                {
                    if (!string.IsNullOrWhiteSpace(_AllowanceRequests.AttachmentPath))
                    {
                        _fileServer.RemoveFile(ModuleName, HrEmployeeRequestTypesEnums.AllowanceRequest.ToString(), _AllowanceRequests.AttachmentPath);
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



        #region Status
        public async Task<Response<string>> ApproveRequest(int requestId, string lang)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();

            var allownecesRequest = await
                _unitOfWork.AllowanceRequests.GetFirstOrDefaultAsync(x =>
                x.Id == requestId && x.company_id == currentCompanyId);

            if (allownecesRequest == null)
            {
                var msg = _sharLocalizer[Localization.NotFound];
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };
            }
            if (allownecesRequest.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved)
            {
                return new Response<string>()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.ApprovedAready]
                };

            }

            var createresult = await _allowanceService.CreateTransAllowanceAsync(new CreateTransAllowanceRequest
            {
                AllowanceId = allownecesRequest.allowance_id,
                ActionMonth = new DateOnly(allownecesRequest.Add_date.Value.Year, allownecesRequest.Add_date.Value.Month, allownecesRequest.Add_date.Value.Day),
                Amount = allownecesRequest.amount,
                SalaryEffectId = allownecesRequest.allowance_type_id,

                EmployeeId = allownecesRequest.EmployeeId,



            }, lang);

            if (!createresult.Check)
            {
                return new Response<string>()
                {
                    Check = false,
                    Msg = createresult.Msg
                };
            }

            var result = await _unitOfWork.AllowanceRequests.UpdateApporvalStatus(x => x.Id
            == requestId, RequestStatusTypes.Approved, userId);
            if (result > 0)
            {
                return new Response<string>()
                {
                    Check = true,
                    Msg = _sharLocalizer[Localization.Approved]
                };
            }
            return new Response<string>()
            {
                Check = false,
                Msg = _sharLocalizer[Localization.NotApproved]
            };
        }
        public async Task<Response<string>> RejectRequest(int requestId, string resoan)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _unitOfWork.AllowanceRequests
                .UpdateApporvalStatus(x => x.Id == requestId && x.company_id == currentCompanyId, RequestStatusTypes.Rejected, userId, resoan);
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
