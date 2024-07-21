using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class LoanRequesService : ILoanRequestService
    {
        public Task<int> AddNewLoanReques(DTOLoanRequest model)
        {
            throw new NotImplementedException();
        }

            return new()
            {
                Data = loanRequest,
                Msg = msg,
                Check = true,
            };

        }
        #endregion

        #region UpdateLoanRequest
        public async Task<Response<LoanRequest>> UpdateLoanRequest(int id, DTOLoanRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
        {
            var result = await unitOfWork.LoanRequestRepository.GetByIdAsync(id);

            if (result == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = localizer[Localization.NotFound]
                };
            }
            var updatingModel = mapper.Map(model, result);
            if (model.Attachment is not null)
            {
                var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
                updatingModel.AttachmentFileName = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                    await fileserver.UploadFile(root, clientName, moduleNameWithType, model.Attachment);
            }
            unitOfWork.LoanRequestRepository.Update(result);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Data = updatingModel,
                Check = true
            };


        }
    }
}
