using Kader_System.Api.Helpers;
using Kader_System.Domain.Constants.Enums;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.Services.AppServices;
using Newtonsoft.Json;
using Serilog;

namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.HR)]
[ApiExplorerSettings(GroupName = Modules.HR)]
[ApiController]
[Route("api/v1/")]
//[Authorize(Permissions.HR.View)]
public class CompaniesController(ICompanyService service, IRequestService requestService, IFileServer fileServer) : ControllerBase
{
    private readonly IRequestService requestService = requestService;
    private readonly IFileServer _fileServer = fileServer;

    #region Retreive

    [HttpGet(ApiRoutes.Company.ListOfCompanies)]
    [Permission(Permission.View, 8)]
    public async Task<IActionResult> ListOfCompaniesAsync() =>
        Ok(await service.ListOfCompaniesAsync(requestService.GetRequestHeaderLanguage));

    [HttpGet(ApiRoutes.Company.GetAllCompanies)]
    [Permission(Permission.View, 8)]

    public async Task<IActionResult> GetAllCompaniesAsync([FromQuery] HrGetAllFiltrationsForCompaniesRequest model) =>
        Ok(await service.GetAllCompaniesAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost));

    [HttpGet(ApiRoutes.Company.GetCompanyById)]
    [Permission(Permission.View, 8)]
    public async Task<IActionResult> GetCompanyByIdAsync(int id)
    {
        var response = await service.GetCompanyByIdAsync(id, requestService.GetRequestHeaderLanguage);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpGet(ApiRoutes.Company.DownloadCompanyContract)]
    [Permission(Permission.View, 8)]
    public async Task<IActionResult> DownloadCompanyContract(int id)
    {
        var response = await service.DownloadCompanyContract(id);
        if (response.Check)
        {
            if (response.Data is not null)
                return response.Data;
            else
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        }
        else
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
    }
    [HttpGet(ApiRoutes.Company.DownloadCompanyLicense)]
    [Permission(Permission.View, 8)]
    public async Task<IActionResult> DownloadCompanylicense(int id)
    {
        var response = await service.DownloadCompanylicense(id);
        if (response.Check)
        {
            if (response.Data is not null)
                return response.Data;
            else
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        }
        else
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
    }


    [HttpGet(ApiRoutes.Company.EmployeeOfCompany)]
    [Permission(Permission.View, 8)]
    public async Task<IActionResult> EmployeeOfCompany(int companyId, [FromQuery] HrGetAllFiltrationsForCompaniesRequest model)
    {
        var response = await service.EmployeeOfCompany(companyId, requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    #endregion


    #region Insert

    [HttpPost(ApiRoutes.Company.CreateCompany)]
    [Permission(Permission.Add, 8)]
    public async Task<IActionResult> CreateCompanyAsync([FromForm] HrCreateCompanyRequest model)
    {
        var response = await service.CreateCompanyAsync(model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Update

    [HttpPut(ApiRoutes.Company.UpdateCompany)]
    [Permission(Permission.Edit, 8)]
    public async Task<IActionResult> UpdateCompanyAsync([FromRoute] int id, [FromForm] HrUpdateCompanyRequest model)
    {

        Log.Information(JsonConvert.SerializeObject(model));
        var response = await service.UpdateCompanyAsync(id, model);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Restore

    [HttpPut(ApiRoutes.Company.RestoreCompany)]
    [Permission(Permission.Edit, 8)]
    public async Task<IActionResult> RestoreCompanyAsync([FromRoute] int id)
    {
        var response = await service.RestoreCompanyAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }

    #endregion

    #region Delete
    [HttpDelete(ApiRoutes.Company.DeleteCompany)]
    [Permission(Permission.Delete, 8)]
    public async Task<IActionResult> DeleteCompanyAsync(int id)
    {
        var response = await service.DeleteCompanyAsync(id);
        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }
    [HttpDelete(ApiRoutes.Company.DeleteCompanyContractAttachement)]
    [Permission(Permission.Delete, 8)]
    public async Task<IActionResult> DeleteCompanyContractAttachement([FromRoute] int id, [FromQuery] HrDirectoryTypes types)
    {
        var response = new Response<string>();
        if (types == HrDirectoryTypes.CompanyContracts)
        {
            response = await service.RemoveCompanyContractsAttachement(id, HrDirectoryTypes.CompanyContracts);
        }
        else if (types == HrDirectoryTypes.CompanyLicesnses)
        {
            response = await service.RemoveCompanyLicensesAttachement(id, HrDirectoryTypes.CompanyLicesnses);
        }

        if (response.Check)
            return Ok(response);
        else if (!response.Check)
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
    }



    #endregion



}
