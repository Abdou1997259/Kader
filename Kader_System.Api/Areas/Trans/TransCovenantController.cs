using Kader_System.Services.IServices.Trans;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    [Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransCovenantController(ITransCovenantService service,IEmployeeService employeeService) : ControllerBase
    {
        #region Get

        [HttpGet(ApiRoutes.TransCovenant.ListOfTransCovenants)]
        public async Task<IActionResult> ListOfTransCovenants() =>
            Ok(await service.ListOfTransCovenantsAsync(GetCurrentRequestLanguage()));

        [HttpGet(ApiRoutes.TransCovenant.GetTransCovenants)]
        public async Task<IActionResult> GetAllTransCovenants([FromQuery] GetAllFilterationForTransCovenant request) =>
            Ok(await service.GetAllTransCovenantsAsync(GetCurrentRequestLanguage(),request, GetCurrentHost()));

        [HttpGet(ApiRoutes.TransCovenant.GetTransCovenantById)]
        public async Task<IActionResult> GetTransCovenantById([FromRoute] int id)
        {
            var result =await service.GetTransCovenantByIdAsync(id, GetCurrentRequestLanguage());
            if (result.Check)
                return Ok(result);
            else if (!result.Check)
                return BadRequest(result);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet(ApiRoutes.TransCovenant.GetLookUps)]
        public async Task<IActionResult> GetLookUps()
        {
            var result = await employeeService.GetEmployeesDataNameAndIdAsLookUp(GetCurrentRequestLanguage());
            if (result.Check)
                return Ok(result);
            else if (!result.Check)
                return BadRequest(result);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, result);
        }
        #endregion

        #region Create

        [HttpPost(ApiRoutes.TransCovenant.CreateTransCovenant)]
        public async Task<IActionResult> CreateTransCovenant([FromBody] CreateTransCovenantRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await service.CreateTransCovenantAsync(request);
                if (response.Check)
                    return Ok(response);
                else if (!response.Check)
                    return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region Put
        [HttpPut(ApiRoutes.TransCovenant.UpdateTransCovenant)]
        public async Task<IActionResult> UpdateTransCovenant([FromRoute] int id, [FromBody] CreateTransCovenantRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await service.UpdateTransCovenantAsync(id, request);
                if (response.Check)
                    return Ok(response);
                else if (!response.Check)
                    return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPut(ApiRoutes.TransCovenant.RestoreTransCovenant)]
        public async Task<IActionResult> RestoreTransCovenant([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var response = await service.RestoreTransCovenantAsync(id);
                if (response.Check)
                    return Ok(response);
                else if (!response.Check)
                    return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion


        #region Delete

        [HttpDelete(ApiRoutes.TransCovenant.DeleteTransCovenant)]
        public async Task<IActionResult> DeleteTransCovenant(int id)
        {
            var response = await service.DeleteTransCovenantAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Helpers
        private string GetCurrentRequestLanguage() =>
            Request.Headers.AcceptLanguage.ToString().Split(',').First();
        private string GetCurrentHost() =>
            HttpContext.Request.Host.Value +
            HttpContext.Request.Path.Value;

        #endregion
    }
}
