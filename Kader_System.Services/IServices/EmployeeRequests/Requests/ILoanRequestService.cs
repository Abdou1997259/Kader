﻿using Kader_System.Domain.DTOs;
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
    public interface ILoanRequestService
    {

        public Task<Response<GetAllLoanRequestResponse>> GetAllLoanRequest(GetFilterationLoanRequest model,string host);
        public Task<Response<LoanRequest>> AddNewLoanRequest(DTOLoanRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.LoanRequest);
        public Task<Response<LoanRequest>> UpdateLoanRequest(int id, DTOLoanRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.LoanRequest);
        public Task<Response<LoanRequest>> DeleteLoanRequest(int id,string path);
        public Task<Response<ListOfLoanRequestResponse>> GetById(int id);    
        public Task<Response<IEnumerable<ListOfLoanRequestResponse>>> ListOfLoanRequest();
        public  Task<Response<string>> RejectRequest(int requestId, string resoan);
        public  Task<Response<string>> ApproveRequest(int requestId,string lang);

    }
}
