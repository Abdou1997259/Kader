namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.HR)]
[ApiExplorerSettings(GroupName = Modules.HR)]
[ApiController]
[Authorize(Permissions.HR.View)]
[Route("api/v1/")]
public class QualificationsController(IQualificationService service) : ControllerBase
{
    #region Retrieve
    [HttpGet(ApiRoutes.Qualification.ListOfQualifications)]
    public async Task<IActionResult> ListOfQualificationsAsync() =>
        Ok(await service.ListOfQualificationsAsync(GetCurrentRequestLanguage()));

    [HttpGet(ApiRoutes.Qualification.GetAllQualifications)]
    public async Task<IActionResult> GetAllDeductionsAsync([FromQuery] HrGetAllFiltrationsForQualificationsRequest model) =>
        Ok(await service.GetAllQualificationsAsync(GetCurrentRequestLanguage(), model, GetCurrentHost()));


    [HttpGet(ApiRoutes.Qualification.GetQualificationById)]
    public async Task<IActionResult> GetDeductionByIdAsync(int id)
    {
        var response = await service.GetQualificationByIdAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    #endregion

    #region Insert

    [HttpPost(ApiRoutes.Qualification.CreateQualification)]
    public async Task<IActionResult> CreateDeductionAsync(HrCreateQualificationRequest model)
    {
        var response = await service.CreateQualificationAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Update

    [HttpPut(ApiRoutes.Qualification.UpdateQualification)]
    public async Task<IActionResult> UpdateQualificationAsync([FromRoute] int id, HrUpdateQualificationRequest model)
    {
        var response = await service.UpdateQualificationAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPut(ApiRoutes.Qualification.RestoreQualification)]
    public async Task<IActionResult> RestoreQualificationAsync([FromRoute] int id)
    {
        var response = await service.RestoreQualificationAsync(id );
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion


    #region Delete
    [HttpDelete(ApiRoutes.Qualification.DeleteQualification)]
    public async Task<IActionResult> DeleteDeductionAsync(int id)
    {
        var response = await service.DeleteQualificationAsync(id);
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
