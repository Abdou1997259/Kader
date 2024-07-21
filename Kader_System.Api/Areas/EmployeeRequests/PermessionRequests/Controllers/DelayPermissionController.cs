using Kader_System.DataAccess.Repositories;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.Interfaces;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.HTTP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
        public async Task<IActionResult> CreateDelayPermissionRequests([FromForm]DTODelayPermissionRequest model)
        {
            if (string.IsNullOrEmpty(requestService.client_id))
                return Unauthorized("client_id is empty");

            var response = await delayPermission.AddNewDelayPermissionRequest(model, _hostEnvironment.WebRootPath, requestService.client_id,
                Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.VacationRequest);

 
            if (response != null)
                return Ok(response);
            else return BadRequest(response);
        }
        #endregion
    }
}
