using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.EmployeeRequests.Requests;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class AllowanceRequestService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : IAllowanceRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        public async Task<Response<DTOVacationRequest>> AddNewAllowanceRequest(DTOAllowanceRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
        {
            var newRequest = _mapper.Map<AllowanceRequest>(model);
            newRequest.allowance_request_date = DateTime.Now;   
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.attachment_file_name = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.Attachment);
            await _unitOfWork.AllowanceRequests.AddAsync(newRequest);
            await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }
    }
}
