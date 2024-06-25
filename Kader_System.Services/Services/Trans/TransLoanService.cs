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


                  }, orderBy: x =>
                    x.OrderByDescending(x => x.Id), includeProperties: "Hr_Employees");


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

        public async Task<Response<CreateLoanReponse>> CreateLoanAsync(CreateLoanRequest model)
        {
            bool exists = false;
            exists = await _unitOfWork.LoanRepository.ExistAsync(x => x.LoanDate == model.LoanDate);
            var empolyee = await _unitOfWork.Employees.GetByIdAsync(model.EmpolyeeId);

            if (exists || empolyee is null)
            {
                string resultMsg = string.Format(_sharLocalizer[Localization.IsExist],
                    _sharLocalizer[Localization.Deduction]);

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            var loan = _mapper.Map<TransLoan>(model);
            await _unitOfWork.LoanRepository.AddAsync(loan
            );
            await _unitOfWork.CompleteAsync();

            var creatingResponse = new CreateLoanReponse
            {
                InstallmentCount = model.InstallmentCount,
                DocumentDate = model.DocumentDate,
                IsDeductedFromSalary = model.IsDeductedFromSalary,
                EndDoDate = model.EndDoDate,
                LoanAmount = model.LoanAmount,
                LoanDate = model.LoanDate,
                MonthlyDeducted = model.MonthlyDeducted,
                PrevDedcutedAmount = model.PrevDedcutedAmount,
                Notes = model.Notes,
                TransLoanslookups = new TransLoanslookups
                {
                    HrEmployees = await _unitOfWork.Employees.GetAllAsync(),
                    AdvancedTypes = await _unitOfWork.AdvancedTypesRepository.GetAllAdvancedTypes()
                }


            };

            return new()
            {
                Msg = _sharLocalizer[Localization.Done],
                Check = true,
                Data = creatingResponse
            };
        }

        public async Task<Response<string>> DeleteLoanAsync(int id)
        {
            var obj = await _unitOfWork.LoanRepository.GetByIdAsync(id);

            if (obj == null)
            {
                string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                    _sharLocalizer[Localization.Laon]);

                return new()
                {
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

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
                                                           (string.IsNullOrEmpty(model.Word) || x.LoanDate == model.LoanDate);
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
                         EmpolyeeName = Localization.Arabic == lang ? x.HrEmployee.FirstNameAr : x.HrEmployee.FirstNameEn

                     }, orderBy: x =>
                       x.OrderByDescending(x => x.Id), includeProperties: "Hr_Employees")).ToList(),
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

        public async Task<Response<GetLoanByIdReponse>> GetLoanByIdAsync(int id)
        {
            var obj = await _unitOfWork.LoanRepository.GetByIdAsync(id);

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
                    LoanDate = obj.LoanDate,

                },
                Check = true
            };
        }



        public async Task<Response<GetLoanByIdReponse>> RestoreLoanAsync(int id)
        {
            var obj = await _unitOfWork.LoanRepository.GetByIdAsync(id);

            if (obj == null)
            {
                string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                    _sharLocalizer[Localization.Laon]);

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
                Check = true,
                Error = string.Empty,
                LookUps = null,
                Data = new GetLoanByIdReponse()
                {
                    Id = obj.Id,
                    LoanDate = obj.LoanDate,

                },
                Msg = string.Format(_sharLocalizer[Localization.Updated],
                    _sharLocalizer[Localization.Deduction]),

            };
        }

        public Task<Response<string>> UpdateActiveOrNotLoanAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<UpdateLoanReponse>> UpdateLoanAsync(int id, UpdateLoanRequest model)
        {
            var obj = await _unitOfWork.LoanRepository.GetByIdAsync(id);
            var empolyee = await _unitOfWork.Employees.GetByIdAsync(model.EmpolyeeId);
            var updateLoanResponse = new UpdateLoanReponse
            {
                InstallmentCount = model.InstallmentCount,
                DocumentDate = model.DocumentDate,
                IsDeductedFromSalary = model.IsDeductedFromSalary,
                EndDoDate = model.EndDoDate,
                LoanAmount = model.LoanAmount,
                LoanDate = model.LoanDate,
                MonthlyDeducted = model.MonthlyDeducted,
                PrevDedcutedAmount = model.PrevDedcutedAmount,
                Notes = model.Notes,
                TransLoanslookups = new TransLoanslookups
                {
                    HrEmployees = await _unitOfWork.Employees.GetAllAsync(),
                    AdvancedTypes = await _unitOfWork.AdvancedTypesRepository.GetAllAdvancedTypes()
                }


            };

            if (obj == null || empolyee is null)
            {
                string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                    _sharLocalizer[Localization.Laon]);

                return new()
                {
                    Data = updateLoanResponse,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            _mapper.Map(model, obj);

            _unitOfWork.LoanRepository.Update(obj);
            await _unitOfWork.CompleteAsync();


            return new()
            {
                Check = true,
                Data = updateLoanResponse,
                Msg = _sharLocalizer[Localization.Updated]
            };
        }

    }
}
