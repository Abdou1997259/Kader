
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class LeavePermissionRequestService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : ILeavePermissionRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        public async Task<Response<DTOLeavePermissionRequest>> AddNewLeavePermissionRequest(DTOCreateLeavePermissionRequest model, string root, string clientName, string moduleName)
        {
            var newRequest = _mapper.Map<LeavePermissionRequest>(model);
            newRequest.AttachmentPath = (model.Attachement == null || model.Attachement.Length == 0) ? null :
                await _fileServer.UploadFile(root, clientName, moduleName, model.Attachement);
            await _unitOfWork.LeavePermissionRequest.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }
    }
}
