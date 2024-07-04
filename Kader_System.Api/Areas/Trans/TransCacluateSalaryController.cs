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

        [HttpPost(ApiRoutes.TransSalaryCalculatorEndpoint.Calculate)]
        public async Task<IActionResult> Calculate([FromBody] CalcluateSalaryModelRequest model)
        {
            return Ok(await service.CalculateSalaryDetailedTrans(model));
        }
        [HttpPost(ApiRoutes.TransSalaryCalculatorEndpoint.DetailedCalculations)]
        public async Task<IActionResult> DetailedCalculation([FromBody] CalcluateEmpolyeeFilters model)
        {
            return Ok(await service.GetDetailsOfCalculation(model, GetCurrentRequestLanguage()));
        }
        private string GetCurrentRequestLanguage() =>
         Request.Headers.AcceptLanguage.ToString().Split(',').First();
        private string GetCurrentHost() =>
            HttpContext.Request.Host.Value +
            HttpContext.Request.Path.Value;
    }
}
