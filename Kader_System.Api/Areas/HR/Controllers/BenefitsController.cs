namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.HR)]
[ApiExplorerSettings(GroupName = Modules.HR)]
[ApiController]
[Route("api/v1/")]
[Authorize(Permissions.HR.View)]
public class BenefitsController(IBenefitService service) : ControllerBase
{
    #region Retrieve

    [HttpGet(ApiRoutes.Benefit.ListOfBenefits)]
    public async Task<IActionResult> ListOfBenefitsAsync() =>
        Ok(await service.ListOfBenefitsAsync(GetCurrentRequestLanguage()));

    [HttpGet(ApiRoutes.Benefit.GetAllBenefits)]
    public async Task<IActionResult> GetAllBenefitsAsync([FromQuery] HrGetAllFiltrationsForBenefitsRequest model) =>
        Ok(await service.GetAllBenefitsAsync(GetCurrentRequestLanguage(), model, GetCurrentHost()));
    [HttpGet(ApiRoutes.Benefit.GetBenefitById)]
    public async Task<IActionResult> GetBenefitByIdAsync(int id)
    {
        var response = await service.GetBenefitByIdAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Insert

    [HttpPost(ApiRoutes.Benefit.CreateBenefit)]
    public async Task<IActionResult> CreateBenefitAsync(HrCreateBenefitRequest model)
    {
        var response = await service.CreateBenefitAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Update

    [HttpPut(ApiRoutes.Benefit.UpdateBenefit)]
    public async Task<IActionResult> UpdateBenefitAsync([FromRoute] int id, HrUpdateBenefitRequest model)
    {
        var response = await service.UpdateBenefitAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPut(ApiRoutes.Benefit.RestoreBenefit)]
    public async Task<IActionResult> RestoreBenefit([FromRoute] int id)
    {
        var response = await service.RestoreBenefitAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Delete

    [HttpDelete(ApiRoutes.Benefit.DeleteBenefit)]
    public async Task<IActionResult> DeleteAllowanceAsync(int id)
    {
        var response = await service.DeleteBenefitAsync(id);
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
    private string GetCurrentHost() =>
        HttpContext.Request.Host.Value +
        HttpContext.Request.Path.Value;
    #endregion

}
