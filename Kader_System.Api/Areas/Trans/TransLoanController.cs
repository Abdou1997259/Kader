using Kader_System.Domain.DTOs.Request.HR.Loan;
using Kader_System.Services.IServices.Trans;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    [Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransLoanController(ITransLoanService service) : ControllerBase

    {



        #region Retrieve


        [HttpGet(ApiRoutes.Loan.ListOfLoans)]
        public async Task<IActionResult> ListOfLoans()
            => Ok(await service.ListLoansAsync(GetCurrentRequestLanguage()));


        [HttpGet(ApiRoutes.Loan.GetAllloans)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllFilltrationForLoanRequest filter)
            => Ok(await service.GetAllLoanAsync(GetCurrentRequestLanguage(), filter, GetCurrentHost()));

        [HttpGet(ApiRoutes.Loan.GetloanById)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await service.GetLoanByIdAsync(id, GetCurrentRequestLanguage());
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpGet(ApiRoutes.Loan.GetLookups)]
        public async Task<IActionResult> GetLookUpsAsync()
        {
            var response = await service.GetDeductionsLookUpsData(GetCurrentRequestLanguage());
            if (response.Check)
                return Ok(response);
            else
                return BadRequest(response);
        }
        #endregion


        #region Create

        [HttpPost(ApiRoutes.Loan.Createloan)]
        public async Task<IActionResult> Create(CreateLoanRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.CreateLoanAsync(request, GetCurrentRequestLanguage());
                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return BadRequest(result);
            }

            return BadRequest(request);
        }


        #endregion

        #region Update

        [HttpPut(ApiRoutes.Loan.Updateloan)]
        public async Task<IActionResult> UpdateTask(int id, UpdateLoanRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.UpdateLoanAsync(id, request, GetCurrentRequestLanguage());
                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, request);
            }
            return BadRequest(request);
        }
        [HttpPut(ApiRoutes.Loan.RestoreLoan)]
        public async Task<IActionResult> RestoreTask(int id)
        {
            var result = await service.RestoreLoanAsync(id, GetCurrentRequestLanguage());
            if (ModelState.IsValid)
            {

                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);
            }
            return BadRequest(result);
        }

        [HttpPut(ApiRoutes.Loan.UpdatePaymentLoan)]
        public async Task<IActionResult> PayTask([FromForm] PayForLoanDetailsRequest request)
        {
            var result = await service.PayForLoanDetails(request);
            if (ModelState.IsValid)
            {

                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);
            }
            return BadRequest(result);
        }
        [HttpPut(ApiRoutes.Loan.UpdateDelayLoan)]
        public async Task<IActionResult> DelayTask([FromForm] DelayForTransLoanRequest request)
        {
            var result = await service.DelayForLoanDetails(request);
            if (ModelState.IsValid)
            {

                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);
            }
            return BadRequest(result);
        }


        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.Loan.Deleteloan)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.DeleteLoanAsync(id);
            if (result.Check) return Ok(result);
            else if (!result.Check) return BadRequest(result);
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);

        }


        #endregion

        #region Helper

        private string GetCurrentRequestLanguage() =>
            Request.Headers.AcceptLanguage.ToString().Split(',').First();
        private string GetCurrentHost() =>
            HttpContext.Request.Host.Value +
            HttpContext.Request.Path.Value;
        #endregion
    }

}
