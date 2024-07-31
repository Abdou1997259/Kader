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
    public class PermessionStructController(IPermessionStructureService permessionStructure, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IPermessionStructureService _permessionStructure = permessionStructure;

        #region Retrieve
        [HttpGet(ApiRoutes.PermessionStruct.GetAllPermessions)]
        public async Task<IActionResult> GetAllPermessions() =>
            Ok(await _permessionStructure.GetAllPermessionStructure(requestService.GetRequestHeaderLanguage));



        #endregion
    }
}
