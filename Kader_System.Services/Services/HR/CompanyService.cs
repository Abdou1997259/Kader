using Kader_System.Domain.DTOs;
using Kader_System.Domain.Models.HR;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.Services.AppServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.HR;

public class CompanyService(IUnitOfWork unitOfWork, IFileServer _fileServer, IStringLocalizer<SharedResource> shareLocalizer, IMapper mapper) : ICompanyService
{
    private HrCompany _instance;



    #region Retrieve

    public async Task<Response<IEnumerable<HrListOfCompaniesResponse>>> ListOfCompaniesAsync(string lang)
    {
        var result =
                await unitOfWork.Companies.GetSpecificSelectAsync(null!,
                select: x => new HrListOfCompaniesResponse
                {
                    Id = x.Id,
                    Name = lang == Localization.Arabic ? x.NameAr : x.NameEn,
                    Company_licenses = x.Company_licenses,
                }, orderBy: x =>
                  x.OrderByDescending(x => x.Id));

        if (!result.Any())
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
            Data = result,
            Check = true
        };
    }

    public async Task<Response<HrGetAllCompaniesResponse>> GetAllCompaniesAsync(string lang, HrGetAllFiltrationsForCompaniesRequest model, string host)
    {
        Expression<Func<HrCompany, bool>> filter = x => x.IsDeleted == model.IsDeleted &&
            (string.IsNullOrEmpty(model.Word) || x.NameAr.Contains(model.Word) || x.NameEn.Contains(model.Word)
             || x.CompanyOwner == model.Word
             || x.CompanyType!.Name.Contains(model.Word));

        var totalRecords = await unitOfWork.Companies.CountAsync(filter: filter);
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize))
            ;
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();


        var result = new HrGetAllCompaniesResponse
        {
            TotalRecords = totalRecords,

            Items = (await unitOfWork.Companies.GetSpecificSelectAsync(filter: filter,
                 take: model.PageSize,
                 skip: (model.PageNumber - 1) * model.PageSize,
                 select: x => new CompanyData
                 {
                     Id = x.Id,
                     Added_by = x.Added_by,
                     Add_date = x.Add_date.ToGetFullyDate(),
                     Company_owner = x.CompanyOwner,
                     Company_type_name = lang == Localization.Arabic ? x.CompanyType.Name : x.CompanyType.NameInEnglish,
                     Name = lang == Localization.Arabic ? x.NameAr : x.NameEn,
                     Employees_count = x.HrManagements.SelectMany(x => x.HrDepartments).SelectMany(x => x.Employees).Count()
                 }, orderBy: x =>
                   x.OrderByDescending(x => x.Id), includeProperties: "HrManagements.HrDepartments.Employees")).ToList(),
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
    public async Task<Response<FileResult>> DownloadCompanyContract(int id)
    {
        var contractAttachment = await unitOfWork.CompanyContracts.GetByIdAsync(id);
        if (contractAttachment is null)
        {
            var msg = shareLocalizer[Localization.IsNotExisted, shareLocalizer[Localization.Contract]];
            return new Response<FileResult>
            {
                Msg = msg,
                Check = false
            };
        }

        if (string.IsNullOrEmpty(contractAttachment.CompanyContracts))
        {
            var msg = shareLocalizer[Localization.HasNoDocument, shareLocalizer[Localization.Contract]];
            return new Response<FileResult>
            {
                Msg = msg,
                Check = false
            };
        }
        HrDirectoryTypes directoryTypes = new();
        directoryTypes = HrDirectoryTypes.Contracts;
        var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);


        if (!_fileServer.FileExist(directoryName, contractAttachment.CompanyContracts))
        {
            var msg = shareLocalizer[Localization.FileHasNoDirectory, shareLocalizer[Localization.Contract]];
            return new Response<FileResult>
            {
                Data = null,
                Msg = msg,
                Check = false
            };
        }


        try
        {
            // Open the file stream


            // Create the FileStreamResult
            var fileStream = await _fileServer.DownloadFileAsync(directoryName, contractAttachment.CompanyContracts);


            // Return the FileStreamResult wrapped in your Response object
            return new Response<FileResult>
            {
                Data = fileStream,
                Check = true,
                // or any success message you want
            };



            // Create and return the FileContentResult


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

        throw new NotImplementedException();
    }

    public Task<Response<FileResult>> DownloadCompanylicense(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<HrGetCompanyByIdResponse>> GetCompanyByIdAsync(int id, string lang)
     {
        var obj = await unitOfWork.Companies.GetFirstOrDefaultAsync(filter => filter.Id == id,
            includeProperties: $"{nameof(_instance.CompanyType)},{nameof(_instance.ListOfsContract)}," +
                              $"{nameof(_instance.Licenses)}");


        var employeesCount = await unitOfWork.Employees.CountAsync(d => d.CompanyId == id);
        var managementsCount = await unitOfWork.Managements.CountAsync(d => d.CompanyId == id);
        var departmentsCount = unitOfWork.Departments.GetDepartmentsByCompanyId(id).Count();

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

        var directoryCompanyContractsTypes = HrDirectoryTypes.CompanyContracts;
        var directoryCompanyLicesnsesTypes = HrDirectoryTypes.CompanyLicesnses;
        var directoryCompanyContractsName = directoryCompanyContractsTypes.GetModuleNameWithType(Modules.HR);
        var directoryCompanyLicesnsesName = directoryCompanyLicesnsesTypes.GetModuleNameWithType(Modules.HR);
        return new()
        {
            Data = new()
            {
                Id = id,
                Name_ar = obj.NameAr,
                Name_en = obj.NameEn,
                Company_owner = obj.CompanyOwner,
                Company_type = obj.CompanyTypeId,
                Company_type_name = lang == Localization.Arabic ? obj.CompanyType!.Name : obj.CompanyType!.NameInEnglish,
                Company_contracts = obj.ListOfsContract.Select(c => new CompanyContractResponse()
                {
                    Contract = _fileServer.GetFilePath(directoryCompanyContractsName, c.CompanyContracts),
                    company_contract_id = c.Id,
                   file_name=c.CompanyContracts,
                    add_date = c.Add_date,
                    file_extension = c.CompanyContractsExtension


                }).ToList(),
                Company_licenses = obj.Licenses.Select(l => new CompanyLicenseResponse()
                {
                    
                    License = _fileServer.GetFilePath(directoryCompanyLicesnsesName, l.LicenseName),
                    company_license_id = l.Id,
                    file_name=l.LicenseName,
                    add_date = l.Add_date,
                    file_extension = l.LicenseExtension



                }).ToList(),
                employees_count = employeesCount,
                managements_count = managementsCount,
                departments_count = departmentsCount,


            },
            Check = true
        };
    }

    #endregion


    #region Insert

    public async Task<Response<HrCreateCompanyRequest>> CreateCompanyAsync(HrCreateCompanyRequest model)
    {
        var exists = await unitOfWork.Companies.ExistAsync(x => x.NameAr.Trim() == model.Name_ar
                                                                && x.NameEn.Trim() == model.Name_en.Trim());

        if (exists)
        {
            string resultMsg = string.Format(shareLocalizer[Localization.IsExist],
                shareLocalizer[Localization.Company]);

            return new()
            {
                Error = resultMsg,
                Msg = resultMsg
            };
        }




        List<GetFileNameAndExtension> getFileNameAnds = [];
        if (model.Company_contracts is not null && model.Company_contracts.Any())
        {
            HrDirectoryTypes directoryTypes = new();
            directoryTypes = HrDirectoryTypes.CompanyContracts;
            var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
            getFileNameAnds = await _fileServer.UploadFilesAsync(directoryName, model.Company_contracts);
        }
        List<GetFileNameAndExtension> getLicenseFileNameAnds = [];
        if (model.Company_licenses is not null && model.Company_licenses.Any())
        {
            HrDirectoryTypes directoryTypes = new();
            directoryTypes = HrDirectoryTypes.CompanyLicesnses;
            var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
            getLicenseFileNameAnds = await _fileServer.UploadFilesAsync(directoryName, model.Company_licenses);
        }

        await unitOfWork.Companies.AddAsync(new()
        {
            NameEn = model.Name_en,
            NameAr = model.Name_ar,
            CompanyOwner = model.Company_owner,
            CompanyTypeId = model.Company_type,
            Licenses = getLicenseFileNameAnds?.Select(l => new CompanyLicense()
            {
                LicenseExtension = l.FileExtension,
                LicenseName = l.FileName,
            }).ToList(),
            ListOfsContract = getFileNameAnds?.Select(y => new HrCompanyContract
            {
                CompanyContracts = y.FileName,
                CompanyContractsExtension = y.FileExtension
            }).ToList()
        });
        await unitOfWork.CompleteAsync();

        return new()
        {
            Msg = shareLocalizer[Localization.Done],
            Check = true,
            Data = model
        };
    }
    #endregion

    #region Update


    public async Task<Response<HrUpdateCompanyRequest>> UpdateCompanyAsync(int id, HrUpdateCompanyRequest model)
    {
        Expression<Func<HrCompany, bool>> filter = x => x.IsDeleted == false && x.Id == id;
        var obj = await unitOfWork.Companies.GetFirstOrDefaultAsync(filter, includeProperties: $"{nameof(_instance.Licenses) + "," + nameof(_instance.ListOfsContract)}");

        if (obj is null)
        {

            string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                shareLocalizer[Localization.Company]);

            return new()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        using var transaction = unitOfWork.BeginTransaction();
        try
        {


            if (obj.ListOfsContract.Any())
            {
                HrDirectoryTypes directoryTypes = new();
                directoryTypes = HrDirectoryTypes.CompanyContracts;
                var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
                _fileServer.RemoveDirectory(directoryName);
                unitOfWork.CompanyContracts.RemoveRange(obj.ListOfsContract);
            }

            if (obj.Licenses.Any())
            {
                HrDirectoryTypes directoryTypes = new();
                directoryTypes = HrDirectoryTypes.CompanyLicesnses;
                var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
                _fileServer.RemoveDirectory(directoryName); unitOfWork.CompanyLicenses.RemoveRange(obj.Licenses);
            }


            obj.NameEn = model.Name_en;
            obj.NameAr = model.Name_ar;
            obj.CompanyOwner = model.Company_owner;
            obj.CompanyTypeId = model.Company_type;


            List<GetFileNameAndExtension> getFileNameAnds = [];
            if (model.company_contracts is not null && model.company_contracts.Any())
            {
                getFileNameAnds = await _fileServer.UploadFilesAsync(Modules.CompanyContracts, model.company_contracts);
            }
            List<GetFileNameAndExtension> getLicenseFileNameAnds = [];
            if (model.company_licenses is not null && model.company_licenses.Any())
            {
                getLicenseFileNameAnds = await _fileServer.UploadFilesAsync(Modules.CompanyLicesnses, model.company_licenses);
            }

            if (getFileNameAnds.Any())
            {
                await unitOfWork.CompanyLicenses.AddRangeAsync(getLicenseFileNameAnds.Select(l => new CompanyLicense()
                {
                    LicenseName = l.FileName,
                    LicenseExtension = l.FileExtension,
                    CompanyId = id
                }).ToList());
            }
            if (getLicenseFileNameAnds.Any())
            {
                await unitOfWork.CompanyContracts.AddRangeAsync(getLicenseFileNameAnds.Select(l => new HrCompanyContract()
                {
                    CompanyContracts = l.FileName,
                    CompanyContractsExtension = l.FileExtension,
                    CompanyId = id
                }).ToList());
            }
            unitOfWork.Companies.Update(obj);

            await unitOfWork.CompleteAsync();
            transaction.Commit();

            return new()
            {
                Msg = shareLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return new()
            {
                Data = model,
                Error = ex.Message,
                Msg = ex.Message
            };
        }



    }

    public Task<Response<string>> UpdateActiveOrNotCompanyAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<object>> RestoreCompanyAsync(int id)
    {
        var obj = await unitOfWork.Companies.GetByIdAsync(id);
        if (obj == null)
        {
            string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                shareLocalizer[Localization.Company]);

            return new()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        obj.IsDeleted = false;
        unitOfWork.Companies.Update(obj);
        await unitOfWork.CompleteAsync();
        var newObject = new
        {
            Id = obj.Id,
            NameAr = obj.NameAr,
            NameEn = obj.NameEn
        };
        return new()
        {
            Data = newObject
            ,
            Error = string.Empty,
            Msg = "Restored Successfully",
            Check = true
        };
    }
    #endregion


    #region Delete

    public async Task<Response<string>> DeleteCompanyAsync(int id)
    {
        {
            Expression<Func<HrCompany, bool>> filter = x => x.Id == id;
            var obj = await unitOfWork.Companies.GetFirstOrDefaultAsync(filter, includeProperties: $"{nameof(_instance.Licenses)},{nameof(_instance.ListOfsContract)}");

            if (obj == null)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                    shareLocalizer[Localization.Company]);

                return new()
                {
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            if (obj.Licenses.Any())
            {
                unitOfWork.CompanyLicenses.RemoveRange(obj.Licenses);
            }

            if (obj.ListOfsContract.Any())
            {
                unitOfWork.CompanyContracts.RemoveRange(obj.ListOfsContract);
            }

            unitOfWork.Companies.Remove(obj);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Check = true,
                Data = string.Empty,
                Msg = shareLocalizer[Localization.Deleted]
            };
        }
    }

    public async Task<Response<EmployeeOfCompanyPagination>> EmployeeOfCompany(int companyId, string lang, HrGetAllFiltrationsForCompaniesRequest model, string host)
    {


        Expression<Func<EmployeeOfCompanyResponse, bool>> filter = x =>
                 (string.IsNullOrEmpty(model.Word) || x.nationality_name.Contains(model.Word) || x.employee_name.Contains(model.Word)
                  || x.management_name == model.Word);


        var totalRecords = (await unitOfWork.Companies.GetEmployeeOfCompany(companyId, lang, filter, null, null)).Count();
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize))
            ;
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();


        var result = new EmployeeOfCompanyPagination
        {
            TotalRecords = totalRecords,

            Items = (await unitOfWork.Companies.GetEmployeeOfCompany(companyId, lang, filter, model.PageSize, skip: (model.PageNumber - 1) * model.PageSize)).ToList()
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

   

    #endregion
}




