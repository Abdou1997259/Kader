using Kader_System.Domain.DTOs.Request.EmployeesRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.Interfaces;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.EmployeeRequests.PermessionRequests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    [Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class DelayPermissionController(IDelayPermissionService delayPermission , IRequestService  requestService, IWebHostEnvironment hostEnvironment, IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IFileServer _fileServer = fileServer;

        #region Insert
        [HttpPost(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.CreateDelayPermissionRequests)]
        public async Task<IActionResult> CreateLeavePermessionasRequests([FromForm] DTODelayPermissionRequest model)
        {
            var response = await delayPermission.AddNewDelayPermissionRequest(model, _hostEnvironment.WebRootPath, requestService.client_id, Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.DelayPermission);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Read
        [HttpGet(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.GetAllDelayPermissionRequests)]
        public async Task<IActionResult> GetAllDelayPermissionRequests([FromQuery] GetAllFilltrationForEmployeeRequests model)
        {

            var response = await delayPermission.GetAllDelayPermissionRequsts(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion
        #region Update
        [HttpPut(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.UpdateDelayPermissionRequests)]
        public async Task<IActionResult> UpdateLeavePermessionasRequests([FromForm] DTODelayPermissionRequest model)
        {
            var response = await delayPermission.UpdateDelayPermissionRequest(model, _hostEnvironment.WebRootPath, requestService.client_id, Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.DelayPermission);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete
        [HttpDelete(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.DeleteDelayPermissionRequests)]
        public async Task<IActionResult> DeleteDelayPermissionRequests(int id)
        {
            var full_path = Path.Combine(_hostEnvironment.WebRootPath, requestService.client_id, Modules.EmployeeRequest);
            var response = await delayPermission.DeleteDelayPermissionRequest(id, full_path);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

    }
}
