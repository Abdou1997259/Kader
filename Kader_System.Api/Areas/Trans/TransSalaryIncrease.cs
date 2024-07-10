using Kader_System.Domain.Interfaces.Trans;
using Kader_System.Services.IServices.Trans;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    [Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransSalaryIncrease(ITransSalaryIncreaseRepository service) : ControllerBase
    {
        #region Create
        [HttpPost(ApiRoutes.SalaryIncrease.CreateSalaryIncrease)]
        public async Task<IActionResult> CreateTransVacation([FromBody] CreateTransSalaryIncreaseRequest request)
        {
            var result = await service.AddNewSalaryIncrease(request);
            if (result > 0)
                return Ok("data added sucessfully");
            else
                return BadRequest("cannot add ");
        }
        #endregion
        #region Read
        [HttpGet(ApiRoutes.SalaryIncrease.GetAllSalaryIncrease)]
        public async Task<IActionResult> GetAllSalaryIncrease()
        {
            var result = await service.GetAllSalaryIncrease();
            return Ok(result);
        } 
        [HttpGet(ApiRoutes.SalaryIncrease.GetSalaryIncreaseById)]
        public async Task<IActionResult> GetSalaryIncreaseById(int id)
        {
            var result = await service.GetSalaryIncreaseById(id);
            return Ok(result);
        }
        #endregion
    }
}
