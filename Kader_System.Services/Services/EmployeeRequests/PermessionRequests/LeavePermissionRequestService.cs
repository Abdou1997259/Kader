
using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class LeavePermissionRequestService(IUnitOfWork unitOfWork, IRequestService requestService, KaderDbContext context, IHttpContextAccessor httpContextAccessor, IHttpContextService contextService, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : ILeavePermissionRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        private readonly IHttpContextService _contextService = contextService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly KaderDbContext _context = context;
        private readonly IRequestService _requestService = requestService;
        #region Create
        public async Task<Response<DTOLeavePermissionRequest>> AddNewLeavePermissionRequest(DTOCreateLeavePermissionRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest)
        {
            var newRequest = _mapper.Map<LeavePermissionRequest>(model);
            StatuesOfRequest statues = new()
            {
                ApporvalStatus = (int)RequestStatusTypes.Pending
            };
            newRequest.StatuesOfRequest = statues;
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            await _unitOfWork.LeavePermissionRequest.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }


        #endregion

        #region Delete
        public async Task<Response<string>> DeleteLeavePermissionRequest(int id, string fullPath)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var msg = $"{_sharLocalizer[Localization.Employee]} {_sharLocalizer[Localization.NotFound]}";
            var leaveRequest = await _unitOfWork.LeavePermissionRequest.GetEntityWithIncludeAsync(x => x.Id == id, "StatuesOfRequest");
            if (leaveRequest != null)
            {
                if (leaveRequest.StatuesOfRequest.ApporvalStatus != 1)
                {
                    msg = _sharLocalizer[Localization.ApproveRejectDelte];
                    return new()
                    {
                        Msg = msg,
                        Check = false,
                    };
                }
                var result = await _unitOfWork.LeavePermissionRequest.SoftDeleteAsync(leaveRequest, DeletedBy: userId);
                if (result > 0)
                {
                    if (!string.IsNullOrWhiteSpace(leaveRequest.AttachmentPath))
                    {
                        _fileServer.RemoveFile(fullPath, HrEmployeeRequestTypesEnums.LeavePermission.ToString(), leaveRequest.AttachmentPath);
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

        #region Read
        public async Task<Response<GetAllLeavePermissionRequestRequestResponse>> GetAllLeavePermissionRequsts(string lang, Domain.DTOs.Request.EmployeesRequests.GetAllFilltrationForEmployeeRequests model, string host)
        {

            #region ApprovalExpression
            Expression<Func<LeavePermissionRequest, bool>> filter = x =>
                x.IsDeleted == false &&
                (model.ApporvalStatus == RequestStatusTypes.All ||
                (model.ApporvalStatus == RequestStatusTypes.Approved && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved) ||
                (model.ApporvalStatus == RequestStatusTypes.ApprovedRejected &&
                    (x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved ||
                     x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected)) ||
                (model.ApporvalStatus == RequestStatusTypes.Rejected && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected) ||
                (model.ApporvalStatus == RequestStatusTypes.Pending && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Pending));

            #endregion

            var totalRecords = await _unitOfWork.LeavePermissionRequest.CountAsync(filter: filter);
            var items = (await _unitOfWork.LeavePermissionRequest.GetSpecificSelectAsync(filter, x => new ListOfLeavePermissionsRequestResponse
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                request_date = x.Add_date.Value.ToString("yyyy-mm-dd"),
                EmployeeName = _requestService.GetRequestHeaderLanguage == Localization.English ? x.Employee.FullNameEn : x.Employee.FullNameAr,
                LeaveTime = x.LeaveTime,
                BackTime = x.BackTime,
                ApporvalStatus = x.StatuesOfRequest.ApporvalStatus,
                reason = x.StatuesOfRequest.StatusMessage,
                Notes = x.Notes,
                AttachmentPath = x.AttachmentPath != null ? _fileServer.CombinePath(Modules.EmployeeRequest, HrEmployeeRequestTypesEnums.LeavePermission.ToString(), x.AttachmentPath) : null
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

            var result = new GetAllLeavePermissionRequestRequestResponse
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

        #region Update
        public async Task<Response<DTOLeavePermissionRequest>> UpdateLeavePermissionRequest(int id, DTOCreateLeavePermissionRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest)
        {

            var leave = await _unitOfWork.LeavePermissionRequest.GetByIdAsync(id);
            if (leave == null || leave.StatuesOfRequest.ApporvalStatus != (int)RequestStatusTypes.Pending)
            {
                var msg = _sharLocalizer[Localization.NotFound] + " or " + _sharLocalizer[Localization.NotPending];
                return new()
                {
                    Check = false,
                    Msg = msg,
                    Data = null
                };
            }
            var mappedleave = _mapper.Map(model, leave);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);

            #region UpdateFile
            if (model.Attachment is not null)
            {
                if (leave.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, leave.AttachmentPath);
                leave.AttachmentPath = await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            }
            else
            {
                if (leave.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, leave.AttachmentPath);
                leave.AttachmentPath = null;
            }

            #endregion

            _unitOfWork.LeavePermissionRequest.Update(leave);
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
            var result = await _unitOfWork.LeavePermissionRequest.UpdateApporvalStatus(x => x.Id == requestId, RequestStatusTypes.Approved, userId);

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
            var result = await _unitOfWork.LeavePermissionRequest.UpdateApporvalStatus(x => x.Id == requestId, RequestStatusTypes.Rejected, userId, resoan);
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
