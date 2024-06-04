using Azure.Core;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [Authorize(Permissions.HR.View)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    [Route("api/v1/")]
    public class DepartmentsController(IDepartmentService service) : ControllerBase
    {

        #region Retrieve

        
        [HttpGet(ApiRoutes.Department.ListOfDepartments)]
        public async Task<IActionResult> ListOfDepartments()
            => Ok(await service.ListOfDepartmentsAsync(GetCurrentRequestLanguage()));


        [HttpGet(ApiRoutes.Department.GetAllDepartments)]
        public async Task<IActionResult> GetAll([FromQuery]GetAllFiltrationsForDepartmentsRequest filter)
            => Ok(await service.GetAllDepartmentsAsync(GetCurrentRequestLanguage(), filter, GetCurrentHost()));

        [HttpGet(ApiRoutes.Department.GetDepartmentById)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await service.GetDepartmentByIdAsync(id, GetCurrentRequestLanguage());
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion


        #region Create

        [HttpPost(ApiRoutes.Department.CreateDepartment)]
        public async Task<IActionResult> Create(CreateDepartmentRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.CreateDepartmentAsync(request);
                if(result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return BadRequest(result);
            }

            return BadRequest(request);
        }


        #endregion

        #region Update

        [HttpPut(ApiRoutes.Department.UpdateDepartment)]
        public async Task<IActionResult> UpdateTask(int id,CreateDepartmentRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.UpdateDepartmentAsync(id, request);
                if (result.Check) return Ok(result);
                else if(!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, request);
            }
            return BadRequest(request);
        }


        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.Department.DeleteDepartment)]
        public async Task<IActionResult> Delete(int id)
        {
            var result=await service.DeleteDepartmentAsync(id);
            if (result.Check) return Ok(result);
            else if (!result.Check)  return BadRequest(result);
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);

        }
        

        #endregion

        #region Helper

        private string GetCurrentRequestLanguage() =>
            Request.Headers.AcceptLanguage.ToString().Split(',').First();
        private string GetCurrentHost() =>
            HttpContext.Request.Host.Value +
            HttpContext.Request.Path.Value;
        #endregion
    }
}
