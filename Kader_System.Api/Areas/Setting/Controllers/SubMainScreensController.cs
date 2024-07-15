using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.Setting)]
[ApiExplorerSettings(GroupName = Modules.Setting)]
[ApiController]
[Authorize(Permissions.Setting.View)]
[Route("api/v1/")]
public class SubSubMainScreensController(ISubMainScreenService service, Services.IServices.HTTP.ITitleService headerService) : ControllerBase
{
    private readonly Services.IServices.HTTP.ITitleService headerService = headerService;
    private readonly ISubMainScreenService service = service;
    #region Retrieve

    [HttpGet(ApiRoutes.SubMainScreen.ListOfSubMainScreens)]
    public async Task<IActionResult> ListOfSubMainScreensAsync() =>
        Ok(await service.ListOfSubMainScreensAsync(headerService.GetRequestHeaderLanguage));


    [HttpGet(ApiRoutes.SubMainScreen.GetAllSubMainScreens)]
    public async Task<IActionResult> GetAllSubMainScreensAsync([FromQuery] StGetAllFiltrationsForSubMainScreenRequest model) =>
        Ok(await service.GetAllSubMainScreensAsync(headerService.GetRequestHeaderLanguage, model));

    [HttpGet(ApiRoutes.SubMainScreen.GetSubMainScreenById)]
    public async Task<IActionResult> GetSubMainScreenByIdAsync([FromRoute] int id)
    {
        var response = await service.GetSubMainScreenByIdAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Insert

    [HttpPost(ApiRoutes.SubMainScreen.CreateSubMainScreen)]
    public async Task<IActionResult> CreateSubMainScreenAsync([FromForm] StCreateSubMainScreenRequest model)
    {
        var response = await service.CreateSubMainScreenAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Update

    [HttpPut(ApiRoutes.SubMainScreen.UpdateSubMainScreen)]
    public async Task<IActionResult> UpdateSubMainScreenAsync([FromRoute] int id, [FromForm] StUpdateSubMainScreenRequest model)
    {
        var response = await service.UpdateSubMainScreenAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Delete
    [HttpDelete(ApiRoutes.SubMainScreen.DeleteSubMainScreen)]
    public async Task<IActionResult> DeleteSubMainScreenAsync([FromRoute] int id)
    {
        var response = await service.DeleteSubMainScreenAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    #endregion



}
