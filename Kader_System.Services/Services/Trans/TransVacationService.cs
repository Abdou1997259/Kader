using Kader_System.Domain.DTOs;



namespace Kader_System.Services.Services.Trans
{
    public class TransVacationService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITransVacationService
    {
        private TransVacation _insatance;
        public async Task<Response<IEnumerable<SelectListOfTransVacationResponse>>> ListOfTransVacationsAsync(string lang)
        {
            var result =
                await unitOfWork.TransVacations.GetSpecificSelectAsync(null!,
                    includeProperties: $"{nameof(_insatance.Vacation)},{nameof(_insatance.Employee)}" 
                                       ,
                    select: x => new SelectListOfTransVacationResponse
                    {
                        Id = x.Id,
                        StartDate = x.StartDate,
                        DaysCount = x.DaysCount,
                        VacationId = x.VacationId,
                        VacationName = lang == Localization.Arabic ? x.Vacation!.NameAr : x.Vacation!.NameEn,
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

        public async Task<Response<GetAllTransVacationResponse>> GetAllTransVacationsAsync(string lang, 
            GetAllFilterationForTransVacationRequest model,string host)
        {
            Expression<Func<TransVacation, bool>> filter = x => x.IsDeleted == model.IsDeleted
                && (string.IsNullOrEmpty(model.Word)
                || x.Employee!.FullNameEn.Contains(model.Word)
                || x.Employee!.FullNameAr.Contains(model.Word)
                || x.Vacation!.NameAr.Contains(model.Word)
                || x.Vacation!.NameEn.Contains(model.Word));

            Expression<Func<TransVacationData, bool>> filterSearch = x =>
                (string.IsNullOrEmpty(model.Word)
                 || x.EmployeeName.Contains(model.Word)
                 || x.VacationName.Contains(model.Word)
                 || x.EmployeeName.Contains(model.Word)
                 || x.AddedBy!.Contains(model.Word));

            var totalRecords = await unitOfWork.TransVacations.CountAsync(filter: filter,
                includeProperties: $"{nameof(_insatance.Vacation)},{nameof(_insatance.Employee)}");
          
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllTransVacationResponse
            {
                TotalRecords = totalRecords,

                Items = unitOfWork.TransVacations.GetTransVacationInfo(filter:filter,filterSearch:filterSearch,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    take: model.PageSize,lang:lang),
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
        public async Task<Response<TransVacationLookUpsData>> GetTransVacationLookUpsData(string lang)
        {
            try
            {

                return await unitOfWork.TransVacations.GetTransVacationLookUpsData(lang);

                //var employees = await unitOfWork.Employees.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                //    select: x => new
                //    {
                //        Id = x.Id,
                //        Name = lang == Localization.Arabic ? x.FullNameAr : x.FullNameEn,
                        
                //    });

                //var vacations = await unitOfWork.Vacations.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                //    select: x => new
                //    {
                //        Id = x.Id,
                //        Name = lang == Localization.Arabic ? x.NameAr : x.NameEn
                //    });

                //return new Response<TransVacationLookUpsData>()
                //{
                //    Check = true,
                //    IsActive = true,
                //    Error = "",
                //    Msg = "",
                //    Data = new TransVacationLookUpsData()
                //    {
                //        vacations = vacations.ToArray(),
                //        employees = employees.ToArray(),
                      
                //    }
                //};
            }
            catch (Exception exception)
            {
                return new Response<TransVacationLookUpsData>()
                {
                    Error = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                    Msg = "Can not able to Get Data",
                    Check = false,
                    Data = null,
                    IsActive = false
                };
            }

        }


        public async Task<Response<CreateTransVacationRequest>> CreateTransVacationAsync(CreateTransVacationRequest model)
        {

            var newTrans = mapper.Map<TransVacation>(model);

            var usedDays = 
                await unitOfWork.TransVacations.GetVacationDaysUsedByEmployee(model.EmployeeId, model.VacationId);
            var totalBalance =
                await unitOfWork.TransVacations.GetVacationTotalBalance( model.VacationId);
            var reminderDays = totalBalance - usedDays;
            if (model.DaysCount > reminderDays)
            {
                return new()
                {
                    Error = string.Empty,
                    Check = false,
                    Data = model,
                    LookUps = null,
                    Msg = sharLocalizer[Localization.BalanceNotEnough],
                    
                };
            }

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

            await unitOfWork.TransVacations.AddAsync(newTrans);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        public async Task<Response<GetTransVacationById>> GetTransVacationByIdAsync(int id,string lang)
        {
            var obj = await unitOfWork.TransVacations.GetTransVacationByIdAsync(id, lang);

            if (obj ==null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = obj,
                Check = true
            };
        }

        public async Task<Response<GetTransVacationById>> UpdateTransVacationAsync(int id, CreateTransVacationRequest model)
        {
            var obj = await unitOfWork.TransVacations.GetByIdAsync(id);
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
            var usedDays =
                await unitOfWork.TransVacations.GetVacationDaysUsedByEmployee(model.EmployeeId, model.VacationId);
            var totalBalance =
                await unitOfWork.TransVacations.GetVacationTotalBalance(model.VacationId);
            var reminderDays = totalBalance +obj.DaysCount - usedDays;
            if (model.DaysCount > reminderDays)
            {
                return new()
                {
                    Error = string.Empty,
                    Check = false,
                    Data = mapper.Map<GetTransVacationById>(obj),
                    LookUps = null,
                    Msg = sharLocalizer[Localization.BalanceNotEnough],

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

            obj.DaysCount = model.DaysCount;
            obj.StartDate = model.StartDate;
            obj.VacationId = model.VacationId;
            obj.Notes = model.Notes;
            obj.EmployeeId = model.EmployeeId;
            unitOfWork.TransVacations.Update(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = mapper.Map<GetTransVacationById>(obj)
            };
        }

        public Task<Response<string>> UpdateActiveOrNotTransVacationAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Response<object>> RestoreTransVacationAsync(int id)
        {
            var obj = await unitOfWork.TransVacations.GetByIdAsync(id);

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

            unitOfWork.TransVacations.Update(obj);
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

        public async Task<Response<string>> DeleteTransVacationAsync(int id)
        {
            var obj = await unitOfWork.TransVacations.GetByIdAsync(id);
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

            unitOfWork.TransVacations.Remove(obj);
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
