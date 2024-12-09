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
    [Route("api/v1/")]
    //[Authorize(Permissions.Setting.View)]
    public class LoanRequestController(ILoanRequestService service, IRequestService requestService
        , IWebHostEnvironment hostEnvironment, IFileServer fileServer) : ControllerBase
    {
        #region Retrieve

        [HttpGet(ApiRoutes.EmployeeRequests.LoanRequests.ListOfLoanRequests)]

        public async Task<IActionResult> ListOfLoanRequestsAsync() =>
            Ok(await service.ListOfLoanRequest());

        [HttpGet(ApiRoutes.EmployeeRequests.LoanRequests.GetAllLoanRequests)]

        public async Task<IActionResult> GetAllLoanRequests([FromQuery] GetFilterationLoanRequest model) =>
            Ok(await service.GetAllLoanRequest(model, requestService.GetCurrentHost));
        [HttpGet(ApiRoutes.EmployeeRequests.LoanRequests.GetLoanRequestsById)]

        public async Task<IActionResult> GetLoanRequestById(int id)
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

        [HttpPost(ApiRoutes.EmployeeRequests.LoanRequests.CreateLoanRequests)]

        public async Task<IActionResult> CreateLoanRequestAsync([FromForm] DTOLoanRequest model)
        {
            var response = await service.AddNewLoanRequest(model,

                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LoanRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Update

        [HttpPut(ApiRoutes.EmployeeRequests.LoanRequests.UpdateLoanRequests)]

        public async Task<IActionResult> UpdateLoanRequestAsync([FromRoute] int id, [FromForm] DTOLoanRequest model)
        {
            var response = await service.UpdateLoanRequest(id, model,
                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.LoanRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.EmployeeRequests.LoanRequests.DeleteLoanRequests)]

        public async Task<IActionResult> DeleteLoanRequests(int id)
        {
            var full_path = Path.Combine(hostEnvironment.WebRootPath, requestService.client_id, Modules.EmployeeRequest);
            var response = await service.DeleteLoanRequest(id, full_path);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion



        #region Status
        [HttpPut(ApiRoutes.EmployeeRequests.LoanRequests.ApproveLoanRequests)]

        public async Task<IActionResult> ApproveLoanRequests([FromRoute] int id)
        {
            var response = await service.ApproveRequest(id, requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.EmployeeRequests.LoanRequests.RejectLoanRequests)]

        public async Task<IActionResult> RejectLoanRequests([FromRoute] int id, [FromBody] GlobalEmployeeRequests model)
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
