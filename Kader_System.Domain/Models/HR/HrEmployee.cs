namespace Kader_System.Domain.Models.HR;

[Table("Hr_Employees")]
public class HrEmployee : BaseEntity
{
    [Key] public int Id { get; set; }
    public required string FirstNameAr { get; set; }
    public required string FirstNameEn { get; set; }
    public required string FatherNameAr { get; set; }
    public required string FatherNameEn { get; set; }
    public required string GrandFatherNameAr { get; set; }
    public required string GrandFatherNameEn { get; set; }
    public required string FamilyNameAr { get; set; }
    public required string FamilyNameEn { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string FullNameAr
    {
        get { return $"{FirstNameAr} {FatherNameAr} {GrandFatherNameAr} {FamilyNameAr}"; }
        private set
        {
        }
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string FullNameEn
    {
        get
        {
            return $"{FirstNameEn} {FatherNameEn} {GrandFatherNameEn} {FamilyNameEn}";
        }
        private set
        {
        }
    }


    public int MaritalStatusId { get; set; }
    [ForeignKey(nameof(MaritalStatusId))]
    public HrMaritalStatus MaritalStatus { get; set; } = default!;

    public required string Address { get; set; }
    public required double FixedSalary { get; set; }
    public required DateOnly HiringDate { get; set; }
    public required DateOnly ImmediatelyDate { get; set; }
    public required bool IsActive { get; set; }
  
    public required double TotalSalary { get; set; }
    public int GenderId { get; set; }
    [ForeignKey(nameof(GenderId))]
    public HrGender Gender { get; set; } = default!;

    public required DateOnly BirthDate { get; set; }
    public int ReligionId { get; set; }
    [ForeignKey(nameof(ReligionId))]
    public HrRelegion Religion { get; set; } = default!;

    public required string Phone { get; set; }
    public required string Email { get; set; }
    public  string NationalId { get; set; }
    public string JobNumber { get; set; }

    public int SalaryPaymentWayId { get; set; }
    [ForeignKey(nameof(SalaryPaymentWayId))]
    public HrSalaryPaymentWay SalaryPaymentWay { get; set; } = default!;

    public string? UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
    [DefaultValue(0)]
    public required int ChildrenNumber { get; set; }



    public int ShiftId { get; set; }
    [ForeignKey(nameof(ShiftId))]
    public HrShift Shift { get; set; } = default!;
    public int CompanyId { get; set; }
    [ForeignKey(nameof(CompanyId))]
    public HrCompany? Company { get; set; } = default!;
    /// <summary>
    /// جهاز البصمه
    /// </summary>
    public int? FingerPrintId { get; set; }

    [ForeignKey(nameof(FingerPrintId))]
    public HrFingerPrint FingerPrint { get; set; } = default!;
    /// <summary>
    /// كود الموظف المسجل على جهاز البصمه
    /// </summary>
    public string? FingerPrintCode { get; set; }

    public string? EmployeeImage { get; set; }
    public string? EmployeeImageExtension { get; set; }

    public int ManagementId { get; set; }
    //[ForeignKey(nameof(ManagementId))]
    public HrManagement Management { get; set; } = default!;

    public int DepartmentId { get; set; }
    //[ForeignKey(nameof(DepartmentId))]
    public HrDepartment Department { get; set; } = default!;

    public int NationalityId { get; set; }
    [ForeignKey(nameof(NationalityId))]
    public HrNationality Nationality { get; set; } = default!;

    public int QualificationId { get; set; }
    [ForeignKey(nameof(QualificationId))]
    public HrQualification Qualification { get; set; } = default!;

    public int VacationId { get; set; }
    [ForeignKey(nameof(VacationId))]
    public HrVacation Vacation { get; set; } = default!;

    public int JobId { get; set; }
    [ForeignKey(nameof(JobId))]
    public HrJob Job { get; set; } = default!;
    /// <summary>
    /// مقيم / مواطن
    /// </summary>
    public int EmployeeTypeId { get; set; }
    [ForeignKey(nameof(EmployeeTypeId))]
    public HrEmployeeType EmployeeType { get; set; } = default!;

    public long? AccountNo { get; set; }
    public string? Note { get; set; }
    public ICollection<HrEmployeeAttachment> ListOfAttachments { get; set; } = [];


}
