using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    //[Authorize(Permissions.HR.View)]
    [Route("api/v1/")]
    public class ContractsController(IContractService contractService, IRequestService requestService,IFileServer fileServer) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IFileServer _fileServer = fileServer;

        [HttpGet(ApiRoutes.Contract.ListOfContracts)]
        [Permission(Permission.View ,17)]
        public async Task<IActionResult> GetListOfContracts() =>
            Ok(await contractService.ListOfContractsAsync(requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.Contract.GetAllContracts)]
        [Permission(Permission.View, 17)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAlFilterationForContractRequest request) =>
            Ok(await contractService.GetAllContractAsync(requestService.GetRequestHeaderLanguage, request, requestService.GetCurrentHost));

        [HttpGet(ApiRoutes.Contract.GetAllEndContracts)]
        [Permission(Permission.View, 17)]
        public async Task<IActionResult> GetAllEndContracts([FromQuery] GetAlFilterationForContractRequest request) =>
            Ok(await contractService.GetAllEndContractsAsync(requestService.GetRequestHeaderLanguage, request, requestService.GetCurrentHost));


        [HttpGet(ApiRoutes.Contract.GetContractById)]
        [Permission(Permission.View, 17)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await contractService.GetContractByIdAsync(id,requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return BadRequest(response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet(ApiRoutes.Contract.GetContractLookups)]
        public async Task<IActionResult> GetLookups()
        {
            var response = await contractService.GetLookUps(requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return BadRequest(response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpGet(ApiRoutes.Contract.GetContractByUser)]
        public async Task<IActionResult> GetContractByUser(int empId)
        {
            var response = await contractService.GetContractByUser(empId,requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return BadRequest(response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpGet(ApiRoutes.Contract.DownloadDocument)]
        public async Task<IActionResult> GetFileStreamResultAsync(int contractId)
        {
            var response = await contractService.GetFileStreamResultAsync(contractId);
            if (response.Check)
            {
                if (response.Data.Length > 0)
                {
                    var contentType = (string)_fileServer.GetContentType(response.DynamicData);
                    return File(response.Data, contentType);
                }
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);

            }
            else
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
        }

        #region Create

        [HttpPost(ApiRoutes.Contract.CreateContract)]
        [Permission(Permission.Add, 17)]
        public async Task<IActionResult> CreateAsync([FromForm] CreateContractRequest request)
        {
            if (ModelState.IsValid)
            {
                var serverPath = HttpContext.Items["ServerPath"]?.ToString();
                var response = await contractService.CreateContractAsync(request, Modules.HR);
                if(response.Check) return Ok(response);
                else if(!response.Check) return BadRequest(response);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
            }
            return  BadRequest(request);
        }


        #endregion

        #region Update

        [HttpPut(ApiRoutes.Contract.UpdateContract)]
        [Permission(Permission.Edit, 17)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id,
            [FromForm] CreateContractRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await contractService.UpdateContractAsync(id, request, Modules.Setting);
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
        [Permission(Permission.Delete, 17)]
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


    }
}
