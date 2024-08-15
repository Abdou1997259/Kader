
using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.HTTP;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class LeavePermissionRequestService(IUnitOfWork unitOfWork, KaderDbContext context, IHttpContextAccessor httpContextAccessor, IHttpContextService contextService, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : ILeavePermissionRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        private readonly IHttpContextService _contextService = contextService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly KaderDbContext _context = context;
        #region Create
        public async Task<Response<DTOLeavePermissionRequest>> AddNewLeavePermissionRequest(DTOCreateLeavePermissionRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest)
        {
            var newRequest = _mapper.Map<LeavePermissionRequest>(model);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(appPath, moduleNameWithType, model.Attachment);
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
            var obj = await unitOfWork.LeavePermissionRequest.GetByIdAsync(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            unitOfWork.LeavePermissionRequest.Remove(obj);
            await unitOfWork.CompleteAsync();
            if (!string.IsNullOrWhiteSpace(obj.AttachmentPath))
                _fileServer.RemoveFile(fullPath, obj.AttachmentPath);

            return new()
            {
                Check = true,
                Data = string.Empty,
                Msg = sharLocalizer[Localization.Deleted]
            };
        }
        #endregion

        #region Read
        public async Task<Response<GetAllLeavePermissionRequestResponse>> GetAllLeavePermissionRequsts(string lang, Domain.DTOs.Request.EmployeesRequests.GetAllFilltrationForEmployeeRequests model, string host)
        {

            Expression<Func<LeavePermissionRequest, bool>> filter = model.ApporvalStatus == RequestStatusTypes.All ?
                x => x.IsDeleted == false :
                x => x.IsDeleted == false && x.StatuesOfRequest.ApporvalStatus == (int)model.ApporvalStatus;

            var totalRecords = await _unitOfWork.LeavePermissionRequest.CountAsync(filter: filter);
            var items = (await _unitOfWork.LeavePermissionRequest.GetSpecificSelectAsync(filter, x => new ListOfLeavePermissionsReponse
            {
                Id = x.Id,
                requet_date = x.Add_date,
                EmployeeName = x.Employee.FirstNameEn,
                LeaveTime = x.LeaveTime,
                BackTime = x.BackTime,
                ApporvalStatus = x.StatuesOfRequest.ApporvalStatus,
                AtachmentPath = _contextService.GetRelativeServerPath(Modules.EmployeeRequest, x.AttachmentPath)
            },
            orderBy: x => x.OrderBy(x => x.Id),
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

            var result = new GetAllLeavePermissionRequestResponse
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
        public async Task<Response<DTOLeavePermissionRequest>> UpdateLeavePermissionRequest(int id, DTOCreateLeavePermissionRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest)
        {

            var leave = await _unitOfWork.LeavePermissionRequest.GetByIdAsync(id);
            if (leave == null)
            {
                var msg = _sharLocalizer[Localization.NotFound];
                return new()
                {
                    Check = false,
                    Msg = msg,
                    Data = null
                };
            }
            var mappedleave = _mapper.Map(model, leave);
            _unitOfWork.LeavePermissionRequest.Update(mappedleave);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);


            if (!string.IsNullOrEmpty(leave.AttachmentPath))
                _fileServer.RemoveFile(appPath, moduleName, leave.AttachmentPath);

            leave.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(appPath, moduleNameWithType, model.Attachment);

            _unitOfWork.LeavePermissionRequest.Update(leave);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }
        public async Task<Response<string>> ApproveRequest(int requestId)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _context.LeavePermissionsRequests.Where(x => x.Id == requestId)
                                 .ExecuteUpdateAsync(x => x.
                                   SetProperty(p => p.StatuesOfRequest.ApporvalStatus, 2).
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
                Msg = "Cannot approve "
            };
        }
        public async Task<Response<string>> RejectRequest(int requestId, string resoan)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _context.LeavePermissionsRequests.Where(x => x.Id == requestId)
                                 .ExecuteUpdateAsync(x => x.
                                     SetProperty(p => p.StatuesOfRequest.ApporvalStatus, 3).
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
