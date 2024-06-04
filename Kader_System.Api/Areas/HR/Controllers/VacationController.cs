

using Kader_System.Domain.DTOs.Request.HR.Vacation;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    [Authorize(Permissions.HR.View)]
    [Route("api/v1/")]
    public class VacationController (IVacationService service): ControllerBase
    {



        #region Retrieve
        [HttpGet(ApiRoutes.Vacation.ListOfVacations)]
        public async Task<ActionResult> ListOfVacations()
            =>
                Ok(await service.ListOfVacationsAsync(GetCurrentRequestLanguage()));

        [HttpGet(ApiRoutes.Vacation.GetAllVacations)]
        public async Task<IActionResult> GetAllVacationsAsync([FromQuery] GetAllFilterationFoVacationRequest model) =>
            Ok(await service.GetAllVacationsWithJoinAsync(GetCurrentRequestLanguage(), model, GetCurrentHost()));


        [HttpGet(ApiRoutes.Vacation.GetVacationById)]
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

        #region Helpers

        private string GetCurrentRequestLanguage() =>
            Request.Headers.AcceptLanguage.ToString().Split(',').First();
        private string GetCurrentHost() =>
            HttpContext.Request.Host.Value +
            HttpContext.Request.Path.Value;
        #endregion
    }
}
