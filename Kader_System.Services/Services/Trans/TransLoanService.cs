using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.HR.Loan;
using Kader_System.Domain.DTOs.Response.Loan;

namespace Kader_System.Services.Services.Trans
{
    public class TransLoanService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITransLoanService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        public async Task<Response<IEnumerable<ListOfLoansResponse>>> ListLoansAsync(string lang)
        {
            var result =
                  await _unitOfWork.LoanRepository.GetSpecificSelectAsync(null!,
                  select: x => new ListOfLoansResponse
                  {
                      Id = x.Id,
                      LoanDate = x.LoanDate,
                      LoanAmount = x.LoanAmount,
                      StartCalculationDate = x.StartCalculationDate,
                      EndCalculationDate = x.EndCalculationDate,
                      StartLoanDate = x.StartLoanDate,
                      EndDoDate = x.EndDoDate,
                      DocumentDate = x.DocumentDate,
                      AdvanceType = x.AdvanceType,
                      MonthlyDeducted = x.MonthlyDeducted,
                      InstallmentCount = x.InstallmentCount,
                      Notes = x.Notes,
                      LoanType = x.LoanType,

                      EmployeeName = Localization.Arabic == lang ? x.HrEmployee.FirstNameAr : x.HrEmployee.FirstNameEn,
                      PrevDedcutedAmount = x.PrevDedcutedAmount,




                  }, orderBy: x =>
                    x.OrderByDescending(x => x.Id), includeProperties: "HrEmployee");


