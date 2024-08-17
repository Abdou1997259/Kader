using AutoMapper;
using Kader_System.DataAccess.Repositories;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface IResignationRequestService
    {
        #region ListOfLoanRequest
        public Task<Response<IEnumerable<ListOfResignationRequestResponse>>> ListOfResignationRequest();
        #endregion

        #region PaginatedLoanRequest
        public Task<Response<GetAllResignationRequestResponse>> GetAllResignationRequest(GetFillterationResignationRequest model, string host);

        #endregion

        #region GetLoanRequetById
        public Task<Response<DtoListOfResignationResposne>> GetById(int id);

        #endregion

        #region AddLoanRequest
        public Task<Response<ResignationRequest>> AddNewResignationRequest(DTOResignationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.ResignationRequest);
        #endregion

        #region DeleteResignationRequest
        public Task<Response<ResignationRequest>> DeleteResignationRequest(int id, string ModuleName);
        #endregion

        #region UpdateLoanRequest
        public Task<Response<ResignationRequest>> UpdateResignationRequest(int id, DTOResignationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.ResignationRequest);
        #endregion

        #region Status
        public Task<Response<string>> RejectRequest(int requestId, string resoan);
        public Task<Response<string>> ApproveRequest(int requestId);

        #endregion

    }
}
