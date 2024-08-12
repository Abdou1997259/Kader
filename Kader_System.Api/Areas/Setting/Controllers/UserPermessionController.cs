using Kader_System.Services.AppServices;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Setting.Controllers
{
    [Area(Modules.Setting)]
    [ApiExplorerSettings(GroupName = Modules.Setting)]
    [ApiController]
    //[Authorize(Permissions.Setting.View)]
    [Route("api/v1/")]
    [DeflateCompression]
    public class UserPermessionController(IUserPermessionService userPermession, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly IUserPermessionService _userPermession = userPermession;

        #region Retrieve
        [HttpGet(ApiRoutes.UserPermession.GetUserPermissionsBySubScreen)]
        [DeflateCompression]
        public async Task<IActionResult> GetUserPermissionsBySubScreen([FromRoute] string userId,[FromRoute]int titleId)
        {
            var result = await _userPermession.GetUserPermissionsBySubScreen(titleId, userId, requestService.GetRequestHeaderLanguage);
           return Ok(result);
        }

        #endregion
    }
}
