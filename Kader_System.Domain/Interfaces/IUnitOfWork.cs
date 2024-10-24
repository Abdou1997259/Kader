﻿using Kader_System.Domain.Interfaces.EmployeeRequest;
using Kader_System.Domain.Interfaces.EmployeeRequest.PermessionRequests;
using Kader_System.Domain.Interfaces.EmployeeRequest.Request;
using Kader_System.Domain.Interfaces.InterViews;

namespace Kader_System.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public string DatabaseName { get; set; }
    IDatabaseTransaction BeginTransaction();
    IDatabaseTransactionScope TransactionScope();

    IUserPermssionRepositroy UserPermssionRepositroy { get; }
    ITitlePermissionRepositroy TitlePermissionRepository { get; }
    ILoanRequestRepository LoanRequestRepository { get; }
    ITransLoanDetailsRepository TransLoanDetails { get; }
    IResignationRequesteRepository ResignationRepository { get; }
    IAdvancedTypesRepository AdvancedTypesRepository { get; }
    IStoredProcuduresRepo StoredProcuduresRepo { get; }
    ITransLoanRepository LoanRepository { get; }
    IUserRepository Users { get; }
    IRoleClaimRepository RoleClaims { get; }
    IUserClaimRepository UserClaims { get; }
    IUserDeviceRepository UserDevices { get; }
    IRoleRepository Roles { get; }
    IUserRoleRepository UserRoles { get; }
    ISubScreenRepository SubScreens { get; }
    ISubMainScreenActionRepository SubMainScreenActions { get; }
    IMainScreenRepository MainScreens { get; }
    IScreenCategoryRepository ScreenCategories { get; }
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
    IEmployeeNotesRepository EmployeeNotes { get; }
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
    IPermessionStructureRepository PermessionStructure { get; }
    IActionsRepository ActionsRepo { get; }
    IApplicantRepository Applicant { get; }
    IEducationRepository Education { get; }

    IExperienceRepository Experience { get; }
    #region Employee_Requests_UOW
    IEmployeeRequestsRepository EmployeeRequests { get; }
    ILeavePermissionRequestRepository LeavePermissionRequest { get; }
    IDelayPermissionServiceRepository DelayPermission { get; }
    IVacationRequestRepository VacationRequests { get; }
    IAllowanceRequestRepository AllowanceRequests { get; }
    ISalaryIncreaseRequestServicesReository SalaryIncreaseRequest { get; }
    IContractTerminationRequestRepositroy ContractTerminationRequest { get; }
    IInterJobRepository InterJob { get; }
    #endregion
    Task<int> CompleteAsync();
}