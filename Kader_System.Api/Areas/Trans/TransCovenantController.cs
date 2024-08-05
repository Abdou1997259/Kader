using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.IServices.Trans;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    //[Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransCovenantController(ITransCovenantService service,IEmployeeService employeeService, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        #region Get

        [HttpGet(ApiRoutes.TransCovenant.ListOfTransCovenants)]

        [Permission(Helpers.Permission.View, 18)]
        public async Task<IActionResult> ListOfTransCovenants() =>
            Ok(await service.ListOfTransCovenantsAsync(requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.TransCovenant.GetTransCovenants)]
        [Permission(Helpers.Permission.View, 18)]
        public async Task<IActionResult> GetAllTransCovenants([FromQuery] GetAllFilterationForTransCovenant request) =>
            Ok(await service.GetAllTransCovenantsAsync(requestService.GetRequestHeaderLanguage,request, requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.TransCovenant.GetTransCovenantById)]
        [Permission(Helpers.Permission.View, 18)]
        public async Task<IActionResult> GetTransCovenantById([FromRoute] int id)
        {
            var result =await service.GetTransCovenantByIdAsync(id, requestService.GetRequestHeaderLanguage);
            if (result.Check)
                return Ok(result);
            else if (!result.Check)
                return BadRequest(result);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet(ApiRoutes.TransCovenant.GetLookUps)]
        [Permission(Helpers.Permission.View, 18)]
        public async Task<IActionResult> GetLookUps()
        {
            var result = await employeeService.GetEmployeesDataNameAndIdAsLookUp(requestService.GetRequestHeaderLanguage);
            if (result.Check)
                return Ok(result);
            else if (!result.Check)
                return BadRequest(result);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, result);
        }
        #endregion

        #region Create

        [HttpPost(ApiRoutes.TransCovenant.CreateTransCovenant)]
        [Permission(Helpers.Permission.Add, 18)]
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
        [Permission(Helpers.Permission.Edit, 18)]
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
        [Permission(Helpers.Permission.Edit, 18)]
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
        [Permission(Helpers.Permission.Delete, 18)]
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

        
    }
}
