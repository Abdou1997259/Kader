namespace Kader_System.Api.Areas.Auth.Controllers;

[Area(Modules.Auth)]
[ApiController]
[ApiExplorerSettings(GroupName = Modules.Auth)]
[Authorize(Permissions.Setting.View)]
[Route("api/v1/")]
public class PermController(IPermService service) : ControllerBase
{
    private readonly IPermService _service = service;

    [HttpGet(ApiRoutes.Perm.GetAllRoles)]
    public async Task<IActionResult> GetAllRolesAsync() =>
        Ok(await _service.GetAllRolesAsync(GetCurrentRequestLanguage()));

    [HttpPost(ApiRoutes.Perm.CreateRole)]
    public async Task<IActionResult> CreateRoleAsync(PermCreateRoleRequest model)
    {
        var response = await _service.CreateRoleAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPut(ApiRoutes.Perm.UpdateRole)]
    public async Task<IActionResult> UpdateRoleAsync([FromRoute] string id, PermUpdateRoleRequest model)
    {
        var response = await _service.UpdateRoleAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpDelete(ApiRoutes.Perm.DeleteRoleById)]
    public async Task<IActionResult> DeleteRoleByIdAsync([FromRoute] string id)
    {
        var response = await _service.DeleteRoleByIdAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpGet(ApiRoutes.Perm.GetEachUserWithHisRoles)]
    public async Task<IActionResult> GetEachUserWithHisRolesAsync([FromQuery] PermGetEachUserWithRolesRequest model)
    {
        var response = await _service.GetEachUserWithHisRolesAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpGet(ApiRoutes.Perm.ManageUserRoles)]
    public async Task<IActionResult> ManageUserRolesAsync([FromRoute] string userId)
    {
        var response = await _service.ManageUserRolesAsync(userId);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPost(ApiRoutes.Perm.UpdateUserRoles)]
    public async Task<IActionResult> UpdateUserRolesAsync(PermGetManagementModelResponse model)
    {
        var response = await _service.UpdateUserRolesAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }


    [HttpGet(ApiRoutes.Perm.ManageRolePermissions)]
    public async Task<IActionResult> ManageRolePermissionsAsync([FromRoute] string roleId)
    {
        var response = await _service.ManageRolePermissionsAsync(roleId, GetCurrentRequestLanguage());
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPost(ApiRoutes.Perm.UpdateRolePermissions)]
    public async Task<IActionResult> UpdateRolePermissionsAsync(PermUpdateRolePermissionsRequest model)
    {
        var response = await _service.UpdateRolePermissionsAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }


    [HttpGet(ApiRoutes.Perm.ManageUserPermissions)]
    public async Task<IActionResult> ManageUserPermissionsAsync([FromRoute] string userId)
    {
        var response = await _service.ManageUserPermissionsAsync(userId, GetCurrentRequestLanguage());
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPost(ApiRoutes.Perm.UpdateUserPermissions)]
    public async Task<IActionResult> UpdateUserPermissionsAsync(PermUpdateUserPermissionsRequest model)
    {
        var response = await _service.UpdateUserPermissionsAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    private string GetCurrentRequestLanguage() =>
        Request.Headers.AcceptLanguage.ToString().Split(',').First();
  
    [HttpPost("TestForAnything")]
    public  IActionResult UpdateTestAsync(/*[FromForm] PermTest model*/IEnumerable<int> ids)
    {
        return Ok(ids);
    }



}
