using Kader_System.Api.Helpers;
using Kader_System.Services.IServices;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    //[Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransSalaryIncrease(ITransSalaryIncreaseService service, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly ITransSalaryIncreaseService _service = service;


        #region Create
        [HttpPost(ApiRoutes.SalaryIncrease.CreateSalaryIncrease)]
        [Permission(Helpers.Permission.Add, 28)]
        public async Task<IActionResult> CreateTransVacation(CreateTransSalaryIncreaseRequest request)
        {
            var result = await _service.CreateTransSalaryIncreaseAsync(request, requestService.GetRequestHeaderLanguage);
            return Ok(result);
        }
        #endregion

        #region Read
        [HttpGet(ApiRoutes.SalaryIncrease.GetAllSalaryIncrease)]
        [Permission(Helpers.Permission.View, 28)]
        public async Task<IActionResult> GetAllSalaryIncrease([FromQuery] GetAlFilterationForSalaryIncreaseRequest model)
        {
            var result = await _service.GetAllTransSalaryIncreaseAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost);
            return Ok(result);
        }
        [HttpGet(ApiRoutes.SalaryIncrease.GetSalaryIncreaseById)]
        [Permission(Helpers.Permission.View, 28)]
        public async Task<IActionResult> GetSalaryIncreaseById(int id)
        {
            var result = await _service.GetTransSalaryIncreaseByIdAsync(id, requestService.GetRequestHeaderLanguage);
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut(ApiRoutes.SalaryIncrease.UpdateSalaryIncrease)]
        [Permission(Helpers.Permission.Edit, 28)]
        public async Task<IActionResult> UpdateSalaryIncrease(int id, CreateTransSalaryIncreaseRequest model)
        {
            var result = await _service.UpdateTransSalaryIncreaseAsync(id, model);
            return Ok(result);
        }
        [HttpPut(ApiRoutes.SalaryIncrease.RestoreSalaryIncrease)]
        [Permission(Helpers.Permission.Edit, 28)]
        public async Task<IActionResult> RestoreSalaryIncrease(int id)
        {
            var result = await _service.RestoreTransSalaryIncreaseAsync(id);
            return Ok(result);
        }
        [HttpGet(ApiRoutes.SalaryIncrease.GetEmployeesLookups)]
        public async Task<IActionResult> GetEmployeesLookups()
        {
            var result = await _service.GetEmployeesLookups(requestService.GetRequestHeaderLanguage);
            return Ok(result);
        }
        #endregion

        #region Delete
        [HttpDelete(ApiRoutes.SalaryIncrease.DeleteSalaryIncrease)]
        [Permission(Helpers.Permission.Delete, 28)]
        public async Task<IActionResult> DeleteSalaryIncrease(int id)
        {
            var result = await _service.DeleteTransSalaryIncreaseAsync(id);
            return Ok(result);
        }
        #endregion



    }
}
