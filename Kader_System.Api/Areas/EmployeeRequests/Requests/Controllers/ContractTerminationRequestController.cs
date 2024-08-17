using Kader_System.Api.Helpers;
using Kader_System.Domain.DTOs.Request.EmployeesRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.Interfaces;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;
namespace Kader_System.Api.Areas.EmployeeRequests.Requests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    //[Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class ContractTerminationRequestController(IContractTerminationRequestService service,
        IRequestService requestService,IWebHostEnvironment hostEnvironment,IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment; 
        private readonly IFileServer _fileServer = fileServer;

        #region Retrieve

        [HttpGet(ApiRoutes.EmployeeRequests.ContractTerminationRequest.ListOContractTerminationRequest)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> ListOfLoanRequestsAsync() =>
            Ok(await service.ListOfContractTerminationRequest());

        [HttpGet(ApiRoutes.EmployeeRequests.ContractTerminationRequest.GetAllContractTerminationRequest)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetAllLoanRequestsAsync([FromQuery] GetFilterationContractTerminationRequest model) =>
            Ok(await service.GetAllContractTerminationRequest(model, requestService.GetCurrentHost));
        [HttpGet(ApiRoutes.EmployeeRequests.ContractTerminationRequest.GetContractTerminationRequestsById)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetLoanRequestByIdAsync(int id)
        {
            var response = await service.GetById(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Insert

        [HttpPost(ApiRoutes.EmployeeRequests.ContractTerminationRequest.CreateContractTerminationRequest)]
        [Permission(Permission.Add, 19)]
        public async Task<IActionResult> CreateLoanRequestAsync([FromForm] DTOContractTerminationRequest model)
        {
            var serverPath = HttpContext.Items["ServerPath"]?.ToString();

            var response = await service.AddNewContractTerminationRequest(model,

                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LoanRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Update

        [HttpPut(ApiRoutes.EmployeeRequests.ContractTerminationRequest.UpdateContractTerminationRequest)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> UpdateLoanRequestAsync([FromRoute] int id, [FromForm] DTOContractTerminationRequest model)
        {
            var serverPath = HttpContext.Items["ServerPath"]?.ToString();

            var response = await service.UpdateContractTerminationRequest(id, model,
                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LoanRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }



        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.EmployeeRequests.ContractTerminationRequest.DeleteContractTerminationRequest)]
        [Permission(Permission.Delete, 19)]
        public async Task<IActionResult> DeleteAllowanceAsync(int id)
        {
            var response = await service.DeleteContracTermniationRequest(id, Modules.EmployeeRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Status
        [HttpPut(ApiRoutes.EmployeeRequests.ContractTerminationRequest.ApproveContractTerminationRequest)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> ApproveContractTerminationRequest([FromRoute] int id)
        {
            var response = await service.ApproveRequest(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.EmployeeRequests.ContractTerminationRequest.RejectContractTerminationRequest)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> RejectContractTerminationRequest([FromRoute] int id, [FromBody] GlobalEmployeeRequests model)
        {
            var response = await service.RejectRequest(id, model.reson);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion
    }
}
