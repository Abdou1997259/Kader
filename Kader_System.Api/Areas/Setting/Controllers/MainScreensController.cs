using Kader_System.Api.Helpers;
using Kader_System.DataAccesss.DbContext;
using Kader_System.Domain.DTOs.Request.Setting;
using Kader_System.Domain.DTOs.Response.Setting;
using Kader_System.Domain.Interfaces.Setting;
using Kader_System.Services.IServices.HTTP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;
namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.Setting)]
[ApiExplorerSettings(GroupName = Modules.Setting)]
[ApiController]
//[Authorize(Permissions.Setting.View)]
[Route("api/v1/")]
public class MainScreensController(IMainScreenService service, IRequestService requestService, KaderDbContext context , IWebHostEnvironment hostEnvironment) : ControllerBase
{
    private readonly IRequestService requestService = requestService;

    private readonly IMainScreenService _mainScreenRepository = service;
    private readonly KaderDbContext _dbcontext = context;
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;





    #region Retrieve
    [HttpGet(ApiRoutes.MainScreen.ListOfMainScreens)]
    [Permission(Helpers.Permission.View, 1)]
    public async Task<IActionResult> ListOfMainScreensAsync() =>
        Ok(await service.ListOfMainScreensAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.MainScreen.GetAllMainScreens)]
    [Permission(Helpers.Permission.View, 1)]
    public async Task<IActionResult> GetAllMainScreensAsync([FromQuery] StGetAllFiltrationsForMainScreenRequest model) =>
        Ok(await service.GetAllMainScreensAsync(requestService.GetRequestHeaderLanguage, model));



    [HttpGet(ApiRoutes.MainScreen.GetMainScreensWithRelatedData)]
    [Permission(Helpers.Permission.View, 1)]
    public async Task<IActionResult> GetMainScreensWithRelatedData([FromQuery] StGetAllFiltrationsForMainScreenRequest model)
    {

        var result = await _mainScreenRepository.GetMainScreensWithRelatedDataAsync(requestService.GetRequestHeaderLanguage);
        return Ok(result);
    }


    [HttpGet(ApiRoutes.MainScreen.GetMainScreenById)]
    [Permission(Helpers.Permission.View, 1)]
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
    [Permission(Helpers.Permission.Add, 1)]
    public async Task<IActionResult> CreateMainScreenAsync([FromForm] StCreateMainScreenRequest model)
    {
        var response = await service.CreateMainScreenAsync(model, _hostEnvironment.WebRootPath, requestService.client_id, Modules.Setting);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Update

    [HttpPut(ApiRoutes.MainScreen.UpdateMainScreen)]
    [Permission(Helpers.Permission.Edit, 1)]
    public async Task<IActionResult> UpdateMainScreenAsync([FromRoute] int id, [FromBody] StUpdateMainScreenRequest model)
    {
        var response = await service.UpdateMainScreenAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Restore
    //[HttpPut(ApiRoutes.MainScreen.RestoreMainScreen)]
    //public async Task<IActionResult> RestoreMainScreen([FromBody] int id)
    //{
    //    if (Response == null)
    //        return Ok(Response);
    //    else return BadRequest(Response);
    //}
    #endregion

    #region Delete

    [HttpDelete(ApiRoutes.MainScreen.DeleteMainScreen)]
    [Permission(Helpers.Permission.Delete, 1)]
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


}