namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.HR)]
[ApiExplorerSettings(GroupName = Modules.HR)]
[ApiController]
[Authorize(Permissions.HR.View)]
[Route("api/v1/")]
public class DeductionsController(IDeductionService service) : ControllerBase
{
    private readonly IDeductionService _service = service;

    [HttpGet(ApiRoutes.Deduction.ListOfDeductions)]
    public async Task<IActionResult> ListOfDeductionsAsync() =>
        Ok(await _service.ListOfDeductionsAsync(GetCurrentRequestLanguage()));

    [HttpGet(ApiRoutes.Deduction.GetAllDeductions)]
    public async Task<IActionResult> GetAllDeductionsAsync([FromQuery] HrGetAllFiltrationsForDeductionsRequest model) =>
        Ok(await _service.GetAllDeductionsAsync(GetCurrentRequestLanguage(), model, GetCurrentHost()));

    [HttpPost(ApiRoutes.Deduction.CreateDeduction)]
    public async Task<IActionResult> CreateDeductionAsync(HrCreateDeductionRequest model)
    {
        var response = await _service.CreateDeductionAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpGet(ApiRoutes.Deduction.GetDeductionById)]
    public async Task<IActionResult> GetDeductionByIdAsync(int id)
    {
        var response = await _service.GetDeductionByIdAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPut(ApiRoutes.Deduction.UpdateDeduction)]
    public async Task<IActionResult> UpdateDeductionAsync([FromRoute] int id, HrUpdateDeductionRequest model)
    {
        var response = await _service.UpdateDeductionAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }


    [HttpPut(ApiRoutes.Deduction.RestoreDeduction)]
    public async Task<IActionResult> RestoreDeduction([FromRoute] int id)
    {
        var response = await _service.RestoreDeductionAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpDelete(ApiRoutes.Deduction.DeleteDeduction)]
    public async Task<IActionResult> DeleteDeductionAsync(int id)
    {
        var response = await _service.DeleteDeductionAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    private string GetCurrentRequestLanguage() =>
        Request.Headers.AcceptLanguage.ToString().Split(',').First();
    private string GetCurrentHost() =>
        HttpContext.Request.Host.Value +
        HttpContext.Request.Path.Value;
}
