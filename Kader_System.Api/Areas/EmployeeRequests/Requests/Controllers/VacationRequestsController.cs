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
    public class VacationRequestsController(IVacationRequestService service,
        IRequestService requestService, IWebHostEnvironment hostEnvironment, IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IFileServer _fileServer = fileServer;

        #region Retrieve

        [HttpGet(ApiRoutes.EmployeeRequests.VacationRequests.ListOfVacationRequests)]

        public async Task<IActionResult> ListOVacationRequestsAsync() =>
            Ok(await service.ListOfVacationRequest());

        [HttpGet(ApiRoutes.EmployeeRequests.VacationRequests.GetAllVacationRequests)]

        public async Task<IActionResult> GetAllResignationRequestsAsync([FromQuery] GetFilterationVacationRequestRequest model) =>
            Ok(await service.GetAllVacationRequest(model, requestService.GetCurrentHost));
        [HttpGet(ApiRoutes.EmployeeRequests.VacationRequests.GetVacationRequestsById)]

        public async Task<IActionResult> GetVacationRequestByIdAsync(int id)
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

        [HttpPost(ApiRoutes.EmployeeRequests.VacationRequests.CreateVacationRequests)]

        public async Task<IActionResult> CreateVacationRequestAsync
            ([FromForm] DTOVacationRequest model)
        {
            var serverPath = HttpContext.Items["ServerPath"]?.ToString();

            var response = await service.AddNewVacationRequest(model,

                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.VacationRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Update

        [HttpPut(ApiRoutes.EmployeeRequests.VacationRequests.UpdateVacationRequests)]

        public async Task<IActionResult> UpdateVacationRequestAsync([FromRoute] int id, [FromForm] DTOVacationRequest model)
        {
            var serverPath = HttpContext.Items["ServerPath"]?.ToString();

            var response = await service.UpdateVacationRequest(id, model,
                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.VacationRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }



        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.EmployeeRequests.VacationRequests.DeleteVacationRequests)]

        public async Task<IActionResult> DeleteVacationAsync(int id)
        {
            var response = await service.DeleteVacationRequest(id, Modules.EmployeeRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Status
        [HttpPut(ApiRoutes.EmployeeRequests.VacationRequests.ApproveVacationRequests)]

        public async Task<IActionResult> ApproveVacationRequests([FromRoute] int id)
        {
            var response = await service.ApproveRequest(id, requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.EmployeeRequests.VacationRequests.RejectVacationRequests)]

        public async Task<IActionResult> RejectLeavePermessionasRequests([FromRoute] int id, [FromBody] GlobalEmployeeRequests model)
        {
            var response = await service.RejectRequest(id, model.reson);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion
    }
}
