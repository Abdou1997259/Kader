using Kader_System.Api.Helpers;
using Kader_System.Domain.DTOs.Request;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    //[Authorize(Permissions.HR.View)]
    [Route("api/v1/")]
    public class EmployeesController(IEmployeeService employeeService, IRequestService requestService, IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IFileServer _fileServer = fileServer;
        #region Get

        [HttpGet(ApiRoutes.Employee.ListOfEmployees)]
        [Permission(Permission.View, 13)]
        public async Task<IActionResult> ListOfEmployeesAsync() =>
            Ok(await employeeService.ListOfEmployeesAsync(requestService.GetRequestHeaderLanguage));



        [HttpGet(ApiRoutes.Employee.GetAllEmployees)]
        [Permission(Permission.View, 13)]
        public async Task<IActionResult> GetAllEmployeesAsync([FromQuery] GetAllEmployeesFilterRequest request) =>
            Ok(await employeeService.GetAllEmployeesAsync(requestService.GetRequestHeaderLanguage, request, requestService.GetCurrentHost));


        [HttpGet(ApiRoutes.Employee.GetEmployeeById)]
        [Permission(Permission.View, 13)]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            var response = employeeService.GetEmployeeById(id, requestService.GetRequestHeaderLanguage);

            var lookUps = await employeeService.GetEmployeesLookUpsData(requestService.GetRequestHeaderLanguage);
            var screenLookup = await employeeService.GetDocuments(id);
            response.LookUpsScreen = screenLookup.Data;
            response.LookUps = lookUps.Data;

            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpGet(ApiRoutes.Employee.GetLookUps)]
        [Permission(Permission.View, 13)]
        public async Task<IActionResult> GetLookUps()
        {
            return Ok(await employeeService.GetEmployeesLookUpsData(requestService.GetRequestHeaderLanguage));
        }
        [HttpGet(ApiRoutes.Employee.GetAllEmpByCompanyId)]
        public async Task<IActionResult> GetAllEmpByCompanyId([FromRoute] int companyId, [FromQuery] GetAllEmployeesFilterRequest request)
        {
            return Ok(await employeeService.GetAllEmployeesByCompanyIdAsync(lang: requestService.GetRequestHeaderLanguage, companyId: companyId, model: request, host: requestService.GetCurrentHost));
        }
        #endregion


        #region Post
        [HttpPost(ApiRoutes.Employee.CreateEmployee)]
        [Permission(Permission.Add, 13)]
        public async Task<IActionResult> CreateEmployeeAsync([FromForm] CreateEmployeeRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await employeeService.CreateEmployeeAsync(request);
                if (response.Check)
                    return Ok(response);
                else if (!response.Check)
                    return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
            }
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, request);
        }


        #endregion


        #region Put

        [HttpPut(ApiRoutes.Employee.Restore)]
        [Permission(Permission.Edit, 13)]
        public async Task<IActionResult> RestoreEmployee([FromRoute] int id)
        {
            var response = await employeeService.RestoreEmployeeAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpPut(ApiRoutes.Employee.UpdateEmployee)]
        [Permission(Permission.Edit, 13)]
        public async Task<IActionResult> UpdateEmployeeAsyncTask([FromRoute] int id, [FromForm]
        CreateEmployeeRequest request)
        {
            var response = await employeeService.UpdateEmployeeAsync(id, request);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.Employee.UpdateEmployeeAttachement)]
        [Permission(Permission.Edit, 13)]
        public async Task<IActionResult> UpdateEmployeeAttachment([FromRoute] int id, [FromForm] UpdateEmployeeAttachemnt request)
        {
            var response = await employeeService.UpdateEmployeeAttachemnt(request, id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion


        #region Delete

        [HttpDelete(ApiRoutes.Employee.DeleteEmployee)]
        [Permission(Permission.Delete, 13)]
        public async Task<IActionResult> DeleteEmployeeAsyncTask(int id)
        {
            var response = await employeeService.DeleteEmployeeAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpDelete(ApiRoutes.Employee.DeleteEmployeeAttachement)]
        [Permission(Permission.Delete, 13)]
        public async Task<IActionResult> DeleteEmployeeAttachement(int id)
        {

            var response = await employeeService.RemoveEmployeeAttachement(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpDelete(ApiRoutes.Employee.DeleteEmployeeProfileImage)]
        [Permission(Permission.Delete, 13)]
        public async Task<IActionResult> DeleteEmployeeProfileImage(int id)
        {

            var response = await employeeService.RemoveEmployeeProfile(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        [HttpGet(ApiRoutes.Employee.DownloadDocument)]
        [Permission(Permission.View, 8)]
        public async Task<IActionResult> DownloadEmployeeDocument([FromRoute] int id)
        {
            var response = await employeeService.DownloadEmployeeAttachement(id);
            if (response.Check)
            {
                if (response.Data is not null)
                    return response.Data;
                else
                    return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            }
            else
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        }


    }
}
