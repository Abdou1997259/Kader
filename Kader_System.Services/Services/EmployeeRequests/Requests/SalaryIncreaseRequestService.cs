using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class SalaryIncreaseRequestService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : ISalaryIncreaseRequestService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;

    

        public async Task<Response<DTOSalaryIncreaseRequest>> AddNewSalaryIncreaseRequest(DTOSalaryIncreaseRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
        {
            var newRequest = _mapper.Map<SalaryIncreaseRequest>(model);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AtachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(root, clientName, moduleNameWithType,model.Attachment);
            await _unitOfWork.SalaryIncreaseRequest.AddAsync(newRequest);   
            var result = await _unitOfWork.CompleteAsync();

            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }

        public Task<int> DeleteSalaryIncreaseRequest(int id)
        {
            throw new NotImplementedException();
        }


        public Task<List<DTOSalaryIncreaseRequest>> GetAllSalaryIncreaseRequest()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateSalaryIncreaseRequest(DTOSalaryIncreaseRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
