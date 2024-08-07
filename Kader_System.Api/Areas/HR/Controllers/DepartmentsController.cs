﻿using Azure.Core;
using Kader_System.Api.Helpers;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Api.Areas.HR.Controllers
{
    [Area(Modules.HR)]
    //[Authorize(Permissions.HR.View)]
    [ApiExplorerSettings(GroupName = Modules.HR)]
    [ApiController]
    [Route("api/v1/")]
    public class DepartmentsController(IDepartmentService service, IRequestService requestService) : ControllerBase
    {
        private readonly IRequestService requestService = requestService;

        #region Retrieve


        [HttpGet(ApiRoutes.Department.ListOfDepartments)]
        [Permission(Permission.View, 11)]
        public async Task<IActionResult> ListOfDepartments()
            => Ok(await service.ListOfDepartmentsAsync(requestService.GetRequestHeaderLanguage));


        [HttpGet(ApiRoutes.Department.GetAllDepartments)]
        [Permission(Permission.View, 11)]

        public async Task<IActionResult> GetAll([FromQuery]GetAllFiltrationsForDepartmentsRequest filter)
            => Ok(await service.GetAllDepartmentsAsync(requestService.GetRequestHeaderLanguage, filter, requestService.GetCurrentHost));

        [HttpGet(ApiRoutes.Department.GetDepartmentById)]
        [Permission(Permission.View, 11)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await service.GetDepartmentByIdAsync(id, requestService.GetRequestHeaderLanguage);
            if (response.Check)
                return Ok(response);
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        #endregion


        #region Create

        [HttpPost(ApiRoutes.Department.CreateDepartment)]
        [Permission(Permission.Add, 11)]
        public async Task<IActionResult> Create(CreateDepartmentRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.CreateDepartmentAsync(request);
                if(result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return BadRequest(result);
            }

            return BadRequest(request);
        }


        #endregion

        #region Update

        [HttpPut(ApiRoutes.Department.UpdateDepartment)]
        [Permission(Permission.Edit, 11)]
        public async Task<IActionResult> UpdateTask(int id,CreateDepartmentRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await service.UpdateDepartmentAsync(id, request);
                if (result.Check) return Ok(result);
                else if(!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, request);
            }
            return BadRequest(request);
        }
        [HttpPut(ApiRoutes.Department.AddEmp)]
        [Permission(Permission.Add, 11)]
        public async Task<IActionResult> AddEmployee( int id,AddEmpolyeeToDepartmentRequest model)
        {
            if (ModelState.IsValid)
            {
              
                var result = await service.AddEmployee(id,model);
                if (result.Check) return Ok(result);
                else if (!result.Check) return BadRequest(result);
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, model);
            }
            return BadRequest(model);
        }


        #endregion

        #region Delete

        [HttpDelete(ApiRoutes.Department.DeleteDepartment)]
        [Permission(Permission.Delete, 11)]
        public async Task<IActionResult> Delete(int id)
        {
            var result=await service.DeleteDepartmentAsync(id);
            if (result.Check) return Ok(result);
            else if (!result.Check)  return BadRequest(result);
            return StatusCode(statusCode: StatusCodes.Status400BadRequest, result);

        }
        

        #endregion

        
    }
}
