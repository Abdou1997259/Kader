using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.Services.EmployeeRequests.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Kader_System.Domain.Constants.SD.ApiRoutes.EmployeeRequests;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class DelayPermissionService(IUnitOfWork unitOfWork, KaderDbContext context,IRequestService requestService, IStringLocalizer<SharedResource> sharLocalizer, IHttpContextAccessor httpContextAccessor, IFileServer fileServer, IMapper mapper) : IDelayPermissionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly KaderDbContext _context = context;
        private readonly IRequestService _requestService = requestService;

        #region ListOfIncreaseSalaryRequest

        public async Task<Response<IEnumerable<DTODelayPermissionRequest>>> ListOfDelayPermissionRequest()
        {
            var result = await unitOfWork.DelayPermission.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id));
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
            var mappingResult = mapper.Map<IEnumerable<DTODelayPermissionRequest>>(result);

            return new()
            {
                Data = mappingResult,
                Msg = msg,
            };
        }
        #endregion




        #region insert

        public async Task<Response<DTODelayPermissionRequest>> AddNewDelayPermissionRequest(DTODelayPermissionRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
        {
            var newRequest = _mapper.Map<DelayPermission>(model);
            StatuesOfRequest statues = new()
            {
                ApporvalStatus = (int)RequestStatusTypes.Pending
            };
            newRequest.StatuesOfRequest = statues;
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(moduleNameWithType, model.Attachment);
            await _unitOfWork.DelayPermission.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();

            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };


        }
        #endregion



        #region Delete
        public async Task<Response<string>> DeleteDelayPermissionRequest(int id, string fullPath)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var _DelayPermissionRequest = await _unitOfWork.DelayPermission.GetByIdAsync(id);
            var msg = $"{_sharLocalizer[Localization.Employee]} {_sharLocalizer[Localization.NotFound]}";
            if (_DelayPermissionRequest != null)
            {
                var result = await _unitOfWork.DelayPermission.SoftDeleteAsync(_DelayPermissionRequest, DeletedBy: userId);
                if (result > 0)
                {
                    if (!string.IsNullOrWhiteSpace(_DelayPermissionRequest.AttachmentPath))
                    {
                        _fileServer.RemoveFile(fullPath, HrEmployeeRequestTypesEnums.DelayPermission.ToString(), _DelayPermissionRequest.AttachmentPath);
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
        public async Task<Response<GetAllDelayPermissionRequestRequestResponse>> GetAllDelayPermissionRequsts(GetAlFilterationDelayPermissionReuquest model, string host)
        {
            #region ApprovalExpression
            Expression<Func<DelayPermission, bool>> filter = x =>
             x.IsDeleted == false &&
             (model.ApporvalStatus == RequestStatusTypes.All || (model.ApporvalStatus == RequestStatusTypes.Approved ?
                 x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved :
             model.ApporvalStatus == RequestStatusTypes.ApprovedRejected ?
                 x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved ||
                 x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected :
             model.ApporvalStatus == RequestStatusTypes.Rejected && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected));
            #endregion

            var totalRecords = await _unitOfWork.DelayPermission.CountAsync(filter);

            var items = await (from x in _context.HrDelayPermissions.AsNoTracking().
                                        Include(x => x.StatuesOfRequest).Where(filter)
                               join emp in _context.Employees on x.EmployeeId equals emp.Id
                               select new ListOfDelayPermissionRequestResponse
                               {
                                   Id = x.Id,
                                   EmployeeId = x.EmployeeId,
                                   request_date = x.Add_date.Value.ToString("yyyy-mm-dd"),
                                   HoursDelay = x.DelayHours,
                                   ArrivalTime = x.Employee.Shift.Start_shift.AddHours(x.DelayHours.Value),
                                   EmployeeName = _requestService.GetRequestHeaderLanguage == Localization.English ? x.Employee.FirstNameEn + " " + x.Employee.FatherNameEn : x.Employee.FirstNameAr + " " + x.Employee.FatherNameAr,
                                   ApporvalStatus = x.StatuesOfRequest.ApporvalStatus,
                                   reason = x.StatuesOfRequest.StatusMessage,
                                   Notes = x.Notes,
                                   AttachmentPath = x.AttachmentPath != null ? _fileServer.GetFilePath(Modules.EmployeeRequest, HrEmployeeRequestTypesEnums.DelayPermission.ToString(), x.AttachmentPath) : null
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

            var result = new GetAllDelayPermissionRequestRequestResponse
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

        public async Task<Response<DtoListOfDelayRequestReponse>> UpdateDelayPermissionRequest(int id, DTODelayPermissionRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest)
        {
            var delay = await _unitOfWork.DelayPermission.GetByIdAsync(id);
            if (delay == null || delay.StatuesOfRequest.ApporvalStatus != (int)RequestStatusTypes.Pending)
            {
                var msg = _sharLocalizer[Localization.NotFound] + " or " + _sharLocalizer[Localization.NotApproval];
                return new()
                {
                    Check = false,
                    Msg = msg,
                    Data = null
                };
            }
            var mappedDelay = _mapper.Map(model, delay);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);


            if (model.Attachment is not null)
            {
                _fileServer.RemoveFile(moduleName, delay.AttachmentPath);
                delay.AttachmentPath = await _fileServer.UploadFile(moduleNameWithType, model.Attachment);
            }

            _unitOfWork.DelayPermission.Update(delay);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }
        #endregion

        #region GetById
        public async Task<Response<DtoListOfDelayRequestReponse>> GetById(int id)
        {
            var result = await unitOfWork.DelayPermission.GetByIdAsync(id);
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

            var mappingResult = mapper.Map<DtoListOfDelayRequestReponse>(result);

            return new()
            {
                Data = mappingResult,
                Check = true,

            };
        }
        #endregion

        #region Status
        public async Task<Response<string>> ApproveRequest(int requestId)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _unitOfWork.DelayPermission.UpdateApporvalStatus(x =>x.Id == requestId,RequestStatusTypes.Approved,userId);
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
            var result = await _unitOfWork.DelayPermission.UpdateApporvalStatus(x => x.Id == requestId, RequestStatusTypes.Rejected, userId, resoan);
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
