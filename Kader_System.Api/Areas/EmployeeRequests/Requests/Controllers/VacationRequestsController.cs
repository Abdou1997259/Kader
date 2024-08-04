using Kader_System.Api.Helpers;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.Interfaces;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
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
        IRequestService requestService,IWebHostEnvironment hostEnvironment,IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment; 
        private readonly IFileServer _fileServer = fileServer;

        #region Retrieve

        [HttpGet(ApiRoutes.EmployeeRequests.VacationRequests.ListOfVacationRequests)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> ListOVacationRequestsAsync() =>
            Ok(await service.ListOfVacationRequest());

        [HttpGet(ApiRoutes.EmployeeRequests.VacationRequests.GetAllVacationRequests)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetAllResignationRequestsAsync([FromQuery] GetFilterationVacationRequestRequest model) =>
            Ok(await service.GetAllVacationRequest(model, requestService.GetCurrentHost));
        [HttpGet(ApiRoutes.EmployeeRequests.VacationRequests.GetVacationRequestsById)]
        [Permission(Permission.View, 19)]
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
        [Permission(Permission.Add, 19)]
        public async Task<IActionResult> CreateVacationRequestAsync([FromForm] DTOVacationRequest model)
        {
            var response = await service.AddNewVacationRequest(model, hostEnvironment.WebRootPath, requestService.client_id,

                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LoanRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Update

        [HttpPut(ApiRoutes.EmployeeRequests.VacationRequests.UpdateVacationRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> UpdateVacationRequestAsync([FromRoute] int id, [FromForm] DTOVacationRequest model)
        {
            var response = await service.UpdateVacationRequest(id, model, hostEnvironment.WebRootPath, requestService.client_id,
                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LoanRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }



        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.EmployeeRequests.VacationRequests.DeleteVacationRequests)]
        [Permission(Permission.Delete, 19)]
        public async Task<IActionResult> DeleteVacationAsync(int id)
        {
            var response = await service.DeleteVacationRequest(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion


    }
}
