﻿using Kader_System.DataAccess.Repositories.EmployeeRequests;
using Kader_System.DataAccess.Repositories.EmployeeRequests.PermessionRequests;
using Kader_System.DataAccess.Repositories.EmployeeRequests.Requests;
using Kader_System.DataAccess.Repositories.InterviewRepositories;
using Kader_System.Domain.Interfaces.EmployeeRequest;
using Kader_System.Domain.Interfaces.EmployeeRequest.PermessionRequests;
using Kader_System.Domain.Interfaces.EmployeeRequest.Request;
using Kader_System.Domain.Interfaces.InterViews;
using Microsoft.AspNetCore.Hosting;

namespace Kader_System.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly KaderDbContext _context;
    protected readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;

    public IAdvancedTypesRepository AdvancedTypesRepository { get; private set; }

    public ITransLoanRepository LoanRepository { get; private set; }
    public IUserRepository Users { get; private set; }
    public IRoleClaimRepository RoleClaims { get; private set; }
    public IUserClaimRepository UserClaims { get; private set; }
    public IRoleRepository Roles { get; private set; }
    public IUserRoleRepository UserRoles { get; private set; }
    public IUserDeviceRepository UserDevices { get; private set; }
    public ISubScreenRepository SubScreens { get; private set; }
    public ISubMainScreenActionRepository SubMainScreenActions { get; private set; }
    public IMainScreenRepository MainScreens { get; private set; }
    public IScreenCategoryRepository ScreenCategories { get; private set; }
    public IScreenRepository Screens { get; private set; }
    public IScreenActionRepository ScreenActions { get; private set; }
    public IAccountingWayRepository AccountingWays { get; private set; }
    public IAllowanceRepository Allowances { get; private set; }
    public IBenefitRepository Benefits { get; private set; }
    public ICompanyRepository Companies { get; private set; }
    public ICompanyContractsRepository CompanyContracts { get; private set; }
    public ICompanyLicenseRepository CompanyLicenses { get; private set; }
    public ICompanyTypeRepository CompanyTypes { get; private set; }
    public IContractAllowancesDetailRepository ContractAllowancesDetails { get; private set; }
    public IContractRepository Contracts { get; private set; }
    public IDeductionRepository Deductions { get; private set; }
    public IDepartmentRepository Departments { get; private set; }
    public IEmployeeAttachmentRepository EmployeeAttachments { get; private set; }
    public IEmployeeRepository Employees { get; private set; }
    public IEmployeeTypeRepository EmployeeTypes { get; private set; }
    public IFingerPrintRepository FingerPrints { get; private set; }
    public IJobRepository Jobs { get; private set; }
    public IQualificationRepository Qualifications { get; private set; }
    public ISalaryPaymentWayRepository SalaryPaymentWays { get; private set; }
    public ISectionDepartmentRepository SectionDepartments { get; private set; }
    public ISectionRepository Sections { get; private set; }
    public IShiftRepository Shifts { get; private set; }
    public IVacationDistributionRepository VacationDistributions { get; private set; }
    public IVacationRepository Vacations { get; private set; }
    public IVacationTypeRepository VacationTypes { get; private set; }

    public ITransAllowanceRepository TransAllowances { get; private set; }
    public ITransSalaryEffectRepository TransSalaryEffects { get; private set; }
    public ITransAmountTypeRepository TransAmountTypes { get; private set; }
    public ITransBenefitRepository TransBenefits { get; private set; }
    public ITransCovenantRepository TransCovenants { get; private set; }
    public ITransDeductionRepository TransDeductions { get; private set; }
    public ITransVacationRepository TransVacations { get; private set; }
    public IManagementRepository Managements { get; private set; }
    public ITitleRepository Titles { get; private set; }
    public INationalityRepository Nationalities { get; private set; }
    public IMaritalStatusRepository MaritalStatus { get; private set; }
    public IGenderRepository Genders { get; private set; }
    public IReligionRepository Religions { get; private set; }

    public ITransLoanDetailsRepository TransLoanDetails { get; private set; }

    public IStoredProcuduresRepo StoredProcuduresRepo { get; private set; }

    public ITransSalaryCalculatorRepo TransSalaryCalculator { get; private set; }

    public ITransSalaryCalculatorDetailsRepo TransSalaryCalculatorDetailsRepo { get; private set; }

    public ITransSalaryIncreaseRepository TransSalaryIncrease { get; private set; }
    public ISalaryIncreaseTypeRepository SalaryIncreaseTypeRepository { get; private set; }
    public IPermessionStructureRepository PermessionStructure { get; private set; }

    #region Employee_Requests_UOW
    public IEmployeeRequestsRepository EmployeeRequests { get; private set; }
    public ILeavePermissionRequestRepository LeavePermissionRequest { get; private set; }

    public IDelayPermissionServiceRepository DelayPermission { get; private set; }
    public IVacationRequestRepository VacationRequests { get; private set; }

    public IAllowanceRequestRepository AllowanceRequests { get; private set; }

    public ISalaryIncreaseRequestServicesReository SalaryIncreaseRequest { get; private set; }

    public IContractTerminationRequestRepositroy ContractTerminationRequest { get; private set; }

    public ILoanRequestRepository LoanRequestRepository { get; private set; }



    public IResignationRequesteRepository ResignationRepository { get; private set; }

    public ITitlePermissionRepositroy TitlePermissionRepository { get; private set; }

    public IUserPermssionRepositroy UserPermssionRepositroy { get; private set; }
    #endregion
    public IActionsRepository ActionsRepo { get; private set; }
    public string DatabaseName { get; set; }

    public IEmployeeNotesRepository EmployeeNotes { get; private set; }

    public IApplicantRepository Applicant { get; private set; }

    public IEducationRepository Education { get; private set; }
    public IExperienceRepository Experience { get; private set; }
    public IInterJobRepository InterJob { get; private set; }


    public UnitOfWork(KaderDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
        UserPermssionRepositroy = new UserPermissionRepository(_context);
        TitlePermissionRepository = new TitlePermssionRepository(_context);
        ContractTerminationRequest = new ContractTerminationRequestRepository(_context);
        TransSalaryCalculator = new TransSalaryCalculatorRepo(_context);
        ResignationRepository = new ResignationRequestRepository(_context);
        TransSalaryCalculatorDetailsRepo = new TransSalaryCalculatorDetailsRepo(_context);
        LoanRequestRepository = new LoanRequestRepository(_context);
        TransLoanDetails = new TransLoanDetailsRepository(_context);
        StoredProcuduresRepo = new StoredProcuduresRepo(_context);
        Nationalities = new NationalityRepository(_context);
        MaritalStatus = new MaritalStatusRepository(_context);
        AdvancedTypesRepository = new AdavnacedTypeRepository(_context);
        Genders = new GenderRepository(_context);
        Religions = new ReligionRepository(_context);
        Users = new UserRepository(_context);
        RoleClaims = new RoleClaimRepository(_context);
        UserDevices = new UserDeviceRepository(_context);
        Roles = new RoleRepository(_context);
        UserRoles = new UserRoleRepository(_context);
        UserClaims = new UserClaimRepository(_context);
        SubScreens = new SubScreenRepository(_context);
        SubMainScreenActions = new SubMainScreenActionRepository(_context);
        MainScreens = new MainScreenRepository(_context);
        Screens = new ScreenRepository(_context);
        ScreenActions = new ScreenActionRepository(_context);
        ScreenCategories = new ScreenCategoryRepository(_context);
        Titles = new TitleRepository(_context);
        AccountingWays = new AccountingWayRepository(_context);
        Allowances = new AllowanceRepository(_context);
        Benefits = new BenefitRepository(_context);
        Companies = new CompanyRepository(_context);
        Managements = new ManagementRepository(_context);
        CompanyContracts = new CompanyContractsRepository(_context);
        CompanyLicenses = new CompanyLicenseRepository(_context);
        CompanyTypes = new CompanyTypeRepository(_context);
        ContractAllowancesDetails = new ContractAllowancesDetailRepository(_context);
        Contracts = new ContractRepository(_context);
        Deductions = new DeductionRepository(_context);
        Departments = new DepartmentRepository(_context);
        EmployeeTypes = new EmployeeTypeRepository(_context);
        Employees = new EmployeeRepository(_context);
        EmployeeAttachments = new EmployeeAttachmentRepository(_context);
        Qualifications = new QualificationRepository(_context);
        FingerPrints = new FingerPrintRepository(_context);
        Jobs = new JobRepository(_context);
        Sections = new SectionRepository(_context);
        SectionDepartments = new SectionDepartmentRepository(_context);
        SalaryPaymentWays = new SalaryPaymentWayRepository(_context);
        VacationTypes = new VacationTypeRepository(_context);
        Vacations = new VacationRepository(_context);
        VacationDistributions = new VacationDistributionRepository(_context);
        Shifts = new ShiftRepository(_context);

        TransAmountTypes = new TransAmountTypeRepository(_context);
        TransSalaryEffects = new TransSalaryEffectRepository(_context);
        TransAllowances = new TransAllowanceRepository(_context);
        TransBenefits = new TransBenefitRepository(_context);
        TransDeductions = new TransDeductionRepository(_context);
        TransCovenants = new TransCovenantRepository(_context);
        TransVacations = new TransVacationRepository(_context);
        LoanRepository = new TransLoanRepository(_context);
        TransSalaryIncrease = new TransSalaryIncreaseRepository(_context);
        SalaryIncreaseTypeRepository = new SalaryIncreaseTypeRepository(_context);
        LeavePermissionRequest = new LeavePermissionRequestRepository(_context);
        VacationRequests = new VacationRequestRepository(_context);
        AllowanceRequests = new AllowanceRequestRepository(_context);
        SalaryIncreaseRequest = new SalaryIncreaseRequestRepository(_context);
        DelayPermission = new DelayPermissionRepository(_context);
        EmployeeRequests = new EmployeeRequestsRepository(_context);
        PermessionStructure = new PermessionStructureRepository(_context);
        ActionsRepo = new ActionsRepository(_context);
        EmployeeNotes = new EmployeeNotesRepository(_context);
        Experience = new ExperienceRepository(_context);
        Education = new EducationRepository(_context);

        Applicant = new ApplicantRepository(_context);
        InterJob = new InterJobRepository(_context);
    }

    public IDatabaseTransaction BeginTransaction() =>
        new EntityDatabaseTransaction(_context);
    public IDatabaseTransactionScope TransactionScope() => new EntityDatabaseTransactionScope();
    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }

    public int Complete() => _context.SaveChanges();
}