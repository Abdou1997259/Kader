namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [Route("api/v1/")]
    [ApiController]
    [Authorize(Permissions.HR.View)]
    public class JobController(IHrJobService jobService) : ControllerBase
    {
        #region Retreive

        [HttpGet(ApiRoutes.Job.ListOfJobs)]
        public async Task<IActionResult> GetAllJobs()
            => Ok(await jobService.ListOfJobsAsync(GetCurrentRequestLanguage()));


        [HttpGet(ApiRoutes.Job.GetJobById)]
        public async Task<IActionResult> GetJobById(int id)
        {
            var response = await jobService.GetJobByIdAsync(id);
            if (response.Check)
                return Ok(response);
            else
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);

        }

        [HttpGet(ApiRoutes.Job.GetAllJobs)]
        public async Task<IActionResult> GetAll([FromQuery] HrGetAllFilterationForJobRequest model)
        {
           
            return Ok(await jobService.GetAllJobsAsync(GetCurrentRequestLanguage(), model,GetCurrentHost()));
        }
        #endregion

        #region Insert

        [HttpPost(ApiRoutes.Job.CreateJob)]
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

        #region Helpers

        private string GetCurrentRequestLanguage() =>
            Request.Headers.AcceptLanguage.ToString().Split(',').First();

        private string GetCurrentHost() =>
            HttpContext.Request.Host.Value+
            HttpContext.Request.Path.Value;

        #endregion

    }
}
