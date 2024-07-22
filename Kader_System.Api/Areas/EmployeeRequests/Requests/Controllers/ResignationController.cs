using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.Interfaces;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Api.Areas.EmployeeRequests.Requests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    [Route("api/v1/")]
    [Authorize(Permissions.Setting.View)]
    public class ResignationController(IResignationRequestService service, IRequestService requestService
        , IWebHostEnvironment hostEnvironment, IFileServer fileServer) : ControllerBase
    {
        #region Retrieve

        [HttpGet(ApiRoutes.EmployeeRequests.ResignationRequests.ListOfResignationRequests)]
        public async Task<IActionResult> ListOfResignationRequestsAsync() =>
            Ok(await service.ListOfResignationRequest());

        [HttpGet(ApiRoutes.EmployeeRequests.ResignationRequests.GetAllResignationRequests)]
        public async Task<IActionResult> GetAllResignationRequestsAsync([FromQuery] GetFillterationResignationRequest model) =>
            Ok(await service.GetAllResignationRequest(model, requestService.GetCurrentHost));
        [HttpGet(ApiRoutes.EmployeeRequests.ResignationRequests.GetResignationRequestsById)]
        public async Task<IActionResult> GetResignationRequestByIdAsync(int id)
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

        [HttpPost(ApiRoutes.EmployeeRequests.ResignationRequests.CreateResignationRequests)]
        public async Task<IActionResult> CreateResignationRequestAsync([FromForm] DTOResignationRequest model)
        {
            var response = await service.AddNewResignationRequest(model, hostEnvironment.WebRootPath, requestService.client_id,

                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LoanRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Update

        [HttpPut(ApiRoutes.EmployeeRequests.ResignationRequests.UpdateResignationRequests)]
        public async Task<IActionResult> UpdateResignationRequestAsync([FromRoute] int id, [FromForm] DTOResignationRequest model)
        {
            var response = await service.UpdateResignationRequest(id, model, hostEnvironment.WebRootPath, requestService.client_id,
                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LoanRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }



        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.EmployeeRequests.ResignationRequests.DeleteResignationRequests)]
        public async Task<IActionResult> DeleteResignationAsync(int id)
        {
            var response = await service.DeleteResignationRequest(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion
    }
}

