using Kader_System.Domain.Constants.Enums;
using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.DataAccess.Repositories.HR;

public class EmployeeRepository(KaderDbContext context) : BaseRepository<HrEmployee>(context), IEmployeeRepository
{

    private readonly KaderDbContext _context = context;

    public Response<GetEmployeeByIdResponse> GetEmployeeByIdAsync(int id, string lang)
    {
        try
        {
            HrDirectoryTypes directoryTypes = new();
            directoryTypes = HrDirectoryTypes.Attachments;
            var directoryAttachmentsName = directoryTypes.GetModuleNameWithType(Modules.Employees);
            directoryTypes = HrDirectoryTypes.EmployeeProfile;
            var directoryProfileName = directoryTypes.GetModuleNameWithType(Modules.Employees);
            var employeeAllowances = context.TransAllowances.Where(e => e.EmployeeId == id && !e.IsDeleted);
            var employeeVacations = context.TransVacations.Where(e => e.EmployeeId == id && !e.IsDeleted);

            var result = from employee in context.Employees

                         join user in context.Users on employee.UserId equals user.Id into userGroup
                         from usr in userGroup.DefaultIfEmpty()
                         join department in context.Departments on employee.DepartmentId equals department.Id into deptGroup
                         from dept in deptGroup.DefaultIfEmpty()
                         join management in context.Managements on employee.ManagementId equals management.Id into manGroup
                         from man in manGroup.DefaultIfEmpty()

                         join qualification in context.HrQualifications on employee.QualificationId equals qualification.Id into
                             qualGroup
                         from qual in qualGroup.DefaultIfEmpty()
                         join job in context.HrJobs on employee.JobId equals job.Id into jobGroup
                         from j in jobGroup.DefaultIfEmpty()
                         join relegion in context.Relegions on employee.ReligionId equals relegion.Id into relegioGroup
                         from r in relegioGroup.DefaultIfEmpty()

                         join company in context.Companys on employee.CompanyId equals company.Id into companyGroup
                         from com in companyGroup.DefaultIfEmpty()

                         join nationality in context.Nationalities on employee.NationalityId equals nationality.Id into
                             nationalityGroup
                         from nat in nationalityGroup.DefaultIfEmpty()

                         join marital in context.MaritalStatus on employee.MaritalStatusId equals marital.Id into maritalGroup
                         from ms in maritalGroup.DefaultIfEmpty()

                         join shift in context.Shifts on employee.MaritalStatusId equals shift.Id into shiftGroup
                         from sh in shiftGroup.DefaultIfEmpty()

                         join c in _context.Contracts
                         on employee.Id equals c.EmployeeId
                         into contractGroup
                         from cGroup in contractGroup.DefaultIfEmpty()

                         where employee.Id == id
                         select new GetEmployeeByIdResponse()
                         {
                             FullNameAr = employee.FullNameAr,
                             FullNameEn = employee.FullNameEn,
                             ManagementId = employee.ManagementId,
                             CompanyId = employee.CompanyId,
                             FirstNameEn = employee.FirstNameEn,
                             FatherNameAr = employee.FatherNameAr,
                             FatherNameEn = employee.FatherNameEn,
                             GrandFatherNameAr = employee.GrandFatherNameAr,
                             GrandFatherNameEn = employee.GrandFatherNameEn,
                             FamilyNameAr = employee.FamilyNameAr,
                             FamilyNameEn = employee.FamilyNameEn,
                             FirstNameAr = employee.FirstNameAr,
                             IsActive = employee.IsActive,
                             VacationId = employee.VacationId,
                             AccountNo = employee.AccountNo,
                             Address = employee.Address,
                             BirthDate = employee.BirthDate,
                             ChildrenNumber = employee.ChildrenNumber,
                             DepartmentId = employee.DepartmentId,
                             Email = employee.Email,
                             EmployeeImageExtension = employee.EmployeeImageExtension,
                             EmployeeTypeId = employee.EmployeeTypeId,
                             FingerPrintCode = employee.FingerPrintCode,
                             FingerPrintId = employee.FingerPrintId,
                             FixedSalary = cGroup == null ? employee.FixedSalary : cGroup.FixedSalary,
                             GenderId = employee.GenderId,
                             HiringDate = employee.HiringDate,
                             ImmediatelyDate = employee.ImmediatelyDate,
                             Id = employee.Id,

                             JobId = employee.JobId,
                             JobNumber = employee.JobNumber,
                             MaritalStatusId = employee.MaritalStatusId,
                             NationalId = employee.NationalId,
                             NationalityId = employee.NationalityId,
                             Phone = employee.Phone,
                             QualificationId = employee.QualificationId,
                             ReligionId = employee.ReligionId,
                             SalaryPaymentWayId = employee.SalaryPaymentWayId,
                             ShiftId = employee.ShiftId,
                             TotalSalary = cGroup.FixedSalary == null ? 0 : cGroup.FixedSalary + cGroup.HousingAllowance == null ? cGroup.HousingAllowance : cGroup.HousingAllowance,
                             EmployeeImage = Path.Combine(directoryProfileName, employee.EmployeeImage == null ? " " : employee.EmployeeImage),
                             qualification_name = lang == Localization.Arabic ? qual.NameAr : qual.NameEn,
                             company_name = lang == Localization.Arabic ? com.NameAr : com.NameEn,
                             management_name = lang == Localization.Arabic ? man.NameAr : man.NameEn,
                             employee_loans_count = 0,
                             vacation_days_count = employeeVacations

                                 .AsEnumerable()
                                 .Where(v => v != null) // Optional: Filter out null values to avoid runtime exceptions
                                 .Sum(v => v.DaysCount),
                             job_name = lang == Localization.Arabic ? j.NameAr : j.NameEn,
                             department_name = lang == Localization.Arabic ? dept.NameAr : dept.NameEn,
                             marital_status_name = lang == Localization.Arabic ? ms.Name : ms.NameInEnglish,
                             nationality_name = lang == Localization.Arabic ? nat.Name : nat.NameInEnglish,
                             religion_name = lang == Localization.Arabic ? r.Name : r.NameInEnglish,
                             note = employee.Note,
                             shift_name = lang == Localization.Arabic ? sh.Name_ar : sh.Name_en,
                             allowances_sum = employeeAllowances.Any() ? employeeAllowances.AsEnumerable().Sum(a => a.Amount) : 0,
                             employee_loans_sum = 0,
                             Username = usr.UserName == null ? "" : usr.UserName,
                             title_id = usr.CurrentTitleId == null ? 0 : usr.CurrentTitleId,
                             employee_attachments = employee.ListOfAttachments == null ? null! : employee.ListOfAttachments.Select(s => new EmployeeAttachmentForEmp
                             {
                                 FileName = s.FileName,
                                 Extention = s.FileExtension,
                                 Id = s.Id,
                                 file_path = s.FileName != null ? Path.Combine(directoryAttachmentsName, s.FileName) : null,
                             }).ToList()

                         };





            if (result?.FirstOrDefault()?.Id == null || result?.FirstOrDefault()?.Id == 0)
            {
                return new()
                {
                    Msg = $"Employee with id {id} can not be found",
                    Check = false,
                    Data = null,
                    Error = string.Empty
                };
            }



            return new Response<GetEmployeeByIdResponse>()
            {
                Check = true,
                Data = result?.FirstOrDefault(),
                Error = string.Empty,
                Msg = string.Empty
            };
        }
        catch (Exception exception)
        {
            return new()
            {
                Msg = $"ERROR",
                Check = false,
                Data = null,
                Error = exception.InnerException != null ? exception.InnerException.ToString() : exception.Message
            };
        }
    }

