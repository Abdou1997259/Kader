using Kader_System.Domain.Interfaces.EmployeeRequest;
using Kader_System.Services.IServices.HTTP;
namespace Kader_System.Api.Areas.EmployeeRequests.PermessionRequests
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    //[Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class EmployeeRequestsController(IEmployeeRequestsRepository service, IUserContextService userContextService, IRequestService requestService) : ControllerBase

    {
        private readonly IRequestService requestService = requestService;
        private readonly IUserContextService _userContextService = userContextService;

        #region Insert
        [HttpGet(ApiRoutes.EmployeeRequests.GetEmployeeRequestsLookups)]
        public async Task<IActionResult> GetEmployeeRequestsLookups()
        {

            var response = await service.GetEmployeeRequestsLookUpsData(
                requestService.GetRequestHeaderLanguage,
                await _userContextService.GetLoggedCurrentCompany(),
                _userContextService?.UserId, _userContextService.IsAdmin());
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion


    }
}
