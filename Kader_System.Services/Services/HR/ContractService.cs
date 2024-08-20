
using Kader_System.Domain.DTOs;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.HTTP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kader_System.Services.Services.HR
{
    public class ContractService
       : IContractService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStringLocalizer<SharedResource> shareLocalizer;
        private readonly IFileServer fileServer;
        private readonly IHttpContextService httpContextService;
        private readonly IMapper mapper;
        private HrContract instanceContract;
        private readonly string serverPath;

        // Constructor
        public ContractService(
            IUnitOfWork _unitOfWork,
            IStringLocalizer<SharedResource> _shareLocalizer,
            IFileServer _fileServer,
            IHttpContextService _httpContextService,
            IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            shareLocalizer = _shareLocalizer;
            fileServer = _fileServer;
            httpContextService = _httpContextService;
            mapper = _mapper;
            instanceContract = new HrContract(); // Initialize if needed
            serverPath = _httpContextService.GetPhysicalServerPath(); // Initialize here
        }




        public async Task<Response<IEnumerable<ListOfContractsResponse>>> ListOfContractsAsync(string lang)
        {
            var result =
                await unitOfWork.Contracts.GetSpecificSelectAsync(null!
                    , includeProperties: $"{nameof(instanceContract.Employee)}",
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
                    employees = new[]
                        {
                            new
                            {
                                Id = obj.EmployeeId,
                                Name = obj.EmployeeName
                            }
                        }
                },
                Check = true
            };
        }


        public async Task<Response<object> > GetLookUps(string lang)
        {
            var employees =await unitOfWork.Employees.GetEmployeesNameIdSalaryWithoutContractAsLookUp(lang);
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

        public async Task<Response<CreateContractRequest>> CreateContractAsync(CreateContractRequest model, string moduleName)
        {


           var haveContract= await unitOfWork.Contracts.ExistAsync(x=>x.EmployeeId==model.employee_id);
            if (haveContract)
            {
                var Msg = string.Format(shareLocalizer[Localization.HaveContract],
                  shareLocalizer[Localization.Contract]);
                return new()
                {
                    Check = false,
                    Msg = Msg,
                    Data = null
                };
            }
            var newContract = new HrContract()
            {
                StartDate = model.start_date,
                EndDate = model.end_date,
                FixedSalary = model.fixed_salary,
                EmployeeId = model.employee_id,
                HousingAllowance = model.housing_allowance,


            };
            if (model.details != null)
            {
                newContract.ListOfAllowancesDetails =

                    model.details.Select(d => new HrContractAllowancesDetail()
                    {
                        AllowanceId = d.allowance_id,
                        Value = d.value,
                        IsPercent = d.is_percent
                    }).ToList()

                ;
            }

            newContract.FileName = model.contract_file == null ? string.Empty : await fileServer.UploadFile( moduleName, model.contract_file);
            if (model.contract_file != null) {
                newContract.FileExtension = Path.GetExtension(model.contract_file.FileName);
            }
            else
            {
                newContract.FileExtension = "";
            }
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

        public async Task<Response<CreateContractRequest>> UpdateContractAsync(int id, CreateContractRequest model,string  moduleName)
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

                if (model.contract_file != null)
                {
                    if (fileServer.FileExist( moduleName, obj.FileName))
                        fileServer.RemoveFile( moduleName, obj.FileName);
                    obj.FileExtension = Path.GetExtension(model.contract_file.FileName);

                    obj.FileName = (model.contract_file.Length == 0) ? null
                        : await fileServer.UploadFile( moduleName, model.contract_file);
                }
              
              

                obj.EmployeeId = model.employee_id;
                obj.EndDate = model.end_date;
                obj.StartDate = model.start_date;
                obj.TotalSalary = model.total_salary;
                obj.FixedSalary = model.fixed_salary;
                obj.HousingAllowance = model.housing_allowance;


                var lstNewInserted = model.details?.Where(d => d.status == RowStatus.Inserted).ToList();
                var lstUpdatedDetails = model.details?.Where(d => d.status == RowStatus.Updated).ToList();
                var lstDeletedDetails = model.details?.Where(d => d.status == RowStatus.Deleted).ToList();
                //Deleted Items
                if (lstDeletedDetails != null && lstDeletedDetails.Any())
                {
                    foreach (var deletedRow in lstDeletedDetails)
                    {
                        var existObj = await unitOfWork.ContractAllowancesDetails.GetByIdAsync(deletedRow.id);
                        if (existObj != null)
                        {
                            unitOfWork.ContractAllowancesDetails.Remove(existObj);
                        }
                        else
                        {
                            return new()
                            {
                                Msg = $"Contract Detail with Id:{deletedRow.id} Can not be found !!!!",
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
                            AllowanceId = newItem.allowance_id,
                            Value = newItem.value,
                            ContractId = id,
                            IsPercent = newItem.is_percent
                        });
                    }
                }
                if (lstUpdatedDetails != null && lstUpdatedDetails.Any())
                {
                    foreach (var updateItem in lstUpdatedDetails)
                    {
                        var existObj = await unitOfWork.ContractAllowancesDetails.GetByIdAsync(updateItem.id);
                        if (existObj != null)
                        {
                            existObj.AllowanceId = updateItem.allowance_id;
                            existObj.IsPercent = updateItem.is_percent;
                            existObj.Value = updateItem.value;
                            existObj.ContractId = id;
                            unitOfWork.ContractAllowancesDetails.Update(existObj);
                        }
                        else
                        {
                            return new()
                            {
                                Msg = $"Contract Detail with Id:{updateItem.id} Can not be found to Update It !!!!",
                                Check = false,
                                Data = model
                            };
                        }
                    }
                }

                GetFileNameAndExtension contractFile = new();
                if (model.contract_file is not null)
                {

                    if (model.contract_file != null)
                    {
                        contractFile = ManageFilesHelper.UploadFile(model.contract_file, GoRootPath.HRFilesPath);
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
                    includeProperties: $"{nameof(instanceContract.ListOfAllowancesDetails)}");
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
                        employee_id = obj.EmployeeId,
                        start_date = obj.StartDate,
                        end_date = obj.EndDate,
                        fixed_salary = obj.FixedSalary,
                        housing_allowance = obj.HousingAllowance,
                        total_salary= obj.TotalSalary,
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

        public Task<Response<CreateContractRequest>> CreateContractAsync(CreateContractRequest model, string appPath, string moduleName)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<GetContractForUserResponse>> GetContractByUser(int EmpId ,string lang)
        {
            var emp = await unitOfWork.Employees.GetByIdAsync(EmpId);
            if (emp is null) {


                var msg = shareLocalizer[Localization.IsNotExisted,shareLocalizer[Localization.Employee]];
                return new Response<GetContractForUserResponse>()
                {
                    Msg = msg,
                    Check = false,

                };


            }
            var contract=await unitOfWork.Contracts.GetFirstOrDefaultAsync(x=>x.EmployeeId==EmpId);

            if (contract == null) {


                var msg = shareLocalizer[Localization.IsNotExisted, shareLocalizer[Localization.Contract]];
                return new Response<GetContractForUserResponse>()
                {
                    Msg = msg,
                    Check = false,

                };


            }
            return new Response<GetContractForUserResponse>()
            {
                Check=true,
                Data = new GetContractForUserResponse
                {
                    EmployeeName = Localization.Arabic == lang ? emp.FullNameAr : emp.FullNameEn,
                    ContractFile = Path.Combine(SD.GoRootPath.GetSettingImagesPath, contract.FileName),
                    SalaryFixed = contract.FixedSalary,
                    SalaryTotal = contract.FixedSalary + contract.HousingAllowance,
                    Active = contract.IsActive,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    AddedBy = contract.Added_by,
                    HousingAllowance = contract.HousingAllowance,
                    Id = contract.Id,

                }
            };

        }

        public async Task<Response<FileResult>> GetFileStreamResultAsync(int contractId ,string serverPath,string moduleName)
        {

            var contract = await unitOfWork.Contracts.GetByIdAsync(contractId);
            if (contract is null)
            {
                var msg = shareLocalizer[Localization.IsNotExisted, shareLocalizer[Localization.Contract]];
                return new Response<FileResult>
                {
                    Msg = msg,
                    Check = false
                };
            }

            if (string.IsNullOrEmpty(contract.FileName))
            {
                var msg = shareLocalizer[Localization.HasNoDocument, shareLocalizer[Localization.Contract]];
                return new Response<FileResult>
                {
                    Msg = msg,
                    Check = false
                };
            }

            var clientPath = Path.Combine(serverPath, moduleName);
            var path = Path.Combine(clientPath, contract.FileName);

            if (!System.IO.File.Exists(path))
            {
                var msg = shareLocalizer[Localization.FileHasNoDirectory, shareLocalizer[Localization.Contract]];
                return new Response<FileResult>
                {
                    Msg = msg,
                    Check = false
                };
            }

            try
            {
                // Open the file stream


                // Create the FileStreamResult
                var fileBytes = await System.IO.File.ReadAllBytesAsync(path);

                // Create and return the FileContentResult
                var fileContentResult = new FileContentResult(fileBytes, "application/octet-stream")
                {
                    FileDownloadName = contract.FileName
                };

                // Return the FileStreamResult wrapped in your Response object
                return new Response<FileResult>
                {
                    Data = fileContentResult,
                    Check = true,
                 // or any success message you want
                };
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file access issues)
                var msg = shareLocalizer[Localization.Error, shareLocalizer[Localization.Contract]];
                return new Response<FileResult>
                {
                    Msg = $"{msg}: {ex.Message}",
                    Check = false
                };
            }
        }

    }
}
