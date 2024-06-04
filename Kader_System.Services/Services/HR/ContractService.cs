
using Kader_System.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Kader_System.Domain.Constants.SD.ApiRoutes;

namespace Kader_System.Services.Services.HR
{
    public class ContractService(
        IUnitOfWork unitOfWork,
        IStringLocalizer<SharedResource> shareLocalizer,
        IMapper mapper) : IContractService
    {
        private HrContract _instanceContract;

        public async Task<Response<IEnumerable<ListOfContractsResponse>>> ListOfContractsAsync(string lang)
        {
            var result =
                await unitOfWork.Contracts.GetSpecificSelectAsync(null!
                    , includeProperties: $"{nameof(_instanceContract.Employee)}",
                    select: x => new ListOfContractsResponse
                    {
                        Id = x.Id,
                        TotalSalary = x.TotalSalary,
                        FixedSalary = x.FixedSalary,
                        EmployeeId = x.EmployeeId,
                        EmployeeName = lang == Localization.Arabic ? x.Employee!.FullNameAr : x.Employee!.FullNameEn,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        HousingAllowance = x.HousingAllowance

                    }, orderBy: x => x.OrderByDescending(x => x.Id));

            var listOfEmployeesResponses = result.ToList();
            if (!listOfEmployeesResponses.Any())
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = [],
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = listOfEmployeesResponses,
                Check = true
            };
        }

        public async Task<Response<GetAllContractsResponse>> GetAllContractAsync(string lang,
            GetAlFilterationForContractRequest model,string host)
        {

            Expression<Func<HrContract, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                                             && (string.IsNullOrEmpty(model.Word) ||
                                                                 x.Employee!.FullNameEn!.Contains(model.Word)
                                                                 || x.Employee!.FullNameAr!.Contains(model.Word)
                                                                 || x.StartDate.ToString().Contains(model.Word)
                                                                 || x.EndDate.ToString().Contains(model.Word));
            var totalRecords = await unitOfWork.Contracts.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllContractsResponse
            {
                TotalRecords = totalRecords,

                Items = ( unitOfWork.Contracts.GetAllContractsAsync
                (contractFilter: filter,
                    lang: lang,
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize)),
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
                string resultMsg = shareLocalizer[Localization.NotFoundData];

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


        public async Task<Response<GetAllContractsResponse>> GetAllEndContractsAsync(string lang,
            GetAlFilterationForContractRequest model, string host)
        {

            Expression<Func<HrContract, bool>> filter = x => x.IsDeleted == model.IsDeleted

                                                             && x.EndDate < DateOnly.FromDateTime(DateTime.UtcNow)
                                                             && x.IsDeleted == model.IsDeleted
                                                             && (string.IsNullOrEmpty(model.Word) ||
                                                                 x.Employee!.FullNameEn!.Contains(model.Word)
                                                                 || x.Employee!.FullNameAr!.Contains(model.Word)
                                                                 || x.StartDate.ToString().Contains(model.Word)
                                                                 || x.EndDate.ToString().Contains(model.Word));
            var totalRecords = await unitOfWork.Contracts.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllContractsResponse
            {
                TotalRecords = totalRecords,

                Items = (unitOfWork.Contracts.GetAllContractsAsync
                (contractFilter: filter,
                    lang: lang,
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize)),
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
                string resultMsg = shareLocalizer[Localization.NotFoundData];

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

        public async Task<Response<GetContractByIdResponse>> GetContractByIdAsync(int id,string lang)
        {
            //Expression<Func<HrContract, bool>> filter = x => x.Id == id;
            //var obj = await unitOfWork.Contracts.GetFirstOrDefaultAsync(filter,
            //    includeProperties:
            //    $"{nameof(_instanceContract.Employee)},{nameof(_instanceContract.ListOfAllowancesDetails)}");

            var obj = unitOfWork.Contracts.GetContractById(id, lang);


            if (obj is null)
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

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
                    Master = obj,
                    allowances =await unitOfWork.Allowances.GetAllowancesDataAsLookUp(lang:lang),
                    employees = await unitOfWork.Employees.GetEmployeesDataAsLookUp(lang)
                },
                Check = true
            };
        }


        public async Task<Response<object> > GetLookUps(string lang)
        {
            var employees =await unitOfWork.Employees.GetEmployeesDataAsLookUp(lang);
            var allowances = await unitOfWork.Allowances.GetAllowancesDataAsLookUp(lang);
            return new()
            {
                Check = true,
                Error = string.Empty,
                Msg = string.Empty,
                Data = new
                {
                    employees,
                    allowances
                },
                IsActive = true
              
            };
        }

        public async Task<Response<CreateContractRequest>> CreateContractAsync(CreateContractRequest model)
        {
            var newContract = new HrContract()
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                FixedSalary = model.FixedSalary,
                EmployeeId = model.EmployeeId,
                HousingAllowance = model.HousingAllowance,


            };
            if (model.Details != null)
            {
                newContract.ListOfAllowancesDetails =

                    model.Details.Select(d => new HrContractAllowancesDetail()
                    {
                        AllowanceId = d.AllowanceId,
                        Value = d.Value,
                        IsPercent = d.IsPercent
                    }).ToList()

                ;
            }

            GetFileNameAndExtension contractFile = new();
            if (!string.IsNullOrEmpty(model.ContractFile))
            {

                contractFile = ManageFilesHelper.SaveBase64StringToFile(model.ContractFile, GoRootPath.HRFilesPath, model.FileName);
            }

            newContract.FileName = contractFile?.FileName;
            newContract.FileExtension = contractFile?.FileExtension;
            await unitOfWork.Contracts.AddAsync(newContract);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Msg = string.Format(shareLocalizer[Localization.Done],
                    shareLocalizer[Localization.Contract]),
                Check = true,
                Data = model
            };

        }