    public async Task<object> GetEmployeesDataAsLookUp(string lang)
    {
        return await context.Employees.
            Where(e => !e.IsDeleted && e.IsActive)
            .Include(j => j.Job)
            .Include(j => j.Company)
            .Include(j => j.Nationality)
            .Include(j => j.Management)
            .Select(e => new
            {
                id = e.Id,
                employee_image = e.EmployeeImage,
                add_date = e.Add_date,
                employee_name = lang == Localization.Arabic ? e.FullNameAr : e.FullNameEn,
                employee_job = lang == Localization.Arabic ? e.Job!.NameAr : e.Job!.NameEn,
                gender = e.GenderId,
                hiring_date = e.HiringDate,
                salary_fixed = e.FixedSalary,
                salary_total = e.TotalSalary,
                immediately_date = e.ImmediatelyDate,
                is_active = e.IsActive,
                employee_serial = e.JobNumber,
                payment_method = e.SalaryPaymentWayId,
                note = e.Note,
                company_name = lang == Localization.Arabic ? e.Company!.NameAr : e.Company!.NameEn,
                nationality_name = lang == Localization.Arabic ? e.Nationality!.Name : e.Nationality!.NameInEnglish,
                management_id = e.ManagementId,
                management_name = lang == Localization.Arabic ? e.Management!.NameAr : e.Management!.NameEn,
                department_id = e.DepartmentId,
                marital_status = e.MaritalStatusId
            }).ToListAsync();
    }

