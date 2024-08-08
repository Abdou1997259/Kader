
﻿using Kader_System.Services.IServices.HTTP;
using Microsoft.Extensions.Hosting;
﻿using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;


namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.Setting)]
[ApiExplorerSettings(GroupName = Modules.Setting)]
[ApiController]
//[Authorize(Permissions.Setting.View)]
[Route("api/v1/")]
public class SubSubMainScreensController(ISubMainScreenService service, IRequestService headerService, IWebHostEnvironment hostEnvironment) : ControllerBase
{
    private readonly IRequestService headerService = headerService;
    private readonly ISubMainScreenService service = service;
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;


    #region Retrieve

    [HttpGet(ApiRoutes.SubMainScreen.ListOfSubMainScreens)]
    public async Task<IActionResult> ListOfSubMainScreensAsync() =>
        Ok(await service.ListOfSubMainScreensAsync(headerService.GetRequestHeaderLanguage));


    [HttpGet(ApiRoutes.SubMainScreen.GetAllSubMainScreens)]
    public async Task<IActionResult> GetAllSubMainScreensAsync([FromQuery] StGetAllFiltrationsForSubMainScreenRequest model) =>
        Ok(await service.GetAllSubMainScreensAsync(headerService.GetRequestHeaderLanguage, model,headerService.GetCurrentHost));

    [HttpGet(ApiRoutes.SubMainScreen.GetSubMainScreenById)]
    public async Task<IActionResult> GetSubMainScreenByIdAsync([FromQuery] int id)
    {


        var response = await service.GetSubMainScreenByIdAsync(id, headerService.GetRequestHeaderLanguage);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Insert

    [HttpPost(ApiRoutes.SubMainScreen.CreateSubMainScreen)]
    [Permission(Helpers.Permission.View, 2)]
    public async Task<IActionResult> CreateSubMainScreenAsync([FromForm] StCreateSubMainScreenRequest model)
    {
        var serverPath = HttpContext.Items["ServerPath"]?.ToString();

        var response = await service.CreateSubMainScreenAsync(model, serverPath, Modules.Setting);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Update

    [HttpPut(ApiRoutes.SubMainScreen.UpdateSubMainScreen)]
    [Permission(Helpers.Permission.Edit, 2)]
    public async Task<IActionResult> UpdateSubMainScreenAsync([FromRoute] int id, [FromForm] StUpdateSubMainScreenRequest model)
    {
        var serverPath = HttpContext.Items["ServerPath"]?.ToString();

        var response = await service.UpdateSubMainScreenAsync(id, model,serverPath,Modules.Setting);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPut(ApiRoutes.SubMainScreen.OrderbyPattern)]
    [Permission(Helpers.Permission.Edit, 2)]
    public async Task<IActionResult> OrderByPattern( [FromBody]int[] model)
    {
        

        var response = await service.OrderByPattern(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPut(ApiRoutes.SubMainScreen.RestoreScreen)]
    [Permission(Helpers.Permission.Edit, 2)]
    public async Task<IActionResult> RestoreScreen( [FromRoute]int id)
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
    [HttpDelete(ApiRoutes.SubMainScreen.DeleteSubMainScreen)]
    [Permission(Helpers.Permission.Delete, 2)]
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
