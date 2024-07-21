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
        public async Task<IActionResult> ListOfLoanRequestsAsync() =>
            Ok(await service.ListOfLoanRequest());

        [HttpGet(ApiRoutes.EmployeeRequests.ResignationRequests.GetAllResignationRequests)]
        public async Task<IActionResult> GetAllLoanRequestsAsync([FromQuery] GetFillterationResignationRequest model) =>
            Ok(await service.GetAllLoanReques(model, requestService.GetCurrentHost));
        [HttpGet(ApiRoutes.EmployeeRequests.ResignationRequests.GetResignationRequestsById)]
        public async Task<IActionResult> GetLoanRequestByIdAsync(int id)
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
        public async Task<IActionResult> CreateLoanRequestAsync([FromForm] DTOResignationRequest model)
        {
            var response = await service.AddNewLoanReques(model, hostEnvironment.WebRootPath, requestService.client_id,

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
        public async Task<IActionResult> UpdateLoanRequestAsync([FromRoute] int id, [FromForm] DTOResignationRequest model)
        {
            var response = await service.UpdateLoanRequest(id, model, hostEnvironment.WebRootPath, requestService.client_id,
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
        public async Task<IActionResult> DeleteAllowanceAsync(int id)
        {
            var response = await service.DeleteLoanRequest(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion
    }
}

