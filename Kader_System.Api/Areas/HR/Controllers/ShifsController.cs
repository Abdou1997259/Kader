using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.HR)]
//[Authorize(Permissions.HR.View)]
[ApiExplorerSettings(GroupName = Modules.HR)]
[ApiController]
[Route("api/v1/")]
public class ShiftsController(IShiftService service, IRequestService requestService) : ControllerBase
{
    private readonly IRequestService requestService = requestService;

    #region Retrieve

    [HttpGet(ApiRoutes.Shift.ListOfShifts)]
    [Permission(Permission.View, 30)]
    public async Task<IActionResult> ListOfShiftsAsync() =>
        Ok(await service.ListOfShiftsAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.Shift.GetAllShifts)]
    [Permission(Permission.View, 30)]
    public async Task<IActionResult> GetAllShiftsAsync([FromQuery] HrGetAllFiltrationsForShiftsRequest model) =>
        Ok(await service.GetAllShiftsAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost));


    #endregion
    [HttpPost(ApiRoutes.Shift.CreateShift)]
    [Permission(Permission.Add, 30)]
    public async Task<IActionResult> CreateShiftAsync(HrCreateShiftRequest model)
    {
        var response = await service.CreateShiftAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpGet(ApiRoutes.Shift.GetShiftById)]
    [Permission(Permission.View, 30)]
    public async Task<IActionResult> GetShiftByIdAsync(int id)
    {
        var response = await service.GetShiftByIdAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPut(ApiRoutes.Shift.UpdateShift)]
    [Permission(Permission.Edit, 30)]
    public async Task<IActionResult> UpdateShiftAsync([FromRoute] int id, HrUpdateShiftRequest model)
    {
        var response = await service.UpdateShiftAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPut(ApiRoutes.Shift.ChangeShift)]
    [Permission(Permission.Edit, 30)]
    public async Task<IActionResult> ChangeShiftAsync([FromForm] ChangeShiftRequest model)
    {
        var response = await service.ChangeShift( model.from_shift,model.to_shift);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPut(ApiRoutes.Shift.RestoreShift)]
    [Permission(Permission.Edit, 30)]
    public async Task<IActionResult> RestoreShiftAsync([FromRoute] int id)
    {
        var response = await service.RestoreShiftAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpDelete(ApiRoutes.Shift.DeleteShift)]
    [Permission(Permission.Delete, 30)]
    public async Task<IActionResult> DeleteShiftAsync(int id)
    {
        var response = await service.DeleteShiftAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

}
