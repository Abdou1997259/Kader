using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.Interfaces;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class DelayPermissionService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : IDelayPermissionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;


        public async Task<Response<DTODelayPermissionRequest>> AddNewDelayPermissionRequest(DTODelayPermissionRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
        {
            var newRequest = _mapper.Map<DelayPermission>(model);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AtachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.Attachment);
            await _unitOfWork.DelayPermission.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();

            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };


        }
    }
}
