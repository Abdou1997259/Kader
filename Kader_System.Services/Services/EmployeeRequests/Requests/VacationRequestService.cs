using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;
using Microsoft.EntityFrameworkCore;
using TransVacation = Kader_System.Domain.Models.Trans.TransVacation;


namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class VacationRequestService(IUnitOfWork unitOfWork, IUserContextService userContextService, KaderDbContext context, ITransVacationService transVacation, IRequestService requestService, IHttpContextAccessor httpContextAccessor, IHttpContextService contextService, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : IVacationRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        private readonly IHttpContextService _contextService = contextService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly KaderDbContext _context = context;
        private readonly IRequestService _requestService = requestService;
        private readonly ITransVacationService _vacationService = transVacation;
        private readonly IUserContextService _userContextService = userContextService;
        #region ListOfVacationRequest
        public async Task<Response<IEnumerable<DTOVacationRequest>>> ListOfVacationRequest()
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();

            var result = await unitOfWork.VacationRequests
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
            var mappingResult = mapper.Map<IEnumerable<DTOVacationRequest>>(result);

            return new()
            {
                Data = mappingResult,
                Msg = msg,
            };
        }
        #endregion

        #region PaginatedVacationRequest
        public async Task<Response<GetAllVacationRequestReponse>> GetAllVacationRequest(GetFilterationVacationRequestRequest model, string host)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            #region ApprovalExpression
            Expression<Func<VacationRequests, bool>> filter = x =>
                x.IsDeleted == false && x.CompanyId == currentCompanyId &&
                (model.ApporvalStatus == RequestStatusTypes.All ||
                (model.ApporvalStatus == RequestStatusTypes.Approved && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved) ||
                (model.ApporvalStatus == RequestStatusTypes.ApprovedRejected &&
                    (x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved ||
                     x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected)) ||
                (model.ApporvalStatus == RequestStatusTypes.Rejected && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected) ||
                (model.ApporvalStatus == RequestStatusTypes.Pending && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Pending));

            #endregion

            var totalRecords = await _unitOfWork.VacationRequests.CountAsync(filter);

            var items = await (from x in _context.HrVacationRequests.AsNoTracking().
                                        Include(x => x.StatuesOfRequest).Where(filter)
                               join emp in _context.Employees on x.EmployeeId equals emp.Id
                               join vac in _context.VacationTypes on x.VacationTypeId equals vac.Id
                               select new ListOfVacationRequestResponse
                               {
                                   Id = x.Id,
                                   EmployeeId = x.EmployeeId,
                                   request_date = x.Add_date.Value.ToString("yyyy-MM-dd"),
                                   EmployeeName = _requestService.GetRequestHeaderLanguage == Localization.English ? x.Employee.FullNameEn : x.Employee.FullNameAr,
                                   DayCounts = x.DayCounts,
                                   StartDate = x.StartDate,
                                   EndDate = x.StartDate.AddDays(x.DayCounts),
                                   VacationTypeId = x.VacationTypeId,
                                   VacationTypeName = _requestService.GetRequestHeaderLanguage == Localization.English ? x.VacationType.NameInEnglish : x.VacationType.Name,
                                   ApporvalStatus = x.StatuesOfRequest.ApporvalStatus,
                                   reason = x.StatuesOfRequest.StatusMessage,
                                   Notes = x.Notes,
                                   AttachmentPath = x.AttachmentPath != null ? _fileServer.CombinePath(Modules.EmployeeRequest, HrEmployeeRequestTypesEnums.VacationRequest.ToString(), x.AttachmentPath) : null
                               }).OrderByDescending(x => x.Id)
                               .Skip((model.PageNumber - 1) * model.PageSize)
                               .Take(model.PageSize).ToListAsync();
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

            var result = new GetAllVacationRequestReponse
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

        #region GetVacationRequetById
        public async Task<Response<ListOfVacationRequestResponse>> GetById(int id)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();

            var result = await unitOfWork.VacationRequests.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompanyId);
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

            var mappingResult = mapper.Map<ListOfVacationRequestResponse>(result);
            return new()
            {
                Data = mappingResult,
                Check = true,

            };

        }
        #endregion

        #region AddVacationRequest
        public async Task<Response<VacationRequests>> AddNewVacationRequest(DTOVacationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.VacationRequest)
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
            if (await _unitOfWork.VacationRequests.ExistAsync
                (x => x.CompanyId == currentCompanyId &&
                x.StartDate == model.StartDate &&
                 x.EmployeeId == model.EmployeeId &&
                 x.VacationTypeId == model.VacationTypeId

                ))
            {
                return new Response<VacationRequests>
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.AlreadyExited, _sharLocalizer[Localization.VacationRequest]]
                };
            }
            var newRequest = _mapper.Map<VacationRequests>(model);
            newRequest.CompanyId = currentCompanyId;
            StatuesOfRequest statues = new()
            {
                ApporvalStatus = (int)RequestStatusTypes.Pending
            };
            newRequest.StatuesOfRequest = statues;
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            await _unitOfWork.VacationRequests.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }
        #endregion

        #region DeleteVacationRequets
        public async Task<Response<VacationRequests>> DeleteVacationRequest(int id, string moduleName)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var msg = $"{_sharLocalizer[Localization.Employee]} {_sharLocalizer[Localization.NotFound]}";
            var vacationRequest = await _unitOfWork.VacationRequests.GetEntityWithIncludeAsync(x => x.Id == id && x.CompanyId == currentCompanyId, "StatuesOfRequest");
            if (vacationRequest != null)
            {
                if (vacationRequest.StatuesOfRequest.ApporvalStatus != 1)
                {
                    msg = _sharLocalizer[Localization.ApproveRejectDelte];
                    return new()
                    {
                        Msg = msg,
                        Check = false,
                    };
                }
                var result = await _unitOfWork.VacationRequests.SoftDeleteAsync(vacationRequest, DeletedBy: userId);
                if (result > 0)
                {
                    if (!string.IsNullOrWhiteSpace(vacationRequest.AttachmentPath))
                    {
                        _fileServer.RemoveFile(moduleName, HrEmployeeRequestTypesEnums.VacationRequest.ToString(), vacationRequest.AttachmentPath);
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

        #region UpdateVacationRequest
        public async Task<Response<VacationRequests>> UpdateVacationRequest(int id, DTOVacationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.VacationRequest)
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



            var vacation =

                await _unitOfWork.VacationRequests.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompanyId);
            if (vacation == null || vacation.StatuesOfRequest.ApporvalStatus != (int)RequestStatusTypes.Pending)
            {
                var msg = _sharLocalizer[Localization.NotFound] + " or " + _sharLocalizer[Localization.NotPending];
                return new()
                {
                    Check = false,
                    Msg = msg,
                    Data = null
                };
            }
            var mappedvacation = _mapper.Map(model, vacation);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);


            #region UpdateFile
            if (model.Attachment is not null)
            {
                if (vacation.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, vacation.AttachmentPath);
                vacation.AttachmentPath = await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            }
            else
            {
                if (vacation.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, vacation.AttachmentPath);
                vacation.AttachmentPath = null;
            }

            #endregion

            _unitOfWork.VacationRequests.Update(vacation);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }



        #endregion

        #region Status
        public async Task<Response<string>> ApproveRequest(int requestId, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var vacationrequest = await _unitOfWork.
                VacationRequests.GetFirstOrDefaultAsync(x => x.Id == requestId && x.CompanyId == currentCompany);


            if (vacationrequest.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved)
            {
                return new Response<string>()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.ApprovedAready]
                };

            }
            var result = await _unitOfWork
                .VacationRequests.UpdateApporvalStatus(
                x => x.Id == requestId, RequestStatusTypes.Approved, userId);
            if (result > 0)
            {
                HrEmployeeRequestTypesEnums hrEmployeeRequests = HrEmployeeRequestTypesEnums.VacationRequest;
                var moduleNameWithType = hrEmployeeRequests.GetModuleNameWithType(Modules.EmployeeRequest);
                TransVacation transVacation = new();

                var emp = await
                    _unitOfWork.Employees.GetFirstOrDefaultAsync(
                        x => x.Id == vacationrequest.EmployeeId && x.CompanyId == currentCompany);
                var vacations = await _unitOfWork.
                    Vacations.GetFirstOrDefaultAsync(x => x.Id ==
                    emp.VacationId);


                transVacation.vacation_id = vacations.Id;
                transVacation.start_date = vacationrequest.StartDate;
                transVacation.employee_id = vacationrequest.EmployeeId;
                transVacation.notes = vacationrequest.Notes;
                transVacation.days_count = vacationrequest.DayCounts;
                transVacation.company_id = vacationrequest.CompanyId;
                #region CopyFileAttachment
                if (vacationrequest.AttachmentPath != null)
                {
                    var SourceFilePath = _fileServer.CombinePathWithServerPath(moduleNameWithType, vacationrequest.AttachmentPath);
                    var newFileName = $"{Guid.NewGuid()}{_fileServer.GetFileEXE(vacationrequest.AttachmentPath)}";
                    moduleNameWithType = hrEmployeeRequests.GetModuleNameWithType(Modules.Trans);
                    var desitnationFile = _fileServer.CombinePathWithServerPath(moduleNameWithType, newFileName);
                    _fileServer.CopyFile(SourceFilePath, desitnationFile);
                    transVacation.attachment = desitnationFile;
                }
                else
                {
                    transVacation.attachment = null;

                }
                #endregion

                await _unitOfWork.TransVacations.AddAsync(transVacation);
                var saveResult = await _unitOfWork.CompleteAsync();
                if (saveResult > 0)
                {
                    return new Response<string>
                    {
                        Msg = _sharLocalizer[Localization.Approved],
                        Check = true,
                    };
                }
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
            var result = await _unitOfWork.VacationRequests
                .UpdateApporvalStatus(x => x.Id == requestId &&
                x.CompanyId == currentCompanyId, RequestStatusTypes.Rejected, userId, resoan);
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
