using Kader_System.Domain.Constants.Enums;
using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.DataAccess.Repositories.HR;

public class ContractRepository(KaderDbContext context) : BaseRepository<HrContract>(context), IContractRepository
{
    public List<ContractData> GetAllContractsAsync(
        Expression<Func<HrContract, bool>> contractFilter,
        string lang,
        int? skip = null,
        int? take = null)
    {

        var contractsData = context.Contracts
                .Include(e => e.Employee)
                .Include(d => d.ListOfAllowancesDetails)
                .ThenInclude(a => a.Allowance)
                .AsQueryable();
        HrDirectoryTypes directoryTypes = new();
        directoryTypes = HrDirectoryTypes.Contracts;
        var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
        if (contractFilter != null)
        {
            contractsData = contractsData.Where(contractFilter);
        }
        if (skip.HasValue)
            contractsData = contractsData.Skip(skip.Value);

        if (take.HasValue)
            contractsData = contractsData.Take(take.Value);

        var result = contractsData
            .GroupJoin(
                context.Users,
                contract => contract.Added_by,
                user => user.Id,
                (contract, usersData) => new { Contract = contract, UsersData = usersData.DefaultIfEmpty() }
            )
            .Select(groupedContract => new ContractData()
            {
                Id = groupedContract.Contract.Id,
                TotalSalary = groupedContract.Contract.TotalSalary,
                FixedSalary = groupedContract.Contract.FixedSalary,
                EmployeeId = groupedContract.Contract.EmployeeId,
                EmployeeName = lang == Localization.Arabic ? groupedContract.Contract.Employee!.FullNameAr : groupedContract.Contract.Employee!.FullNameEn,
                StartDate = groupedContract.Contract.StartDate,
                EndDate = groupedContract.Contract.EndDate,
                HousingAllowance = groupedContract.Contract.HousingAllowance,
                AddedByUser = groupedContract.UsersData.FirstOrDefault()!.UserName,
                ContractFile = groupedContract.Contract.FileName != null ? Path.Combine(directoryName, groupedContract.Contract.FileName) : null,
                Details = groupedContract.Contract.ListOfAllowancesDetails.Select(a => new GetAllContractDetailsResponse()
                {
                    Id = a.Id,
                    AllowanceId = a.AllowanceId,
                    Value = a.Value,
                    AllowanceName = lang == Localization.Arabic ? a.Allowance.Name_ar : a.Allowance.Name_en,
                    IsPercent = a.IsPercent
                }).ToList()
            });


        //var groupedContracts = contractsData
        //   // Assuming Id is the primary key of Contract
        //    .Select(groupedContract => new ContractData()
        //    {
        //        Id = groupedContract.Id,
        //        TotalSalary = groupedContract.TotalSalary,
        //        FixedSalary = groupedContract.FixedSalary,
        //        EmployeeId = groupedContract.EmployeeId,
        //        EmployeeName = lang == Localization.Arabic ? groupedContract.Employee!.FullNameAr : groupedContract.Employee!.FullNameEn,
        //        StartDate = groupedContract.StartDate,
        //        EndDate = groupedContract.EndDate,
        //        HousingAllowance = groupedContract.HousingAllowance,
        //        ContractFile = ManageFilesHelper.ConvertFileToBase64(GoRootPath.HRFilesPath + groupedContract.FileName ),

        //        Details = groupedContract.ListOfAllowancesDetails.Select(a => new GetAllContractDetailsResponse()
        //        {
        //            AllowanceId = a.AllowanceId,
        //            Value = a.Value,
        //            AllowanceName = lang == Localization.Arabic ? a.Allowance.Name_ar : a.Allowance.Name_en,
        //            IsPercent = a.IsPercent 
        //        }).ToList()
        //    });


        return result.ToList();
    }



    public GetContractDataByIdResponse GetContractById(int id, string lang, int companyId)
    {
        HrDirectoryTypes directoryTypes = new();
        directoryTypes = HrDirectoryTypes.Contracts;
        var directoryName = directoryTypes.GetModuleNameWithType(Modules.HR);
        var contractsData = context.Contracts
            .Include(e => e.Employee)
            .Include(d => d.ListOfAllowancesDetails)
            .ThenInclude(a => a.Allowance)
            .Where(c => c.Id == id && c.CompanyId == companyId);
        var result = contractsData
            .GroupJoin(
                context.Users,
                contract => contract.Added_by,
                user => user.Id,
                (contract, usersData) => new { Contract = contract, UsersData = usersData.DefaultIfEmpty() }
            )
            .Select(groupedContract => new GetContractDataByIdResponse()
            {
                Id = groupedContract.Contract.Id,
                TotalSalary = groupedContract.Contract.TotalSalary,
                FixedSalary = groupedContract.Contract.FixedSalary,
                EmployeeId = groupedContract.Contract.EmployeeId,
                EmployeeName = lang == Localization.Arabic ? groupedContract.Contract.Employee!.FullNameAr : groupedContract.Contract.Employee!.FullNameEn,
                StartDate = groupedContract.Contract.StartDate,
                EndDate = groupedContract.Contract.EndDate,
                HousingAllowance = groupedContract.Contract.HousingAllowance,
                ContractFile = groupedContract.Contract.FileName != null ? Path.Combine(directoryName, groupedContract.Contract.FileName) : null,
                FileName = $"{groupedContract.Contract.FileName}",
                AddedByUser = groupedContract.UsersData.FirstOrDefault()!.UserName,
                Details = groupedContract.Contract.ListOfAllowancesDetails.Select(a => new GetAllContractDetailsResponse()
                {
                    Id = a.Id,
                    AllowanceId = a.AllowanceId,
                    Value = a.Value,
                    AllowanceName = lang == Localization.Arabic ? a.Allowance.Name_ar : a.Allowance.Name_en,
                    IsPercent = a.IsPercent,
                    Status = RowStatus.None
                }).ToList()
            });




        return result?.FirstOrDefault();
    }

}

