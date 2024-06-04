using Kader_System.Services.IServices.Trans;
using Kader_System.Services.Services.HR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    [Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransDeductionController(ITransDeductionService service) : ControllerBase
    {
        [HttpGet(ApiRoutes.TransDeduction.ListOfTransDeductions)]
        public async Task<IActionResult> ListOfTransDeductions() =>
            Ok(await service.ListOfTransDeductionsAsync(GetCurrentRequestLanguage()));

        [HttpGet(ApiRoutes.TransDeduction.GetTransDeductions)]
        public async Task<IActionResult> GetAllTransDeductions([FromQuery] GetAllFilterationForTransDeductionRequest request) =>
            Ok(await service.GetAllTransDeductionsAsync(GetCurrentRequestLanguage(),request, GetCurrentHost()));
        [HttpGet(ApiRoutes.TransDeduction.GetTransDeductionById)]
        public async Task<IActionResult> GetTransDeductionById(int id)
        {
            var response =await service.GetTransDeductionByIdAsync(id,GetCurrentRequestLanguage());

            var lookUps = await service.GetDeductionsLookUpsData(GetCurrentRequestLanguage());


            if (response.Check)
            {
                response.LookUps = lookUps.Data;
                return Ok(response);
            }
                
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            
               
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet(ApiRoutes.TransDeduction.GetLookUps)]
        public async Task<IActionResult> GetLookUpsAsync()
        {
            var response =await service.GetDeductionsLookUpsData(GetCurrentRequestLanguage());
            if (response.Check)
                return Ok(response);
            else 
                return BadRequest(response);
        }

        [HttpPost(ApiRoutes.TransDeduction.CreateTransDeduction)]
        public async Task<IActionResult> CreateTransDeduction([FromBody] CreateTransDeductionRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await service.CreateTransDeductionAsync(request);
                if (response.Check)
                    return Ok(response);
                else if (!response.Check)
                    return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut(ApiRoutes.TransDeduction.UpdateTransDeduction)]
        public async Task<IActionResult> UpdateTransDeduction([FromRoute] int id, [FromBody] CreateTransDeductionRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await service.UpdateTransDeductionAsync(id,request);
                if (response.Check)
                    return Ok(response);
                else if (!response.Check)
                    return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
                return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut(ApiRoutes.TransDeduction.RestoreTransDeduction)]
        public async Task<IActionResult> RestoreTransDeduction([FromRoute] int id)
        {

            var response = await service.RestoreTransDeductionAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpDelete(ApiRoutes.TransDeduction.DeleteTransDeduction)]
        public async Task<IActionResult> DeleteTransDeduction([FromRoute] int id)
        {

            var response = await service.DeleteTransDeductionAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #region Helpers
        private string GetCurrentRequestLanguage() =>
            Request.Headers.AcceptLanguage.ToString().Split(',').First();
        private string GetCurrentHost() =>
            HttpContext.Request.Host.Value +
            HttpContext.Request.Path.Value;

        #endregion
    }
}
