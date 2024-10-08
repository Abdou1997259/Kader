﻿using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.HR)]
[ApiExplorerSettings(GroupName = Modules.HR)]
[ApiController]
[Route("api/v1/")]
/*[Authorize(Permissions.HR.View)*/
public class BenefitsController(IBenefitService service, IRequestService requestService) : ControllerBase
{
    private readonly IRequestService requestService = requestService;

    #region Retrieve

    [HttpGet(ApiRoutes.Benefit.ListOfBenefits)]
    [Permission(Permission.View, 16)]
    public async Task<IActionResult> ListOfBenefitsAsync() =>
        Ok(await service.ListOfBenefitsAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.Benefit.GetAllBenefits)]
    [Permission(Permission.View, 16)]
    public async Task<IActionResult> GetAllBenefitsAsync([FromQuery] HrGetAllFiltrationsForBenefitsRequest model) =>
        Ok(await service.GetAllBenefitsAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost));
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
    [Permission(Permission.Add, 16)]
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
    [Permission(Permission.Edit, 16)]
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
    [Permission(Permission.Edit, 16)]
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
    [Permission(Permission.Delete, 16)]
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


}
