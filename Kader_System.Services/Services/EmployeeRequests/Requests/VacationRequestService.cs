
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class VacationRequestService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : IVacationRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        public async Task<Response<DTOVacationRequest>> AddNewVacationRequest(DTOVacationRequest model, string root, string clientName, string moduleName,HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
        {
            var newRequest = _mapper.Map<VacationRequests>(model);
            #region HrEmployeeRequestTypesEnums
            var moduleNameWithType = hrEmployeeRequest switch
            {
                HrEmployeeRequestTypesEnums.LoanRequest => @$"{moduleName}\{HrEmployeeRequestTypesEnums.LoanRequest}",
                HrEmployeeRequestTypesEnums.VacationRequest => @$"{moduleName}\{HrEmployeeRequestTypesEnums.VacationRequest}",
                HrEmployeeRequestTypesEnums.AllowanceRequest => @$"{moduleName}\{HrEmployeeRequestTypesEnums.AllowanceRequest}",
                HrEmployeeRequestTypesEnums.SalaryIncreaseRequest => @$"{moduleName}\{HrEmployeeRequestTypesEnums.SalaryIncreaseRequest}",
                HrEmployeeRequestTypesEnums.TerminateContract => @$"{moduleName}\{HrEmployeeRequestTypesEnums.TerminateContract}",
                HrEmployeeRequestTypesEnums.DelayPermission => @$"{moduleName}\{HrEmployeeRequestTypesEnums.DelayPermission}",
                HrEmployeeRequestTypesEnums.ResignationRequest => @$"{moduleName}\{HrEmployeeRequestTypesEnums.ResignationRequest}",
                HrEmployeeRequestTypesEnums.LeavePermission => @$"{moduleName}\{HrEmployeeRequestTypesEnums.LeavePermission}",
                HrEmployeeRequestTypesEnums.None => moduleName,
                _ => moduleName,
            }; 
            #endregion
            newRequest.AttachmentFileName = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.Attachment);
            await _unitOfWork.VacationRequests.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }
    }
}