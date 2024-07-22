using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.EmployeeRequests.Requests;


namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class LoanRequesService(
        IUnitOfWork unitOfWork,
        IStringLocalizer<SharedResource> 
        localizer,IFileServer fileserver,
        IMapper mapper
        ) : ILoanRequestService
    {

        #region ListOfLoanRequest
        public async Task<Response<IEnumerable<DTOListOfLoanRequestResponse>>> ListOfLoanRequest()
        {
            var result = await unitOfWork.LoanRepository.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id));
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
            var mappingResult = mapper.Map<IEnumerable<DTOListOfLoanRequestResponse>>(result);

            return new()
            {
                Data = mappingResult,
                Msg = msg,
            };
        }
        #endregion

        #region PaginatedLoanRequest
        public async Task<Response<GetAllLoanRequestResponse>> GetAllLoanRequest(GetFilterationLoanRequest model, string host)
        {
            Expression<Func<LoanRequest, bool>> filter = x => x.IsDeleted == model.IsDeleted;
            var totalRecords = await unitOfWork.LoanRequestRepository.CountAsync(filter: filter);
            var data =  await unitOfWork.LoanRequestRepository.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id), take: model.PageSize,
                     skip: (model.PageNumber - 1) * model.PageSize);
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
            var mappingResult = mapper.Map<List<DTOListOfLoanRequestResponse>>(data);


            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllLoanRequestResponse()
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

        #region GetLoanRequetById
        public async Task<Response<DTOListOfLoanRequestResponse>> GetById(int id)
        {
            var result =await unitOfWork.LoanRequestRepository.GetByIdAsync(id);
            if (result == null) {
                var msg = localizer[Localization.NotFoundData];
                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };
            
            }

            var mappingResult=mapper.Map<DTOListOfLoanRequestResponse>(result);
            return new()
            {
                Data = mappingResult,
                Check = true,

            };
          
        }
        #endregion

        #region AddLoanRequest
        public async Task<Response<LoanRequest>> AddNewLoanRequest(DTOLoanRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
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
            var newRequest = mapper.Map<LoanRequest>(model);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentFileName = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await fileserver.UploadFile(root, clientName, moduleNameWithType, model.Attachment);
            await unitOfWork.LoanRequestRepository.AddAsync(newRequest);
            var result = await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = localizer[Localization.Done],
                Check = true,
            };

        }
        #endregion

        #region DeleteLoanRequets
        public async Task<Response<LoanRequest>> DeleteLoanRequest(int id)
        {
            var loanRequest = await unitOfWork.LoanRequestRepository.GetByIdAsync(id);
            var msg = $"{localizer[Localization.Employee]} {localizer[Localization.NotFound]}";
            if (loanRequest == null)
            {

                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };
            }
            unitOfWork.LoanRequestRepository.Remove(loanRequest);
            await unitOfWork.CompleteAsync();
            msg = localizer[Localization.Deleted];

            return new()
            {
                Data = loanRequest,
                Msg = msg,
                Check = true,
            };

        }
        #endregion

        #region UpdateLoanRequest
        public async Task<Response<LoanRequest>> UpdateLoanRequest(int id, DTOLoanRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
        {
            var result = await unitOfWork.LoanRequestRepository.GetByIdAsync(id);

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
                    await fileserver.UploadFile(root, clientName, moduleNameWithType, model.Attachment);
            }
            unitOfWork.LoanRequestRepository.Update(result);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Data = updatingModel,
                Check = true
            };


        }

     

        #endregion











    }
}
