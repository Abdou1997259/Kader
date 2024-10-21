
using Kader_System.DataAccess.Configrurations.SalaryCalculatorDetailsStoredConfig;
using Kader_System.DataAccess.Configrurations.SpCaclauateSalaryDetailsConfiguration;
using Kader_System.Domain.Constants.Enums;
using Kader_System.Domain.Models.EmployeeRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using Kader_System.Domain.Models.Interviews;

namespace Kader_System.DataAccesss.Context;

public class KaderDbContext(DbContextOptions<KaderDbContext> options, IHttpContextAccessor accessor) :
    IdentityDbContext<ApplicationUser, ApplicationRole, string,
             ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
             ApplicationRoleClaim, ApplicationUserToken>(options)
{
    public IHttpContextAccessor Accessor { get; set; } = accessor;

    #region Data Sets

    public DbSet<SpCacluateSalary> SpCacluateSalariesModel { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<SpGetScreen> SpGetScreens { get; set; }
    public DbSet<CompanyYear> CompanyYears { get; set; }
    public DbSet<SpCaclauateSalaryDetails> SpCaclauateSalaryDetailsModel { get; set; }
    public DbSet<SpCaclauateSalaryDetailedTrans> SpCaclauateSalaryDetailedTransModel { get; set; }

    public DbSet<ApplicationUserDevice> UserDevices { get; set; }
    public DbSet<TransLoan> Loans { get; set; }
    public DbSet<ComLog> Logs { get; set; }
    public DbSet<StMainScreen> MainScreens { get; set; }
    public DbSet<Screen> Screens { get; set; }
    public DbSet<StScreenCat> ScreenCategories { get; set; }
    public DbSet<StScreenSub> SubScreens { get; set; }
    public DbSet<StAction> Actions { get; set; }
    public DbSet<StSubMainScreenAction> SubMainScreenActions { get; set; }

    public DbSet<HrSalaryCalculator> AccountingWays { get; set; }
    public DbSet<TransSalaryCalculator> SalaryCalculator { get; set; }
    public DbSet<TransSalaryCalculatorDetail> TransSalaryCalculatorsDetails { get; set; }
    public DbSet<HrAllowance> Allowances { get; set; }
    public DbSet<HrBenefit> Benefits { get; set; }
    public DbSet<HrCompany> Companys { get; set; }
    public DbSet<HrCompanyContract> CompanyContracts { get; set; }
    public DbSet<CompanyLicense> CompanyLicenses { get; set; }
    public DbSet<HrCompanyType> CompanyTypes { get; set; }
    public DbSet<HrContract> Contracts { get; set; }
    public DbSet<HrContractAllowancesDetail> ContractAllowancesDetails { get; set; }
    public DbSet<HrDeduction> Deductions { get; set; }
    public DbSet<HrEmployeeAttendance> EmployeeAttendances { get; set; }
    public DbSet<HrDepartment> Departments { get; set; }
    public DbSet<HrEmployee> Employees { get; set; }
    public DbSet<QueryLookup> QueryLookups { get; set; }
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
    public DbSet<HrEmployeeNotes> HrEmployeeNotes { get; set; }
    public DbSet<AdvancedType> AdvancedTypes { get; set; }
    public DbSet<HrQualification> HrQualifications { get; set; }
    public DbSet<HrManagement> Managements { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<TitlePermission> TitlePermissions { get; set; }
    public DbSet<HrNationality> Nationalities { get; set; }
    public DbSet<HrRelegion> Relegions { get; set; }
    public DbSet<HrMaritalStatus> MaritalStatus { get; set; }
    public DbSet<MainScreenTree> MainScreenTrees { get; set; }
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }

    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<University> Universities { get; set; }



    #region SP
    public DbSet<SPUserPermissionsBySubScreen> SPUserPermissionsBySubScreens { get; set; }
    public DbSet<SPTitlePermissionsBySubScreen> SPTitlePermissionsBySubScreens { get; set; }
    public DbSet<SPPermissionStruct> SPPermissionsBySubScreen { get; set; }
    public DbSet<SpGetAllScreens> SpGetAllScreens { get; set; }
    public DbSet<SPEmployeeDetails> SPEmployeeDetails { get; set; }
    public DbSet<Job> InterJobs { get; set; }
    public DbSet<JobOffer> JobOffers { get; set; }
    #endregion

    #region EmployeeRequest_Dbset
    public DbSet<HrEmployeeRequests> HrEmployeeRequests { get; set; }
    public DbSet<LeavePermissionRequest> LeavePermissionsRequests { get; set; }
    public DbSet<DelayPermission> HrDelayPermissions { get; set; }
    public DbSet<LoanRequest> HrLoanRequests { get; set; }
    public DbSet<ResignationRequest> HrResignationRequests { get; set; }
    public DbSet<VacationRequests> HrVacationRequests { get; set; }
    public DbSet<AllowanceRequest> AllowanceRequests { get; set; }
    public DbSet<SalaryIncreaseRequest> SalaryIncreaseRequests { get; set; }
    public DbSet<ContractTerminationRequest> ContractTerminationRequests { get; set; }
    public DbSet<StScreenAction> ScreenActions { get; set; }





    #endregion

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
            .HasMany(contract => contract.list_of_allowances_details)
            .WithOne(detail => detail.Contract)
            .HasForeignKey(detail => detail.ContractId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MainScreenTree>()
            .HasOne(p => p.Parent)
            .WithMany(p => p.Children)
            .HasForeignKey(p => p.ParentId)
            .IsRequired(false);

        modelBuilder.Entity<TransLoan>()
            .HasMany(x => x.TransLoanDetails)
            .WithOne(x => x.TransLoan)
            .HasForeignKey(x => x.TransLoanId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);



        modelBuilder.Entity<TransSalaryCalculator>()
      .Property(p => p.Status)
      .HasConversion(
          x => x.ToString(),
          v => string.IsNullOrEmpty(v)
               ? Status.None
               : (Status)Enum.Parse(typeof(Status), v)
      );

        modelBuilder.Entity<SpCacluateSalary>().HasNoKey();


        modelBuilder.ApplyConfiguration(new SpCaclauateSalaryDetailsConfiguration());


        modelBuilder.ApplyConfiguration(new SpCaclauateSalaryDetailedTransConfiguration());
        #region Fluent_API_for_HM

        // Configure HrCompany to HrManagement (one-to-many)
        modelBuilder.Entity<HrCompany>()
            .HasMany(c => c.HrManagements)
            .WithOne(m => m.Company)
            .HasForeignKey(m => m.CompanyId);

        // Configure HrManagement to HrDepartment (one-to-many)
        modelBuilder.Entity<HrManagement>()
            .HasMany(m => m.HrDepartments)
            .WithOne(d => d.Management)
            .HasForeignKey(d => d.ManagementId);

        // Configure HrDepartment to HrEmployee (one-to-many)
        modelBuilder.Entity<HrDepartment>()
            .HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId);

        modelBuilder.Entity<ApplicationUser>()
       .HasOne(u => u.CompanyYear)
       .WithMany(cy => cy.Users)
       .HasForeignKey(u => u.CompanyYearId);
        #endregion

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

        modelBuilder.Entity<TransLoan>()
                .Property(p => p.LoanAmount)
                .HasPrecision(18, 2);

        modelBuilder.Entity<TransLoan>()
           .Property(p => p.PrevDedcutedAmount)
           .HasPrecision(18, 2);

        modelBuilder.Entity<TransLoan>()
           .Property(p => p.MonthlyDeducted)
           .HasPrecision(18, 2);
        modelBuilder.Entity<TransLoanDetails>()
            .Property(p => p.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<StScreenSub>().HasMany(x => x.ListOfActions)
            .WithOne(x => x.ScreenSub);



        modelBuilder.Entity<Applicant>().HasIndex(x => new { x.email, x.phone }).IsUnique();
        #endregion

        #region SubScreen
        //modelBuilder.Entity<StMainScreenCat>()
        //    .HasMany(s => s.StScreenSub)
        //    .WithOne(sub => sub.ScreenCat)
        //    .HasForeignKey(sub => sub.ScreenCatId);
        modelBuilder.Entity<SpGetScreen>().HasNoKey();
        modelBuilder.Entity<StMainScreen>().
            HasMany(x => x.CategoryScreen).
            WithOne(x => x.ScreenMain).
            HasForeignKey(x => x.MainScreenId);

        modelBuilder.Entity<StScreenCat>().
            HasMany(x => x.StScreenSub).
            WithOne(x => x.ScreenCat).
            HasForeignKey(x => x.ScreenCatId);



        modelBuilder.Entity<SPUserPermissionsBySubScreen>(e =>
        {
            e.HasNoKey();
            e.ToView(null);

        });
        modelBuilder.Entity<SPTitlePermissionsBySubScreen>(e =>
        {
            e.HasNoKey();
            e.ToView(null);

        });

        modelBuilder.Entity<SPPermissionStruct>(e =>
        {
            e.HasNoKey();
            e.ToView(null);

        });
        modelBuilder.Entity<SPEmployeeDetails>(e =>
        {
            e.HasNoKey();
            e.ToView(null);

        });
        modelBuilder.Entity<SpGetAllScreens>(g =>
        {
            g.HasNoKey();
            g.ToView(null);

        });




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
