using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.Interfaces.EmployeeRequest;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.HTTP;
namespace Kader_System.Api.Areas.EmployeeRequests.PermessionRequests
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    [Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class EmployeeRequestsController(IEmployeeRequestsRepository service, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;

        #region Insert
        [HttpPost(ApiRoutes.EmployeeRequests.GetEmployeeRequestsLookups)]
        public async Task<IActionResult> GetEmployeeRequestsLookups()
        {
            var response = await service.GetEmployeeRequestsLookUpsData(requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion


    }
}