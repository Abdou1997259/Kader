using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.HTTP;
namespace Kader_System.Api.Areas.EmployeeRequests.PermessionRequests.Controllers
{
    //[Area(Modules.EmployeeRequest)]
    //[ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    [Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class LeavePermessionController(ILeavePermissionRequestService service, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;

        #region Insert
        //[HttpPost(ApiRoutes.LeavePermessionasRequests.CreateLeavePermessionasRequests)]
        //public async Task<IActionResult> CreateLeavePermessionasRequests([FromForm] DTOLeavePermissionRequest model)
        //{
        //    var response = await service.AddNewLeavePermissionRequest(model);
        //    if (response.Check)
        //        return Ok(response);
        //    else if (!response.Check)
        //        return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        //    return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        //}

        #endregion


    }
}
