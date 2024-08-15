using Kader_System.Api.Helpers;
using Kader_System.Domain.Constants.Enums;
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
    //[Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class LeavePermessionController(ILeavePermissionRequestService service,
        IRequestService requestService, IWebHostEnvironment hostEnvironment) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;

        #region Insert
        [HttpPost(ApiRoutes.EmployeeRequests.LeavePermessionasRequests.CreateLeavePermessionasRequests)]
        [Permission(Permission.Add, 19)]
        public async Task<IActionResult> CreateLeavePermessionasRequests([FromForm] DTOCreateLeavePermissionRequest model)
        {
            var serverPath = HttpContext.Items["ServerPath"]?.ToString();
            var response = await service.AddNewLeavePermissionRequest(model,
                serverPath, Modules.EmployeeRequest, HrEmployeeRequestTypesEnums.LeavePermission);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Read
        [HttpGet(ApiRoutes.EmployeeRequests.LeavePermessionasRequests.GetAllLeavePermessionasRequests)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetAllLeavePermessionasRequests([FromQuery] GetAllFilltrationForEmployeeRequests model)
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
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> UpdateLeavePermessionasRequests([FromRoute] int id, [FromForm] DTOCreateLeavePermissionRequest model)
        {
            var serverPath = HttpContext.Items["ServerPath"]?.ToString();
            var response = await service.UpdateLeavePermissionRequest(id, model, serverPath, Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LeavePermission);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.EmployeeRequests.LeavePermessionasRequests.ApproveLeavePermessionasRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> ApproveLeavePermessionasRequests([FromRoute] int id)
        {
            var response = await service.ApproveRequest(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.EmployeeRequests.LeavePermessionasRequests.RejectLeavePermessionasRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> RejectLeavePermessionasRequests([FromRoute] int id, [FromBody] GlobalEmployeeRequests model)
        {
            var response = await service.RejectRequest(id,model.reson);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete
        [HttpDelete(ApiRoutes.EmployeeRequests.LeavePermessionasRequests.DeleteLeavePermessionasRequests)]
        [Permission(Permission.Delete, 19)]
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
