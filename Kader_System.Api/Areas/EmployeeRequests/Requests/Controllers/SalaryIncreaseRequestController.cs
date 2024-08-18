using Kader_System.Api.Helpers;
using Kader_System.Domain.DTOs.Request.EmployeesRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.EmployeeRequests.Requests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    [Route("api/v1/")]
    //[Authorize(Permissions.Setting.View)]
    public class SalaryIncreaseRequestController(ISalaryIncreaseRequestService increaseRequestService, IRequestService requestService, IWebHostEnvironment hostEnvironment, IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IFileServer _fileServer = fileServer;


        #region Retrieve
        [HttpGet(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.GetAllSalaryIncreaseRequests)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetAllSalaryIncreaseRequests([FromRoute] GetAlFilterationForSalaryIncreaseRequest model) =>
            Ok(await increaseRequestService.GetAllSalaryIncreaseRequest(model, requestService.GetCurrentHost));



        [HttpGet(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.GetSalaryIncreaseId)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetSalaryIncreaseId(int id)
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
        [Permission(Permission.Add, 19)]
        public async Task<IActionResult> SalaryIncreaseRequest([FromForm] DTOSalaryIncreaseRequest model)
        {
            var response = await increaseRequestService.AddNewSalaryIncreaseRequest(model,
                Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.SalaryIncreaseRequest);

            if (response != null)
                return Ok(response);
            else return BadRequest(response);   
        }
        #endregion

        #region Update
        [HttpPut(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.UpdateIncreaseSalary)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> UpdateIncreaseSalary([FromRoute]int id ,[FromForm] DTOSalaryIncreaseRequest model)
        {
            var response = await increaseRequestService.UpdateSalaryIncreaseRequest(id, model,
                 Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.SalaryIncreaseRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.DeleteSalaryIncreaseRequest)]
        [Permission(Permission.Delete, 19)]
        public async Task<IActionResult> DeleteSalaryIncreaseRequest(int id)
        {
            var response = await increaseRequestService.DeleteSalaryIncreaseRequest(id,Modules.EmployeeRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Status
        [HttpPut(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.ApproveSalaryIncreaseRequest)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> ApproveSalaryIncreaseRequest([FromRoute] int id)
        {
            var response = await increaseRequestService.ApproveRequest(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.RejectSalaryIncreaseRequest)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> RejectSalaryIncreaseRequest([FromRoute] int id, [FromBody] GlobalEmployeeRequests model)
        {
            var response = await increaseRequestService.RejectRequest(id, model.reson);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion
    }
}
