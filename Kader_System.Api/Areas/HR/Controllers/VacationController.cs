

using Kader_System.Api.Helpers;
using Kader_System.Domain.DTOs.Request.HR.Vacation;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    //[Authorize(Permissions.HR.View)]
    [Route("api/v1/")]
    public class VacationController(IVacationService service, IRequestService requestService) : ControllerBase
    {

        private readonly IRequestService requestService = requestService;


        #region Retrieve
        [HttpGet(ApiRoutes.Vacation.ListOfVacations)]
        [Permission(Permission.View, 12)]
        public async Task<ActionResult> ListOfVacations()
            =>
                Ok(await service.ListOfVacationsAsync(requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.Vacation.GetAllVacations)]
        [Permission(Permission.View, 12)]
        public async Task<IActionResult> GetAllVacationsAsync([FromQuery] GetAllFilterationFoVacationRequest model) =>
                Ok(await service.GetAllVacationsWithJoinAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost));


        [HttpGet(ApiRoutes.Vacation.GetVacationById)]
        [Permission(Permission.View, 12)]
        public async Task<IActionResult> GetVacationById(int id)
        {
            var response = await service.GetVacationByIdAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Insert

        [HttpPost(ApiRoutes.Vacation.CreateVacation)]
        [Permission(Permission.Add, 12)]
        public async Task<IActionResult> CreateVacationAsync(CreateVacationRequest model)
        {
            var response = await service.CreateVacationAsync(model);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion


        #region Update

        [HttpPut(ApiRoutes.Vacation.UpdateVacation)]
        [Permission(Permission.Edit, 12)]
        public async Task<IActionResult> UpdateVacationAsync([FromRoute] int id, UpdateVacationRequest model)
        {
            var response = await service.UpdateVacationAsync(id, model);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.Vacation.RestoreVacation)]
        [Permission(Permission.Edit, 12)]
        public async Task<IActionResult> RestoreVacationAsync([FromRoute] int id)
        {
            var response = await service.RestoreVacationAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion


        #region Delete
        [HttpDelete(ApiRoutes.Vacation.DeleteVacation)]
        [Permission(Permission.Delete, 12)]
        public async Task<IActionResult> DeleteVacationAsync(int id)
        {
            var response = await service.DeleteVacationAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

    }
}
