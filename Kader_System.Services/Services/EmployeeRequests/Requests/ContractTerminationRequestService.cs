using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.EmployeeRequests.Requests;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class ContractTerminationRequestService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper)
        : IContractTerminationRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;


        #region ListOfLoanRequest
        public async Task<Response<IEnumerable<DTOListOfContractTerminationResponse>>> ListOfContractTerminationRequest()
        {
            var result = unitOfWork.ContractTerminationRequest.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id));
            var msg = _sharLocalizer[Localization.NotFound];
            if (result == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var mappingResult = mapper.Map<IEnumerable<DTOListOfContractTerminationResponse>>(result);

            return new()
            {
                Data = mappingResult,
                Msg = msg,
            };
        }
        #endregion

        #region PaginatedLoanRequest
        public async Task<Response<GetAllContractTermiantionResponse>> GetAllContractTerminationRequest(GetFilterationContractTerminationRequest model, string host)
        {
            Expression<Func<ContractTerminationRequest, bool>> filter = x => x.IsDeleted == model.IsDeleted;
            var totalRecords = await unitOfWork.ContractTerminationRequest.CountAsync(filter: filter);
            var data = await unitOfWork.ContractTerminationRequest.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, orderBy: x => x.OrderBy(x => x.Id),take:model.PageSize
                , skip: (model.PageNumber - 1)*model.PageSize
                );

            var msg = _sharLocalizer[Localization.NotFound];
            if (data == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var mappingResult = mapper.Map<List<DTOListOfContractTerminationResponse>>(data);


            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllContractTermiantionResponse()
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
                string resultMsg =_sharLocalizer[Localization.NotFoundData];

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
        public async Task<Response<DTOListOfContractTerminationResponse>> GetById(int id)
        {
            var result =await unitOfWork.ContractTerminationRequest.GetByIdAsync(id);
            if (result == null)
            {
                var msg = _sharLocalizer[Localization.NotFoundData];
                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };

            }

            var mappingResult = mapper.Map<DTOListOfContractTerminationResponse>(result);
            return new()
            {
                Data = mappingResult,
                Check = true,

            };

        }
        #endregion

        #region AddLoanRequest
        public async Task<Response<ContractTerminationRequest>> AddNewContractTerminationRequest(DTOContractTerminationRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
        {
            var IsEmpolyeeExisted = await unitOfWork.Employees.ExistAsync(model.EmployeeId);
            if (!IsEmpolyeeExisted)
            {

                var msg = $"{_sharLocalizer[Localization.Employee]} {_sharLocalizer[Localization.NotFound]}";
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };

            }
            var newRequest = mapper.Map<ContractTerminationRequest>(model);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentFileName = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.Attachment);
            await unitOfWork.ContractTerminationRequest.AddAsync(newRequest);
            var result = await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = _sharLocalizer[Localization.Done],
                Check = true,
            };

        }
        #endregion

        #region DeleteLoanRequets
        public async Task<Response<ContractTerminationRequest>> DeleteContracTermniationRequest(int id, string fullPath)
        {
            var loanRequest = await unitOfWork.ContractTerminationRequest.GetByIdAsync(id);
            var msg = $"{_sharLocalizer[Localization.Employee]} {_sharLocalizer[Localization.NotFound]}";
            if (loanRequest == null)
            {

                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };
            }
            if (!string.IsNullOrWhiteSpace(loanRequest.AttachmentFileName))
            {
                _fileServer.RemoveFile(fullPath, loanRequest.AttachmentFileName);
            }
            unitOfWork.ContractTerminationRequest.Remove(loanRequest);
            await unitOfWork.CompleteAsync();
            msg = _sharLocalizer[Localization.Deleted];

            return new()
            {
                Data = loanRequest,
                Msg = msg,
                Check = true,
            };

        }
        #endregion

        #region UpdateLoanRequest
        public async Task<Response<ContractTerminationRequest>> UpdateContractTerminationRequest(int id, DTOContractTerminationRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None)
        {
            var result = await unitOfWork.ContractTerminationRequest.GetByIdAsync(id);

            if (result == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = _sharLocalizer[Localization.NotFound]
                };
            }
            var updatingModel = mapper.Map(model, result);
            if (model.Attachment is not null)
            {

                var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
                updatingModel.AttachmentFileName = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                    await _fileServer.UploadFile(root, clientName, moduleNameWithType, model.Attachment);


            }
            unitOfWork.ContractTerminationRequest.Update(result);
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
