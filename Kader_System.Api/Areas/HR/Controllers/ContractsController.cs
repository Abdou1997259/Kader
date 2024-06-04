namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    [Authorize(Permissions.HR.View)]
    [Route("api/v1/")]
    public class ContractsController(IContractService contractService) : ControllerBase
    {
        [HttpGet(ApiRoutes.Contract.ListOfContracts)]
        public async Task<IActionResult> GetListOfContracts() =>
            Ok(await contractService.ListOfContractsAsync(GetCurrentRequestLanguage()));

        [HttpGet(ApiRoutes.Contract.GetAllContracts)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAlFilterationForContractRequest request) =>
            Ok(await contractService.GetAllContractAsync(GetCurrentRequestLanguage(), request, GetCurrentHost()));

        [HttpGet(ApiRoutes.Contract.GetAllEndContracts)]
        public async Task<IActionResult> GetAllEndContracts([FromQuery] GetAlFilterationForContractRequest request) =>
            Ok(await contractService.GetAllEndContractsAsync(GetCurrentRequestLanguage(), request, GetCurrentHost()));


        [HttpGet(ApiRoutes.Contract.GetContractById)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await contractService.GetContractByIdAsync(id,GetCurrentRequestLanguage());
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return BadRequest(response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet(ApiRoutes.Contract.GetContractLookups)]
        public async Task<IActionResult> GetLookups()
        {
            var response = await contractService.GetLookUps(GetCurrentRequestLanguage());
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return BadRequest(response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #region Create

        [HttpPost(ApiRoutes.Contract.CreateContract)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateContractRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await contractService.CreateContractAsync(request);
                if(response.Check) return Ok(response);
                else if(!response.Check) return BadRequest(response);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
            }
            return  BadRequest(request);
        }


        #endregion

        #region Update

        [HttpPut(ApiRoutes.Contract.UpdateContract)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id,
            [FromBody] CreateContractRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await contractService.UpdateContractAsync(id, request);
                if (response.Check) return Ok(response);
                else if (!response.Check) return BadRequest(response);

                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
            }
            return BadRequest(request);
        }
        [HttpPut(ApiRoutes.Contract.RestoreContract)]
        public async Task<IActionResult> RestoreAsync([FromRoute] int id)
        {
          
                var response = await contractService.RestoreContractAsync(id);
                if (response.Check) return Ok(response);
                else if (!response.Check) return BadRequest(response);

                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.Contract.DeleteContract)]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            var response= await contractService.DeleteContractAsync(id);
            if (response.Check) return Ok(response);
            else
            {
                return BadRequest(response);
            }
        }

        #endregion

        #region Helpers

        private string GetCurrentRequestLanguage() =>
            Request.Headers.AcceptLanguage.ToString().Split(',').First();
        private string GetCurrentHost() =>
            HttpContext.Request.Host.Value +
            HttpContext.Request.Path.Value;
        #endregion
    }
}
