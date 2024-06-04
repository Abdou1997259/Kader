namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.Setting)]
[ApiExplorerSettings(GroupName = Modules.Setting)]
[ApiController]
[Authorize(Permissions.MainScreenCat.View)]
[Route("api/v1/")]

public class MainScreensCategoriesController(IMainScreenCategoryService service) : ControllerBase
{
    #region Retrieve
    [HttpGet(ApiRoutes.MainScreenCategory.ListOfMainScreensCategories)]
    public async Task<IActionResult> ListOfMainScreensCategoriesAsync() =>
        Ok(await service.ListOfMainScreensCategoriesAsync(GetCurrentRequestLanguage()));

    [HttpGet(ApiRoutes.MainScreenCategory.GetAllMainScreenCategories)]
    public async Task<IActionResult> GetAllMainScreensCategoriesAsync([FromQuery] StGetAllFiltrationsForMainScreenCategoryRequest model) =>
        Ok(await service.GetAllMainScreensCategoriesAsync(GetCurrentRequestLanguage(), model));

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
    public async Task<IActionResult> UpdateServiceAsync([FromRoute] int id, [FromForm] StUpdateMainScreenCategoryRequest model)
    {
        var response = await service.UpdateMainScreenCategoryAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

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

    #region Helpers

    private string GetCurrentRequestLanguage() =>
        Request.Headers.AcceptLanguage.ToString().Split(',').First();

    #endregion


}
