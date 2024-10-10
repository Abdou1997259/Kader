
using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;


namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.Setting)]
[ApiExplorerSettings(GroupName = Modules.Setting)]
[ApiController]
//[Authorize(Permissions.Setting.View)]
[Route("api/v1/")]
public class SubScreensController(ISubScreenService service, IRequestService headerService, IWebHostEnvironment hostEnvironment) : ControllerBase
{
    private readonly IRequestService headerService = headerService;
    private readonly ISubScreenService service = service;
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;


    #region Retrieve

    [HttpGet(ApiRoutes.SubScreenRoute.ListOfSubMainScreens)]
    public async Task<IActionResult> ListOfSubScreensAsync() =>
        Ok(await service.ListOfSubScreensAsync(headerService.GetRequestHeaderLanguage));


    [HttpGet(ApiRoutes.SubScreenRoute.GetAllSubScreens)]
    public async Task<IActionResult> GetAllSubScreensAsync([FromQuery] StGetAllFiltrationsForSubScreenRequest model) =>
        Ok(await service.GetAllSubScreensAsync(headerService.GetRequestHeaderLanguage, model, headerService.GetCurrentHost));

    [HttpGet(ApiRoutes.SubScreenRoute.GetSubScreenById)]
    public async Task<IActionResult> GetSubMainScreenByIdAsync([FromRoute] int id)
    {


        var response = await service.GetSubScreenByIdAsync(id, headerService.GetRequestHeaderLanguage);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Insert

    [HttpPost(ApiRoutes.SubScreenRoute.CreateSubScreen)]
    [Permission(Helpers.Permission.View, 2)]
    public async Task<IActionResult> CreateSubScreenAsync([FromForm] StCreateSubScreenRequest model)
    {
        var serverPath = HttpContext.Items["ServerPath"]?.ToString();

        var response = await service.CreateSubScreenAsync(model, serverPath, Modules.Setting);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Update

    [HttpPut(ApiRoutes.SubScreenRoute.UpdateSubScreen)]
    [Permission(Helpers.Permission.Edit, 2)]
    public async Task<IActionResult> UpdateSubScreenAsync([FromRoute] int id, [FromForm] StUpdateSubScreenRequest model)
    {
        var serverPath = HttpContext.Items["ServerPath"]?.ToString();

        var response = await service.UpdateSubScreenAsync(id, model, serverPath, Modules.Setting);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPost(ApiRoutes.SubScreenRoute.OrderbyPattern)]
    [Permission(Helpers.Permission.Edit, 2)]
    public async Task<IActionResult> OrderByPattern([FromRoute] int catId, [FromBody] int[] model)
    {


        var response = await service.OrderByPattern(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPut(ApiRoutes.SubScreenRoute.RestoreScreen)]
    [Permission(Helpers.Permission.Edit, 2)]
    public async Task<IActionResult> RestoreScreen([FromRoute] int id)
    {


        var response = await service.RestoreSubScreenAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    #endregion

    #region Delete
    [HttpDelete(ApiRoutes.SubScreenRoute.DeleteSubMainScreen)]
    [Permission(Helpers.Permission.Delete, 2)]
    public async Task<IActionResult> DeleteSubScreenAsync([FromRoute] int id)
    {
        var response = await service.DeleteSubScreenAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpDelete(ApiRoutes.SubScreenRoute.RemoveScreenCodeSpace)]
    [Permission(Helpers.Permission.Delete, 2)]
    public async Task<IActionResult> RemoveScreenCodeSpace()
    {
        var response = await service.DeleteScreenCodeSpace();
        if (response > 0)
            return Ok(new { msg = $"{response} Screen codes  updated sucessfully" });
        else
            return BadRequest(new { msg = "cannot update please try again" });
    }
    #endregion



}
