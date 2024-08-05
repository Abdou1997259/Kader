using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.IServices.Trans;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    //[Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransBenefitsController(ITransBenefitService service, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        #region Get

        [HttpGet(ApiRoutes.TransBenefit.ListOfTransBenefits)]
        [Permission(Helpers.Permission.View, 22)]
        public async Task<IActionResult> ListOfTransBenefits() =>
            Ok(await service.ListOfTransBenefitsAsync(GetCurrentRequestLanguage()));

        [HttpGet(ApiRoutes.TransBenefit.GetTransBenefits)]
        [Permission(Helpers.Permission.View, 22)]
        public async Task<IActionResult> GetAllTransBenefits([FromQuery] GetAllFilterationForTransBenefitRequest request) =>
            Ok(await service.GetAllTransBenefitsAsync(GetCurrentRequestLanguage(), request, GetCurrentHost()));
        [HttpGet(ApiRoutes.TransBenefit.GetTransBenefitById)]
        [Permission(Helpers.Permission.View, 22)]
        public async Task<IActionResult> GetTransDeductionById(int id)
        {
            var response = await service.GetTransBenefitByIdAsync(id, GetCurrentRequestLanguage());

            var lookUps = await service.GetBenefitsLookUpsData(GetCurrentRequestLanguage());


            if (response.Check)
            {
                response.LookUps = lookUps.Data;
                return Ok(response);
            }

            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);


            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet(ApiRoutes.TransBenefit.GetLookUps)]
        [Permission(Helpers.Permission.View, 22)]
        public async Task<IActionResult> GetLookUpsAsync()
        {
            var response = await service.GetBenefitsLookUpsData(GetCurrentRequestLanguage());
            if (response.Check)
                return Ok(response);
            else
                return BadRequest(response);
        }

        #endregion


        #region Create


        [HttpPost(ApiRoutes.TransBenefit.CreateTransBenefit)]
        [Permission(Helpers.Permission.Add, 22)]

        public async Task<IActionResult> CreateTransBenefit([FromBody] CreateTransBenefitRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await service.CreateTransBenefitAsync(request);
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

        #endregion

        #region Update

        [HttpPut(ApiRoutes.TransBenefit.UpdateTransBenefit)]
        [Permission(Helpers.Permission.Edit, 22)]
        public async Task<IActionResult> UpdateTransBenefit([FromRoute] int id, [FromBody] CreateTransBenefitRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await service.UpdateTransBenefitAsync(id, request);
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

        [HttpPut(ApiRoutes.TransBenefit.RestoreTransBenefit)]
        [Permission(Helpers.Permission.Edit, 22)]
        public async Task<IActionResult> RestoreTransBenefit([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var response = await service.RestoreTransBenefitAsync(id);
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

        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.TransBenefit.DeleteTransBenefit)]
        [Permission(Helpers.Permission.Delete, 22)]
        public async Task<IActionResult> DeleteTransBenefit(int id)
        {
            var response = await service.DeleteTransBenefitAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
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