            if (!result.Any())
            {
                string resultMsg = _sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = [],
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

        public async Task<Response<CreateLoanReponse>> CreateLoanAsync(CreateLoanRequest model, string lang)
        {

            var empolyee = await _unitOfWork.Employees.GetByIdAsync(model.EmployeeId);

            if (empolyee is null)
            {
                string resultMsg = $"{_sharLocalizer[Localization.Employee]} {_sharLocalizer[Localization.IsNotExisted]} ";
                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }


            var loan = _mapper.Map<TransLoan>(model);

            await _unitOfWork.LoanRepository.AddAsync(loan);
            await _unitOfWork.CompleteAsync();

            var startMonth = model.StartCalculationDate;
            for (int i = 1; i <= model.InstallmentCount; i++)
            {

                await _unitOfWork.TransLoanDetails.AddAsync(new TransLoanDetails
                {
                    DeductionDate = startMonth,
                    Amount = model.MonthlyDeducted,
                    TransLoanId = loan.Id,
                    PaymentDate = null



                });
                startMonth = startMonth.AddMonths(1);
            }
            await _unitOfWork.CompleteAsync();



            return new()
            {
                Msg = _sharLocalizer[Localization.Done],
                Check = true,
                Data = null
            };
        }

        public async Task<Response<string>> DeleteLoanAsync(int id)
        {

            var obj = await _unitOfWork.LoanRepository.GetByIdAsync(id);

            if (obj == null)
            {
                string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                    _sharLocalizer[Localization.Loan]);

                return new()
                {
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            var details = await _unitOfWork.TransLoanDetails.GetSpecificSelectAsync(x => x.TransLoanId == id, x => x);
            _unitOfWork.TransLoanDetails.RemoveRange(details);
            await _unitOfWork.CompleteAsync();
            _unitOfWork.LoanRepository.Remove(obj);
            await _unitOfWork.CompleteAsync();

            return new()
            {
                Check = true,
                Data = string.Empty,
                Msg = _sharLocalizer[Localization.Deleted]
            };
        }

        public async Task<Response<GetAllLoansResponse>> GetAllLoanAsync(string lang, GetAllFilltrationForLoanRequest model, string host)
        {
            Expression<Func<TransLoan, bool>> filter = x => x.IsDeleted == model.IsDeleted &&
                                                           (string.IsNullOrEmpty(model.Word) || x.LoanDate.ToString() == model.Word);
            var totalRecords = await _unitOfWork.LoanRepository.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllLoansResponse
            {
                TotalRecords = await _unitOfWork.LoanRepository.CountAsync(filter: filter),

                Items = (await _unitOfWork.LoanRepository.GetSpecificSelectAsync(filter: filter,
                     take: model.PageSize,
                     skip: (model.PageNumber - 1) * model.PageSize,
                     select: x => new ListOfLoansResponse
                     {
                         Id = x.Id,
                         EmployeeName = Localization.Arabic == lang ? x.HrEmployee.FirstNameAr : x.HrEmployee.FirstNameEn,

                         LoanDate = x.LoanDate,
                         LoanAmount = x.LoanAmount,
                         StartCalculationDate = x.StartCalculationDate,
                         EndCalculationDate = x.EndCalculationDate,
                         StartLoanDate = x.StartLoanDate,
                         EndDoDate = x.EndDoDate,
                         DocumentDate = x.DocumentDate,
                         AdvanceType = x.AdvanceType,
                         MonthlyDeducted = x.MonthlyDeducted,
                         InstallmentCount = x.InstallmentCount,
                         Notes = x.Notes,
                         LoanType = x.LoanType,

                         PrevDedcutedAmount = x.PrevDedcutedAmount,



                     }, orderBy: x =>
                       x.OrderByDescending(x => x.Id), includeProperties: "HrEmployee")).ToList(),
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

        public async Task<Response<GetLoanByIdReponse>> GetLoanByIdAsync(int id, string lang)
        {
            var obj = (await _unitOfWork.LoanRepository.GetSpecificSelectAsync(x => x.Id == id, x => x, includeProperties: "TransLoanDetails")).FirstOrDefault();
            var empolyee = await _unitOfWork.Employees.GetByIdAsync(obj.EmployeeId);
            var empolyees = await _unitOfWork.Employees.GetAllAsync();
            var advancedTypes = await _unitOfWork.AdvancedTypesRepository.GetAllAdvancedTypes();

            if (obj is null)
            {
                string resultMsg = _sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = new()
                {
                    Id = id,
                    EmployeeId = obj.EmployeeId,
                    LoanDate = obj.LoanDate,
                    StartLoanDate = obj.StartLoanDate,
                    EndDoDate = obj.EndDoDate,
                    DocumentDate = obj.DocumentDate,
                    LoanType = obj.LoanType switch
                    {
                        1 => Localization.Arabic == lang ? "أنشاء سند دفع" : "Create Payment Voucher ",
                        2 => Localization.Arabic == lang ? " تخصم من الراتب" : "Deducted From Salary "

                    },
                    MonthlyDeducted = obj.MonthlyDeducted,
                    LoanAmount = obj.LoanAmount,
                    PrevDedcutedAmount = obj.PrevDedcutedAmount,
                    InstallmentCount = obj.InstallmentCount,
                    StartCalculationDate = obj.StartCalculationDate,
                    EndCalculationDate = obj.EndCalculationDate,
                    AdvanceType = obj.AdvanceType,
                    Notes = obj.Notes,
                    EmployeeName = Localization.Arabic == lang ? empolyee.FirstNameAr : empolyee.FirstNameEn,
                    TransLoanDetails = obj.TransLoanDetails.Select(x => new TransLoanDetailsReponse
                    {
                        Amount = x.Amount,
                        Id = x.Id,
                        DeductionDate = x.DeductionDate,
                        DelayCount = x.DelayCount,
                        PaymentDate = x.PaymentDate,
                    }),
                    TransLoanslookups = new TransLoanslookups
                    {
                        AdvancedTypes = advancedTypes,
                        HrEmployees = empolyees.Select(x => new EmployeeLookup
                        {
                            EmployeeName = Localization.Arabic == lang ? x.FirstNameAr : x.FirstNameEn,
                            Id = x.Id,
                        })
                    }
                },
                Check = true
            };
        }
        public async Task<Response<TransLoanslookups>> GetDeductionsLookUpsData(string lang)
        {
            try
            {
                var employees = await unitOfWork.Employees.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                    select: x => new EmployeeLookup
                    {
                        Id = x.Id,
                        EmployeeName = lang == Localization.Arabic ? x.FullNameAr : x.FullNameEn
                    });

                var advancesTypes = await unitOfWork.AdvancedTypesRepository.GetAllAdvancedTypes();




                return new Response<TransLoanslookups>()
                {
                    Check = true,
                    IsActive = true,
                    Error = "",
                    Msg = "",
                    Data = new TransLoanslookups()
                    {
                        HrEmployees = employees.ToArray(),
                        AdvancedTypes = advancesTypes.ToArray(),

                    }
                };
            }
            catch (Exception exception)
            {
                return new Response<TransLoanslookups>()
                {
                    Error = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                    Msg = "Can not able to Get Data",
                    Check = false,
                    Data = null,
                    IsActive = false
                };
            }

        }



        public async Task<Response<object>> RestoreLoanAsync(int id, string lang)
        {
            var obj = await _unitOfWork.LoanRepository.GetByIdAsync(id);

            if (obj == null)
            {
                string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                    _sharLocalizer[Localization.Loan]);

                return new()
                {
                    Data = null,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            obj.IsDeleted = false;

            unitOfWork.LoanRepository.Update(obj);

            await _unitOfWork.CompleteAsync();
            return new()
            {
                Data = obj,
                Check = true,
                Error = string.Empty,
                LookUps = null,
                Msg = sharLocalizer[Localization.Restored]



            };
        }

        public Task<Response<string>> UpdateActiveOrNotLoanAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<UpdateLoanReponse>> UpdateLoanAsync(int id, UpdateLoanRequest model, string lang)
        {
            Expression<Func<TransLoan, bool>> filter = x => x.Id == id;
            var obj = (await _unitOfWork.LoanRepository.GetSpecificSelectAsync(filter: filter, select: x => x, includeProperties: "TransLoanDetails")).FirstOrDefault();
            var empolyee = await _unitOfWork.Employees.GetByIdAsync(model.EmployeeId);

            if (empolyee is null)
            {
                string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                    _sharLocalizer[Localization.Employee]);

                return new()
                {
                    Data = null,
                    Error = resultMsg,
                    Msg = resultMsg
                };

            }

            if (obj == null)
            {
                string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                    _sharLocalizer[Localization.Loan]);

                return new()
                {
                    Data = null,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            _mapper.Map(model, obj);

            var startMonth = model.StartCalculationDate;
            var objOfDetails = await _unitOfWork.TransLoanDetails.GetSpecificSelectAsync(x => x.TransLoanId == id, x => x);
            _unitOfWork.TransLoanDetails.RemoveRange(objOfDetails);
            await _unitOfWork.CompleteAsync();
            for (int i = 1; i <= model.InstallmentCount; i++)
            {
                await _unitOfWork.TransLoanDetails.AddAsync(new TransLoanDetails
                {
                    DeductionDate = startMonth,
                    Amount = model.MonthlyDeducted,
                    PaymentDate = null,
                    TransLoanId = id


                });

                startMonth = startMonth.AddMonths(1);
            }
            await _unitOfWork.CompleteAsync();

            _unitOfWork.LoanRepository.Update(obj);
            await _unitOfWork.CompleteAsync();


            return new()
            {
                Check = true,
                Data = null,
                Msg = _sharLocalizer[Localization.Updated]
            };
        }

        public async Task<Response<PayForLoanDetailsResponse>> PayForLoanDetails(PayForLoanDetailsRequest model, int id)
        {
            var obj = await _unitOfWork.TransLoanDetails.GetByIdAsync(id);
            if (obj == null)
            {

                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = _sharLocalizer[Localization.NotFoundData]
                };


            }
            obj.PaymentDate = model.PaymentDate;
            _unitOfWork.TransLoanDetails.Update(obj);
            await _unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = new()
                {
                    Id = obj.Id,
                    Amount = obj.Amount,
                    DeductionDate = obj.DeductionDate,
                    PaymentDate = obj.PaymentDate,
                    DelayCount = obj.DelayCount



                },
                Msg = _sharLocalizer[Localization.PaidSuccessfuly]
            };
        }

        public async Task<Response<DelayForTransLoanResponse>> DelayForLoanDetails(DelayForTransLoanRequest model, int id)
        {
            var obj = await _unitOfWork.TransLoanDetails.GetByIdAsync(id);
            if (obj == null)
            {

                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = _sharLocalizer[Localization.NotFoundData]
                };


            }
            obj.DeductionDate = model.DeductionDate;
            obj.DelayCount += 1;

            _unitOfWork.TransLoanDetails.Update(obj);
            await _unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = new DelayForTransLoanResponse
                {
                    Id = obj.Id,
                    DeductionDate = obj.DeductionDate,
                    PaymentDate = obj.PaymentDate,
                    Amount = obj.Amount,
                    DelayCount = obj.DelayCount

                },
                Msg = _sharLocalizer[Localization.DelayedSuccessfully]
            };
        }

