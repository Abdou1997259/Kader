using Kader_System.Domain.DTOs;
using Kader_System.Services.IServices.AppServices;

namespace Kader_System.Services.Services.Trans
{
    public class TransCovenantService(IUnitOfWork unitOfWork, IFileServer fileServer, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITransCovenantService
    {
        private TransCovenant _insatance;
        private IFileServer _fileServer = fileServer;
        public async Task<Response<IEnumerable<SelectListOfCovenantResponse>>> ListOfTransCovenantsAsync(string lang)
        {
            var result =
                await unitOfWork.TransCovenants.GetSpecificSelectAsync(null!,
                    includeProperties: $"{nameof(_insatance.Employee)}",
                    select: x => new SelectListOfCovenantResponse
                    {
                        Id = x.Id,
                        Date = x.Date,
                        NameAr = x.NameAr,
                        NameEn = x.NameEn,
                        Amount = x.Amount,
                        EmployeeId = x.EmployeeId,
                        EmployeeName = lang == Localization.Arabic ? x.Employee!.FullNameAr : x.Employee!.FullNameEn,
                        Notes = x.Notes

                    }, orderBy: x =>
               x.OrderByDescending(x => x.Id));

            if (!result.Any())
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

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

        public async Task<Response<GetAllTransCovenantResponse>> GetAllTransCovenantsAsync(string lang,
            GetAllFilterationForTransCovenant model, string host)
        {
            Expression<Func<TransCovenant, bool>> filter = x =>
            x.IsDeleted == model.IsDeleted &&
            (string.IsNullOrEmpty(model.Word) ||
                x.NameAr.Contains(model.Word) ||
                x.NameEn.Contains(model.Word) ||
                (x.Employee != null && (x.Employee.FullNameAr.Contains(model.Word) || x.Employee.FullNameEn.Contains(model.Word)))) &&
            (!model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId);



            var totalRecords = await unitOfWork.TransCovenants.CountAsync(filter: filter,
                includeProperties: $"{nameof(_insatance.Employee)}");


            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();

            var items = await unitOfWork.TransCovenants.GetSpecificSelectAsync(filter, includeProperties: "Employee,Employee.User,Employee.Job", select: x => new TransCovenantData
            {
                AddedBy = x.Employee.User.FullName,
                AddedOn = x.Add_date,
                Amount = x.Amount,
                EmployeeId = x.EmployeeId,
                EmployeeName = lang == Localization.Arabic ? x.Employee.FullNameAr : x.Employee.FullNameEn,
                Id = x.Id,
                Notes = x.Notes,
                NameAr = x.NameAr,
                NameEn = x.NameEn,
                Date = x.Date,
                JobName = lang == Localization.Arabic ? x.Employee.Job.NameAr : x.Employee.Job.NameEn,

            }, skip: model.PageSize * (model.PageNumber - 1), take: model.PageSize);
            var result = new GetAllTransCovenantResponse
            {
                TotalRecords = totalRecords,
                Items = items.ToList(),
                // Items = unitOfWork.TransCovenants.GetTransCovenantDataInfo(filter: filter, filterSearch: filterSearch,
                //     skip: (model.PageNumber - 1) * model.PageSize,
                //     take: model.PageSize, lang: lang).Where(x => !model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId).ToList()
                //,
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
                string resultMsg = sharLocalizer[Localization.NotFoundData];

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

        public async Task<Response<CreateTransCovenantRequest>> CreateTransCovenantAsync(CreateTransCovenantRequest model, string lang)
        {
            var emp = await unitOfWork.Employees.GetByIdAsync(model.EmployeeId);
            if (emp is null)
            {


                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Employee]]
                };
            }
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x => x.EmployeeId == model.EmployeeId, x => x)).FirstOrDefault();
            if (contract is null)
            {
                string resultMsg = $" {sharLocalizer[Localization.Employee]} {sharLocalizer[Localization.ContractNotFound]}";

                return new()
                {
                    Check = false,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            if (await unitOfWork.TransCovenants.ExistAsync(x => x.NameAr.Trim() == model.NameAr.Trim() ||
            x.NameEn.Trim() == model.NameEn.Trim()))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.AlreadyExitedWithSameName, lang == Localization.Arabic ? model.NameAr : model.NameEn]

                };
            }

