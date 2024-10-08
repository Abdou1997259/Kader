﻿using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.HR)]
[ApiExplorerSettings(GroupName = Modules.HR)]
[ApiController]
//[Authorize(Permissions.HR.View)]
[Route("api/v1/")]
public class DeductionsController(IDeductionService service, IRequestService requestService) : ControllerBase
{
    private readonly IDeductionService _service = service;
    private readonly IRequestService requestService = requestService;


    [HttpGet(ApiRoutes.Deduction.ListOfDeductions)]
    [Permission(Permission.View, 15)]
    public async Task<IActionResult> ListOfDeductionsAsync() =>
        Ok(await _service.ListOfDeductionsAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.Deduction.GetAllDeductions)]
    public async Task<IActionResult> GetAllDeductionsAsync([FromQuery] HrGetAllFiltrationsForDeductionsRequest model) =>
        Ok(await _service.GetAllDeductionsAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost));

    [HttpPost(ApiRoutes.Deduction.CreateDeduction)]
    [Permission(Permission.Add, 15)]
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
    [Permission(Permission.View, 15)]
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
    [Permission(Permission.Edit, 15)]
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
    [Permission(Permission.Edit, 15)]
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
    [Permission(Permission.Delete, 15)]
    public async Task<IActionResult> DeleteDeductionAsync(int id)
    {
        var response = await _service.DeleteDeductionAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

}
