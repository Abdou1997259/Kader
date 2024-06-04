namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.Setting)]
[ApiExplorerSettings(GroupName = Modules.Setting)]
[ApiController]
[Authorize(Permissions.Setting.View)]
[Route("api/v1/")]
public class MainScreensController(IMainScreenService service) : ControllerBase
{
    #region Retrieve
    [HttpGet(ApiRoutes.MainScreen.ListOfMainScreens)]
    public async Task<IActionResult> ListOfMainScreensAsync() =>
        Ok(await service.ListOfMainScreensAsync(GetCurrentRequestLanguage()));


    [HttpGet(ApiRoutes.MainScreen.GetAllMainScreens)]
    public async Task<IActionResult> GetAllMainScreensAsync([FromQuery] StGetAllFiltrationsForMainScreenRequest model) =>
        Ok(await service.GetAllMainScreensAsync(GetCurrentRequestLanguage(), model));

    [HttpGet(ApiRoutes.MainScreen.GetMainScreenById)]
    public async Task<IActionResult> GetMainScreenByIdAsync([FromRoute] int id)
    {
        var response = await service.GetMainScreenByIdAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }


    #endregion

    #region Insert

    [HttpPost(ApiRoutes.MainScreen.CreateMainScreen)]
    public async Task<IActionResult> CreateMainScreenAsync([FromForm] StCreateMainScreenRequest model)
    {
        var response = await service.CreateMainScreenAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Update

    [HttpPut(ApiRoutes.MainScreen.UpdateMainScreen)]
    public async Task<IActionResult> UpdateMainScreenAsync([FromRoute] int id, [FromForm] StUpdateMainScreenRequest model)
    {
        var response = await service.UpdateMainScreenAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion


    #region Delete

    [HttpDelete(ApiRoutes.MainScreen.DeleteMainScreen)]
    public async Task<IActionResult> DeleteMainScreenAsync([FromRoute] int id)
    {
        var response = await service.DeleteMainScreenAsync(id);
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

    #endregion

}
