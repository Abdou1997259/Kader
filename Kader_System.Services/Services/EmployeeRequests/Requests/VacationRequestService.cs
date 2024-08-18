using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class VacationRequestService(IUnitOfWork unitOfWork, KaderDbContext context, IRequestService requestService, IHttpContextAccessor httpContextAccessor, IHttpContextService contextService, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : IVacationRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        private readonly IHttpContextService _contextService = contextService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly KaderDbContext _context = context;
        private readonly IRequestService _requestService =requestService;
        #region ListOfLoanRequest
        public async Task<Response<IEnumerable<DTOVacationRequest>>> ListOfVacationRequest()
        {
            var result = await unitOfWork.LoanRepository.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id));
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

        #region PaginatedLoanRequest
        public async Task<Response<GetAllVacationRequestReponse>> GetAllVacationRequest(GetFilterationVacationRequestRequest model, string host)
        {
            Expression<Func<VacationRequests, bool>> filter = model.ApporvalStatus == RequestStatusTypes.All ?
             x => x.IsDeleted == false :
             x => x.IsDeleted == false && x.StatuesOfRequest.ApporvalStatus == (int)model.ApporvalStatus;

            var totalRecords = await _unitOfWork.VacationRequests.CountAsync(filter: filter);
            var items = (await _unitOfWork.VacationRequests.GetSpecificSelectAsync(filter, x => new ListOfVacationRequestResponse
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                request_date = x.Add_date.Value.ToString("yyyy-mm-dd"),
                EmployeeName = x.Employee.FirstNameEn,
                DayCounts = x.DayCounts,
                StartDate = x.StartDate,
                VacationTypeId = x.VacationTypeId,
                VacationTypeName = _requestService.GetRequestHeaderLanguage == Localization.English ?x.VacationType.NameInEnglish : x.VacationType.Name,
                ApporvalStatus = x.StatuesOfRequest.ApporvalStatus,
                reason = x.StatuesOfRequest.StatusMessage,
                Notes = x.Notes,
                AttachmentPath = x.AttachmentPath != null ? _fileServer.GetFilePath(Modules.EmployeeRequest, HrEmployeeRequestTypesEnums.VacationRequest.ToString(), x.AttachmentPath) : null
            },
            orderBy: x => x.OrderBy(x => x.Id),
                skip: (model.PageNumber - 1) * model.PageSize, take: model.PageSize, includeProperties: "Employee,StatuesOfRequest,VacationType")).ToList();
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

        #region GetLoanRequetById
        public async Task<Response<ListOfVacationRequestResponse>> GetById(int id)
        {
            var result = await unitOfWork.VacationRequests.GetByIdAsync(id);
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

        #region AddLoanRequest
        public async Task<Response<VacationRequests>> AddNewVacationRequest(DTOVacationRequest model , string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.VacationRequest)
        {
            var newRequest = _mapper.Map<VacationRequests>(model);
            StatuesOfRequest statues = new()
            {
                ApporvalStatus = (int)RequestStatusTypes.Pending
            };
            newRequest.StatuesOfRequest = statues;
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(moduleNameWithType, model.Attachment);
            await _unitOfWork.VacationRequests.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }
        #endregion

        #region DeleteLoanRequets
        public async Task<Response<VacationRequests>> DeleteVacationRequest(int id,string moduleName)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var vacationRequest = await _unitOfWork.VacationRequests.GetByIdAsync(id);
            var msg = $"{_sharLocalizer[Localization.Employee]} {_sharLocalizer[Localization.NotFound]}";
            if (vacationRequest != null)
            {
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

        #region UpdateLoanRequest
        public async Task<Response<VacationRequests>> UpdateVacationRequest(int id, DTOVacationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.VacationRequest)
        {
            var vacation = await _unitOfWork.VacationRequests.GetByIdAsync(id);
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


            if (model.Attachment is not null)
            {
                _fileServer.RemoveFile(moduleName, vacation.AttachmentPath);
                vacation.AttachmentPath = await _fileServer.UploadFile(moduleNameWithType, model.Attachment);
            }

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
        public async Task<Response<string>> ApproveRequest(int requestId)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _context.HrVacationRequests.Where(x => x.Id == requestId)
                                 .ExecuteUpdateAsync(x => x.
                                   SetProperty(p => p.StatuesOfRequest.ApporvalStatus, (int)RequestStatusTypes.Approved).
                                   SetProperty(p => p.StatuesOfRequest.ApprovedDate, DateTime.Now).
                                   SetProperty(p => p.StatuesOfRequest.ApprovedBy, userId)

                                 );
            if (result > 0)
            {
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
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _context.HrVacationRequests.Include(x => x.StatuesOfRequest).
                                                  Where(x => x.Id == requestId && x.IsDeleted == false && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Pending)
                                                 .ExecuteUpdateAsync(x => x.
                                                 SetProperty(p => p.StatuesOfRequest.ApporvalStatus, (int)RequestStatusTypes.Rejected).
                                                 SetProperty(p => p.StatuesOfRequest.ApprovedDate, DateTime.Now).
                                                 SetProperty(p => p.StatuesOfRequest.ApprovedBy, userId).
                                                 SetProperty(p => p.StatuesOfRequest.StatusMessage, resoan));
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
