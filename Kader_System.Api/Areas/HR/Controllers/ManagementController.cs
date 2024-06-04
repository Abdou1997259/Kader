namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [Authorize(Permissions.HR.View)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    [Route("api/v1/")]
    public class ManagementController(IManagementService service) : ControllerBase
    {

        #region Retrieve
        [HttpGet(ApiRoutes.Management.ListOfManagements)]
        public async Task<IActionResult> List()
            => Ok(await service.ListOfManagementsAsync(GetCurrentRequestLanguage()));

        [HttpGet(ApiRoutes.Management.GetAllManagements)]
        public async Task<IActionResult> GetAll([FromQuery] HrGetAllFiltrationsFoManagementsRequest model)
            => Ok(await service.GetAllManagementsAsync(GetCurrentRequestLanguage(), model, GetCurrentHost()));

        [HttpGet(ApiRoutes.Management.GetManagementById)]
        public async Task<IActionResult> GetById (int id)
        {
            var response = await service.GetManagementByIdAsync(id,GetCurrentRequestLanguage());
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }


        #endregion


        #region Create

        [HttpPost(ApiRoutes.Management.CreateManagement)]
        public async Task<IActionResult> CreateManagement(CreateManagementRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.CreateManagementAsync(request);
                if(result.Check)
                    return Ok(result);
                else if (!result.Check)
                    return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, result);
            }

            return StatusCode(statusCode: StatusCodes.Status400BadRequest, request);
        }

        #endregion

        #region Update
        [HttpPut(ApiRoutes.Management.UpdateManagement)]
        public async Task<IActionResult> UpdateManagement(int id, CreateManagementRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.UpdateManagementAsync(id,request);
                if (result.Check)
                    return Ok(result);
                else if (!result.Check)
                    return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, result);
            }

            return StatusCode(statusCode: StatusCodes.Status400BadRequest, request);
        }


        #endregion

        #region Delete
        [HttpDelete(ApiRoutes.Management.DeleteManagement)]
        public async Task<IActionResult> DeleteManagement(int id)
        {
            var response = await service.DeleteManagementAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
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
