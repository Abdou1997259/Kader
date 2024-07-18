using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.HTTP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kader_System.Api.Areas.EmployeeRequests.PermessionRequests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    [Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class DelayPermissionController(IDelayPermissionService delayPermission , IRequestService  requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;

        #region Insert 
        [HttpPost(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.CreateDelayPermissionRequests)]
        public async Task<IActionResult> CreateDelayPermissionRequests([FromForm]DTODelayPermissionRequest model)
        {
            var response = await delayPermission.AddNewDelayPermissionRequest(model);

            if (response != null)
                return Ok(response);
            else return BadRequest(response);
        }
        #endregion
    }
}
