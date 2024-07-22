using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.Interfaces;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kader_System.Api.Areas.EmployeeRequests.Requests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    [Route("api/v1/")]
    [Authorize(Permissions.Setting.View)]
    public class SalaryIncreaseRequestController(ISalaryIncreaseRequestService increaseRequestService, IRequestService requestService, IWebHostEnvironment hostEnvironment, IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IFileServer _fileServer = fileServer;


        #region Retrieve
        [HttpGet(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.CreateSalaryIncreaseRequests)]
        public async Task<IActionResult> ListOfSalaryIncreaseRequest([FromQuery] GetAlFilterationForSalaryIncreaseRequest model) =>
           Ok(await increaseRequestService.GetAllSalaryIncreaseRequest(model, requestService.GetCurrentHost));

        [HttpGet(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.CreateSalaryIncreaseRequests)]
        public async Task<IActionResult> GetAllLoanRequestsAsync([FromQuery] GetAlFilterationForSalaryIncreaseRequest model) =>
            Ok(await increaseRequestService.GetAllSalaryIncreaseRequest(model, requestService.GetCurrentHost));
        [HttpGet(ApiRoutes.EmployeeRequests.LoanRequests.GetLoanRequestsById)]
        public async Task<IActionResult> GetSalaryIncreaseIdAsync(int id)
        {
            var response = await increaseRequestService.GetById(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Insert
        [HttpPost(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.CreateSalaryIncreaseRequests)]
       public async Task<IActionResult> SalaryIncreaseRequest([FromForm] DTOSalaryIncreaseRequest model)
        {
            if (string.IsNullOrEmpty(requestService.client_id))
                return Unauthorized("client_id is empty");

            var response = await increaseRequestService.AddNewSalaryIncreaseRequest(model, _hostEnvironment.WebRootPath, requestService.client_id,
                Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.VacationRequest);

            if (response != null)
                return Ok(response);
            else return BadRequest(response);   
        }
        #endregion

        #region Update
        [HttpPut(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.UpdateIncreaseSalary)]
        public async Task<IActionResult> UpdateIncreaseSalary ([FromQuery]int id ,[FromForm] DTOSalaryIncreaseRequest model)
        {
            var response = await increaseRequestService.UpdateSalaryIncreaseRequest(id, model, hostEnvironment.WebRootPath, requestService.client_id,
                 Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LoanRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.DeleteSalaryIncreaseRequest)]
        public async Task<IActionResult> DeleteSalaryIncreaseRequest(int id)
        {
            var response = await increaseRequestService.DeleteSalaryIncreaseRequest(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion
    }
}
