

using Kader_System.Api.Helpers;
using Kader_System.Domain.DTOs.Request.HR.Vacation;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    //[Authorize(Permissions.HR.View)]
    [Route("api/v1/")]
    public class EmployeeNotesController(IEmployeeNotesServices service, IRequestService requestService): ControllerBase
    {

        private readonly IRequestService requestService = requestService;
        private readonly IEmployeeNotesServices _service = service;


        #region Retrieve

        [HttpGet(ApiRoutes.EmployeeNotes.GetAllEmployeeNotes)]
        [Permission(Permission.View, 12)]
        public async Task<IActionResult> GetAllEmployeeNotes([FromQuery] GetAllEmployeeNotesRequest model) =>
            Ok(await _service.GetAllEmployeeNotesAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost));


        [HttpGet(ApiRoutes.EmployeeNotes.GetEmployeeNotesById)]
        [Permission(Permission.View, 12)]
        public async Task<IActionResult> GetEmployeeNotesById(int id)
        {
            var response = await _service.GetEmployeeNotesByIdAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Insert

        [HttpPost(ApiRoutes.EmployeeNotes.CreateEmployeeNotes)]
        [Permission(Permission.Add, 12)]
        public async Task<IActionResult> CreateEmployeeNotes(CreateEmployeeNotes model)
        {
            var response = await _service.CreateEmployeeNotesAsync(model);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion




        #region Delete
        [HttpDelete(ApiRoutes.EmployeeNotes.DeleteEmployeeNotes)]
        [Permission(Permission.Delete, 12)]
        public async Task<IActionResult> DeleteVacationAsync(int id)
        {
            var response = await _service.DeleteEmployeeNotesAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

    }
}
