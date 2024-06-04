using Kader_System.Domain.DTOs;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.Trans
{
    public class TransCovenantService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITransCovenantService
    {
        private TransCovenant _insatance;
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
            Expression<Func<TransCovenant, bool>> filter = x => x.IsDeleted == model.IsDeleted
                && (string.IsNullOrEmpty(model.Word)
                   || x.NameAr.Contains(model.Word)
                   || x.NameEn.Contains(model.Word)
                   || x.Employee!.FullNameAr.Contains(model.Word)
                   || x.Employee!.FullNameEn.Contains(model.Word));

            Expression<Func<TransCovenantData, bool>> filterSearch = x =>
                (string.IsNullOrEmpty(model.Word)
                 || x.NameAr.Contains(model.Word)
                 || x.EmployeeName.Contains(model.Word)
                 || x.NameEn.Contains(model.Word)
                 || x.EmployeeName.Contains(model.Word)
                 || x.AddedBy!.Contains(model.Word));

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
            var result = new GetAllTransCovenantResponse
            {
                TotalRecords = totalRecords,

                Items = unitOfWork.TransCovenants.GetTransCovenantDataInfo(filter: filter, filterSearch: filterSearch,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    take: model.PageSize, lang: lang)
               ,
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

        public async Task<Response<CreateTransCovenantRequest>> CreateTransCovenantAsync(CreateTransCovenantRequest model)
        {
            var newTrans = mapper.Map<TransCovenant>(model);

            if (!string.IsNullOrEmpty(model.Attachment))
            {
                var fileNameAndExt = ManageFilesHelper.SaveBase64StringToFile(model.Attachment!, GoRootPath.TransFilesPath, model.FileName!);
                if (fileNameAndExt != null)
                {
                    newTrans.Attachment = fileNameAndExt.FileName;
                    newTrans.AttachmentExtension = fileNameAndExt.FileExtension;
                }
                else
                {
                    newTrans.Attachment = null;
                    newTrans.AttachmentExtension = null;
                }
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

        public async Task<Response<GetTransCovenantById>> GetTransCovenantByIdAsync(int id,string lang)
        {
            var obj = await unitOfWork.TransCovenants.GetFirstOrDefaultAsync(c=>c.Id==id, 
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

            var lookups =await unitOfWork.Employees.GetEmployeesDataNameAndIdAsLookUp(lang);
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
                    EmployeeName =lang==Localization.Arabic? obj.Employee!.FullNameAr:obj.Employee!.FullNameEn,
                    Id = obj.Id
                },
                Check = true,
                LookUps =new
                {
                    employees= lookups
                } 
            };
        }

        public async Task<Response<CreateTransCovenantRequest>> UpdateTransCovenantAsync(int id, CreateTransCovenantRequest model)
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

            if (!string.IsNullOrEmpty(obj.Attachment))
            {
                ManageFilesHelper.RemoveFile(Path.Combine(GoRootPath.TransFilesPath, obj.Attachment));
            }

            if (!string.IsNullOrEmpty(model.Attachment))
            {
                var fileNameAndExt = ManageFilesHelper.SaveBase64StringToFile(model.Attachment!, GoRootPath.TransFilesPath, model.FileName!);

                obj.Attachment = fileNameAndExt?.FileName;
                obj.AttachmentExtension = fileNameAndExt?.FileExtension;

            }
            else
            {
                obj.Attachment = null;
                obj.AttachmentExtension = null;
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