    public async Task<object> GetEmployeesDataNameAndIdAsLookUp(string lang, int companyId)
    {

        return await context.Employees.
            Where(e => !e.IsDeleted && e.IsActive && e.CompanyId == companyId)

            .Select(e => new
            {
                id = e.Id,
                employee_name = lang == Localization.Arabic ? e.FullNameAr : e.FullNameEn,
            }).ToArrayAsync();
    }
    public async Task<object> GetEmployeesNameIdSalaryAsLookUp(string lang)
    {

        return await context.Employees.
            Where(e => !e.IsDeleted && e.IsActive)
            .Select(e => new
            {
                id = e.Id,
                employee_name = lang == Localization.Arabic ? e.FullNameAr : e.FullNameEn,
                Salary = e.FixedSalary
            }).ToArrayAsync();
    }
    public async Task<object> GetEmployeesNameIdSalaryWithoutContractAsLookUp(string lang, int companyId)
    {

        var employees = from e in context.Employees
                        join c in context.Contracts
                        on e.Id equals c.EmployeeId into ecgroup
                        from u in ecgroup.DefaultIfEmpty()
                        where u.EmployeeId == null && !e.IsDeleted && e.CompanyId == companyId
                        select new
                        {
                            Id = e.Id,
                            Name = Localization.Arabic == lang ? e.FullNameAr : e.FullNameEn,

                        };

        return employees;




    }

    public async Task<List<EmployeesData>> GetAllEmployeeDetails(bool isDeleted, int companyId, int skip = 0, int take = 10, string lang = "en")
    {
        HrDirectoryTypes directoryTypes = new();
        directoryTypes = HrDirectoryTypes.Attachments;
        var directoryAttachmentsName = directoryTypes.GetModuleNameWithType(Modules.Employees);
        directoryTypes = HrDirectoryTypes.EmployeeProfile;
        var directoryProfileName = directoryTypes.GetModuleNameWithType(Modules.Employees);



        var query = await _context.SPEmployeeDetails
            .FromSql($"EXEC SP_GetEmployeeDetails {isDeleted}, {lang} ,{companyId}")


            .AsNoTracking().
            ToListAsync();

        var result = query.OrderBy(x => x.Id).Skip(skip).Take(take).
             Select(emp => new EmployeesData
             {
                 Management = emp.ManagementName,
                 Address = emp.Address,
                 BirthDate = DateOnly.FromDateTime(emp.BirthDate.Value),
                 ChildrenNumber = emp.ChildrenNumber,
                 Department = emp.DepartmentName,
                 Email = emp.Email,
                 MaritalStatus = emp.MaritalStatusName,
                 EmployeeType = emp.EmployeeType,
                 //FingerPrint = lang == Localization.Arabic ? (fingerPrint != null ? fingerPrint.NameAr : "") : (fingerPrint != null ? fingerPrint.NameEn : ""),
                 //FingerPrintCode = emp.FingerPrintCode != null ? emp.FingerPrintCode : "",
                 FullName = emp.FullName,
                 HiringDate = DateOnly.FromDateTime(emp.HiringDate.Value),
                 ImmediatelyDate = DateOnly.FromDateTime(emp.ImmediatelyDate.Value),
                 IsActive = emp.IsActive,
                 JobNumber = emp.JobNumber,
                 NationalId = emp.NationalId,
                 Nationality = emp.Nationality,
                 Religion = emp.Nationality,
                 Job = emp.Job,
                 qualification_name = emp.QualificationName,
                 title_id = emp.TitleId,
                 Phone = emp.Phone,
                 vacation_days_count = emp.VacationDaysCount,
                 Username = emp.Username,
                 Vacation = emp.Vacation,
                 department_name = emp.DepartmentName,
                 management_name = emp.ManagementName,
                 marital_status_name = emp.MaritalStatusName,
                 Id = emp.Id,
                 company_name = emp.CompanyName,
                 Company = emp.CompanyName,
                 Shift = emp.Shift,
                 //employee_loans_count = loansCount,
                 SalaryPaymentWay = emp.SalaryPaymentWay,
                 Gender = emp.Gender,
             }).ToList();
        return result;
    }

