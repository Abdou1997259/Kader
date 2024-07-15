using AutoMapper;
using Kader_System.Services.IServices;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    [Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransSalaryIncrease(ITransSalaryIncreaseService service, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;
        private readonly ITransSalaryIncreaseService _service = service;


        #region Create
        [HttpPost(ApiRoutes.SalaryIncrease.CreateSalaryIncrease)]
        public async Task<IActionResult> CreateTransVacation([FromBody] CreateTransSalaryIncreaseRequest request)
        {
            var result = await _service.CreateTransSalaryIncreaseAsync(request, requestService.GetRequestHeaderLanguage);
            return Ok(result);
        }
        #endregion

        #region Read
        [HttpGet(ApiRoutes.SalaryIncrease.GetAllSalaryIncrease)]
        public async Task<IActionResult> GetAllSalaryIncrease(GetAlFilterationForSalaryIncreaseRequest model)
        {
            var result = await _service.GetAllTransSalaryIncreaseAsync(requestService.GetRequestHeaderLanguage, model, requestService.GetCurrentHost);
            return Ok(result);
        }
        [HttpGet(ApiRoutes.SalaryIncrease.GetSalaryIncreaseById)]
        public async Task<IActionResult> GetSalaryIncreaseById(int id)
        {
            var result = await _service.GetTransSalaryIncreaseByIdAsync(id, requestService.GetRequestHeaderLanguage);
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut(ApiRoutes.SalaryIncrease.UpdateSalaryIncrease)]
        public async Task<IActionResult> UpdateSalaryIncrease(CreateTransSalaryIncreaseRequest model)
        {
            var result = await _service.UpdateTransSalaryIncreaseAsync(model);
            return Ok(result);
        }
        #endregion

        #region Delete
        [HttpDelete(ApiRoutes.SalaryIncrease.DeleteSalaryIncrease)]
        public async Task<IActionResult> DeleteSalaryIncrease(int id)
        {
            var result = await _service.DeleteTransSalaryIncreaseAsync(id);
            return Ok(result);
        }
        #endregion

    }
}
