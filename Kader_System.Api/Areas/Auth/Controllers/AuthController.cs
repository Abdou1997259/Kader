using Kader_System.Api.Helpers;
using Kader_System.Domain.Constants.Enums;
using Kader_System.Domain.DTOs.Request.Auth;
using Kader_System.Domain.DTOs.Response.Auth;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Auth.Controllers;

[Area(Modules.Auth)]
[ApiController]
[ApiExplorerSettings(GroupName = Modules.Auth)]
//[Authorize(Permissions.Setting.View)]
//[Authorize("Superadmin")]
[Route("api/v1/")]
public class AuthController(IAuthService service,IWebHostEnvironment hostEnvironment, IRequestService requestService) : ControllerBase
{
    private readonly IAuthService _service = service;

    [AllowAnonymous]
    [HttpPost(ApiRoutes.User.LoginUser)]


    public async Task<IActionResult> LoginUserAsync(AuthLoginUserRequest model)
     {
        var response = await _service.LoginUserAsync(model);
        if (response.Check)
        {
            //if (!string.IsNullOrEmpty(response.Data.RefreshToken))
            //    SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);
            return Ok(response);
        }
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    //[AllowAnonymous]
    //[HttpPost("upload")]
    //public IActionResult UploadFile([FromForm] IFormFileCollection company_contracts)
    //{ // Combine the directory path and the file name
    //    string filePath = Path.Combine(Directory.GetCurrentDirectory() + GoRootPath.HRFilesPath, company_contracts[0].FileName);
    //    string uploadDirectory = Directory.GetCurrentDirectory() + GoRootPath.HRFilesPath;
    //    foreach (var file in company_contracts)
    //    {
    //        if (file != null && file.Length > 0)
    //        {
    //            ManageFilesHelper.UploadFile(file, GoRootPath.HRFilesPath);
    //            //// Create the directory if it doesn't exist
    //            //if (!Directory.Exists(uploadDirectory))
    //            //{
    //            //    Directory.CreateDirectory(uploadDirectory);
    //            //}

    //            //// Create a FileStream to write the uploaded file
    //            //using (var stream = new FileStream(filePath, FileMode.Create))
    //            //{
    //            //    // Copy the file stream to the FileStream
    //            //    file.CopyTo(stream);
    //            //}

    //            // Process the model as needed, e.g., save to a database
    //            // ...


    //        }

    //        return Ok("File uploaded successfully");
    //    }


    //    return BadRequest("No file or empty file");
    //}

    [AllowAnonymous]
    [HttpPost("upload")]
    public IActionResult UploadFile([FromBody] string image)
    {
        var ii = ManageFilesHelper.SaveBase64StringToFile(image, Directory.GetCurrentDirectory(), "test");

        return Ok(ii.FileExtension);
    }

    [AllowAnonymous]
    [HttpDelete(ApiRoutes.User.LogOutUser)]
    public async Task<IActionResult> LogOutUserAsync()
    {
        var response = await _service.LogOutUserAsync();
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPost(ApiRoutes.User.AddUser)]
    [Permission(Permission.Add, 4)]

    public async Task<IActionResult> CreatUser(CreateUserRequest model)
    {
      var response =  await _service.CreateUserAsync(model, hostEnvironment.WebRootPath, requestService.client_id, Modules.Auth, Domain.Constants.Enums.UsereEnum.Users);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPut(ApiRoutes.User.UpdateUser)]
    [Permission(Permission.Edit, 4)]
    public async Task<IActionResult> UpdateUserAsync([FromRoute]
    string id, [FromForm] UpdateUserRequest  model)

    {

        var response = await _service.UpdateUserAsync(id, requestService.GetRequestHeaderLanguage,  model,
            hostEnvironment.WebRootPath, requestService.client_id, Modules.Auth, Domain.Constants.Enums.UsereEnum.Users);

        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpGet(ApiRoutes.User.ShowPasswordToSpecificUser)]
    public async Task<IActionResult> ShowPasswordToSpecificUserAsync([FromRoute] string id)
    {
        var response = await _service.ShowPasswordToSpecificUserAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPut(ApiRoutes.User.SetNewPasswordToSpecificUser)]
    public async Task<IActionResult> SetNewPasswordToSpecificUserAsync(AuthSetNewPasswordRequest model)
    {
        var response = await _service.SetNewPasswordToSpecificUserAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    [HttpPut(ApiRoutes.User.SetNewPasswordToSuperAdmin)]
    public async Task<IActionResult> SetNewPasswordToSuperAdminAsync([FromRoute] string newPassword)
    {
        var response = await _service.SetNewPasswordToSuperAdminAsync(newPassword);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpDelete(ApiRoutes.User.DeleteUser)]
    [Permission(Permission.Delete, 4)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var response = await _service.DeleteUser(id );
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPut(ApiRoutes.User.RestoreUser)]
    [Permission(Permission.Edit, 4)]
    public async Task<IActionResult> RestoreUser(string id)
    {
        var response = await _service.RestoreUser(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpGet(ApiRoutes.User.GetAllUsers)]
    [Permission(Permission.View, 4)]
    public async Task<IActionResult> GetAllUsers([FromQuery] FilterationUsersRequest filterationUsersRequest)
    {
        var response = await _service.GetAllUsers(filterationUsersRequest,requestService.GetCurrentHost,requestService.GetRequestHeaderLanguage);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpGet(ApiRoutes.User.GetUserById)]
    [Permission(Permission.View, 4)]
    public async Task<IActionResult> GetUsersById(string id)
    {
        var response = await _service.GetUserById(id, requestService.GetRequestHeaderLanguage);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpGet(ApiRoutes.User.GetListOfUser)]
    [Permission(Permission.View, 4)]
    public async Task<IActionResult> GetListOfUsers()
    {
        var response = await _service.ListListOfUsers( requestService.GetRequestHeaderLanguage);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }


    [HttpGet(ApiRoutes.User.GetLookups)]
    public async Task<IActionResult> GetUserLookups()
    {
        var response = await _service.UsersGetLookups(requestService.GetRequestHeaderLanguage);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);

    }
    [HttpPost(ApiRoutes.User.AssginPermssionToUser)]
    public async Task<IActionResult> GetUserLookups([FromRoute] string id,[FromBody]  IEnumerable<Kader_System.Domain.DTOs.Request.Auth.Permissions> model, [FromQuery] bool all = false, 
        [FromQuery] int titleId = 1)
    {
        var response = await _service.AssignPermissionForUser(id,all,titleId,model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);

    }
    [HttpGet(ApiRoutes.User.GetMyProfile)]
    public async Task<IActionResult> GetMyProfile()
    {
        var response = await _service.GetMyProfile( requestService.GetRequestHeaderLanguage);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPut(ApiRoutes.User.UpdateTitle)]
    public async Task<IActionResult> UpdateTitle(int title)
    {
        var response = await _service.ChangeTitle(title);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpPut(ApiRoutes.User.UpdateCompany)]
    public async Task<IActionResult> UpdateCompany(int company)
    {
        var response = await _service.ChangeCompany(company);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    public class CompanyContractModel
    {
        // Other properties...

        public byte[] FilePath { get; set; }
    }
    //private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
    //{
    //    var cookieOptions = new CookieOptions
    //    {
    //        HttpOnly = true,
    //        Expires = expires.ToLocalTime(),
    //        Secure = true,
    //        IsEssential = true,
    //        SameSite = SameSiteMode.None
    //    };

    //    Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    //}
}