    //public List<EmployeesData> GetEmployeesInfo(
    //    Expression<Func<HrEmployee, bool>> filter,
    //    Expression<Func<EmployeesData, bool>> filterSearch,
    //    int? skip = null,
    //    int? take = null, string lang = "ar"
    //   )
    //{
    //    HrDirectoryTypes directoryTypes = new();
    //    directoryTypes = HrDirectoryTypes.Attachments;
    //    var directoryAttachmentsName = directoryTypes.GetModuleNameWithType(Modules.Employees);
    //    directoryTypes = HrDirectoryTypes.EmployeeProfile;
    //    var directoryProfileName = directoryTypes.GetModuleNameWithType(Modules.Employees);
    //    var employees = context.Employees.AsNoTracking().Where(filter);


    //    #region Old
    //    //var query = from emp in employees
    //    //            join u in context.Users on emp.Added_by equals u.Id into userGroup
    //    //            from u in userGroup.DefaultIfEmpty()
    //    //            join management in context.Managements on emp.ManagementId equals management.Id into managementGroup
    //    //            from management in managementGroup.DefaultIfEmpty()
    //    //            join job in context.HrJobs on emp.JobId equals job.Id into jobGroup
    //    //            from job in jobGroup.DefaultIfEmpty()
    //    //            join marital in context.MaritalStatus on emp.MaritalStatusId equals marital.Id into maritalGroup
    //    //            from marital in maritalGroup.DefaultIfEmpty()
    //    //            join empType in context.EmployeeTypes on emp.EmployeeTypeId equals empType.Id into empTypeGroup
    //    //            from empType in empTypeGroup.DefaultIfEmpty()
    //    //            join nationality in context.Nationalities on emp.NationalityId equals nationality.Id into nationalityGroup
    //    //            from nationality in nationalityGroup.DefaultIfEmpty()
    //    //            join religion in context.Relegions on emp.ReligionId equals religion.Id into religionGroup
    //    //            from religion in religionGroup.DefaultIfEmpty()
    //    //            join qual in context.HrQualifications on emp.QualificationId equals qual.Id into qualGroup
    //    //            from qual in qualGroup.DefaultIfEmpty()
    //    //            join vacation in context.Vacations on emp.VacationId equals vacation.Id into vacationGroup
    //    //            from vacation in vacationGroup.DefaultIfEmpty()
    //    //            join company in context.Companys on emp.CompanyId equals company.Id into companyGroup
    //    //            from company in companyGroup.DefaultIfEmpty()
    //    //            join shift in context.Shifts on emp.ShiftId equals shift.Id into shiftGroup
    //    //            from shift in shiftGroup.DefaultIfEmpty()
    //    //            join department in context.Departments on emp.DepartmentId equals department.Id into departmentGroup
    //    //            from department in departmentGroup.DefaultIfEmpty()
    //    //            join salary in context.SalaryPaymentWays on emp.SalaryPaymentWayId equals salary.Id into salaryGroup
    //    //            from salary in salaryGroup.DefaultIfEmpty()
    //    //            join gender in context.Genders on emp.GenderId equals gender.Id into genderGroup
    //    //            from gender in genderGroup.DefaultIfEmpty()
    //    //            join att in context.EmployeeAttachments on emp.Id equals att.EmployeeId into attGroup
    //    //            from att in attGroup.DefaultIfEmpty()            
    //    //            //join fingerPrint in context.FingerPrints on emp.Id equals fingerPrint.emp into attGroup
    //    //            //from att in attGroup.DefaultIfEmpty()
    //    //            select new EmployeesData()
    //    //            {
    //    //                Management = lang == Localization.Arabic ? (management != null ? management.NameAr : "") : (management != null ? management.NameEn : ""),
    //    //                Address = emp.Address != null ? emp.Address : "",
    //    //                BirthDate = emp.BirthDate,
    //    //                ChildrenNumber = emp.ChildrenNumber != null ? emp.ChildrenNumber : 0,
    //    //                Department = lang == Localization.Arabic ? (department != null ? department.NameAr : "") : (department != null ? department.NameEn : ""),
    //    //                Email = emp.Email != null ? emp.Email : "",
    //    //                MaritalStatus = lang == Localization.Arabic ? (marital != null ? marital.Name : "") : (marital != null ? marital.NameInEnglish : ""),
    //    //                EmployeeType = lang == Localization.Arabic ? (empType != null ? empType.Name : "") : (empType != null ? empType.NameInEnglish : ""),
    //    //                //FingerPrint = lang == Localization.Arabic ? (fingerPrint != null ? fingerPrint.NameAr : "") : (fingerPrint != null ? fingerPrint.NameEn : ""),
    //    //                FingerPrintCode = emp.FingerPrintCode != null ? emp.FingerPrintCode : "",
    //    //                FullName = lang == Localization.Arabic ? (emp.FullNameAr != null ? emp.FullNameAr : "") : (emp.FullNameEn != null ? emp.FullNameEn : ""),
    //    //                HiringDate = emp.HiringDate,
    //    //                ImmediatelyDate = emp.ImmediatelyDate,
    //    //                IsActive = emp.IsActive,
    //    //                JobNumber = emp.JobNumber != null ? emp.JobNumber : " ",
    //    //                NationalId = emp.NationalId != null ? emp.NationalId : " ",
    //    //                Nationality = lang == Localization.Arabic ? (nationality != null ? nationality.Name : "") : (nationality != null ? nationality.NameInEnglish : ""),
    //    //                Religion = lang == Localization.Arabic ? (religion != null ? religion.Name : "") : (religion != null ? religion.NameInEnglish : ""),
    //    //                Job = lang == Localization.Arabic ? (job != null ? job.NameAr : "") : (job != null ? job.NameEn : ""),
    //    //                qualification_name = lang == Localization.Arabic ? (qual != null ? qual.NameAr : "") : (qual != null ? qual.NameEn : ""),
    //    //                title_id = u != null ? u.CurrentTitleId : 0,
    //    //                Phone = emp.Phone != null ? emp.Phone : "",
    //    //                vacation_days_count = vacation != null ? (vacation.TotalBalance != null ? vacation.TotalBalance : 0) : 0,
    //    //                Username = u != null ? u.UserName : "",
    //    //                Vacation = lang == Localization.Arabic ? (vacation != null ? vacation.NameAr : "") : (vacation != null ? vacation.NameEn : ""),
    //    //                department_name = lang == Localization.Arabic ? (department != null ? department.NameAr : "") : (department != null ? department.NameEn : ""),
    //    //                management_name = lang == Localization.Arabic ? (management != null ? management.NameAr : "") : (management != null ? management.NameEn : ""),
    //    //                marital_status_name = lang == Localization.Arabic ? (marital != null ? marital.Name : "") : (marital != null ? marital.NameInEnglish : ""),
    //    //                Id = emp.Id,
    //    //                company_name = lang == Localization.Arabic ? (company != null ? company.NameAr : "") : (company != null ? company.NameEn : ""),
    //    //                Company = lang == Localization.Arabic ? (company != null ? company.NameAr : "") : (company != null ? company.NameEn : ""),
    //    //                Shift = lang == Localization.Arabic ? (shift != null ? shift.Name_ar : "") : (shift != null ? shift.Name_en : ""),
    //    //                //employee_loans_count = loansCount,
    //    //                SalaryPaymentWay = lang == Localization.Arabic ? (salary != null ? salary.Name : "") : (salary != null ? salary.NameInEnglish : ""),
    //    //                Gender = lang == Localization.Arabic ? (gender != null ? gender.Name : "") : (gender != null ? gender.NameInEnglish : "")
    //    //            }; 
    //    #endregion

    //    //var test2 = query.ToQueryString();
    //    //if (filterSearch != null)
    //    //    query = query.Where(filterSearch);

    //    //if (skip.HasValue)
    //    //    query = query.Skip(skip.Value);
    //    //if (take.HasValue)
    //    //    query = query.Take(take.Value);

    //    //var stquery = query.ToQueryString();
    //    //var data = query.ToList();
    //    //return data;

    //}


}
