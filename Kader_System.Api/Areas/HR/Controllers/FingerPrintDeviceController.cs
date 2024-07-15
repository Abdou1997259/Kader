using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    [Authorize(Permissions.HR.View)]
    [Route("api/v1/")]
    public class FingerPrintDeviceController(IFingerPrintDeviceService fingerPrintDeviceService,ICompanyService companyService, Services.IServices.HTTP.ITitleService requestService) : ControllerBase
    {
        private readonly Services.IServices.HTTP.ITitleService requestService = requestService;

        #region Get Methods

        [HttpGet(ApiRoutes.FingerPrint.ListOfFingerPrintDevices)]
        public async Task<IActionResult> ListOfDevices()
            => Ok(await fingerPrintDeviceService.GetFingerPrintDevicesAsync(requestService.GetRequestHeaderLanguage));
        [HttpGet(ApiRoutes.FingerPrint.GetAllFingerPrintDevices)]
        public async Task<IActionResult> ListOfDevices([FromQuery]GetAllFingerPrintDevicesFilterrationRequest request)
            => Ok(await fingerPrintDeviceService.GetAllFingerPrintDevicesAsync(requestService.GetRequestHeaderLanguage, request, requestService.GetCurrentHost));
        [HttpGet(ApiRoutes.FingerPrint.GetLookups)]
        public async Task<IActionResult> GetLookUps()
            => Ok(await fingerPrintDeviceService.GetFingerPrintsLookUpsData(requestService.GetRequestHeaderLanguage));
        [HttpGet(ApiRoutes.FingerPrint.GetFingerPrintDeviceById)]
        public async Task<IActionResult> GetFingerPrintDeviceById(int id)
        {
            var response = await fingerPrintDeviceService.GetFingerPrintDeviceByIdAsync(id);
            if (response.Check)
            {
                var lookups = await fingerPrintDeviceService.GetFingerPrintsLookUpsData(requestService.GetRequestHeaderLanguage); // add comment 
                response.LookUps = lookups.Data;
                return Ok(response);
            }
               
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Post Method

        [HttpPost(ApiRoutes.FingerPrint.CreateFingerPrintDevice)]
        public async Task<IActionResult> CreateDevice([FromBody] CreateFingerPrintDeviceRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await fingerPrintDeviceService.CreateFingerPrintDevicesAsync(request);
                if (response.Check)
                    return Ok(response);
                else if (!response.Check)
                    return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
            }
            else
            {
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, ModelState);
            }
        }


        #endregion

        #region Put Methods

        [HttpPut(ApiRoutes.FingerPrint.UpdateFingerPrintDevice)]
        public async Task<IActionResult> UpdateDeviceTask([FromRoute] int id,
            [FromBody] CreateFingerPrintDeviceRequest request)
        {
            var response = await fingerPrintDeviceService.UpdateFingerPrintDevicesAsync(id, request);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpPut(ApiRoutes.FingerPrint.RestoreFingerPrint)]
        public async Task<IActionResult> RestoreFingerPrint([FromRoute] int id)
        {
            var response = await fingerPrintDeviceService.RestoreFingerPrintAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion


        #region Delete Methods

        [HttpDelete(ApiRoutes.FingerPrint.DeleteFingerPrintDevice)]
        public async Task<IActionResult> DeleteDevice([FromRoute]int id)
        {
            var response = await fingerPrintDeviceService.DeleteFingerPrintAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        

        #endregion


    }
}
