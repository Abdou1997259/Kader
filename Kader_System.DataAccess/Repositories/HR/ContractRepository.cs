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
                .Include(x => x.User)
                .Include(e => e.employee)

                .Include(d => d.list_of_allowances_details)
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
                Id = groupedContract.Contract.id,
                TotalSalary = groupedContract.Contract.total_salary,
                FixedSalary = groupedContract.Contract.fixed_salary,
                EmployeeId = groupedContract.Contract.employee_id,

                EmployeeName = lang == Localization.Arabic ?
                groupedContract.Contract.employee!.FullNameAr :
                groupedContract.Contract.employee!.FullNameEn,
                StartDate = groupedContract.Contract.start_date,
                EndDate = groupedContract.Contract.end_date,
                HousingAllowance = groupedContract.Contract.housing_allowance,
                AddedByUser = groupedContract.Contract.User.FullName,
                ContractFile = groupedContract.Contract.file_name != null ?
                Path.Combine(directoryName, groupedContract.Contract.file_name) : null,
                Details = groupedContract.Contract.list_of_allowances_details.Select(a => new GetAllContractDetailsResponse()
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
            .Include(e => e.employee)
            .Include(d => d.list_of_allowances_details)
            .ThenInclude(a => a.Allowance)
            .Where(c => c.id == id && c.company_id == companyId);
        var result = contractsData
            .GroupJoin(
                context.Users,
                contract => contract.Added_by,
                user => user.Id,
                (contract, usersData) => new { Contract = contract, UsersData = usersData.DefaultIfEmpty() }
            )
            .Select(groupedContract => new GetContractDataByIdResponse()
            {
                Id = groupedContract.Contract.id,
                TotalSalary = groupedContract.Contract.total_salary,
                FixedSalary = groupedContract.Contract.fixed_salary,
                EmployeeId = groupedContract.Contract.employee_id,
                EmployeeName = lang == Localization.Arabic ? groupedContract
                .Contract.employee!.FullNameAr : groupedContract.Contract.employee!.FullNameEn,
                StartDate = groupedContract.Contract.start_date,
                EndDate = groupedContract.Contract.end_date,
                HousingAllowance = groupedContract.Contract.housing_allowance,
                ContractFile = groupedContract.Contract.file_name != null ? Path.Combine(directoryName, groupedContract.Contract.file_name) : null,
                FileName = $"{groupedContract.Contract.file_name}",
                AddedByUser = groupedContract.UsersData.FirstOrDefault()!.UserName,
                Details = groupedContract.Contract.list_of_allowances_details.Select(a => new GetAllContractDetailsResponse()
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

