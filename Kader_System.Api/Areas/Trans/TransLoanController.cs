﻿using Kader_System.Api.Helpers;
using Kader_System.Domain.DTOs.Request.HR.Loan;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.IServices.Trans;

namespace Kader_System.Api.Areas.Trans
{
    [Area(Modules.Trans)]
    //[Authorize(Permissions.Transaction.View)]
    [ApiExplorerSettings(GroupName = Modules.Trans)]
    [ApiController]
    [Route("api/v1/")]
    public class TransLoanController(ITransLoanService service, IRequestService requestService) : ControllerBase

    {
        private readonly IRequestService requestService = requestService;


        #region Retrieve


        [HttpGet(ApiRoutes.Loan.ListOfLoans)]
        [Permission(Helpers.Permission.View, 23)]
        public async Task<IActionResult> ListOfLoans()
            => Ok(await service.ListLoansAsync(requestService.GetRequestHeaderLanguage));


        [HttpGet(ApiRoutes.Loan.GetAllloans)]
        [Permission(Helpers.Permission.View, 23)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllFilltrationForLoanRequest filter)
            => Ok(await service.GetAllLoanAsync(requestService.GetRequestHeaderLanguage, filter, requestService.GetRequestHeaderLanguage));

        [HttpGet(ApiRoutes.Loan.GetloanById)]
        [Permission(Helpers.Permission.View, 23)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await service.GetLoanByIdAsync(id, requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpGet(ApiRoutes.Loan.GetLookups)]
        [Permission(Helpers.Permission.View, 23)]
        public async Task<IActionResult> GetLookUpsAsync()
        {
            var response = await service.GetLookUpsData(requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else
                return BadRequest(response);
        }
        #endregion


        #region Create

        [HttpPost(ApiRoutes.Loan.Createloan)]
        [Permission(Helpers.Permission.Add, 23)]
        public async Task<IActionResult> Create(CreateLoanRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.CreateLoanAsync(request, requestService.GetRequestHeaderLanguage);
                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return BadRequest(result);
            }

            return BadRequest(request);
        }


        #endregion

        #region Update

        [HttpPut(ApiRoutes.Loan.Updateloan)]
        [Permission(Helpers.Permission.Edit, 23)]
        public async Task<IActionResult> UpdateTask(int id, UpdateLoanRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.UpdateLoanAsync(id, request, requestService.GetRequestHeaderLanguage);
                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, request);
            }
            return BadRequest(request);
        }
        [HttpPut(ApiRoutes.Loan.RestoreLoan)]
        public async Task<IActionResult> RestoreTask(int id)
        {
            var result = await service.RestoreLoanAsync(id, requestService.GetRequestHeaderLanguage);
            if (ModelState.IsValid)
            {

                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);
            }
            return BadRequest(result);
        }

        [HttpPut(ApiRoutes.Loan.UpdatePaymentLoan)]
        [Permission(Helpers.Permission.Edit, 23)]
        public async Task<IActionResult> PayTask([FromBody] PayForLoanDetailsRequest request, int id)
        {
            var result = await service.PayForLoanDetails(request, id);
            if (ModelState.IsValid)
            {

                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);
            }
            return BadRequest(result);
        }
        [HttpPut(ApiRoutes.Loan.UpdateDelayLoan)]
        [Permission(Helpers.Permission.Edit, 23)]
        public async Task<IActionResult> DelayTask([FromBody] DelayForTransLoanRequest request, int id)
        {
            var result = await service.DelayForLoanDetails(request, id);
            if (ModelState.IsValid)
            {

                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);
            }
            return BadRequest(result);
        }
        [HttpPut(ApiRoutes.Loan.ReInstallment)]
        [Permission(Helpers.Permission.Edit, 23)]
        public async Task<IActionResult> ReInstallment(ReInstallmentRequest request, int id)
        {
            var result = await service.ReInstallmentAsync(request, id);
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
        [Permission(Helpers.Permission.Delete, 23)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.DeleteLoanAsync(id);
            if (result.Check) return Ok(result);
            else if (!result.Check) return BadRequest(result);
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);

        }


        #endregion

        
    }

}
