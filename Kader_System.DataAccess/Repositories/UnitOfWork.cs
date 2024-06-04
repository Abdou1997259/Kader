using Kader_System.Domain.Models;
using Kader_System.Domain.Models.HR;
using Kader_System.Domain.Models.Setting;

namespace Kader_System.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly KaderDbContext _context;
    protected readonly IConfiguration _config;

    public IUserRepository Users { get; private set; }
    public IRoleClaimRepository RoleClaims { get; private set; }
    public IUserClaimRepository UserClaims { get; private set; }
    public IRoleRepository Roles { get; private set; }
    public IUserRoleRepository UserRoles { get; private set; }
    public IUserDeviceRepository UserDevices { get; private set; }
    public ISubMainScreenRepository SubMainScreens { get; private set; }
    public ISubMainScreenActionRepository SubMainScreenActions { get; private set; }
    public IMainScreenRepository MainScreens { get; private set; }
    public IMainScreenCategoryRepository MainScreenCategories { get; private set; }
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
    public UnitOfWork(KaderDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
        Nationalities = new NationalityRepository(_context);
        MaritalStatus = new MaritalStatusRepository(_context);
        Genders=new GenderRepository(_context);
        Religions=new ReligionRepository(_context);
          Users = new UserRepository(_context);
        RoleClaims = new RoleClaimRepository(_context);
        UserDevices = new UserDeviceRepository(_context);
        Roles = new RoleRepository(_context);
        UserRoles = new UserRoleRepository(_context);
        UserClaims=new UserClaimRepository(_context);
        SubMainScreens = new SubMainScreenRepository(_context);
        SubMainScreenActions = new SubMainScreenActionRepository(_context);
        MainScreens = new MainScreenRepository(_context);
        Screens=new ScreenRepository(_context);
        ScreenActions=new ScreenActionRepository(_context);
        MainScreenCategories = new MainScreenCategoryRepository(_context);
        Titles=new TitleRepository(_context);
        AccountingWays = new AccountingWayRepository(_context);
        Allowances = new AllowanceRepository(_context);
        Benefits = new BenefitRepository(_context);
        Companies = new CompanyRepository(_context);
        Managements = new ManagementRepository(_context);
        CompanyContracts =new CompanyContractsRepository(_context);
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
        TransVacations=new TransVacationRepository(_context);
    }

    public IDatabaseTransaction BeginTransaction() =>
        new EntityDatabaseTransaction(_context);

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }

    public int Complete() => _context.SaveChanges();
}
