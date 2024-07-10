using Kader_System.Domain.Interfaces.HR;

namespace Kader_System.Api.Areas.Setting.Controllers;

[Area(Modules.HR)]
[Authorize(Permissions.HR.View)]
[ApiExplorerSettings(GroupName = Modules.HR)]
[ApiController]
[Route("api/v1/")]
public class SalaryIncreaseTypeController(ISalaryIncreaseTypeRepository service) : ControllerBase
{
    #region Retreieve
    [HttpGet(ApiRoutes.SalaryIncreaseType.GetAllSalaryIncreaseTypes)]
    public async Task<IActionResult> GetAllSalaryIncreaseTypes() =>
        Ok(await service.GetAllSalaryIncreaseTypes());

    [HttpGet(ApiRoutes.SalaryIncreaseType.GetSalaryIncreaseTypesById)]
    public async Task<IActionResult> GetSalaryIncreaseTypesById(int id)
    {
        var response = await service.GetSalaryIncreaseTypesById(id);
        return Ok(response);
    }

    #endregion

    #region Insert
    [HttpPost(ApiRoutes.SalaryIncreaseType.CreateSalaryIncreaseTypes)]
    public async Task<IActionResult> CreateSalaryIncreaseTypes(HrCreateSalaryIncreaseTypesRequest model)
    {

        var response = await service.AddSalaryIncreaseType(model);
        if (response > 0)
            return Ok(response);
        else
            return BadRequest("cannot add ");
    }



    #endregion



}
