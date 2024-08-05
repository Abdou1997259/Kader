using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [Route("api/v1/")]
    [ApiController]
    //[Authorize(Permissions.HR.View)]
    public class JobController(IHrJobService jobService, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;

        #region Retreive

        [HttpGet(ApiRoutes.Job.ListOfJobs)]
        [Permission(Permission.View, 9)]
        public async Task<IActionResult> GetAllJobs()
            => Ok(await jobService.ListOfJobsAsync(requestService.GetRequestHeaderLanguage));


        [HttpGet(ApiRoutes.Job.GetJobById)]
        [Permission(Permission.View, 9)]
        public async Task<IActionResult> GetJobById(int id)
        {
            var response = await jobService.GetJobByIdAsync(id);
            if (response.Check)
                return Ok(response);
            else
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);

        }

        [HttpGet(ApiRoutes.Job.GetAllJobs)]
        [Permission(Permission.View, 9)]
        public async Task<IActionResult> GetAll([FromQuery] HrGetAllFilterationForJobRequest model)
        {
           
            return Ok(await jobService.GetAllJobsAsync(requestService.GetRequestHeaderLanguage, model,requestService.GetCurrentHost));
        }
        #endregion

        #region Insert

        [HttpPost(ApiRoutes.Job.CreateJob)]
        [Permission(Permission.Add, 9)]
        public async Task<IActionResult> CreateJob(HrCreateJobRequest model)
        {
            var response = await jobService.CreateJobAsync(model);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Update

        [HttpPut(ApiRoutes.Job.UpdateJob)]
        [Permission(Permission.Edit, 9)]
        public async Task<IActionResult> UpdateJob([FromRoute] int id, HrUpdateJobRequest model)
        {
            var response =await jobService.UpdateJobAsync(id, model);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return BadRequest(response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }


        [HttpPut(ApiRoutes.Job.RestoreJob)]
        [Permission(Permission.Edit, 9)]
        public async Task<IActionResult> RestoreJob([FromRoute] int id)
        {
            var response = await jobService.RestoreJobAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return BadRequest(response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.Job.DeleteJob)]
        [Permission(Permission.Delete, 9)]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var response=await jobService.DeleteJobAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return BadRequest(response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

    }
}
