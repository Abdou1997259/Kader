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


        //[HttpGet(ApiRoutes.Job.GetJobById)]
        //[Permission(Permission.View, 9)]
        //public async Task<IActionResult> GetJobById(int id)
        //{
        //    var response = await jobService.GetJobByIdAsync(id);
        //    if (response.Check)
        //        return Ok(response);
        //    else
        //        return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);

        //}

        //[HttpGet(ApiRoutes.Job.GetAllJobs)]
        //[Permission(Permission.View, 9)]
        //public async Task<IActionResult> GetAll([FromQuery] HrGetAllFilterationForJobRequest model)
        //{

        //    return Ok(await jobService.GetAllJobsAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost));
        //}
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
    }
}
