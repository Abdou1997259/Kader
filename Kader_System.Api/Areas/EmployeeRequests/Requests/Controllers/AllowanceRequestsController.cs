using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.Interfaces;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;
namespace Kader_System.Api.Areas.EmployeeRequests.Requests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    [Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class AllowanceRequestsController(IAllowanceRequestService service,
        IRequestService requestService,IWebHostEnvironment hostEnvironment,IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment; 
        private readonly IFileServer _fileServer = fileServer;  

        #region Insert
        [HttpPost(ApiRoutes.EmployeeRequests.AllowanceRequests.CreateAllowanceRequests)]
        public async Task<IActionResult> CreateAllowanceRequests([FromForm] DTOAllowanceRequest model)
        {
            var response = await service.AddNewAllowanceRequest(model, _hostEnvironment.WebRootPath, requestService.client_id,
                Modules.EmployeeRequest,Domain.Constants.Enums.HrEmployeeRequestTypesEnums.AllowanceRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion


    }
}
