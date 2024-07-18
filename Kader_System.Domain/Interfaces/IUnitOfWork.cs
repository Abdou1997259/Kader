using Kader_System.Domain.Interfaces.EmployeeRequest.PermessionRequests;
using Kader_System.Domain.Interfaces.EmployeeRequest.Request;

namespace Kader_System.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{

    IDatabaseTransaction BeginTransaction();
    ITransLoanDetailsRepository TransLoanDetails { get; }
    IAdvancedTypesRepository AdvancedTypesRepository { get; }
    IStoredProcuduresRepo StoredProcuduresRepo { get; }
    ITransLoanRepository LoanRepository { get; }
    IUserRepository Users { get; }
    IRoleClaimRepository RoleClaims { get; }
    IUserClaimRepository UserClaims { get; }
    IUserDeviceRepository UserDevices { get; }
    IRoleRepository Roles { get; }
    IUserRoleRepository UserRoles { get; }
    ISubMainScreenRepository SubMainScreens { get; }
    ISubMainScreenActionRepository SubMainScreenActions { get; }
    IMainScreenRepository MainScreens { get; }
    IMainScreenCategoryRepository MainScreenCategories { get; }
    ITitleRepository Titles { get; }
    IScreenRepository Screens { get; }
    IScreenActionRepository ScreenActions { get; }
    IAccountingWayRepository AccountingWays { get; }
    IAllowanceRepository Allowances { get; }
    IBenefitRepository Benefits { get; }
    ICompanyRepository Companies { get; }
    ICompanyContractsRepository CompanyContracts { get; }
    ICompanyLicenseRepository CompanyLicenses { get; }
    ICompanyTypeRepository CompanyTypes { get; }
    IContractAllowancesDetailRepository ContractAllowancesDetails { get; }
    IContractRepository Contracts { get; }
    IDeductionRepository Deductions { get; }
    IDepartmentRepository Departments { get; }
    IEmployeeAttachmentRepository EmployeeAttachments { get; }
    IEmployeeRepository Employees { get; }
    IEmployeeTypeRepository EmployeeTypes { get; }
    IFingerPrintRepository FingerPrints { get; }
    IJobRepository Jobs { get; }
    IQualificationRepository Qualifications { get; }
    ISalaryPaymentWayRepository SalaryPaymentWays { get; }
    ITransSalaryCalculatorRepo TransSalaryCalculator { get; }
    ITransSalaryCalculatorDetailsRepo TransSalaryCalculatorDetailsRepo { get; }
    ISectionDepartmentRepository SectionDepartments { get; }
    IManagementRepository Managements { get; }
    ISectionRepository Sections { get; }
    IShiftRepository Shifts { get; }
    IVacationDistributionRepository VacationDistributions { get; }
    IVacationRepository Vacations { get; }
    IVacationTypeRepository VacationTypes { get; }


    ITransAllowanceRepository TransAllowances { get; }
    ITransSalaryEffectRepository TransSalaryEffects { get; }
    ITransSalaryIncreaseRepository TransSalaryIncrease { get; }
    ISalaryIncreaseTypeRepository SalaryIncreaseTypeRepository { get; }

    ITransAmountTypeRepository TransAmountTypes { get; }
    ITransBenefitRepository TransBenefits { get; }
    ITransCovenantRepository TransCovenants { get; }
    ITransDeductionRepository TransDeductions { get; }
    ITransVacationRepository TransVacations { get; }
    INationalityRepository Nationalities { get; }
    IMaritalStatusRepository MaritalStatus { get; }
    IGenderRepository Genders { get; }
    IReligionRepository Religions { get; }
    #region Employee_Requests_UOW
    ILeavePermissionRequestRepository LeavePermissionRequest { get; }
    IVacationRequestRepository VacationRequests { get; }
    IAllowanceRequestRepository AllowanceRequests { get; }
    #endregion
    Task<int> CompleteAsync();
}
