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
        [HttpGet(ApiRoutes.TransSalaryCalculatorEndpoint.GetLookUps)]
        [ProducesResponseType(typeof(Response<GetLookupsCalculatedSalaries>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLookUps()
        {
            var result = await service.GetLookups(GetCurrentRequestLanguage());
            if (result == null)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet(ApiRoutes.TransSalaryCalculatorEndpoint.GetTransCalculator)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await service.GetAllCalculators();

            if (result == null)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpDelete(ApiRoutes.TransSalaryCalculatorEndpoint.DeleteCalculator)]
        public async Task<IActionResult> DeleteCalcluator(int Id)
        {
            var result = await service.DeleteCalculator(Id);

            if (result == null)
                return BadRequest(result);
            return Ok(result);
        }
        private string GetCurrentRequestLanguage() =>
         Request.Headers.AcceptLanguage.ToString().Split(',').First();
        private string GetCurrentHost() =>
            HttpContext.Request.Host.Value +
            HttpContext.Request.Path.Value;
    }
}
