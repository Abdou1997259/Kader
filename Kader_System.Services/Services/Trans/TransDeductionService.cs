using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Response;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.Trans
{
    public class TransDeductionService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITransDeductionService
    {
        private TransDeduction _insatance;
        public async Task<Response<IEnumerable<SelectListOfTransDeductionResponse>>> ListOfTransDeductionsAsync(string lang)
        {
            var result =
                await unitOfWork.TransDeductions.GetSpecificSelectAsync(null!,
                    includeProperties: $"{nameof(_insatance.Deduction)},{nameof(_insatance.Employee)},{nameof(_insatance.SalaryEffect)}" +
                                       $",{nameof(_insatance.AmountType)}",
                    select: x => new SelectListOfTransDeductionResponse
                    {
                        Id = x.Id,
                        ActionMonth = x.ActionMonth,
                        SalaryEffect = lang == Localization.Arabic ? x.SalaryEffect!.Name : x.SalaryEffect!.NameInEnglish,
                        AddedOn = x.Add_date,
                        DeductionId = x.DeductionId,
                        DeductionName = lang == Localization.Arabic ? x.Deduction!.Name_ar : x.Deduction!.Name_en,
                        Amount = x.Amount,
                        EmployeeId = x.EmployeeId,
                        EmployeeName = lang == Localization.Arabic ? x.Employee!.FullNameAr : x.Employee!.FullNameEn,
                        Notes = x.Notes,
                        SalaryEffectId = x.SalaryEffectId,
                        AmountTypeId = x.AmountTypeId,
                        ValueTypeName = lang == Localization.Arabic ? x.AmountType!.Name : x.AmountType!.NameInEnglish
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

        public async Task<Response<GetAllTransDeductionResponse>> GetAllTransDeductionsAsync(string lang,
            GetAllFilterationForTransDeductionRequest model,string host)
        {
            Expression<Func<TransDeduction, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                                                 && (string.IsNullOrEmpty(model.Word) || x.ActionMonth.ToString().Contains(model.Word)
                                                                     || x.AmountType!.Name.Contains(model.Word) || x.AmountType!.NameInEnglish.Contains(model.Word)
                                                                     || x.Deduction!.Name_en.Contains(model.Word)
                                                                     || x.Deduction!.Name_ar.Contains(model.Word)
                                                                     || x.Employee!.FullNameEn.Contains(model.Word)
                                                                     || x.Employee!.FullNameAr.Contains(model.Word))
                ;
            Expression<Func<TransDeductionData, bool>> filterSearch = x =>
                (string.IsNullOrEmpty(model.Word)
                 || x.DeductionName.Contains(model.Word)
                 || x.EmployeeName.Contains(model.Word)
                 || x.DiscountType.Contains(model.Word));

            var totalRecords = await unitOfWork.TransDeductions.CountAsync(filter: filter,
                includeProperties: $"{nameof(_insatance.Deduction)},{nameof(_insatance.Employee)}," +
                                   $"{nameof(_insatance.SalaryEffect)}" +
                                   $",{nameof(_insatance.AmountType)}");
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllTransDeductionResponse
            {
                TotalRecords = totalRecords,

                Items =unitOfWork.TransDeductions.GetTransDeductionInfo(filter:filter, filterSearch:filterSearch, skip: (model.PageNumber - 1) * model.PageSize
                    ,take: model.PageSize,lang:lang)
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


        public async Task<Response<DeductionLookUps>> GetDeductionsLookUpsData(string lang)
        {
            try
            {
                var employees = await unitOfWork.Employees.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                    select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.FullNameAr : x.FullNameEn
                    });

                var deductions = await unitOfWork.Deductions.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                    select: x => new
                    {
                        Id=x.Id,
                        Name=lang==Localization.Arabic?x.Name_ar:x.Name_en
                    });

                var salaryEffect = await unitOfWork.TransSalaryEffects.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                    select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name : x.NameInEnglish,

                    });
                var amountType = await unitOfWork.TransAmountTypes.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                    select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name : x.NameInEnglish,

                    });
                
                return new Response<DeductionLookUps>()
                {
                    Check = true,
                    IsActive = true,
                    Error = "",
                    Msg = "",
                    Data = new DeductionLookUps()
                    {
                       deductions = deductions.ToArray(),
                       employees = employees.ToArray(),
                       salary_effects = salaryEffect.ToArray(),
                       trans_amount_types = amountType.ToArray()
                    }
                };
            }
            catch (Exception exception)
            {
                return new Response<DeductionLookUps>()
                {
                    Error = exception.InnerException!=null ? exception.InnerException.Message:exception.Message,
                    Msg = "Can not able to Get Data",
                    Check = false,
                    Data = null,
                    IsActive = false
                };
            }

        }

        public async Task<Response<CreateTransDeductionRequest>> CreateTransDeductionAsync(CreateTransDeductionRequest model)
        {
            var newTrans = mapper.Map<TransDeduction>(model);

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

            await unitOfWork.TransDeductions.AddAsync(newTrans);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        public async Task<Response<GetTransDeductionById>> GetTransDeductionByIdAsync(int id,string lang)
        {
            var obj = await unitOfWork.TransDeductions.GetFirstOrDefaultAsync(d=>d.Id==id,
                includeProperties: $"{nameof(_insatance.Deduction)},{nameof(_insatance.Employee)}," +
                                   $"{nameof(_insatance.SalaryEffect)}" +
                                   $",{nameof(_insatance.AmountType)}");

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

            return new()
            {
                Data = new GetTransDeductionById()
                {
                    ActionMonth = obj.ActionMonth,
                    AddedOn = obj.Add_date,
                    DeductionId = obj.DeductionId,
                    DeductionName =lang==Localization.Arabic? obj.Deduction!.Name_ar:obj.Deduction!.Name_en,
                    EmployeeId = obj.EmployeeId,
                    EmployeeName = lang==Localization.Arabic?obj.Employee!.FullNameAr:obj.Employee.FullNameEn,
                    Id = obj.Id,
                    SalaryEffect = lang==Localization.Arabic?obj.SalaryEffect!.Name: obj.SalaryEffect!.NameInEnglish,
                    SalaryEffectId = obj.SalaryEffectId,
                    Notes = obj.Notes,
                    AmountTypeId = obj.AmountTypeId,
                    Amount = obj.Amount,
                    discount_type =lang==Localization.Arabic ? obj.AmountType.Name:obj.AmountType.NameInEnglish,
                    
                },
                Check = true
            };
        }

        public async Task<Response<GetTransDeductionById>> UpdateTransDeductionAsync(int id, CreateTransDeductionRequest model)
        {
            var obj = await unitOfWork.TransDeductions.GetByIdAsync(id);
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
            obj.AmountTypeId=model.AmountTypeId;
            obj.SalaryEffectId=model.SalaryEffectId;
            obj.DeductionId=model.DeductionId;
            obj.ActionMonth=model.ActionMonth;
            obj.Notes=model.Notes;
            obj.EmployeeId=model.EmployeeId;
            unitOfWork.TransDeductions.Update(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = mapper.Map<GetTransDeductionById>(obj)
            };
        }

        public async Task<Response<object>> RestoreTransDeductionAsync(int id)
        {
            var obj = await unitOfWork.TransDeductions.GetByIdAsync(id);

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

            unitOfWork.TransDeductions.Update(obj);
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


        public Task<Response<string>> UpdateActiveOrNotTransDeductionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> DeleteTransDeductionAsync(int id)
        {
            var obj = await unitOfWork.TransDeductions.GetByIdAsync(id);
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
                ManageFilesHelper.RemoveFile(GoRootPath.TransFilesPath+obj.Attachment);
            }

            unitOfWork.TransDeductions.Remove(obj);
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
