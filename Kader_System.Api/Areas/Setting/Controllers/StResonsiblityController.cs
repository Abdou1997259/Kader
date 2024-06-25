using Kader_System.Domain.DTOs.Request.Setting;

namespace Kader_System.Api.Areas.Setting.Controllers
{
    [Area(Modules.Setting)]
    [ApiExplorerSettings(GroupName = Modules.Setting)]
    [ApiController]
    [Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class StResonsiblityController(IStResonsiblityService service) : ControllerBase
    {
        #region Retrieve




        [HttpGet(ApiRoutes.StResponsiblity.GetAllResponsiblitites)]
        public async Task<IActionResult> GetAllSubMainScreensAsync([FromQuery] GetAllFilterationStResonsiblity model) =>
            Ok(await service.GetAllStResonsiblitysAsync(GetCurrentRequestLanguage(), GetCurrentHost(), model));

        [HttpGet(ApiRoutes.StResponsiblity.GetResponsiblityById)]
        public async Task<IActionResult> GetSubMainScreenByIdAsync([FromRoute] int id)
        {
            var response = await service.GetStResonsiblityByIdAsync(id, GetCurrentRequestLanguage());
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Insert

        [HttpPost(ApiRoutes.StResponsiblity.CreateResponsiblity)]
        public async Task<IActionResult> CreateResponsiblity([FromForm] CreateStResonsiblityRequest model)
        {
            var response = await service.CreateStResonsiblityAsync(model);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Update

        [HttpPut(ApiRoutes.StResponsiblity.UpdateResponsiblity)]
        public async Task<IActionResult> UpdateResponsiblity([FromRoute] int id, [FromForm] UpdateStResonsiblityRequest model)
        {
            var response = await service.UpdateStResonsiblityAsync(id, model);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Delete
        [HttpDelete(ApiRoutes.StResponsiblity.DeleteResponsiblity)]
        public async Task<IActionResult> DeleteSubMainScreenAsync([FromRoute] int id)
        {
            var response = await service.DeleteStResonsiblityAsync(id);
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
