using AutoMapper;
using Kader_System.DataAccess.Repositories;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface ISalaryIncreaseRequestService
    {
        #region ListOfIncreaseSalaryRequest

        public  Task<Response<IEnumerable<DTOSalaryIncreaseRequest>>> ListOfSalaryIncreaseRequest();

        #endregion


        #region PaginatedSalaryIncrease

        public  Task<Response<GetAllSalaryIncreaseRequestResponse>> GetAllSalaryIncreaseRequest(GetAlFilterationForSalaryIncreaseRequest model, string host);
        #endregion


        #region SalaryIncreaseGetById
        public  Task<Response<ListOfSalaryIncreaseRequestResponse>> GetById(int id);

        #endregion

        #region AddSalaryIncrease
        public Task<Response<SalaryIncreaseRequest>> AddNewSalaryIncreaseRequest(DTOSalaryIncreaseRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.SalaryIncreaseRequest);
        #endregion


        #region UpdateSalaryIncrease
        public Task<Response<SalaryIncreaseRequest>> UpdateSalaryIncreaseRequest(int id, DTOSalaryIncreaseRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.SalaryIncreaseRequest);

        #endregion

        #region DeleteSalaryIncrease
        public Task<Response<SalaryIncreaseRequest>> DeleteSalaryIncreaseRequest(int id, string moduleName);
        #endregion

        #region Status
        public  Task<Response<string>> ApproveRequest(int requestId,string lang);
        public  Task<Response<string>> RejectRequest(int requestId, string resoan);
        #endregion

    }
}