        public async Task<Response<IEnumerable<ReInstallmentResponse>>> ReInstallmentAsync(ReInstallmentRequest request, int id)
        {
            var loanDetail = await _unitOfWork.TransLoanDetails.GetSpecificSelectAsync(x => x.TransLoanId == id, x => x);
            if (loanDetail == null)
            {
                return new()
                {
                    Check = false,
                    Data = null,
                    Msg = _sharLocalizer[Localization.NotFoundData]
                };

            }

            await _unitOfWork.TransLoanDetails.ExecuteDeleteAsync(x => x.TransLoanId == id && x.PaymentDate == null);
            await _unitOfWork.CompleteAsync();
            var startMonth = request.StartCalculationDate;
            var monthlyDeducted = request.InstallmentCount != 0 ? request.RestAmount / (decimal)request.InstallmentCount : 0m;

            for (int i = 1; i <= request.InstallmentCount; i++)
            {
                await _unitOfWork.TransLoanDetails.AddAsync(new TransLoanDetails
                {
                    DeductionDate = startMonth,
                    Amount = monthlyDeducted,
                    PaymentDate = null,
                    TransLoanId = id


                });

                startMonth = startMonth.AddMonths(1);
            }

            await _unitOfWork.CompleteAsync();
            var loanDetails = await _unitOfWork.TransLoanDetails.GetSpecificSelectAsync(x => x.TransLoanId == id && x.PaymentDate == null, x => x);


            return new()
            {
                Check = true,
                Data = loanDetails.Select(x => new ReInstallmentResponse
                {
                    DelayCount = x.DelayCount,
                    Id = x.Id,
                    TransLoanId = x.TransLoanId,
                    Amount = x.Amount,
                    PaymentDate = x.PaymentDate,
                    DeductionDate = x.DeductionDate
                })
                ,
                Msg = _sharLocalizer[Localization.Updated]
            };
        }
    }
}
