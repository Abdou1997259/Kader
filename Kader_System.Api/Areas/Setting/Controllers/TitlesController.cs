using Kader_System.Api.Helpers;
using Kader_System.Domain.Dtos.Response;
using Kader_System.Domain.DTOs.Request.Auth;
using Kader_System.Domain.DTOs.Request.Setting;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.IServices.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Kader_System.Api.Areas.Setting.Controllers
{
    [Area(Modules.Setting)]
    [ApiExplorerSettings(GroupName = Modules.Setting)]
    [ApiController]
    //[Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]

    public class TitlesController(ITitleService titleService ,IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly ITitleService _titleService = titleService;

        #region Retrieve
        [HttpGet(ApiRoutes.Title.GetAllTitles)]
        [Permission(Helpers.Permission.View, 4)]
        public async Task<IActionResult> GetAllTitles([FromQuery] GetAllFilterrationForTitleRequest model, ITitleService titleService) =>
            Ok(await titleService.GetAllTitlesAsync(requestService.GetRequestHeaderLanguage, model));


        [HttpGet(ApiRoutes.Title.GetTitleById)]
        [Permission(Helpers.Permission.View, 4)]
        public async Task<IActionResult> GetTitleById(int id)
        {
            var response = await titleService.GetTitleByIdAsync(id, requestService.GetRequestHeaderLanguage);
            if (response == null)
                return Ok(response);
            else
                return BadRequest(response);

        }



        #endregion

        #region Insert

        [HttpPost(ApiRoutes.Title.CreateTitle)]
        [Permission(Helpers.Permission.Add, 4)]
        public async Task<IActionResult> CreateTittle([FromBody] CreateTitleRequest model)
        {
            if (ModelState.IsValid)
            {
                var reponse = await titleService.CreateTitleAsync(model);

                if (reponse != null)
                    return Ok(reponse);
                else return BadRequest(reponse);
            }

            // Custom error message for ScreenType validation
            if (ModelState.TryGetValue("ScreenType", out var screenTypeErrors) && screenTypeErrors.Errors.Any())
            {
                var errorMessage = "Invalid ScreenType value. Allowed values are 1, 2, or 3.";
                ModelState.AddModelError("ScreenType", errorMessage);
            }

            var errorMessages = ModelState.Values
                .Where(v => v.Errors.Any())
                .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));


            var errorsResponse = new
            {
                errors = ModelState.ToDictionary
                (
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                )
            };

            // Return only the custom errors response
            return new BadRequestObjectResult(errorsResponse);
        }
        #endregion

        #region Update

        [HttpPut(ApiRoutes.Title.UpdateTitle)]
        [Permission(Helpers.Permission.Edit, 4)]
        public async Task<IActionResult> UpdateTitle(
            [FromRoute] int id, [FromBody] UpdateTitleRequest model
           )
        {
            var respone = await titleService.UpdateTitleAsync(id, model,requestService.GetRequestHeaderLanguage);

            if (respone.Check == true)
                return Ok(respone);
            else
                return BadRequest(respone);
        }

        [HttpPut(ApiRoutes.Title.RestoreTitle)]
        [Permission(Helpers.Permission.Edit, 4)]
        public async Task<IActionResult> RestoreTitle([FromBody] int id)
        {
            if (Response == null)
                return Ok(Response);
            else return BadRequest(Response);
        }
        #endregion



        #region Delete 
        [HttpDelete(ApiRoutes.Title.DeleteTitle)]
        [Permission(Helpers.Permission.Delete, 4)]
        public async Task<IActionResult> DeleteTitle(int id)
        {
            var respone = await titleService.DeleteTitleAsync(id);

            if (respone == null)
                return Ok(respone);
            else return BadRequest(respone);    
        }
        #endregion
    }
}
