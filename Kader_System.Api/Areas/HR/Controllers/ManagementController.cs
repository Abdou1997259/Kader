using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    //[Authorize(Permissions.HR.View)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    [Route("api/v1/")]
    public class ManagementController(IManagementService service, IStructureMangement structure, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IStructureMangement _structure = structure;
        #region Retrieve
        [HttpGet(ApiRoutes.Management.ListOfManagements)]
        [Permission(Permission.View, 11)]
        public async Task<IActionResult> List()
            => Ok(await service.ListOfManagementsAsync(requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.Management.GetAllManagements)]
        [Permission(Permission.View, 11)]
        public async Task<IActionResult> GetAll([FromQuery] HrGetAllFiltrationsFoManagementsRequest model)
            => Ok(await service.GetAllManagementsAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost));

        [HttpGet(ApiRoutes.Management.GetManagementById)]
        [Permission(Permission.View, 11)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await service.GetManagementByIdAsync(id, requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet(ApiRoutes.Management.GetStructure)]
        [Permission(Permission.View, 11)]
        public async Task<IActionResult> GetStructure(int companyid)
        {
            var response = await _structure.GetStructureMangementAsync(companyid, requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }


        #endregion


        #region Create

        [HttpPost(ApiRoutes.Management.CreateManagement)]
        [Permission(Permission.Add, 11)]
        public async Task<IActionResult> CreateManagement(CreateManagementRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.CreateManagementAsync(request);
                if (result.Check)
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
        [Permission(Permission.Edit, 11)]
        public async Task<IActionResult> UpdateManagement(int id, CreateManagementRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.UpdateManagementAsync(id, request);
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
        [Permission(Permission.Delete, 11)]
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
    }
}
