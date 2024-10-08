﻿using Kader_System.Api.Helpers;
using Kader_System.Domain.DTOs.Request.EmployeesRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.EmployeeRequests.Requests.Controllers
{
    [Area(Modules.EmployeeRequest)]
    [ApiExplorerSettings(GroupName = Modules.EmployeeRequest)]
    [ApiController]
    [Route("api/v1/")]
    //[Authorize(Permissions.Setting.View)]
    public class ResignationController(IResignationRequestService service, IRequestService requestService
        , IWebHostEnvironment hostEnvironment, IFileServer fileServer) : ControllerBase
    {
        #region Retrieve

        [HttpGet(ApiRoutes.EmployeeRequests.ResignationRequests.ListOfResignationRequests)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> ListOfResignationRequestsAsync() =>
            Ok(await service.ListOfResignationRequest());

        [HttpGet(ApiRoutes.EmployeeRequests.ResignationRequests.GetAllResignationRequests)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetAllResignationRequestsAsync([FromQuery] GetFillterationResignationRequest model) =>
            Ok(await service.GetAllResignationRequest(model, requestService.GetCurrentHost));
        [HttpGet(ApiRoutes.EmployeeRequests.ResignationRequests.GetResignationRequestsById)]
        [Permission(Permission.View, 19)]
        public async Task<IActionResult> GetResignationRequestByIdAsync(int id)
        {
            var response = await service.GetById(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Insert

        [HttpPost(ApiRoutes.EmployeeRequests.ResignationRequests.CreateResignationRequests)]
        [Permission(Permission.Add, 19)]
        public async Task<IActionResult> CreateResignationRequestAsync([FromForm] DTOResignationRequest model)
        {
            var response = await service.AddNewResignationRequest(model,

                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.ResignationRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion

        #region Update

        [HttpPut(ApiRoutes.EmployeeRequests.ResignationRequests.UpdateResignationRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> UpdateResignationRequestAsync([FromRoute] int id, [FromForm] DTOResignationRequest model)
        {
            var response = await service.UpdateResignationRequest(id, model,
                     Modules.EmployeeRequest, Domain.Constants.Enums.HrEmployeeRequestTypesEnums.ResignationRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }



        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.EmployeeRequests.ResignationRequests.DeleteResignationRequests)]
        [Permission(Permission.Delete, 19)]
        public async Task<IActionResult> DeleteResignationAsync(int id)
        {
            var response = await service.DeleteResignationRequest(id,Modules.EmployeeRequest);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }

        #endregion


        #region Status
        [HttpPut(ApiRoutes.EmployeeRequests.ResignationRequests.ApproveResignationRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> ApproveResignationRequests([FromRoute] int id)
        {
            var response = await service.ApproveRequest(id);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPut(ApiRoutes.EmployeeRequests.ResignationRequests.RejectResignationRequests)]
        [Permission(Permission.Edit, 19)]
        public async Task<IActionResult> RejectResignationRequests([FromRoute] int id, [FromBody] GlobalEmployeeRequests model)
        {
            var response = await service.RejectRequest(id, model.reson);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion
    }
}

