using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.Interfaces;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kader_System.Api.Areas.EmployeeRequests.Requests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    [Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class SalaryIncreaseRequestController(ISalaryIncreaseRequestService increaseRequestService, IRequestService requestService, IWebHostEnvironment hostEnvironment, IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IFileServer _fileServer = fileServer;


        #region Insert
        [HttpPost(ApiRoutes.EmployeeRequests.SalaryIncreaseRequest.CreateSalaryIncreaseRequests)]
public async Task<IActionResult> SalaryIncreaseRequest([FromForm] DTOSalaryIncreaseRequest model)
        {
            if (string.IsNullOrEmpty(requestService.client_id))
                return Unauthorized("client_id is empty");

            var response = await increaseRequestService.AddNewSalaryIncreaseRequest(model, _hostEnvironment.WebRootPath, requestService.client_id,
                Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.VacationRequest);

            if (response != null)
                return Ok(response);
            else return BadRequest(response);   
        }
        #endregion
    }
}
