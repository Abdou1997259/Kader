using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.Setting)]
[ApiExplorerSettings(GroupName = Modules.Setting)]
[ApiController]
//[Authorize(Permissions.MainScreenCat.View)]
[Route("api/v1/")]

public class ScreensCategoriesController(IScreenCategoryService service, IRequestService requestService, IWebHostEnvironment hostEnvironment) : ControllerBase
{
    private readonly IRequestService requestService = requestService;
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;


    #region Retrieve
    [HttpGet(ApiRoutes.ScreenCategory.ListOfScreensCategories)]

    public async Task<IActionResult> ListOfScreensCategoriesAsync() =>
        Ok(await service.ListOfScreensCategoriesAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.ScreenCategory.GetAllScreenCategories)]
    public async Task<IActionResult> GetAllScreensCategoriesAsync([FromQuery] StGetAllFiltrationsForScreenCategoryRequest model) =>
        Ok(await service.GetAllScreensCategoriesAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost));

    [HttpGet(ApiRoutes.ScreenCategory.GetScreenCategoryById)]
    public async Task<IActionResult> GetScreenCategoryByIdAsync(int id)
    {
        var response = await service.GetScreenCategoryByIdAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    #endregion

    #region Insert

    [HttpPost(ApiRoutes.ScreenCategory.CreateScreenCategory)]

    public async Task<IActionResult> CreateServiceAsync([FromForm] StCreateScreenCategoryRequest model)
    {
        var response = await service.CreateScreenCategoryAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Update

    [HttpPut(ApiRoutes.ScreenCategory.UpdateScreenCategory)]
    public async Task<IActionResult> UpdateServiceAsync([FromRoute] int id, [FromForm] StUpdateScreenCategoryRequest model)
    {
        //this is not the first time this happens from now on i will record everything for the record 
        var serverPath = HttpContext.Items["ServerPath"]?.ToString();

        var response = await service.UpdateScreenCategoryAsync(id, model, requestService.GetRequestHeaderLanguage, serverPath, Modules.Setting);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPut(ApiRoutes.ScreenCategory.restore)]
    public async Task<IActionResult> RestoreAsync([FromRoute] int id)
    {


        var response = await service.RestoreCatScreenAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPost(ApiRoutes.ScreenCategory.OrderbyPattern)]
    public async Task<IActionResult> orderbyPatttern(int[] Ids)
    {


        var response = await service.OrderByPattern(Ids);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    #endregion

    #region Delete

    [HttpDelete(ApiRoutes.ScreenCategory.DeleteScreenCategory)]
    public async Task<IActionResult> DeleteScreenCategoryAsync(int id)
    {
        var response = await service.DeleteScreenCategoryAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

}
