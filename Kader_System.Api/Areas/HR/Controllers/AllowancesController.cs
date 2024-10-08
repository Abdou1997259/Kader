﻿using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.HR)]
//[Authorize(Permissions.HR.View)]
[ApiExplorerSettings(GroupName = Modules.HR)]
[ApiController]
[Route("api/v1/")]
public class AllowancesController(IAllowanceService service, IRequestService requestService) : ControllerBase
{

    private readonly IRequestService requestService = requestService;

    #region Retreieve
    [HttpGet(ApiRoutes.Allowance.ListOfAllowances)]
    public async Task<IActionResult> ListOfAllowancesAsync() =>
        Ok(await service.ListOfAllowancesAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.Allowance.GetAllAllowances)]
    [Permission(Permission.View, 14)]
    public async Task<IActionResult> GetAllAllowancesAsync([FromQuery] HrGetAllFiltrationsForAllowancesRequest model) =>
        Ok(await service.GetAllAllowancesAsync(requestService.GetRequestHeaderLanguage, model,requestService.GetCurrentHost));

    [HttpGet(ApiRoutes.Allowance.GetAllowanceById)]
    [Permission(Permission.View, 14)]
    public async Task<IActionResult> GetAllowanceByIdAsync(int id)
    {
        var response = await service.GetAllowanceByIdAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Insert
    [HttpPost(ApiRoutes.Allowance.CreateAllowance)]
    [Permission(Permission.Add, 14)]
    public async Task<IActionResult> CreateCompanyAsync(HrCreateAllowanceRequest model)
    {
        var response = await service.CreateAllowanceAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }



    #endregion

    #region Update

    [HttpPut(ApiRoutes.Allowance.UpdateAllowance)]
    [Permission(Permission.Edit, 14)]
    public async Task<IActionResult> UpdateAllowanceAsync([FromRoute] int id, HrUpdateAllowanceRequest model)
    {
        var response = await service.UpdateAllowanceAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPut(ApiRoutes.Allowance.RestoreAllowance)]
    public async Task<IActionResult> RestoreAllowanceAsync([FromRoute] int id)
    {
        var response = await service.RestoreAllowanceAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPost(ApiRoutes.Allowance.OrderbyPattern)]
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

    [HttpDelete(ApiRoutes.Allowance.DeleteAllowance)]
    [Permission(Permission.Delete, 14)]
    public async Task<IActionResult> DeleteAllowanceAsync(int id)
    {
        var response = await service.DeleteAllowanceAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion



}
