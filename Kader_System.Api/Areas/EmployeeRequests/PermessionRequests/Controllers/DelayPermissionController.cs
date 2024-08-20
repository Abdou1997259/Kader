using Kader_System.Api.Helpers;
using Kader_System.Domain.DTOs.Request.EmployeesRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.Interfaces;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.Services.EmployeeRequests.Requests;

namespace Kader_System.Api.Areas.EmployeeRequests.PermessionRequests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    //[Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class DelayPermissionController(IDelayPermissionService delayPermission, IRequestService requestService, IWebHostEnvironment hostEnvironment, IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IFileServer _fileServer = fileServer;

        #region Retrieve
        [HttpGet(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.GetAllDelayPermissionRequests)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetAllSalaryIncreaseRequests([FromQuery] GetAlFilterationDelayPermissionReuquest model) =>
            Ok(await delayPermission.GetAllDelayPermissionRequsts(model, requestService.GetCurrentHost));



        [HttpGet(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.GetDelayPermissionRequestsById)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetDelayPermissionRequestsById(int id)
        {
            var response = await delayPermission.GetById(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Insert
        [HttpPost(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.CreateDelayPermissionRequests)]
        [Permission(Permission.Add, 19)]
        public async Task<IActionResult> AddNewDelayPermissionRequest([FromForm] DTODelayPermissionRequest model)
        {
            var response = await delayPermission.AddNewDelayPermissionRequest(model, Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.DelayPermission);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Update
        [HttpPut(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.UpdateDelayPermissionRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> UpdateDelayPermissionRequest([FromRoute] int id, [FromForm] DTODelayPermissionRequest model)
        {
            var serverPath = HttpContext.Items["ServerPath"]?.ToString();

            var response = await delayPermission.UpdateDelayPermissionRequest(id, model, Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.DelayPermission);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete
        [HttpDelete(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.DeleteDelayPermissionRequests)]
        [Permission(Permission.Delete, 19)]
        public async Task<IActionResult> DeleteDelayPermissionRequests(int id)
        {
            var response = await delayPermission.DeleteDelayPermissionRequest(id, Modules.EmployeeRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Status
        [HttpPut(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.ApproveDelayPermissionRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> ApproveDelayPermissionRequests([FromRoute] int id)
        {

            var response = await delayPermission.ApproveRequest(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.EmployeeRequests.DelayPermessionasRequests.RejectDelayPermissionRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> RejectDelayPermissionRequests([FromRoute] int id, [FromBody] GlobalEmployeeRequests model)
        {
            var response = await delayPermission.RejectRequest(id, model.reson);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        //[HttpGet("DownloadFile")]
        //[Permission(Permission.Edit, 19)]
        //public async Task<IActionResult> DownloadFile()
        //{
            
        //}

    }
}
