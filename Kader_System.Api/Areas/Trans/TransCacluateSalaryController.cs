using Kader_System.Services.IServices.Trans;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    [Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransCacluateSalaryController(ITransCalcluateSalaryService service) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> getDetailsSalary([FromBody] CalcluateSalaryModelRequest model)
        {
            return Ok(await service.CalculateSalary(model));
        }
    }
}
