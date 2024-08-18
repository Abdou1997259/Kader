using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class SalaryIncreaseRequestService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> localizer, IFileServer fileServer, IFileServer fileserver,
    IMapper mapper) : ISalaryIncreaseRequestService
    {
 


        #region ListOfIncreaseSalaryRequest

        public async Task<Response<IEnumerable<DTOSalaryIncreaseRequest>>> ListOfSalaryIncreaseRequest()
        {
            var result =  await unitOfWork.LoanRepository.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id));
            var msg = localizer[Localization.NotFound];
            if (result == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var mappingResult = mapper.Map<IEnumerable<DTOSalaryIncreaseRequest>>(result);

            return new()
            {
                Data = mappingResult,
                Msg = msg,
            };
        }
        #endregion


        #region PaginatedSalaryIncrease

        public async Task<Response<GetSalaryIncreseRequestResponse>> GetAllSalaryIncreaseRequest(GetAlFilterationForSalaryIncreaseRequest model, string host)
        {
            Expression<Func<SalaryIncreaseRequest, bool>> filter = model.ApporvalStatus == RequestStatusTypes.All ?
                   x => x.IsDeleted == false :
                   x => x.IsDeleted == false && x.StatuesOfRequest.ApporvalStatus == (int)model.ApporvalStatus; var totalRecords = await unitOfWork.SalaryIncreaseRequest.CountAsync(filter: filter);
            var data = await unitOfWork.SalaryIncreaseRequest.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id));
            var msg = localizer[Localization.NotFound];
            if (data == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var mappingResult = mapper.Map<List<DTOListOfSalaryIncreaseRepostory>>(data);


            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetSalaryIncreseRequestResponse()
            {
                TotalRecords = totalRecords,

                Items = mappingResult,
                CurrentPage = model.PageNumber,
                FirstPageUrl = host + $"?PageSize={model.PageSize}&PageNumber=1&IsDeleted={model.IsDeleted}",
                From = (page - 1) * model.PageSize + 1,
                To = Math.Min(page * model.PageSize, totalRecords),
                LastPage = totalPages,
                LastPageUrl = host + $"?PageSize={model.PageSize}&PageNumber={totalPages}&IsDeleted={model.IsDeleted}",
                PreviousPage = page > 1 ? host + $"?PageSize={model.PageSize}&PageNumber={page - 1}&IsDeleted={model.IsDeleted}" : null,
                NextPageUrl = page < totalPages ? host + $"?PageSize={model.PageSize}&PageNumber={page + 1}&IsDeleted={model.IsDeleted}" : null,
                Path = host,
                PerPage = model.PageSize,
                Links = pageLinks

            };

            if (result.TotalRecords is 0)
            {
                string resultMsg = localizer[Localization.NotFoundData];

                return new()
                {
                    Data = new()
                    {
                        Items = []
                    },
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = result,
                Check = true
            };

        }
        #endregion


        #region SalaryIncreaseGetById
        public async Task<Response<DTOListOfSalaryIncreaseRepostory>> GetById(int id)

        {
            var result = await unitOfWork.SalaryIncreaseRequest.GetByIdAsync(id);
            if (result == null)
            {
                var msg = localizer[Localization.NotFoundData];

                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };

            }

            var mappingResult = mapper.Map<DTOListOfSalaryIncreaseRepostory>(result);

            return new()
            {
                Data = mappingResult,
                Check = true,

            };

        }

        #endregion

        #region AddSalaryIncrease
        public async Task<Response<SalaryIncreaseRequest>> AddNewSalaryIncreaseRequest(DTOSalaryIncreaseRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.SalaryIncreaseRequest)
        {
             
        
        var IsEmpolyeeExisted = await unitOfWork.Employees.ExistAsync(model.EmployeeId);
            if (!IsEmpolyeeExisted)
            {

                var msg = $"{localizer[Localization.Employee]} {localizer[Localization.NotFound]}";
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var newRequest = mapper.Map<SalaryIncreaseRequest>(model);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentFileName = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await fileserver.UploadFile(appPath, moduleNameWithType, model.Attachment);
            await unitOfWork.SalaryIncreaseRequest.AddAsync(newRequest);
            var result = await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = localizer[Localization.Done],
                Check = true,
            };

        }
        #endregion


        #region UpdateSalaryIncrease
        public async Task<Response<SalaryIncreaseRequest>> UpdateSalaryIncreaseRequest(int id, DTOSalaryIncreaseRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.SalaryIncreaseRequest)
        {
            var result = await unitOfWork.SalaryIncreaseRequest.GetByIdAsync(id);

            if (result == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = localizer[Localization.NotFound]
                };
            }
            var updatingModel = mapper.Map(model, result);
            if (model.Attachment is not null)
            {
                var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
                updatingModel.AttachmentFileName = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                    await fileserver.UploadFile(appPath, moduleNameWithType, model.Attachment);
            }
            unitOfWork.SalaryIncreaseRequest.Update(result);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Data = updatingModel,
                Check = true
            };


        }

        #endregion

        #region DeleteSalaryIncrease
        public async Task<Response<SalaryIncreaseRequest>> DeleteSalaryIncreaseRequest(int id)
        {
            var salaryIncrease = await unitOfWork.SalaryIncreaseRequest.GetByIdAsync(id);
            var msg = $"{localizer[Localization.Employee]} {localizer[Localization.NotFound]}";
            if (salaryIncrease == null)
            {

                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };
            }
            unitOfWork.SalaryIncreaseRequest.Remove(salaryIncrease);
            await unitOfWork.CompleteAsync();
            msg = localizer[Localization.Deleted];

            return new()
            {
                Data = salaryIncrease,
                Msg = msg,
                Check = true,
            };

        }

      

      
 
        #endregion

    }
}
