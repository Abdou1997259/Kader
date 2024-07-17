using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class LeavePermissionRequestService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ILeavePermissionRequestService
    {
        public async Task<Response<DTOLeavePermissionRequest>> AddNewLeavePermissionRequest(DTOLeavePermissionRequest model)
        {
            var newTrans = mapper.Map<LeavePermissionRequest>(model);

            if (!string.IsNullOrEmpty(model.AttachmentPath))
            {
                var fileNameAndExt = ManageFilesHelper.SaveBase64StringToFile(model.AttachmentPath!, GoRootPath.EmployeeRequestPath, model.FileName!);
                newTrans.AttachmentPath = fileNameAndExt != null ? $"{fileNameAndExt.FileName}.{fileNameAndExt.FileExtension}" : null;
            }

            await unitOfWork.LeavePermissionRequest.AddAsync(newTrans);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }
    }
}
