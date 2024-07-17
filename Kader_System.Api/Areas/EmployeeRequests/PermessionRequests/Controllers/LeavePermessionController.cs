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
    public class EmployeeRequestsController(ILeavePermissionRequestService service,
        IRequestService requestService,IWebHostEnvironment hostEnvironment,IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment; 
        private readonly IFileServer _fileServer = fileServer;  

        #region Insert
        [HttpPost(ApiRoutes.LeavePermessionasRequests.CreateLeavePermessionasRequests)]
        public async Task<IActionResult> CreateLeavePermessionasRequests([FromForm] DTOCreateLeavePermissionRequest model)
        {
            if (string.IsNullOrEmpty(requestService.GetClientId))
                return Unauthorized("ClientId is empty");

            var response = await service.AddNewLeavePermissionRequest(model, _hostEnvironment.WebRootPath, requestService.GetClientId, Modules.EmployeeRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion


    }
}
