using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    [Authorize(Permissions.HR.View)]
    [Route("api/v1/")]
    public class EmployeesController(IEmployeeService employeeService, Services.IServices.HTTP.ITitleService requestService) : ControllerBase
    {
        private readonly Services.IServices.HTTP.ITitleService requestService = requestService;

        #region Get

        [HttpGet(ApiRoutes.Employee.ListOfEmployees)]
        public async Task<IActionResult> ListOfEmployeesAsync() =>
            Ok(await employeeService.ListOfEmployeesAsync(requestService.GetRequestHeaderLanguage));



        [HttpGet(ApiRoutes.Employee.GetAllEmployees)]
        public async Task<IActionResult> GetAllEmployeesAsync([FromQuery]GetAllEmployeesFilterRequest request) =>
            Ok(await employeeService.GetAllEmployeesAsync(requestService.GetRequestHeaderLanguage, request, requestService.GetCurrentHost));


        [HttpGet(ApiRoutes.Employee.GetEmployeeById)]

        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            var response =  employeeService.GetEmployeeById(id,requestService.GetRequestHeaderLanguage);

            var lookUps =await employeeService.GetEmployeesLookUpsData(requestService.GetRequestHeaderLanguage);

            response.LookUps=lookUps.Data;

            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpGet(ApiRoutes.Employee.GetLookUps)]
        public async Task<IActionResult> GetLookUps()
        {
          return Ok(await employeeService.GetEmployeesLookUpsData(requestService.GetRequestHeaderLanguage));
        }
        [HttpGet(ApiRoutes.Employee.GetAllEmpByCompanyId)]
        public async Task<IActionResult> GetAllEmpByCompanyId([FromRoute]int companyId, [FromQuery] GetAllEmployeesFilterRequest request)
        {
            return Ok(await employeeService.GetAllEmployeesByCompanyIdAsync(lang:requestService.GetRequestHeaderLanguage,companyId: companyId,model:request,host:requestService.GetCurrentHost));
        }
        #endregion


        #region Post
        [HttpPost(ApiRoutes.Employee.CreateEmployee)]
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
        public async Task<IActionResult> RestoreEmployee([FromRoute]int id)
        {
            var response = await employeeService.RestoreEmployeeAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpPut(ApiRoutes.Employee.UpdateEmployee)]
        public async Task<IActionResult> UpdateEmployeeAsyncTask([FromRoute] int id, [FromForm] CreateEmployeeRequest request)
        {
            var response = await employeeService.UpdateEmployeeAsync(id, request);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion


        #region Delete

        [HttpDelete(ApiRoutes.Employee.DeleteEmployee)]
        public async Task<IActionResult> DeleteEmployeeAsyncTask(int id)
        {
            var response = await employeeService.DeleteEmployeeAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion


    }
}
