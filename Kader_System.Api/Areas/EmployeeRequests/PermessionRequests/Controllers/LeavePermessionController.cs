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
    public class LeavePermessionController(ILeavePermissionRequestService service,
        IRequestService requestService, IWebHostEnvironment hostEnvironment, IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IFileServer _fileServer = fileServer;

        #region Insert
        [HttpPost(ApiRoutes.EmployeeRequests.LeavePermessionasRequests.CreateLeavePermessionasRequests)]
        public async Task<IActionResult> CreateLeavePermessionasRequests([FromForm] DTOCreateLeavePermissionRequest model)
        {
            var response = await service.AddNewLeavePermissionRequest(model, _hostEnvironment.WebRootPath, requestService.client_id, Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LeavePermission);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Read
        [HttpGet(ApiRoutes.EmployeeRequests.LeavePermessionasRequests.GetAllLeavePermessionasRequests)]
        public async Task<IActionResult> GetAllLeavePermessionasRequests([FromBody] GetAllFilltrationForEmployeeRequests model)
        {

            var response = await service.GetAllLeavePermissionRequsts(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Update
        [HttpPut(ApiRoutes.EmployeeRequests.LeavePermessionasRequests.UpdateLeavePermessionasRequests)]
        public async Task<IActionResult> UpdateLeavePermessionasRequests([FromForm] DTOCreateLeavePermissionRequest model)
        {
            var response = await service.UpdateLeavePermissionRequest(model, _hostEnvironment.WebRootPath, requestService.client_id, Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LeavePermission);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete
        [HttpDelete(ApiRoutes.EmployeeRequests.LeavePermessionasRequests.DeleteLeavePermessionasRequests)]
        public async Task<IActionResult> DeleteLeavePermessionasRequests(int id)
        {
            var full_path = Path.Combine(_hostEnvironment.WebRootPath, requestService.client_id, Modules.EmployeeRequest);
            var response = await service.DeleteLeavePermissionRequest(id,full_path);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion
    }
}
