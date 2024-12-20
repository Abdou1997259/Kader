﻿using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Services.IServices;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class SalaryIncreaseRequestService(IUnitOfWork unitOfWork, IUserContextService userContextService, ITransSalaryIncreaseService transSalaryIncrease, KaderDbContext context, IRequestService requestService, IHttpContextAccessor httpContextAccessor, IHttpContextService contextService, IStringLocalizer<SharedResource> sharLocalizer, IFileServer fileServer, IMapper mapper) : ISalaryIncreaseRequestService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IFileServer _fileServer = fileServer;
        private readonly IHttpContextService _contextService = contextService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly KaderDbContext _context = context;
        private readonly IRequestService _requestService = requestService;
        private readonly ITransSalaryIncreaseService _transSalaryIncreaseService = transSalaryIncrease;
        private readonly IUserContextService _userContextService = userContextService;


        #region ListOfIncreaseSalaryRequest

        public async Task<Response<IEnumerable<DTOSalaryIncreaseRequest>>> ListOfSalaryIncreaseRequest()
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var result = await unitOfWork.LoanRepository
                .GetSpecificSelectAsync(x => x.IsDeleted == false
                && x.CompanyId == currentCompanyId,
                x => x, orderBy: x => x.OrderBy(x => x.Id));
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
            var mappingResult = mapper.Map<IEnumerable<DTOSalaryIncreaseRequest>>(result);

            return new()
            {
                Data = mappingResult,
                Msg = msg,
            };
        }
        #endregion


        #region PaginatedSalaryIncrease

        public async Task<Response<GetAllSalaryIncreaseRequestResponse>> GetAllSalaryIncreaseRequest(GetAlFilterationForSalaryIncreaseRequest model, string host)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();

            #region ApprovalExpression
            Expression<Func<SalaryIncreaseRequest, bool>> filter = x =>
                x.IsDeleted == false && x.CompanyId == currentCompanyId &&
                (model.ApporvalStatus == RequestStatusTypes.All ||
                (model.ApporvalStatus == RequestStatusTypes.Approved && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved) ||
                (model.ApporvalStatus == RequestStatusTypes.ApprovedRejected &&
                    (x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved ||
                     x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected)) ||
                (model.ApporvalStatus == RequestStatusTypes.Rejected && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Rejected) ||
                (model.ApporvalStatus == RequestStatusTypes.Pending && x.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Pending));

            #endregion

            var totalRecords = await _unitOfWork.SalaryIncreaseRequest.CountAsync(filter: filter);
            var items = (await _unitOfWork.SalaryIncreaseRequest.GetSpecificSelectAsync(filter, x => new ListOfSalaryIncreaseRequestResponse
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                request_date = x.Add_date.Value.ToString("yyyy-mm-dd"),
                EmployeeName = _requestService.GetRequestHeaderLanguage == Localization.English ? x.employee.FullNameEn : x.employee.FullNameAr,
                Amount = x.Amount,
                ApporvalStatus = x.StatuesOfRequest.ApporvalStatus,

                reason = x.StatuesOfRequest.StatusMessage,
                Notes = x.Notes,
                AttachmentPath = x.AttachmentPath != null ? _fileServer.CombinePath(Modules.EmployeeRequest, HrEmployeeRequestTypesEnums.SalaryIncreaseRequest.ToString(), x.AttachmentPath) : null
            },
            orderBy: x => x.OrderByDescending(x => x.Id),
                skip: (model.PageNumber - 1) * model.PageSize, take: model.PageSize, includeProperties: "employee,StatuesOfRequest")).ToList();
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

            var result = new GetAllSalaryIncreaseRequestResponse
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


        #region SalaryIncreaseGetById
        public async Task<Response<ListOfSalaryIncreaseRequestResponse>> GetById(int id)

        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var result = await unitOfWork.SalaryIncreaseRequest.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompanyId);
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

            var mappingResult = mapper.Map<ListOfSalaryIncreaseRequestResponse>(result);

            return new()
            {
                Data = mappingResult,
                Check = true,

            };

        }

        #endregion

        #region AddSalaryIncrease
        public async Task<Response<SalaryIncreaseRequest>>
            AddNewSalaryIncreaseRequest(DTOSalaryIncreaseRequest model
            , string moduleName, HrEmployeeRequestTypesEnums
            hrEmployeeRequest =
            HrEmployeeRequestTypesEnums.SalaryIncreaseRequest)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            if (!await _unitOfWork.Employees.ExistAsync(x =>
            x.Id == model.EmployeeId && x.CompanyId == currentCompanyId))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Employee]]
                };

            }



            var newRequest = _mapper.Map<SalaryIncreaseRequest>(model);
            newRequest.IncreaseType = 1;
            newRequest.CompanyId = currentCompanyId;
            StatuesOfRequest statues = new()
            {
                ApporvalStatus = (int)RequestStatusTypes.Pending
            };
            newRequest.StatuesOfRequest = statues;
            var moduleNameWithType = hrEmployeeRequest
                .GetModuleNameWithType(moduleName);
            newRequest.AttachmentPath = (model.Attachment == null || model.Attachment.Length == 0) ? null :
                await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            await _unitOfWork.SalaryIncreaseRequest.AddAsync(newRequest);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }
        #endregion


        #region UpdateSalaryIncrease
        public async Task<Response<SalaryIncreaseRequest>>
            UpdateSalaryIncreaseRequest(int id, DTOSalaryIncreaseRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.SalaryIncreaseRequest)
        {
            var currentCompanyId =
                await _userContextService.GetLoggedCurrentCompany();
            if (!await _unitOfWork.Employees.ExistAsync(
                x => x.Id == model.EmployeeId && x.CompanyId ==
                currentCompanyId))
            {
                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Employee]]
                };
            }


            var salaryIncrease = await _unitOfWork.SalaryIncreaseRequest
                .GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompanyId);
            if (salaryIncrease == null || salaryIncrease.StatuesOfRequest.ApporvalStatus != (int)RequestStatusTypes.Pending)
            {
                var msg = _sharLocalizer[Localization.NotFound] + " or " + _sharLocalizer[Localization.NotPending];
                return new()
                {
                    Check = false,
                    Msg = msg,
                    Data = null
                };
            }
            var mappedsalaryIncrease = _mapper.Map(model, salaryIncrease);
            var moduleNameWithType = hrEmployeeRequest.GetModuleNameWithType(moduleName);


            #region UpdateFile
            if (model.Attachment is not null)
            {
                if (salaryIncrease.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, salaryIncrease.AttachmentPath);
                salaryIncrease.AttachmentPath = await _fileServer.UploadFileAsync(moduleNameWithType, model.Attachment);
            }
            else
            {
                if (salaryIncrease.AttachmentPath != null)
                    _fileServer.RemoveFile(moduleName, salaryIncrease.AttachmentPath);
                salaryIncrease.AttachmentPath = null;
            }

            #endregion

            _unitOfWork.SalaryIncreaseRequest.Update(mappedsalaryIncrease);
            var result = await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };

        }

        #endregion

        #region DeleteSalaryIncrease
        public async Task<Response<SalaryIncreaseRequest>> DeleteSalaryIncreaseRequest(int id, string moduleName)
        {
            var currentCompanyId = await _userContextService.GetLoggedCurrentCompany();
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var msg =
                $"{_sharLocalizer[Localization.Employee]}" +
                $" {_sharLocalizer[Localization.NotFound]}";

            var salaryIncreaseRequest = await
                _unitOfWork.SalaryIncreaseRequest
                .GetEntityWithIncludeAsync(x => x.Id ==
                id && x.CompanyId == currentCompanyId, "StatuesOfRequest");
            if (salaryIncreaseRequest != null)
            {
                if (salaryIncreaseRequest.StatuesOfRequest.ApporvalStatus != 1)
                {
                    msg = _sharLocalizer[Localization.ApproveRejectDelte];
                    return new()
                    {
                        Msg = msg,
                        Check = false,
                    };
                }
                var result = await _unitOfWork.SalaryIncreaseRequest.SoftDeleteAsync(salaryIncreaseRequest, DeletedBy: userId);
                if (result > 0)
                {
                    if (!string.IsNullOrWhiteSpace(salaryIncreaseRequest.AttachmentPath))
                    {
                        _fileServer.RemoveFile(moduleName, HrEmployeeRequestTypesEnums.SalaryIncreaseRequest.ToString(), salaryIncreaseRequest.AttachmentPath);
                    }
                    msg = _sharLocalizer[Localization.Deleted];
                    return new()
                    {
                        Msg = msg,
                        Check = true,
                    };
                }
            }
            return new()
            {
                Check = false,
                Data = null,
                Msg = msg
            };

        }





        #endregion

        #region Status
        public async Task<Response<string>> ApproveRequest(int
            requestId, string lang)
        {
            var currentCompanyId =
                await _userContextService.GetLoggedCurrentCompany();
            var userId = _userContextService.UserId;
            var increaseRequest = await
                _unitOfWork.SalaryIncreaseRequest
                .GetFirstOrDefaultAsync(x => x.Id == requestId &&
                x.CompanyId == currentCompanyId);

            if (increaseRequest == null)
            {
                var msg = _sharLocalizer[Localization.NotFound];
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = msg
                };
            }
            if (increaseRequest.StatuesOfRequest.ApporvalStatus == (int)RequestStatusTypes.Approved)
            {
                return new Response<string>()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.ApprovedAready]
                };

            }



            var transaction = _unitOfWork.BeginTransaction();
            try
            {


                var result = await _unitOfWork.SalaryIncreaseRequest.
                    UpdateApporvalStatus(x => x.Id == requestId,
                    RequestStatusTypes.Approved, userId);

                if (result > 0)
                {
                    var createResult = await _transSalaryIncreaseService
                          .CreateTransSalaryIncreaseAsync(new CreateTransSalaryIncreaseRequest
                          {
                              Amount = increaseRequest.Amount,
                              Employee_id = increaseRequest.EmployeeId,
                              Increase_type = increaseRequest.IncreaseType,
                              TransactionDate = DateOnly.FromDateTime(increaseRequest.Add_date.Value),
                              Notes = increaseRequest.Notes


                          }, lang);




                    transaction.Commit();
                    return new Response<string>
                    {
                        Msg = _sharLocalizer[Localization.Approved],
                        Check = true,
                    };



                }
                return new Response<string>()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.NotApproved]
                };



            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return new Response<string>()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.Error]
                };

            }

        }
        public async Task<Response<string>> RejectRequest(int requestId, string resoan)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _unitOfWork.SalaryIncreaseRequest.UpdateApporvalStatus(x => x.Id == requestId, RequestStatusTypes.Rejected, userId, resoan);
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
                Msg = "Cannot approve"
            };
        }
        #endregion


    }
}
