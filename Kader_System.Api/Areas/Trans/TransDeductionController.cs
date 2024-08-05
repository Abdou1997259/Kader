using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.IServices.Trans;
using Kader_System.Services.Services.HR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    //[Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransDeductionController(ITransDeductionService service , IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        [HttpGet(ApiRoutes.TransDeduction.ListOfTransDeductions)]
        [Permission(Helpers.Permission.View, 21)]
        public async Task<IActionResult> ListOfTransDeductions() =>
            Ok(await service.ListOfTransDeductionsAsync(requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.TransDeduction.GetTransDeductions)]
        [Permission(Helpers.Permission.View, 21)]
        public async Task<IActionResult> GetAllTransDeductions([FromQuery] GetAllFilterationForTransDeductionRequest request) =>
            Ok(await service.GetAllTransDeductionsAsync(requestService.GetRequestHeaderLanguage, request, requestService.GetRequestHeaderLanguage));
        [HttpGet(ApiRoutes.TransDeduction.GetTransDeductionById)]
        [Permission(Helpers.Permission.View, 21)]
        public async Task<IActionResult> GetTransDeductionById(int id)
        {
            var response =await service.GetTransDeductionByIdAsync(id,requestService.GetRequestHeaderLanguage);

            var lookUps = await service.GetDeductionsLookUpsData(requestService.GetRequestHeaderLanguage);


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
        [Permission(Helpers.Permission.View, 21)]
        public async Task<IActionResult> GetLookUpsAsync()
        {
            var response =await service.GetDeductionsLookUpsData(requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else 
                return BadRequest(response);
        }

        [HttpPost(ApiRoutes.TransDeduction.CreateTransDeduction)]

        [Permission(Helpers.Permission.Add, 21)]
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
        [Permission(Helpers.Permission.Edit, 21)]
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
        [Permission(Helpers.Permission.Edit, 21)]
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
        [Permission(Helpers.Permission.Delete, 21)]
        public async Task<IActionResult> DeleteTransDeduction([FromRoute] int id)
        {

            var response = await service.DeleteTransDeductionAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        
    }
}
