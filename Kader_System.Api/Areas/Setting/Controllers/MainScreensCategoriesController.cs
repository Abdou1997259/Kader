using Kader_System.Services.IServices.HTTP;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.Setting)]
[ApiExplorerSettings(GroupName = Modules.Setting)]
[ApiController]
//[Authorize(Permissions.MainScreenCat.View)]
[Route("api/v1/")]

public class MainScreensCategoriesController(IMainScreenCategoryService service, IRequestService requestService,IWebHostEnvironment hostEnvironment) : ControllerBase
{
    private readonly IRequestService requestService = requestService;
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;


    #region Retrieve
    [HttpGet(ApiRoutes.MainScreenCategory.ListOfMainScreensCategories)]
    
    public async Task<IActionResult> ListOfMainScreensCategoriesAsync() =>
        Ok(await service.ListOfMainScreensCategoriesAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.MainScreenCategory.GetAllMainScreenCategories)]
    public async Task<IActionResult> GetAllMainScreensCategoriesAsync([FromQuery] StGetAllFiltrationsForMainScreenCategoryRequest model) =>
        Ok(await service.GetAllMainScreensCategoriesAsync(requestService.GetRequestHeaderLanguage, model,requestService.GetCurrentHost));

    [HttpGet(ApiRoutes.MainScreenCategory.GetMainScreenCategoryById)]
    public async Task<IActionResult> GetMainScreenCategoryByIdAsync(int id)
    {
        var response = await service.GetMainScreenCategoryByIdAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    #endregion

    #region Insert

    [HttpPost(ApiRoutes.MainScreenCategory.CreateMainScreenCategory)]
    
    public async Task<IActionResult> CreateServiceAsync([FromForm] StCreateMainScreenCategoryRequest model)
    {
        var response = await service.CreateMainScreenCategoryAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Update

    [HttpPut(ApiRoutes.MainScreenCategory.UpdateMainScreenCategory)]
    public async Task<IActionResult> UpdateServiceAsync([FromRoute] int id, [FromForm] StUpdateMainScreenCategoryRequest model,string lang)
    {
        var serverPath = HttpContext.Items["ServerPath"]?.ToString();

        var response = await service.UpdateMainScreenCategoryAsync(id, model,lang, serverPath, Modules.Setting);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    //[HttpPut(ApiRoutes.MainScreenCategory.restore)]
    //public async Task<IActionResult> RestoreAsync([FromRoute] int id)
    //{


    //    var response = await service.RestoreCatScreenAsync(id);
    //    if (response.Check)
    //        return Ok(response);
    //    else if (!response.Check)
    //        return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
    //    return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    //}
    //[HttpPut(ApiRoutes.MainScreenCategory.OrderbyPattern)]
    //public async Task<IActionResult> orderbyPatttern(int[] id)
    //{


    //    var response = await service.OrderByPattern(id);
    //    if (response.Check)
    //        return Ok(response);
    //    else if (!response.Check)
    //        return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
    //    return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    //}
    #endregion

    #region Delete

    [HttpDelete(ApiRoutes.MainScreenCategory.DeleteMainScreenCategory)]
    public async Task<IActionResult> DeleteMainScreenCategoryAsync(int id)
    {
        var response = await service.DeleteMainScreenCategoryAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

}
