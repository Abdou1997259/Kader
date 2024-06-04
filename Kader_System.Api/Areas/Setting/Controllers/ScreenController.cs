using Kader_System.Domain.DTOs.Request.Setting;

namespace Kader_System.Api.Areas.Setting.Controllers
{
    [Area(Modules.Setting)]
    [ApiExplorerSettings(GroupName = Modules.Setting)]
    [ApiController]
    [Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    public class ScreenController(IScreenService service) : ControllerBase
    {
        #region Retrieve
        
        [HttpGet(ApiRoutes.Screen.GetAllScreens)]
        public async Task<IActionResult> GetAllScreens([FromQuery] GetAllFilterationForScreen model) =>
            Ok(await service.GetAllScreensAsync(GetCurrentRequestLanguage(),GetCurrentHost() ,model));

        [HttpGet(ApiRoutes.Screen.GetScreenById)]
        public async Task<IActionResult> GetScreenById(int id)
        {
            var response = await service.GetScreenByIdAsync(id,GetCurrentRequestLanguage());
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Insert

        [HttpPost(ApiRoutes.Screen.CreateScreen)]
        public async Task<IActionResult> CreateScreen([FromForm] CreateScreenRequest model)
        {
            if (ModelState.IsValid)
            {
                var response = await service.CreateScreenAsync(model);

                if (response.Check)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }

            // Custom error message for ScreenType validation
            if (ModelState.TryGetValue("ScreenType", out var screenTypeErrors) && screenTypeErrors.Errors.Any())
            {
                var errorMessage = "Invalid ScreenType value. Allowed values are 1, 2, or 3.";
                ModelState.AddModelError("ScreenType", errorMessage);
            }

            var errorMessages = ModelState.Values
                .Where(v => v.Errors.Any())
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            var errorsResponse = new
            {
                errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                )
            };

            // Return only the custom errors response
            return new BadRequestObjectResult(errorsResponse);
        }

        #endregion

        #region Update

        [HttpPut(ApiRoutes.Screen.UpdateScreen)]
        public async Task<IActionResult> UpdateScreen([FromRoute] int id, [FromForm] CreateScreenRequest model)
        {
            var response = await service.UpdateScreenAsync(id, model);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.Screen.RestoreScreen)]
        public async Task<IActionResult> RestoreScreen([FromRoute] int id)
        {
            var response = await service.RestoreScreenAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.Screen.DeleteScreen)]
        public async Task<IActionResult> DeleteScreen(int id)
        {
            var response = await service.DeleteScreenAsync(id);
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
