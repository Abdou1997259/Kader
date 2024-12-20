﻿using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Services.IServices.AppServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.HR;

public class CompanyService(IUnitOfWork unitOfWork, IUserContextService userContextService, KaderDbContext context, IFileServer _fileServer, KaderDbContext _context, IStringLocalizer<SharedResource> shareLocalizer, IMapper mapper) : ICompanyService
{
    private HrCompany _instance;
    private KaderDbContext _context = context;
    private readonly IUserContextService _userContextService = userContextService;
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
        var isAdmin = _userContextService.IsAdmin();
        var currentCompanyIds = await _userContextService.GetLoggedCurrentCompanies();
        Expression<Func<HrCompany, bool>> filter = x => x.IsDeleted ==
        model.IsDeleted
        &&
             (isAdmin || currentCompanyIds.Contains(x.Id)) &&
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
                     Employees_count = x.HrEmployees.Count()
                 }, orderBy: x =>
                   x.OrderByDescending(x => x.Id), includeProperties: "HrEmployees")).ToList(),
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
    public async Task<Response<FileContentResult>> DownloadCompanyContract(int id)
    {
        var contractAttachment = await unitOfWork.CompanyContracts.GetByIdAsync(id);
        if (contractAttachment is null)
        {
            var msg = shareLocalizer[Localization.IsNotExisted, shareLocalizer[Localization.Contract]];
            return new Response<FileContentResult>
            {
                Msg = msg,
                Check = false
            };
        }

        if (string.IsNullOrEmpty(contractAttachment.CompanyContracts))
        {
            var msg = shareLocalizer[Localization.HasNoDocument, shareLocalizer[Localization.Contract]];
            return new Response<FileContentResult>
            {
                Msg = msg,
                Check = false
            };
        }
        HrDirectoryTypes directoryTypes = new();
        directoryTypes = HrDirectoryTypes.CompanyContracts;
        var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
        if (!_fileServer.FileExist(directoryName, contractAttachment.CompanyContracts))
        {
            var msg = shareLocalizer[Localization.FileHasNoDirectory, shareLocalizer[Localization.Contract]];
            return new Response<FileContentResult>
            {
                Data = null,
                Msg = msg,
                Check = false
            };
        }
        try
        {
            var fileStream = await _fileServer.DownloadFileAsync(directoryName, contractAttachment.CompanyContracts);
            return new Response<FileContentResult>
            {
                Data = fileStream,
                Check = true,
            };

        }
        catch (Exception ex)
        {

            return new Response<FileContentResult>
            {
                Msg = $": {ex.Message}",
                Check = false
            };
        }


    }

    public async Task<Response<FileContentResult>> DownloadCompanylicense(int id)
    {
        var contractAttachment = await unitOfWork.CompanyLicenses.GetByIdAsync(id);
        if (contractAttachment is null)
        {
            var msg = shareLocalizer[Localization.IsNotExisted, shareLocalizer[Localization.Contract]];
            return new Response<FileContentResult>
            {
                Msg = msg,
                Check = false
            };
        }

        if (string.IsNullOrEmpty(contractAttachment.LicenseName))
        {
            var msg = shareLocalizer[Localization.HasNoDocument, shareLocalizer[Localization.Contract]];
            return new Response<FileContentResult>
            {
                Msg = msg,
                Check = false
            };
        }
        HrDirectoryTypes directoryTypes = new();
        directoryTypes = HrDirectoryTypes.CompanyLicesnses;
        var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
        if (!_fileServer.FileExist(directoryName, contractAttachment.LicenseName))
        {
            var msg = shareLocalizer[Localization.FileHasNoDirectory, shareLocalizer[Localization.Contract]];
            return new Response<FileContentResult>
            {
                Data = null,
                Msg = msg,
                Check = false
            };
        }
        try
        {
            var fileStream = await _fileServer.DownloadFileAsync(directoryName, contractAttachment.LicenseName);
            return new Response<FileContentResult>
            {
                Data = fileStream,
                Check = true,
                DynamicData = contractAttachment.LicenseName
            };

        }
        catch (Exception ex)
        {

            return new Response<FileContentResult>
            {
                Msg = $": {ex.Message}",
                Check = false
            };
        }
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
                    file_path = _fileServer.CombinePath(directoryCompanyContractsName, c.CompanyContracts),
                    id = c.Id,
                    file_name = c.CompanyContracts,
                    add_date = c.Add_date,
                    file_extension = c.CompanyContractsExtension


                }).ToList(),
                Company_licenses = obj.Licenses.Select(l => new CompanyLicenseResponse()
                {

                    file_path = _fileServer.CombinePath(directoryCompanyLicesnsesName, l.LicenseName),
                    id = l.Id,
                    file_name = l.LicenseName,
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
        if (!_userContextService.IsAdmin())
        {
            return new()
            {
                Check = false,
                Msg = shareLocalizer[Localization.NoAdminCreate]
            };

        }
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
        try
        {
            List<GetFileNameAndExtension> getFileNameAnds = [];
            List<GetFileNameAndExtension> getLicenseFileNameAnds = [];


            #region UpdateCompanyContracts
            if (model.company_contracts is not null)
            {
                var directoryTypes = HrDirectoryTypes.CompanyContracts;
                var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
                getFileNameAnds = await _fileServer.UploadFilesAsync(directoryName, model.company_contracts);
                var companyContract = getFileNameAnds.Select(x => new HrCompanyContract { CompanyContracts = x.FileName, CompanyId = id }).ToList();
                await unitOfWork.CompanyContracts.AddRangeAsync(companyContract);
                await unitOfWork.CompleteAsync();
            }
            #endregion

            #region UpdateCompanyLicesnses
            if (model.company_licenses is not null)
            {
                var directoryTypes = HrDirectoryTypes.CompanyLicesnses;
                var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
                getFileNameAnds = await _fileServer.UploadFilesAsync(directoryName, model.company_licenses);
                var companyContract = getFileNameAnds.Select(x => new CompanyLicense { LicenseName = x.FileName, CompanyId = id, LicenseExtension = x.FileExtension }).ToList();
                await unitOfWork.CompanyLicenses.AddRangeAsync(companyContract);
                await unitOfWork.CompleteAsync();
            }


            #endregion

            #region UpdateCompany
            obj.NameEn = model.Name_en;
            obj.NameAr = model.Name_ar;
            obj.CompanyOwner = model.Company_owner;
            obj.CompanyTypeId = model.Company_type;
            unitOfWork.Companies.Update(obj);
            await unitOfWork.CompleteAsync();
            #endregion


            return new()
            {
                Msg = shareLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }
        catch (Exception ex)
        {
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
            var users = await _context.Users
                .Where(x => !string.IsNullOrEmpty(x.CompanyId))
                .ToListAsync();

            // Check if any user has the company linked
            if (users.Any(x => x.CompanyId.Splitter().Contains(id)))
            {
                return new()
                {
                    Check = false,
                    Msg = shareLocalizer[Localization.CompanyLinkedToUser]
                };
            }
            var companyEmps = _context.Companys.Include("HrEmployees").Where(x => x.Id == id).Any(x => x.HrEmployees.Any());
            if (companyEmps)
            {
                return new()
                {
                    Check = false,
                    Msg = shareLocalizer[Localization.CompanyContainsEmployee]
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

    public async Task<Response<string>> RemoveCompanyContractsAttachement(int companyContractId, HrDirectoryTypes directoryTypes, bool isCompany)
    {
        // check if you want to remove with companyId or Id of Attachment
        Expression<Func<HrCompanyContract, bool>> filter = isCompany ? x => x.CompanyId == companyContractId : x => x.Id == companyContractId;
        var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
        var attachements = await _context.CompanyContracts.AsNoTracking().Where(filter).ToListAsync();
        var fileNames = attachements.Select(x => x.CompanyContracts).ToList();
        _fileServer.RemoveFiles(directoryName, fileNames);
        unitOfWork.CompanyContracts.RemoveRange(attachements);
        await unitOfWork.CompleteAsync();
        unitOfWork.CompanyContracts.RemoveRange(attachements);
        var result = await unitOfWork.CompleteAsync();
        if (result > 0)
        {
            return new()
            {
                Check = true,
                Msg = $"Attachement {companyContractId} is deleted sucessfully"
            };
        }
        return new()
        {
            Check = false,
            Msg = $"Attachement {companyContractId} is deleted sucessfully"
        };
    }
    public async Task<Response<string>> RemoveCompanyLicensesAttachement(int companyLicensesId, HrDirectoryTypes directoryTypes, bool isCompany)
    {
        Expression<Func<CompanyLicense, bool>> filter = isCompany ? x => x.CompanyId == companyLicensesId : x => x.Id == companyLicensesId;
        var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
        var attachements = await _context.CompanyLicenses.AsNoTracking().Where(filter).ToListAsync();
        var fileNames = attachements.Select(x => x.LicenseName).ToList();
        _fileServer.RemoveFiles(directoryName, fileNames);
        unitOfWork.CompanyLicenses.RemoveRange(attachements);
        await unitOfWork.CompleteAsync();
        unitOfWork.CompanyLicenses.RemoveRange(attachements);
        var result = await unitOfWork.CompleteAsync();
        if (result > 0)
        {
            return new()
            {
                Check = true,
                Msg = $"Attachement {companyLicensesId} is deleted sucessfully"
            };
        }
        return new()
        {
            Check = false,
            Msg = $"Attachement {companyLicensesId} is deleted sucessfully"
        };
    }



    #endregion
}




