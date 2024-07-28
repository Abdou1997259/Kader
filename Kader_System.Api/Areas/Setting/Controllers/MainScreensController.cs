using Kader_System.Domain.DTOs.Response.Setting;
using Kader_System.Domain.Interfaces.Setting;
using Kader_System.Services.IServices.HTTP;
namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.Setting)]
[ApiExplorerSettings(GroupName = Modules.Setting)]
[ApiController]
[Authorize(Permissions.Setting.View)]
[Route("api/v1/")]
public class MainScreensController(IMainScreenService service, IRequestService requestService) : ControllerBase
{
    private readonly IRequestService requestService = requestService;

    private readonly IMainScreenService _mainScreenRepository = service;

    
     

    #region Retrieve
    [HttpGet(ApiRoutes.MainScreen.ListOfMainScreens)]
    public async Task<IActionResult> ListOfMainScreensAsync() => 
        Ok(await service.ListOfMainScreensAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.MainScreen.GetAllMainScreens)]
    public async Task<IActionResult> GetAllMainScreensAsync([FromQuery] StGetAllFiltrationsForMainScreenRequest model) =>
        Ok(await service.GetAllMainScreensAsync(requestService.GetRequestHeaderLanguage, model));



    [HttpGet(ApiRoutes.MainScreen.GetMainScreensWithRelatedData)]
    public async Task<IActionResult> GetMainScreensWithRelatedData()
    {
        var mainScreens = await service.GetMainScreensWithRelatedDataAsync();

        var response = mainScreens.Select(ms => new StMainScreen
        {
            Screen_main_title_ar = ms.Screen_main_title_ar,
            Screen_main_title_en = ms .Screen_main_title_en,

            // Map other properties

            CategoryScreen = new StMainScreenCat
            {
                Id = ms.CategoryScreen.Id,
                Screen_cat_title_ar =ms.Screen_main_title_ar,
                Screen_cat_title_en =ms.Screen_main_title_en,
                // Map other properties

                subScreen = new StScreenSub
                {
                    Id = ms.CategoryScreen.subScreen.Id,
                    Screen_sub_title_ar =ms.Screen_main_title_ar,
                    Screen_sub_title_en = ms.Screen_main_title_en,
                    Url = ms.Url,
                    Name =ms.Name,
                    // Map other properties
                }
            }
        }).ToList();

        return Ok(response);
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


}
