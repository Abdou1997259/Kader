using Kader_System.Api.Helpers;
using Kader_System.Domain.DTOs.Request.EmployeesRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;
namespace Kader_System.Api.Areas.EmployeeRequests.Requests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    //[Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class AllowanceRequestsController(IAllowanceRequestService service,
        IRequestService requestService,IWebHostEnvironment hostEnvironment,IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment; 
        private readonly IFileServer _fileServer = fileServer;


        #region Retrieve

        [HttpGet(ApiRoutes.EmployeeRequests.AllowanceRequests.GetAllowanceRequests)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> Get([FromQuery] GetAllFilterationAllowanceRequest  model) =>
            Ok(await service.GetAllowanceRequest(model, requestService.GetCurrentHost));

        [HttpGet(ApiRoutes.EmployeeRequests.AllowanceRequests.GetAllowanceRequestById)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await service.GetById(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Insert
        [HttpPost(ApiRoutes.EmployeeRequests.AllowanceRequests.CreateAllowanceRequests)]
        [Permission(Permission.Add, 19)]
        public async Task<IActionResult> CreateAllowanceRequests([FromForm] DTOAllowanceRequest model)
        {
            var response = await service.AddNewAllowanceRequest(model,
                Modules.EmployeeRequest,Domain.Constants.Enums.HrEmployeeRequestTypesEnums.AllowanceRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Update
        [HttpPut(ApiRoutes.EmployeeRequests.AllowanceRequests.UpdateAllowanceRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> UpdateAllowanceRequests([FromRoute] int id, [FromForm] DTOAllowanceRequest model)
        {
            var response = await service.UpdateAllowanceRequest(id, model,
                 Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LoanRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.EmployeeRequests.AllowanceRequests.DeleteAllowanceRequests)]
        [Permission(Permission.Delete, 19)]
        public async Task<IActionResult> DeleteAllowanceRequests(int id)
        {
            var response = await service.DeleteAllowanceRequest(id, Modules.EmployeeRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Status
        [HttpPut(ApiRoutes.EmployeeRequests.AllowanceRequests.ApproveAllowanceRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> ApproveAllowanceRequests([FromRoute] int id)
        {
            var response = await service.ApproveRequest(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }  
        [HttpPut(ApiRoutes.EmployeeRequests.AllowanceRequests.RejectAllowanceRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> RejectAllowanceRequests([FromRoute] int id, [FromBody] GlobalEmployeeRequests model)
        {
            var response = await service.RejectRequest(id,model.reson);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion
    }
}
