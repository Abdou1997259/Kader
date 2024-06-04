namespace Kader_System.DataAccesss.DbContext;

public class KaderDbContext(DbContextOptions<KaderDbContext> options, IHttpContextAccessor accessor) : IdentityDbContext<ApplicationUser, ApplicationRole, string,
             ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
             ApplicationRoleClaim, ApplicationUserToken>(options)
{
    public IHttpContextAccessor Accessor { get; set; } = accessor;

    #region Data Sets

    public DbSet<ApplicationUserDevice> UserDevices { get; set; }
    public DbSet<ComLog> Logs { get; set; }
    public DbSet<StMainScreen> MainScreenCategories { get; set; }
    public DbSet<Screen> Screens { get; set; }
    public DbSet<StMainScreenCat> MainScreens { get; set; }
    public DbSet<StScreenSub> SubMainScreens { get; set; }
    public DbSet<StAction> Actions { get; set; }
    public DbSet<StSubMainScreenAction> SubMainScreenActions { get; set; }

    public DbSet<HrSalaryCalculator> AccountingWays { get; set; }
    public DbSet<HrAllowance> Allowances { get; set; }
    public DbSet<HrBenefit> Benefits { get; set; }
    public DbSet<HrCompany> Companys { get; set; }
    public DbSet<HrCompanyContract> CompanyContracts { get; set; }
    public DbSet<CompanyLicense> CompanyLicenses { get; set; }
    public DbSet<HrCompanyType> CompanyTypes { get; set; }
    public DbSet<HrContract> Contracts { get; set; }
    public DbSet<HrContractAllowancesDetail> ContractAllowancesDetails { get; set; }
    public DbSet<HrDeduction> Deductions { get; set; }
    public DbSet<HrDepartment> Departments { get; set; }
    public DbSet<HrEmployee> Employees { get; set; }
    public DbSet<HrEmployeeAttachment> EmployeeAttachments { get; set; }
    public DbSet<HrEmployeeType> EmployeeTypes { get; set; }
    public DbSet<HrFingerPrint> FingerPrints { get; set; }
    public DbSet<HrSection> Sections { get; set; }
    public DbSet<HrSalaryPaymentWay> SalaryPaymentWays { get; set; }
    public DbSet<HrGender> Genders { get; set; }
    public DbSet<HrSectionDepartment> SectionDepartments { get; set; }
    public DbSet<HrVacationDistribution> VacationDistributions { get; set; }
    public DbSet<HrShift> Shifts { get; set; }
    public DbSet<HrVacation> Vacations { get; set; }
    public DbSet<HrVacationType> VacationTypes { get; set; }
    public DbSet<HrValueType> ValueTypes { get; set; }

    public DbSet<TransSalaryIncrease> TransSalaryIncreases { get; set; }
    public DbSet<TransAllowance> TransAllowances { get; set; }
    public DbSet<TransAmountType> TransAmountTypes { get; set; }
    public DbSet<TransBenefit> TransBenefits { get; set; }
    public DbSet<TransCovenant> TransCovenants { get; set; }
    public DbSet<TransDeduction> TransDeductions { get; set; }
    public DbSet<TransSalaryEffect> TransSalaryEffects { get; set; }
    public DbSet<TransVacation> TransVacations { get; set; }
    public DbSet<HrJob> HrJobs { get; set; }
    public DbSet<HrQualification> HrQualifications { get; set; }
    public DbSet<HrManagement> Managements { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<TitlePermission> TitlePermissions { get; set; }
    public DbSet<HrNationality> Nationalities { get; set; }
    public DbSet<HrRelegion> Relegions { get; set; }
    public DbSet<HrMaritalStatus> MaritalStatus { get; set; }
    
    #endregion
    //


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.SeedData();
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        //modelBuilder.AddQueryFilterToAllEntitiesAssignableFrom<BaseEntity>(x => x.IsDeleted == false);
        modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()).ToList().ForEach(x => x.DeleteBehavior = DeleteBehavior.NoAction);

        modelBuilder.Entity<ApplicationUser>().ToTable("Auth_Users", "dbo");
        modelBuilder.Entity<ApplicationRole>().ToTable("Auth_Roles", "dbo");
        modelBuilder.Entity<ApplicationUserRole>().ToTable("Auth_UserRoles", "dbo");
        modelBuilder.Entity<ApplicationUserClaim>().ToTable("Auth_UserClaims", "dbo");
        modelBuilder.Entity<ApplicationUserLogin>().ToTable("Auth_UserLogins", "dbo");
        modelBuilder.Entity<ApplicationRoleClaim>().ToTable("Auth_RoleClaims", "dbo");
        modelBuilder.Entity<ApplicationUserToken>().ToTable("Auth_UserTokens", "dbo");
        modelBuilder.Entity<HrEmployee>().Property(e => e.FullNameAr)
            .HasComputedColumnSql("[FirstNameAr]+' '+[FatherNameAr]+' '+[GrandFatherNameAr]+' '+[FamilyNameAr]");
        modelBuilder.Entity<HrEmployee>().Property(e => e.FullNameEn)
            .HasComputedColumnSql("[FirstNameEn]+' '+[FatherNameEn]+' '+[GrandFatherNameEn]+' '+[FamilyNameEn]");

        modelBuilder.Entity<HrContract>()
            .HasMany(contract => contract.ListOfAllowancesDetails)
            .WithOne(detail => detail.Contract)
            .HasForeignKey(detail => detail.ContractId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        #region Fluent Api
        //modelBuilder.Entity<HrManagement>()
        //    .HasOne(m => m.Manager)
        //    .WithOne(e => e.Management)
        //    .HasForeignKey<HrManagement>(m => m.ManagerId);


        //modelBuilder.Entity<HrDepartment>()
        //    .HasOne(d => d.Manager)
        //    .WithOne(e => e.Department)
        //    .HasForeignKey<HrEmployee>(e => e.DepartmentId);

        //modelBuilder.Entity<HrEmployee>()
        //    .HasOne(x => x.User)
        //    .WithOne(y => y.Employee)
        //    .HasForeignKey<ApplicationUser>(z => z.Employee_Id);

        #endregion
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        DateTime dateNow = new DateTime().NowEg();
        string userId = Accessor!.HttpContext == null ? string.Empty : Accessor!.HttpContext!.User.GetUserId();

        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified
                    || e.State == EntityState.Deleted)
        );
        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).UpdateDate = dateNow;
            ((BaseEntity)entityEntry.Entity).UpdateBy = userId;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).Add_date = dateNow;
                ((BaseEntity)entityEntry.Entity).Added_by = userId;
            }
            if (entityEntry.State == EntityState.Deleted)
            {
                if (((BaseEntity)entityEntry.Entity).IsDeleted)
                {
                    entityEntry.State = EntityState.Deleted;
                }
                else
                {
                    entityEntry.State = EntityState.Modified;
                    ((BaseEntity)entityEntry.Entity).DeleteDate = dateNow;
                    ((BaseEntity)entityEntry.Entity).DeleteBy = userId;
                    ((BaseEntity)entityEntry.Entity).IsDeleted = true;
                }

            }
        }
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
