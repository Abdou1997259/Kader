using Kader_System.DataAccesss.DbContext;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    [Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransCacluateSalaryController(KaderDbContext context) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> getDetailsSalary(int empId, DateOnly date)
        {
            return Ok(await context.SpCacluateSalaries(date, empId));
        }
    }
}
