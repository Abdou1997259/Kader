using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;



namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class LoanRequesService(IUnitOfWork _unitOfWork, IUserContextService userContextService, ITransLoanService loanService, IStringLocalizer<SharedResource> _sharLocalizer, IRequestService _requestService, IFileServer _fileServer, IHttpContextAccessor _httpContextAccessor, KaderDbContext _context, IMapper _mapper) : ILoanRequestService
    {

        private readonly ITransLoanService _loanService = loanService;
        private readonly IUserContextService _userContextService = userContextService;
        #region ListOfLoanRequest

        public async Task<Response<IEnumerable<ListOfLoanRequestResponse>>> ListOfLoanRequest()
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var result = _unitOfWork.LoanRepository
                .GetSpecificSelectAsync(x => x.IsDeleted == false && x.CompanyId == currentCompanyId, x => x, orderBy: x => x.OrderBy(x => x.Id));
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
            var mappingResult = _mapper.Map<IEnumerable<ListOfLoanRequestResponse>>(result);

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
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            #region ApprovalExpression
            Expression<Func<LoanRequest, bool>> filter = x =>
                x.IsDeleted == false && x.CompanyId == currentCompanyId &&
                (model.ApporvalStatus == RequestStatusTypes.All ||
                (model.ApporvalStatus == RequestStatusTypes.Approved && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved) ||
                (model.ApporvalStatus == RequestStatusTypes.ApprovedRejected &&
                    (x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved ||
                     x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected)) ||
                (model.ApporvalStatus == RequestStatusTypes.Rejected && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected) ||
                (model.ApporvalStatus == RequestStatusTypes.Pending && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Pending));

            #endregion

            var totalRecords = await _unitOfWork.LoanRequestRepository.CountAsync(filter: filter);
            var items = (await _unitOfWork.LoanRequestRepository.GetSpecificSelectAsync(filter, x => new ListOfLoanRequestResponse
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                InstallmentsCount = x.InstallmentsCount,
                Amount = x.Amount,
                StartDate = x.StartDate,
                request_date = x.Add_date.Value.ToString("yyyy-MM-dd"),
                EmployeeName = _requestService.GetRequestHeaderLanguage == Localization.English ? x.Employee.FullNameEn : x.Employee.FullNameAr,
                ApporvalStatus = x.StatuesOfRequest.ApporvalStatus,
                reason = x.StatuesOfRequest.StatusMessage,
                Notes = x.Notes,
                AttachmentPath = x.AttachmentPath != null ? _fileServer.CombinePath(Modules.EmployeeRequest, HrEmployeeRequestTypesEnums.LoanRequest.ToString(), x.AttachmentPath) : null
            },
            orderBy: x => x.OrderByDescending(x => x.Id),
                skip: (model.PageNumber - 1) * model.PageSize, take: model.PageSize, includeProperties: "Employee,StatuesOfRequest")).ToList();
            #region Pagination

            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            #endregion

            var result = new GetAllLoanRequestResponse
            {
                TotalRecords = totalRecords,
                Items = items,
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
                Links = pageLinks,
            };

            if (result.TotalRecords is 0)
            {
                string resultMsg = _sharLocalizer[Localization.NotFoundData];

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
        public async Task<Response<ListOfLoanRequestResponse>> GetById(int id)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var result = await _unitOfWork.LoanRequestRepository.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompanyId);

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

            var mappingResult = _mapper.Map<ListOfLoanRequestResponse>(result);
            return new()
            {
                Data = mappingResult,
                Check = true,

            };

        }
        #endregion

        #region AddLoanRequest
        public async Task<Response<LoanRequest>> AddNewLoanRequest(DTOLoanRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.LoanRequest)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            if (!await _unitOfWork.Employees.ExistAsync(x => x.Id == model.EmployeeId && x.CompanyId == currentCompanyId))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Employee]]
                };
            }
            var newRequest = _mapper.Map<LoanRequest>(model);
            newRequest.CompanyId = currentCompanyId;
            StatuesOfRequest statues = new()
            {
                ApporvalStatus = (int)RequestStatusTypes.Pending
            };
            newRequest.StatuesOfRequest = statues;
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            await _unitOfWork.LoanRequestRepository.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = _sharLocalizer[Localization.Done],
                Check = true,
            };

        }
        #endregion

        #region DeleteLoanRequets
        public async Task<Response<LoanRequest>> DeleteLoanRequest(int id, string fullPath)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var loanRequest = await _unitOfWork.LoanRequestRepository
                .GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompanyId);
            if (loanRequest == null)
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.CannotBeFound, _sharLocalizer[Localization.LoanRequest]]
                };

            }

            if (loanRequest?.StatuesOfRequest?.ApporvalStatus != 1)
            {

                return new()
                {
                    Msg = _sharLocalizer[Localization.ApproveRejectDelte],
                    Check = false,
                };
            }
            if (!string.IsNullOrWhiteSpace(loanRequest.AttachmentPath))
            {
                _fileServer.RemoveFile(fullPath, HrEmployeeRequestTypesEnums.LoanRequest.ToString(), loanRequest.AttachmentPath);
            }
            loanRequest.IsDeleted = true;
            loanRequest.DeleteDate = DateTime.Now;
            loanRequest.DeleteBy = userId;
            _unitOfWork.LoanRequestRepository.Update(loanRequest);

            await _unitOfWork.CompleteAsync();

            return new()
            {
                Msg = _sharLocalizer[Localization.Deleted],
                Check = true,
            };

        }
        #endregion

        #region UpdateLoanRequest
        public async Task<Response<LoanRequest>> UpdateLoanRequest(int id, DTOLoanRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.LoanRequest)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();

            if (!await _unitOfWork.Employees.ExistAsync(x => x.Id == model.EmployeeId && x.CompanyId == currentCompanyId))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Employee]]
                };
            }
            var loan = await _unitOfWork.LoanRequestRepository.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompanyId);
            if (loan == null || loan.StatuesOfRequest.ApporvalStatus != (int)RequestStatusTypes.Pending)
            {
                var msg = _sharLocalizer[Localization.NotFound] + " or " + _sharLocalizer[Localization.NotPending];
                return new()
                {
                    Check = false,
                    Msg = msg,
                    Data = null
                };
            }
            var mappedleave = _mapper.Map(model, loan);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);


            #region UpdateFile
            if (model.Attachment is not null)
            {
                if (loan.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, loan.AttachmentPath);
                loan.AttachmentPath = await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            }
            else
            {
                if (loan.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, loan.AttachmentPath);
                loan.AttachmentPath = null;
            }

            #endregion

            _unitOfWork.LoanRequestRepository.Update(loan);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = _sharLocalizer[Localization.Done],
                Check = true,
            };


        }
        #endregion

        #region Status
        public async Task<Response<string>> ApproveRequest(int requestId, string lang)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var loanRequest = await _unitOfWork.LoanRequestRepository.GetFirstOrDefaultAsync(x => x.Id == requestId);
            if (loanRequest == null)
            {
                var msg = _sharLocalizer[Localization.NotFound];
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };
            }
            if (loanRequest.StatuesOfRequest.ApporvalStatus == 1)
            {
                return new Response<string>()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.ApprovedAready]
                };

            }

            var creatResult = await _loanService.CreateLoanAsync(new Domain.DTOs.Request.HR.Loan.CreateLoanRequest
            {
                LoanAmount = loanRequest.Amount,
                StartCalculationDate = loanRequest.StartDate,
                InstallmentCount = loanRequest.InstallmentsCount,
                EmployeeId = loanRequest.EmployeeId.Value,
                StartLoanDate = loanRequest.StartDate,
                EndCalculationDate = loanRequest.StartDate.AddMonths(loanRequest.InstallmentsCount - 1),
                MonthlyDeducted = (decimal)(loanRequest.Amount / loanRequest.InstallmentsCount),
                AdvanceType = 1




            }, lang);
            if (!creatResult.Check)
            {
                return new Response<string>
                {
                    Check = false,
                    Msg = creatResult.Msg
                };
            }

            var result = await _unitOfWork.LoanRequestRepository.
                UpdateApporvalStatus(x => x.Id == requestId,
                RequestStatusTypes.Approved, userId);



            if (result > 0)
            {
                return new Response<string>()
                {
                    Check = true,
                    Msg = _sharLocalizer[Localization.Approved]
                };
            }
            return new Response<string>()
            {
                Check = false,
                Msg = _sharLocalizer[Localization.NotApproved]
            };
        }
        public async Task<Response<string>> RejectRequest(int requestId, string resoan)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _unitOfWork.LoanRequestRepository.UpdateApporvalStatus(x => x.Id == requestId, RequestStatusTypes.Rejected, userId, resoan);
            if (result > 0)
            {
                return new Response<string>()
                {
                    Check = true,
                    Msg = "Rejected sucessfully"
                };
            }
            return new Response<string>()
            {
                Check = false,
                Msg = "Cannot approve , request is not pending or is deleted"
            };
        }
        #endregion
    }
}