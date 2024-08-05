using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.HR)]
[ApiExplorerSettings(GroupName = Modules.HR)]
[ApiController]
//[Authorize(Permissions.HR.View)]
[Route("api/v1/")]
public class QualificationsController(IQualificationService service, IRequestService requestService) : ControllerBase
{
    private readonly IRequestService requestService = requestService;

    #region Retrieve
    [HttpGet(ApiRoutes.Qualification.ListOfQualifications)]
    [Permission(Permission.View, 9)]
    public async Task<IActionResult> ListOfQualificationsAsync() =>
        Ok(await service.ListOfQualificationsAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.Qualification.GetAllQualifications)]
    [Permission(Permission.View, 9)]
    public async Task<IActionResult> GetAllDeductionsAsync([FromQuery] HrGetAllFiltrationsForQualificationsRequest model) =>
        Ok(await service.GetAllQualificationsAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost));


    [HttpGet(ApiRoutes.Qualification.GetQualificationById)]
    [Permission(Permission.View, 9)]
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
    [Permission(Permission.Add, 9)]
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
    [Permission(Permission.Edit, 9)]
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
    [Permission(Permission.Edit, 9)]
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
    [Permission(Permission.Delete, 9)]
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

}
