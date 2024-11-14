using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.IServices.InterviewServices;

namespace Kader_System.Api.Areas.Interview
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.Interview)]
    [Route("api/v1/")]
    [ApiController]
    public class InterJobController : ControllerBase
    {
        private readonly IInterJobServices _service;
        private readonly IRequestService _requestService;
        public InterJobController(IInterJobServices service, IRequestService requestService)
        {
            _service = service;
            _requestService = requestService;

        }
        #region Retreive

        [HttpGet(ApiRoutes.InterJobRoute.GetAll)]

        public async Task<IActionResult> GetPaginatedJobs([FromQuery] GetAllFilteredJobRequests model)
        {
            var response = await _service.GetPaginatedJobs(model, _requestService.GetRequestHeaderLanguage, _requestService.GetCurrentHost);

            return Ok(response);

        }

        [HttpGet(ApiRoutes.InterJobRoute.GetInterJobById)]

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

        [HttpPost(ApiRoutes.InterJobRoute.CreateInterJob)]
        //[Permission(Permission.Add, 9)]
        public async Task<IActionResult> CreateAsync([FromForm] CreateInterJobRequest model)
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
        [HttpPut(ApiRoutes.InterJobRoute.UpdateInterJob)]

        public async Task<IActionResult> UpdateAsync([FromForm] CreateInterJobRequest model, [FromRoute] int id)
        {
            var response = await _service.UpdateAsync(id, model, _requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.InterJobRoute.ReplayJob)]

        public async Task<IActionResult> ReplayJob([FromForm] ReplayJobRequest model, [FromRoute] int id)
        {
            var response = await _service.ReplayJob(id, model);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.InterJobRoute.RestoreInterJob)]

        public async Task<IActionResult> RestoreAsync([FromRoute] int id)
        {
            var response = await _service.RestoreAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        [HttpPut(ApiRoutes.InterJobRoute.SuspendedJob)]

        public async Task<IActionResult> SuspendJob([FromRoute] int id)
        {
            var response = await _service.SuspendJob(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.InterJobRoute.ResumeJob)]

        public async Task<IActionResult> ResumeJob([FromRoute] int id)
        {
            var response = await _service.ResumeJob(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.InterJobRoute.FinishJob)]

        public async Task<IActionResult> FinishJob([FromRoute] int id)
        {
            var response = await _service.FinishJob(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion
        #region Delete
        [HttpDelete(ApiRoutes.InterJobRoute.DeleteInterJob)]
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



        #region Lookups
        [HttpGet(ApiRoutes.InterJobRoute.GetUniversitiesAndFaculties)]

        public async Task<IActionResult> GetUniversitiesAndFaculties() =>

           Ok(await _service.GetUniversitiesAndFacultiesLookups(_requestService.GetRequestHeaderLanguage));

        #endregion
    }
}