        public Task<Response<string>> UpdateActiveOrNotContractAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<CreateContractRequest>> UpdateContractAsync(int id, CreateContractRequest model)
        {
            using var transaction = unitOfWork.BeginTransaction();
            {
               
                var obj = await unitOfWork.Contracts.GetByIdAsync(id);
                if (obj is null)
                {
                    string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                        shareLocalizer[Localization.Contract]);
                    return new()
                    {
                        Check = false,
                        Data = model,
                        Error = resultMsg,
                        Msg = resultMsg
                    };
                }

                if (!model.FileName.Contains(GoRootPath.HRFilesPath)  || model.ContractFile==null)
                {
                    if (!string.IsNullOrEmpty(obj.FileName))
                    {
                        ManageFilesHelper.RemoveFile(Path.Combine(GoRootPath.HRFilesPath, obj.FileName));
                    }
                }
                

                obj.EmployeeId = model.EmployeeId;
                obj.EndDate = model.EndDate;
                obj.StartDate = model.StartDate;
                obj.TotalSalary = model.TotalSalary;
                obj.FixedSalary = model.FixedSalary;
                obj.HousingAllowance = model.HousingAllowance;


                var lstNewInserted = model.Details?.Where(d => d.Status == RowStatus.Inserted).ToList();
                var lstUpdatedDetails = model.Details?.Where(d => d.Status == RowStatus.Updated).ToList();
                var lstDeletedDetails = model.Details?.Where(d => d.Status == RowStatus.Deleted).ToList();
                //Deleted Items
                if (lstDeletedDetails != null && lstDeletedDetails.Any())
                {
                    foreach (var deletedRow in lstDeletedDetails)
                    {
                        var existObj = await unitOfWork.ContractAllowancesDetails.GetByIdAsync(deletedRow.Id);
                        if (existObj != null)
                        {
                            unitOfWork.ContractAllowancesDetails.Remove(existObj);
                        }
                        else
                        {
                            return new()
                            {
                                Msg = $"Contract Detail with Id:{deletedRow.Id} Can not be found !!!!",
                                Check = false,
                                Data = model
                            };
                        }
                    }
                }
                //New Details Inserted
                if (lstNewInserted != null && lstNewInserted.Any())
                {
                    foreach (var newItem in lstNewInserted)
                    {
                        await unitOfWork.ContractAllowancesDetails.AddAsync(new HrContractAllowancesDetail()
                        {
                            AllowanceId = newItem.AllowanceId,
                            Value = newItem.Value,
                            ContractId = id,
                            IsPercent = newItem.IsPercent
                        });
                    }
                }
                if (lstUpdatedDetails != null && lstUpdatedDetails.Any())
                {
                    foreach (var updateItem in lstUpdatedDetails)
                    {
                        var existObj = await unitOfWork.ContractAllowancesDetails.GetByIdAsync(updateItem.Id);
                        if (existObj != null)
                        {
                            existObj.AllowanceId = updateItem.AllowanceId;
                            existObj.IsPercent = updateItem.IsPercent;
                            existObj.Value = updateItem.Value;
                            existObj.ContractId = id;
                            unitOfWork.ContractAllowancesDetails.Update(existObj);
                        }
                        else
                        {
                            return new()
                            {
                                Msg = $"Contract Detail with Id:{updateItem.Id} Can not be found to Update It !!!!",
                                Check = false,
                                Data = model
                            };
                        }
                    }
                }

                GetFileNameAndExtension contractFile = new();
                if (model.ContractFile is not null)
                {

                    if (model.ContractFile != null)
                    {
                        contractFile = ManageFilesHelper.SaveBase64StringToFile(model.ContractFile, GoRootPath.HRFilesPath, model.FileName);
                    }
                    obj.FileName = contractFile?.FileName;
                    obj.FileExtension = contractFile?.FileExtension;

                }

               
                unitOfWork.Contracts.Update(obj);
                await unitOfWork.CompleteAsync();
                transaction.Commit();
                return new()
                {
                    Msg = string.Format(shareLocalizer[Localization.Done],
                        shareLocalizer[Localization.Contract]),
                    Check = true,
                    Data = model
                };
            }
           

        }


        public async Task<Response<CreateContractRequest>> RestoreContractAsync(int id)
        {
            using var transaction = unitOfWork.BeginTransaction();
            try
            {

                var obj = await unitOfWork.Contracts.GetFirstOrDefaultAsync(c => c.Id == id,
                    includeProperties: $"{nameof(_instanceContract.ListOfAllowancesDetails)}");
                if (obj is null)
                {
                    string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                        shareLocalizer[Localization.Contract]);
                    return new()
                    {
                        Check = false,
                        Data = null,
                        Error = resultMsg,
                        Msg = resultMsg
                    };
                }

                obj.IsDeleted = false;
                if (obj != null && obj.ListOfAllowancesDetails.Any())
                {
                    foreach (var detail in obj.ListOfAllowancesDetails.ToList())
                    {
                        detail.IsDeleted = false;
                    }

                    unitOfWork.ContractAllowancesDetails.UpdateRange(obj.ListOfAllowancesDetails);
                }



                unitOfWork.Contracts.Update(obj);
                await unitOfWork.CompleteAsync();
                transaction.Commit();
                return new()
                {
                    Msg = string.Format(shareLocalizer[Localization.Done],
                        shareLocalizer[Localization.Contract]),
                    Check = true,
                    Data = new()
                    {
                        EmployeeId = obj.EmployeeId,
                        StartDate = obj.StartDate,
                        EndDate = obj.EndDate,
                        FixedSalary = obj.FixedSalary,
                        HousingAllowance = obj.HousingAllowance,
                        TotalSalary = obj.TotalSalary,
                    }
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    Msg = ex.Message,
                    Check = true,
                    Data = null,
                    Error = ex.InnerException!=null ? ex.InnerException.Message :ex.Message
                };
            }


        }

        public async Task<Response<string>> DeleteContractAsync(int id)
        {
            using var transaction = unitOfWork.BeginTransaction();
            try
            {
                var contractExist = await unitOfWork.Contracts.GetByIdAsync(id);
                if (contractExist is null)
                {
                    string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                        shareLocalizer[Localization.Contract]);
                    return new()
                    {
                        Check = false,
                        Error = resultMsg,
                        Msg = resultMsg,
                        Data = string.Empty
                    };
                }

                if (!string.IsNullOrEmpty(contractExist.FileName))
                {
                    ManageFilesHelper.RemoveFile(Path.Combine(GoRootPath.HRFilesPath, contractExist.FileName));
                }

                var contractDetails = await unitOfWork.ContractAllowancesDetails.GetAllAsync(c => c.ContractId == id);
                if (contractDetails.Any())
                {
                    unitOfWork.ContractAllowancesDetails.RemoveRange(contractDetails);
                    await unitOfWork.CompleteAsync();
                }

                unitOfWork.Contracts.Remove(contractExist);
                await unitOfWork.CompleteAsync();
                transaction.Commit();
                return new()
                {
                    Check = true,
                    Data = string.Empty,
                    Error = string.Empty,
                    Msg = string.Format(shareLocalizer[Localization.Deleted], shareLocalizer[Localization.Contract])
                };
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return new()
                {
                    Check = true,
                    Data = string.Empty,
                    Error = string.Empty,
                    Msg = e.Message

                };


            }








        }
    }
}
