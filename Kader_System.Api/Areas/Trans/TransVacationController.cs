﻿using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.IServices.Trans;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    //[Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransVacationController(ITransVacationService service, IRequestService requestService) : ControllerBase
    {
        #region Retrieve

        private readonly IRequestService requestService = requestService;
        [HttpGet(ApiRoutes.TransVacation.ListOfTransVacations)]

        [Permission(Helpers.Permission.View, 25)]
        public async Task<IActionResult> ListOfTransVacations() =>
            Ok(await service.ListOfTransVacationsAsync(requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.TransVacation.GetTransVacations)]
        [Permission(Helpers.Permission.View, 25)]
        public async Task<IActionResult> GetAllTransVacations([FromQuery] GetAllFilterationForTransVacationRequest request) =>
            Ok(await service.GetAllTransVacationsAsync(requestService.GetRequestHeaderLanguage, request, requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.TransVacation.GetTransVacationsLookUps)]
        [Permission(Helpers.Permission.View, 25)]
        public async Task<IActionResult> GetTransVacationLookUpsData() =>
            Ok(await service.GetTransVacationLookUpsData(requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.TransVacation.GetTransVacationById)]
        [Permission(Helpers.Permission.View, 25)]
        public async Task<IActionResult> GetTransVacationById([FromRoute] int id)
        {
            var response = await service.GetTransVacationByIdAsync(id, requestService.GetRequestHeaderLanguage);
            var lookUps = await service.GetTransVacationLookUpsData(requestService.GetRequestHeaderLanguage);
            if (response.Check)
            {
                response.LookUps = lookUps.Data;
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        #endregion

        #region Create

        [HttpPost(ApiRoutes.TransVacation.CreateTransVacation)]
        [Permission(Helpers.Permission.Add, 25)]
        public async Task<IActionResult> CreateTransVacation([FromForm] CreateTransVacationRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await service.CreateTransVacationAsync(request, requestService.GetRequestHeaderLanguage);
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
        [HttpPut(ApiRoutes.TransVacation.UpdateTransVacation)]
        [Permission(Helpers.Permission.Edit, 25)]
        public async Task<IActionResult> UpdateTransVacation([FromRoute] int id, [FromForm] CreateTransVacationRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await service.UpdateTransVacationAsync(id, request, requestService.GetRequestHeaderLanguage);
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

        [HttpPut(ApiRoutes.TransVacation.RestoreTransVacation)]
        [Permission(Helpers.Permission.Edit, 25)]
        public async Task<IActionResult> RestoreTransVacation([FromRoute] int id)
        {

            var response = await service.RestoreTransVacationAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.TransVacation.DeleteTransVacation)]
        [Permission(Helpers.Permission.Delete, 25)]
        public async Task<IActionResult> DeleteTransVacation(int id)
        {
            var response = await service.DeleteTransVacationAsync(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion


    }
}
