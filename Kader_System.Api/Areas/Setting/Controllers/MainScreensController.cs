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

        var mainScreens = await _dbcontext.MainScreenCategories
        .Include(ms => ms.CategoryScreen)
            .ThenInclude(cs => cs.StScreenSub)
        .ToListAsync();

        if (mainScreens == null)
        {
            return NotFound();
        }


        var ChildScreens = mainScreens.Select(ms => new GetAllStMainScreen
        {
            Screen_main_title_ar = ms.Screen_main_title_ar,
            //Screen_main_title_en = ms.Screen_main_title_en,
            CategoryScreen = ms.CategoryScreen.Select(x => new GetAllStMainScreenCat
            {
                Ids = ms.CategoryScreen.Select(x => x.Id).ToList(),
                Screen_cat_title_ar = ms.CategoryScreen.Select(x => x.Screen_cat_title_ar).ToList(),
                //Screen_cat_title_en = ms.CategoryScreen.Select(x => x.Screen_cat_title_en).ToList(),
                StScreenSub = x.StScreenSub.Select(k => new GetAllStScreenSub
                {
                    Ids = k.ScreenCat.StScreenSub.Select(x => x.Id).ToList(),
                    Screen_sub_title_ar = k.ScreenCat.StScreenSub.Select(y => y.Screen_sub_title_ar).ToList(),
                    Url = k.ScreenCat.StScreenSub.Select(y => y.Url).ToList(),
                    Screen_main_cat_image = k.ScreenCat.StScreenSub.Select(y => y.Screen_main_cat_image).ToList(),

                }).ToList(),
            }).ToList()
        });


        return Ok(ChildScreens);
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