            var newTrans = mapper.Map<TransCovenant>(model);

            if (model.Attachment_File is not null)
            {
                var dirType = HrDirectoryTypes.Covenant;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                newTrans.Attachment = await _fileServer.UploadFileAsync(dir, model.Attachment_File);

            }


            await unitOfWork.TransCovenants.AddAsync(newTrans);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        public async Task<Response<GetTransCovenantById>> GetTransCovenantByIdAsync(int id, string lang)
        {
            var obj = await unitOfWork.TransCovenants.GetFirstOrDefaultAsync(c => c.Id == id,
                includeProperties: $"{nameof(_insatance.Employee)}");



            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            var lookups = await unitOfWork.Employees.GetEmployeesDataNameAndIdAsLookUp(lang);
            var dirType = HrDirectoryTypes.Covenant;
            var dir = dirType.GetModuleNameWithType(Modules.Trans);
            return new()
            {
                Data = new GetTransCovenantById()
                {
                    AddedOn = obj.Add_date,
                    Amount = obj.Amount,
                    Date = obj.Date,
                    NameAr = obj.NameAr,
                    NameEn = obj.NameEn,
                    Notes = obj.Notes,
                    EmployeeId = obj.EmployeeId,
                    EmployeeName = lang == Localization.Arabic ? obj.Employee!.FullNameAr : obj.Employee!.FullNameEn,
                    Id = obj.Id,

                },
                Check = true,
                LookUps = new
                {
                    employees = lookups
                }
            };
        }

        public async Task<Response<CreateTransCovenantRequest>> UpdateTransCovenantAsync(int id, CreateTransCovenantRequest model, string lang)
        {
            var obj = await unitOfWork.TransCovenants.GetByIdAsync(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }
            var emp = await unitOfWork.Employees.GetByIdAsync(model.EmployeeId);
            if (emp is null)
            {


                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Employee]]
                };
            }
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x => x.EmployeeId == model.EmployeeId, x => x)).FirstOrDefault();
            if (contract is null)
            {
                string resultMsg = $" {sharLocalizer[Localization.Employee]} {sharLocalizer[Localization.ContractNotFound]}";

                return new()
                {
                    Check = false,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            if (await unitOfWork.TransCovenants.ExistAsync(x => x.Id != id && (x.NameAr.Trim() == model.NameAr.Trim() ||
           x.NameEn.Trim() == model.NameEn.Trim())))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.AlreadyExitedWithSameName, lang == Localization.Arabic ? model.NameAr : model.NameEn]

                };
            }

            if (model.Attachment_File is not null)
            {
                var dirType = HrDirectoryTypes.Covenant;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                if (!string.IsNullOrEmpty(obj?.Attachment))
                    _fileServer.RemoveFile(dir, obj.Attachment);
                obj.Attachment = await _fileServer.UploadFileAsync(dir, model.Attachment_File);
            }



            obj.Amount = model.Amount;
            obj.Date = model.Date;
            obj.NameAr = model.NameAr;
            obj.NameEn = model.NameEn;
            obj.Notes = model.Notes;
            obj.EmployeeId = model.EmployeeId;
            unitOfWork.TransCovenants.Update(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }
        public async Task<Response<object>> RestoreTransCovenantAsync(int id)
        {
            var obj = await unitOfWork.TransCovenants.GetByIdAsync(id);

            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            obj.IsDeleted = false;

            unitOfWork.TransCovenants.Update(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Error = string.Empty,
                Check = true,
                Data = obj,
                LookUps = null,
                Msg = sharLocalizer[Localization.Restored]
            };
        }
        public Task<Response<string>> UpdateActiveOrNotTransCovenantAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> DeleteTransCovenantAsync(int id)
        {
            var obj = await unitOfWork.TransCovenants.GetByIdAsync(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            if (!string.IsNullOrEmpty(obj.Attachment))
            {
                ManageFilesHelper.RemoveFile(GoRootPath.TransFilesPath + obj.Attachment);
            }

            unitOfWork.TransCovenants.Remove(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = string.Empty,
                Msg = sharLocalizer[Localization.Deleted]
            };
        }
    }
}
