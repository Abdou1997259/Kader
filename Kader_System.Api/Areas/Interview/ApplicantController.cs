using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.IServices.InterviewServices;

namespace Kader_System.Api.Areas.Interview
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.Interview)]
    [Route("api/v1/")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantServices _service;
        private readonly IRequestService _requestService;
        public ApplicantController(IApplicantServices service, IRequestService requestService)
        {
            _service = service;
            _requestService = requestService;

        }
        #region Retreive

        [HttpGet(ApiRoutes.ApplicantRoute.ListOfApplicant)]

        public async Task<IActionResult> ListOfAsync()
            => Ok(await _service.ListOfAsync(_requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.ApplicantRoute.GetAllApplicants)]

        public async Task<IActionResult> GetPaginatedApplicants(
            [FromQuery] GetApplicantsFilterationRequest model)
        {
            var response = await _service.GetPaginatedApplicants(model,
                _requestService.GetRequestHeaderLanguage, _requestService.GetCurrentHost);

            return Ok(response);

        }
        [HttpGet(ApiRoutes.ApplicantRoute.GetDetailedApplicant)]

        public async Task<IActionResult> GetDetails([FromRoute] int id)
        {
            var response = await _service.GetDetails(id,
                _requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet(ApiRoutes.ApplicantRoute.GetApplicantById)]

        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id, _requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }



        #endregion


        #region Insert

        [HttpPost(ApiRoutes.ApplicantRoute.CreateApplicant)]
        //[Permission(Permission.Add, 9)]
        public async Task<IActionResult> CreateAsync([FromForm] CreateApplicantRequest model)
        {
            var response = await _service.CreateAsync(model, Modules.Interview, _requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion


        #region Update
        [HttpPut(ApiRoutes.ApplicantRoute.UpdateApplicant)]

        public async Task<IActionResult> UpdateAsync([FromForm] CreateApplicantRequest model, [FromRoute] int id)
        {
            var response = await _service.UpdateAsync(id, model, _requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.ApplicantRoute.Reject)]

        public async Task<IActionResult> Reject([FromRoute] int id)
        {
            var response = await _service.Reject(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.ApplicantRoute.RateMe)]

        public async Task<IActionResult> RateMe([FromRoute] int id, [FromRoute] float rate)
        {
            var response = await _service.RateMe(id, rate);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.ApplicantRoute.Accept)]

        public async Task<IActionResult> Accept([FromRoute] int id, [FromForm] AcceptApplicantRequest model)
        {
            var response = await _service.Accept(id, model);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.ApplicantRoute.RestoreApplicant)]

        public async Task<IActionResult> RestoreAsync([FromRoute] int id)
        {
            var response = await _service.RestoreAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion
        #region Delete
        [HttpDelete(ApiRoutes.ApplicantRoute.DeleteApplicant)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var response = await _service.DeleteAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);

        }
        #endregion
    }
}
