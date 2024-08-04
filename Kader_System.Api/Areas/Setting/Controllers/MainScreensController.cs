using Kader_System.DataAccesss.DbContext;
using Kader_System.Domain.DTOs.Request.Setting;
using Kader_System.Domain.DTOs.Response.Setting;
using Kader_System.Domain.Interfaces.Setting;
using Kader_System.Services.IServices.HTTP;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.Setting)]
[ApiExplorerSettings(GroupName = Modules.Setting)]
[ApiController]
//[Authorize(Permissions.Setting.View)]
[Route("api/v1/")]
public class MainScreensController(IMainScreenService service, IRequestService requestService, KaderDbContext context) : ControllerBase
{
    private readonly IRequestService requestService = requestService;

    private readonly IMainScreenService _mainScreenRepository = service;
    private readonly KaderDbContext _dbcontext = context;




    #region Retrieve
    [HttpGet(ApiRoutes.MainScreen.ListOfMainScreens)]
    public async Task<IActionResult> ListOfMainScreensAsync() =>
        Ok(await service.ListOfMainScreensAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.MainScreen.GetAllMainScreens)]
    public async Task<IActionResult> GetAllMainScreensAsync([FromQuery] StGetAllFiltrationsForMainScreenRequest model) =>
        Ok(await service.GetAllMainScreensAsync(requestService.GetRequestHeaderLanguage, model));



    [HttpGet(ApiRoutes.MainScreen.GetMainScreensWithRelatedData)]
    public async Task<IActionResult> GetMainScreensWithRelatedData([FromQuery] StGetAllFiltrationsForMainScreenRequest model)
    {

        var result = await _mainScreenRepository.GetMainScreensWithRelatedDataAsync(requestService.GetRequestHeaderLanguage);
        return Ok(result);
    }


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
    public async Task<IActionResult> CreateMainScreenAsync([FromBody] StCreateMainScreenRequest model)
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