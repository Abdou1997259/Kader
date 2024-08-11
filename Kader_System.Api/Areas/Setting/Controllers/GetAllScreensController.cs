using Kader_System.Services.IServices.HTTP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kader_System.Api.Areas.Setting.Controllers
{
    [Area(Modules.Setting)]
    [ApiExplorerSettings(GroupName = Modules.Setting)]
    [ApiController]
    [Route("api/v1/")]

    public class GetAllScreensController(IGetAllScreensService getAllScreensService , IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService _requestService = requestService;
        private readonly IGetAllScreensService _getallScreensService = getAllScreensService;

        

        [HttpGet(ApiRoutes.GetAllScreens.SpGetAllScreens)]

        public async Task<IActionResult> GetAllScreens() =>
            Ok(await _getallScreensService.GetAllScreens(requestService.GetRequestHeaderLanguage));

    }
}
