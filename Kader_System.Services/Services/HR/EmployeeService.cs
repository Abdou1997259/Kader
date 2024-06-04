using Kader_System.Domain.DTOs;

namespace Kader_System.Services.Services.HR
{
    public class EmployeeService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> shareLocalizer, IMapper mapper
    , UserManager<ApplicationUser> userManager) : IEmployeeService
    {
        private HrEmployee _instanceEmployee;

        #region Retreive

        public async Task<Response<IEnumerable<ListOfEmployeesResponse>>> ListOfEmployeesAsync(string lang)
        {
            var result =
                await unitOfWork.Employees.GetSpecificSelectAsync(null!
                    , includeProperties: $"{nameof(_instanceEmployee.Company)},{nameof(_instanceEmployee.Management)}," +
                                         $"{nameof(_instanceEmployee.Department)},{nameof(_instanceEmployee.MaritalStatus)}," +
                                         $"{nameof(_instanceEmployee.Nationality)},{nameof(_instanceEmployee.Job)}",
                    select: x => new ListOfEmployeesResponse
                    {
                        Id = x.Id,
                        FullName = lang == Localization.Arabic ? x.FullNameAr : x.FullNameEn,
                        Company = lang == Localization.Arabic ? x.Company!.NameAr : x.Company!.NameEn,
                        Management = lang == Localization.Arabic ? x.Management!.NameAr : x.Management!.NameEn,
                        Job = lang == Localization.Arabic ? x.Job!.NameAr : x.Job!.NameEn,
                        HiringDate = x.HiringDate,
                        IsActive = x.IsActive,
                        Address = x.Address,
                        ImmediatelyDate = x.ImmediatelyDate,
                        MaritalStatus = lang == Localization.Arabic ? x.MaritalStatus!.Name : x.MaritalStatus!.NameInEnglish,
                        Nationality = lang == Localization.Arabic ? x.Nationality!.Name : x.Nationality!.NameInEnglish,

                    }, orderBy: x =>
                        x.OrderByDescending(x => x.Id));

            var listOfEmployeesResponses = result.ToList();
            if (!listOfEmployeesResponses.Any())
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = [],
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = listOfEmployeesResponses,
                Check = true
            };
        }


        public async Task<Response<GetEmployeeByIdResponse>> GetEmployeeByIdAsync(int id, string lang)
        {
            Expression<Func<HrEmployee, bool>> filter = x => x.Id == id;
            var obj = await unitOfWork.Employees.GetFirstOrDefaultAsync(filter,
                includeProperties: $"{nameof(_instanceEmployee.Management)},{nameof(_instanceEmployee.Job)}," +
                                   $"{nameof(_instanceEmployee.User)},{nameof(_instanceEmployee.Company)}," +
                                   $"{nameof(_instanceEmployee.Qualification)},{nameof(_instanceEmployee.Management)}," +
                                   $"{nameof(_instanceEmployee.Department)},{nameof(_instanceEmployee.MaritalStatus)}," +
                                   $"{nameof(_instanceEmployee.Nationality)},{nameof(_instanceEmployee.Religion)}");

            if (obj is null)
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = new()
                {
                    ManagementId = obj.ManagementId,
                    CompanyId = obj.CompanyId,
                    FirstNameEn = obj.FirstNameEn,
                    FatherNameAr = obj.FatherNameAr,
                    FatherNameEn = obj.FatherNameEn,
                    GrandFatherNameAr = obj.GrandFatherNameAr,
                    GrandFatherNameEn = obj.GrandFatherNameEn,
                    FamilyNameAr = obj.FamilyNameAr,
                    FamilyNameEn = obj.FamilyNameEn,
                    FirstNameAr = obj.FirstNameAr,
                    IsActive = obj.IsActive,
                    VacationId = obj.VacationId,
                    AccountNo = obj.AccountNo,
                    Address = obj.Address,
                    BirthDate = obj.BirthDate,
                    ChildrenNumber = obj.ChildrenNumber,
                    DepartmentId = obj.DepartmentId,
                    Email = obj.Email,
                    EmployeeImageExtension = obj.EmployeeImageExtension,
                    EmployeeTypeId = obj.EmployeeTypeId,
                    FingerPrintCode = obj.FingerPrintCode,
                    FingerPrintId = obj.FingerPrintId,
                    FixedSalary = obj.FixedSalary,
                    GenderId = obj.GenderId,
                    HiringDate = obj.HiringDate,
                    ImmediatelyDate = obj.ImmediatelyDate,
                    Id = obj.Id,
                    JobId = obj.JobId,
                    JobNumber = obj.JobNumber,
                    MaritalStatusId = obj.MaritalStatusId,
                    NationalId = obj.NationalId,
                    NationalityId = obj.NationalityId,
                    Phone = obj.Phone,
                    QualificationId = obj.QualificationId,
                    ReligionId = obj.ReligionId,
                    SalaryPaymentWayId = obj.SalaryPaymentWayId,
                    ShiftId = obj.ShiftId,
                    TotalSalary = obj.TotalSalary,
                    Username = obj.User!.UserName,
                    EmployeeImage = $"{ReadRootPath.EmployeeImagesPath}{obj.EmployeeImage}",
                    qualification_name = lang == Localization.Arabic ? obj.Qualification!.NameAr : obj.Qualification!.NameEn,
                    company_name = lang == Localization.Arabic ? obj.Company!.NameAr : obj.Company!.NameEn,
                    management_name = lang == Localization.Arabic ? obj.Management!.NameAr : obj.Management!.NameEn,
                    employee_loans_count = 0,
                    vacation_days_count = 0,
                    job_name = lang == Localization.Arabic ? obj.Job!.NameAr : obj.Job!.NameEn,
                    department_name = lang == Localization.Arabic ? obj.Department!.NameAr : obj.Department!.NameEn,
                    marital_status_name = lang == Localization.Arabic ? obj.MaritalStatus!.Name : obj.MaritalStatus!.NameInEnglish,
                    nationality_name = lang == Localization.Arabic ? obj.Nationality!.Name : obj.Nationality!.NameInEnglish,
                    religion_name = lang == Localization.Arabic ? obj.Religion!.Name : obj.Religion!.NameInEnglish,
                    note = obj.Note
                },
                Check = true
            };
        }


        public Response<GetEmployeeByIdResponse> GetEmployeeById(int id, string lang)
        {
            return unitOfWork.Employees.GetEmployeeByIdAsync(id, lang);
        }
        public async Task<Response<GetAllEmployeesResponse>> GetAllEmployeesAsync(string lang,
            GetAllEmployeesFilterRequest model, string host)
        {
            Expression<Func<HrEmployee, bool>> filter = x => x.IsDeleted == model.IsDeleted;

            Expression<Func<EmployeesData, bool>> filterSearch = x =>
                (string.IsNullOrEmpty(model.Word)
                 || x.FullName.Contains(model.Word)
                 || x.Job.Contains(model.Word)
                 || x.Department.Contains(model.Word)
                 || x.Nationality.Contains(model.Word)
                 || x.Company.Contains(model.Word)
                 || x.Management.Contains(model.Word));

            var totalRecords = await unitOfWork.Employees.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllEmployeesResponse
            {
                TotalRecords = totalRecords,

                Items = unitOfWork.Employees.GetEmployeesInfo(filter: filter, filterSearch: filterSearch, skip: (model.PageNumber - 1) * model.PageSize, take: model.PageSize),
                CurrentPage = model.PageNumber,
                FirstPageUrl = host + $"?PageSize={model.PageSize}&PageNumber=1&IsDeleted={model.IsDeleted}",
                From = (page - 1) * model.PageSize + 1,
                To = Math.Min(page * model.PageSize, totalRecords),
                LastPage = totalPages,
                LastPageUrl = host + $"?PageSize={model.PageSize}&PageNumber={totalPages}&IsDeleted={model.IsDeleted}",
                PreviousPage = page > 1 ? host + $"?PageSize={model.PageSize}&PageNumber={page - 1}&IsDeleted={model.IsDeleted}" : null,
                NextPageUrl = page < totalPages ? host + $"?PageSize={model.PageSize}&PageNumber={page + 1}&IsDeleted={model.IsDeleted}" : null,
                Path = host,
                PerPage = model.PageSize,
                Links = pageLinks
            };

            if (result.TotalRecords is 0)
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new()
                    {
                        Items = []
                    },
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = result,
                Check = true
            };
        }


        public async Task<Response<GetAllEmployeesResponse>> GetAllEmployeesByCompanyIdAsync(string lang,
           GetAllEmployeesFilterRequest model, string host, int companyId)
        {
            Expression<Func<HrEmployee, bool>> filter = x => x.IsDeleted == model.IsDeleted && x.CompanyId == companyId;

            Expression<Func<EmployeesData, bool>> filterSearch = x =>
                                                             (string.IsNullOrEmpty(model.Word)
                                                              || x.FullName.Contains(model.Word)
                                                              || x.Job.Contains(model.Word)
                                                              || x.Department.Contains(model.Word)
                                                              || x.Nationality.Contains(model.Word)
                                                              || x.Company.Contains(model.Word)
                                                              || x.Management.Contains(model.Word));


            var totalRecords = await unitOfWork.Employees.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllEmployeesResponse
            {
                TotalRecords = totalRecords,

                Items = unitOfWork.Employees.GetEmployeesInfo(filter: filter, filterSearch: filterSearch, skip: (model.PageNumber - 1) * model.PageSize, take: model.PageSize),
                CurrentPage = model.PageNumber,
                FirstPageUrl = host + $"?PageSize={model.PageSize}&PageNumber=1&IsDeleted={model.IsDeleted}",
                From = (page - 1) * model.PageSize + 1,
                To = Math.Min(page * model.PageSize, totalRecords),
                LastPage = totalPages,
                LastPageUrl = host + $"?PageSize={model.PageSize}&PageNumber={totalPages}&IsDeleted={model.IsDeleted}",
                PreviousPage = page > 1 ? host + $"?PageSize={model.PageSize}&PageNumber={page - 1}&IsDeleted={model.IsDeleted}" : null,
                NextPageUrl = page < totalPages ? host + $"?PageSize={model.PageSize}&PageNumber={page + 1}&IsDeleted={model.IsDeleted}" : null,
                Path = host,
                PerPage = model.PageSize,
                Links = pageLinks
            };

            if (result.TotalRecords is 0)
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new()
                    {
                        Items = []
                    },
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = result,
                Check = true
            };
        }
        public async Task<Response<EmployeesLookUps>> GetEmployeesLookUpsData(string lang)
        {
            try
            {
                var companies = await unitOfWork.Companies.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                    select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.NameAr : x.NameEn,

                    });

                var departments = await unitOfWork.Departments.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.NameAr : x.NameEn,
                        ManagementId = x.ManagementId

                    });
                var jobs = await unitOfWork.Jobs.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.NameAr : x.NameEn,

                    });
                var vacations = await unitOfWork.Vacations.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.NameAr : x.NameEn,
                    });
                var qualifications = await unitOfWork.Qualifications.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.NameAr : x.NameEn,
                    });
                var managements = await unitOfWork.Managements.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.NameAr : x.NameEn,
                        CompanyId = x.CompanyId,
                    });
                var nationalities = await unitOfWork.Nationalities.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name : x.NameInEnglish,
                    });
                var shifts = await unitOfWork.Shifts.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name_ar : x.Name_en,
                    });
                var documents = new List<object>();

                var maritals = await unitOfWork.MaritalStatus.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name : x.NameInEnglish,
                    });
                var genders = await unitOfWork.Genders.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name : x.NameInEnglish,
                    });
                var relegions = await unitOfWork.Religions.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name : x.NameInEnglish,
                    });
                var salary_payments_ways = await unitOfWork.SalaryPaymentWays.GetSpecificSelectAsync(
                    filter: filter => filter.IsDeleted == false
                    , select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name : x.NameInEnglish,
                    });

                return new Response<EmployeesLookUps>()
                {
                    Check = true,
                    IsActive = true,
                    Error = "",
                    Msg = "",
                    Data = new EmployeesLookUps()
                    {
                        companies = companies.ToArray(),
                        departments = departments.ToArray(),
                        documents = documents.ToArray(),
                        genders = genders.ToArray(),
                        jobs = jobs.ToArray(),
                        managements = managements.ToArray(),
                        maritals = maritals.ToArray(),
                        nationalities = nationalities.ToArray(),
                        qualifications = qualifications.ToArray(),
                        relegions = relegions.ToArray(),
                        salary_payments_ways = salary_payments_ways.ToArray(),
                        shifts = shifts.ToArray(),
                        vacations = vacations.ToArray(),
                    }
                };
            }
            catch (Exception exception)
            {
                return new Response<EmployeesLookUps>()
                {
                    Error = exception.Message,
                    Msg = "Can not able to Get Data",
                    Check = false,
                    Data = null,
                    IsActive = false
                };
            }

        }

        public async Task<Response<object>> GetEmployeesDataNameAndIdAsLookUp(string lang)
        {
            var result = new
            {
                employees = await unitOfWork.Employees.GetEmployeesDataNameAndIdAsLookUp(lang)
            };

            return new()
            {
                Check = true,
                Error = string.Empty,
                Data = result,
                LookUps = null,
                Msg = string.Empty
            };
        }

        #endregion



        #region Insert

        public async Task<Response<CreateEmployeeRequest>> CreateEmployeeAsync(CreateEmployeeRequest model)
        {
            var fullNameAr = $"{model.first_name_ar} {model.father_name_ar} {model.grand_father_name_ar} {model.family_name_ar}";
            var fullNameEn = $"{model.first_name_en} {model.father_name_en} {model.grand_father_name_en} {model.family_name_en}";

            var exists = await unitOfWork.Employees.ExistAsync(x => x.FullNameEn.Trim() == fullNameEn.Trim()
                                                                    && x.FullNameAr.Trim() == fullNameAr.Trim());
            if (exists)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.IsExist],
                    shareLocalizer[Localization.Employee]);

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg,
                    Check = false
                };
            }

            using var transaction = unitOfWork.BeginTransaction();
            try
            {
                var newEmployee = mapper.Map<HrEmployee>(model);

                GetFileNameAndExtension imageFile = new();
                if (model.employee_image != null)
                {
                    imageFile = ManageFilesHelper.UploadFile(model.employee_image, GoRootPath.EmployeeImagesPath);
                }

                List<GetFileNameAndExtension> employeeAttachments = [];
                if (model.employee_attachments is not null && model.employee_attachments.Any())
                {
                    employeeAttachments =
                        ManageFilesHelper.UploadFiles(model.employee_attachments, GoRootPath.HRFilesPath);
                }

                newEmployee.EmployeeImage = imageFile?.FileName;
                newEmployee.EmployeeImageExtension = imageFile?.FileExtension;
                newEmployee.ListOfAttachments = employeeAttachments.Select(f => new HrEmployeeAttachment
                {
                    FileExtension = f.FileExtension,
                    FileName = f.FileName,
                    IsActive = true
                }).ToList();
                await unitOfWork.Employees.AddAsync(newEmployee);

                var newUser = await unitOfWork.Users.AddAsync(new ApplicationUser()
                {
                    UserName = model.username,
                    Id = Guid.NewGuid().ToString(),
                    NormalizedUserName = model.username.ToUpper(),
                    Email = newEmployee.Email,
                    NormalizedEmail = newEmployee.Email.ToUpper(),
                    EmailConfirmed = true,
                    IsActive = true,
                    PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null!, model.password),
                    VisiblePassword = model.password

                });
                if (newUser != null)
                {
                    newEmployee.UserId = newUser.Id;
                    await unitOfWork.UserRoles.AddAsync(new ApplicationUserRole()
                    {
                        RoleId = UserRole.Id,
                        UserId = newUser.Id
                    });
                }


                await unitOfWork.CompleteAsync();
                transaction.Commit();
                return new()
                {
                    Msg = string.Format(shareLocalizer[Localization.Done],
                        shareLocalizer[Localization.Employee]),
                    Check = true,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new()
                {
                    Msg = string.Format(shareLocalizer[Localization.Error],
                        shareLocalizer[Localization.Employee]),
                    Check = false,
                    Data = model,
                    Error = ex.InnerException != null ? ex.InnerException.ToString() : ex.Message
                };
            }


        }


        #endregion

        #region Update

        public async Task<Response<CreateEmployeeRequest>> UpdateEmployeeAsync(int id, CreateEmployeeRequest model)
        {


            Expression<Func<HrEmployee, bool>> filter = x => x.IsDeleted == false && x.Id == id;
            var obj = await unitOfWork.Employees.GetFirstOrDefaultAsync(filter, includeProperties: $"{nameof(_instanceEmployee.ListOfAttachments)}");

            if (obj is null)
            {

                string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                    shareLocalizer[Localization.Employee]);

                return new()
                {
                    Data = model,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }


            var fullNameAr = $"{model.first_name_ar} {model.father_name_ar} {model.grand_father_name_ar} {model.family_name_ar}";
            var fullNameEn = $"{model.first_name_en} {model.father_name_en} {model.grand_father_name_en} {model.family_name_en}";

            var exists = await unitOfWork.Employees.ExistAsync(x => x.FullNameEn.Trim() == fullNameEn.Trim()
                                                                    && x.FullNameAr.Trim() == fullNameAr.Trim() && x.Id != id);
            if (exists)
            {
                string resultMsg = string.Format(shareLocalizer[Localization.IsExist],
                    shareLocalizer[Localization.Employee]);

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg,
                    Check = false
                };
            }

            var transaction = unitOfWork.BeginTransaction();
            try
            {
                if (!string.IsNullOrEmpty(obj.EmployeeImage))
                {
                    string file = Path.Combine(GoRootPath.EmployeeImagesPath, obj.EmployeeImage);
                    ManageFilesHelper.RemoveFile(file);
                }

                if (obj.ListOfAttachments.Any())
                {
                    ManageFilesHelper.RemoveFiles(obj.ListOfAttachments.Select(f => Path.Combine(GoRootPath.HRFilesPath, f.FileName)).ToList());
                    unitOfWork.EmployeeAttachments.RemoveRange(obj.ListOfAttachments);
                }



                GetFileNameAndExtension imageFile = new();
                if (model.employee_image != null)
                {
                    imageFile = ManageFilesHelper.UploadFile(model.employee_image, GoRootPath.EmployeeImagesPath);
                }

                List<GetFileNameAndExtension> employeeAttachments = [];
                if (model.employee_attachments is not null && model.employee_attachments.Any())
                {
                    employeeAttachments = ManageFilesHelper.UploadFiles(model.employee_attachments, GoRootPath.HRFilesPath);
                }
                obj.Address = model.address;
                obj.AccountNo = model.account_no;
                obj.Email = model.email;
                obj.BirthDate = model.birth_date;
                obj.ChildrenNumber = model.children_number;
                obj.CompanyId = model.company_id;
                obj.DepartmentId = model.department_id;
                obj.EmployeeTypeId = model.employee_type_id;
                obj.VacationId = model.vacation_id;
                obj.FamilyNameAr = model.family_name_ar;
                obj.FamilyNameEn = model.family_name_en;
                obj.FirstNameEn = model.first_name_en;
                obj.FirstNameAr = model.first_name_ar;
                obj.FatherNameAr = model.father_name_ar;
                obj.FatherNameEn = model.father_name_en;
                obj.GrandFatherNameAr = model.grand_father_name_ar;
                obj.GrandFatherNameEn = model.grand_father_name_en;
                obj.FingerPrintCode = model.finger_print_code;
                obj.FingerPrintId = model.finger_print_id;
                obj.Note = model.note;
                obj.Phone = model.phone;
                obj.GenderId = model.gender_id;
                obj.QualificationId = model.qualification_id;
                obj.ShiftId = model.shift_id;
                obj.NationalId = model.national_id;
                obj.NationalityId = model.nationality_id;
                obj.JobId = model.job_id;
                obj.JobNumber = model.job_number;
                obj.HiringDate = model.hiring_date;
                obj.ImmediatelyDate = model.immediately_date;
                obj.IsActive = model.is_active;
                obj.EmployeeImage = imageFile?.FileName;
                obj.EmployeeImageExtension = imageFile?.FileExtension;
                obj.ListOfAttachments = employeeAttachments?.Select(f => new HrEmployeeAttachment
                {
                    FileExtension = f.FileExtension,
                    FileName = f.FileName,
                    IsActive = true
                }).ToList();
                unitOfWork.Employees.Update(obj);


                var userExist = await userManager.FindByNameAsync(model.username);
                if (userExist != null)
                {
                    userExist.VisiblePassword = model.password;
                    userExist.Email = obj.Email;
                    userExist.NormalizedEmail = obj.Email.ToUpper();
                    userExist.PasswordHash =
                        new PasswordHasher<ApplicationUser>().HashPassword(userExist, model.password);
                    obj.UserId = userExist.Id;
                    await userManager.UpdateAsync(userExist);
                }
                else
                {
                    var newUser = await unitOfWork.Users.AddAsync(new ApplicationUser()
                    {
                        VisiblePassword = model.password,
                        Email = obj.Email,
                        ConcurrencyStamp = "1",
                        NormalizedEmail = obj.Email.ToUpper(),
                        PhoneNumber = obj.Phone,
                        IsActive = true,
                        PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null!, model.password),
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.username,
                        NormalizedUserName = model.username.ToUpper(),

                    });
                    if (newUser != null)
                    {
                        obj.UserId = newUser.Id;
                        await unitOfWork.UserRoles.AddAsync(new ApplicationUserRole()
                        {
                            RoleId = UserRole.Id,
                            UserId = newUser.Id,

                        });
                    }
                }

                await unitOfWork.CompleteAsync();
                transaction.Commit();
                return new()
                {
                    Msg = string.Format(shareLocalizer[Localization.Done],
                        shareLocalizer[Localization.Employee]),
                    Check = true,
                    Data = model
                };

            }
            catch (Exception e)
            {
                transaction.Rollback();
                return new()
                {
                    Msg = shareLocalizer[Localization.Error],
                    Check = false,
                    Data = model,
                    Error = e.InnerException != null ? e.InnerException.Message : e.Message
                };
            }
        }



        public async Task<Response<CreateEmployeeRequest>> RestoreEmployeeAsync(int id)
        {
            var obj = await unitOfWork.Employees.GetByIdAsync(id);
            if (obj is null)
            {

                string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                    shareLocalizer[Localization.Employee]);

                return new()
                {
                    Data = null
                    ,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }


            obj.IsDeleted = false;
            unitOfWork.Employees.Update(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = mapper.Map<CreateEmployeeRequest>(obj)
                ,
                Error = string.Empty,
                Msg = shareLocalizer[Localization.Restored]
            };
        }
        #endregion

        public Task<Response<string>> UpdateActiveOrNotEmployeeAsync(int id)
        {
            throw new NotImplementedException();
        }

        #region Delete

        public async Task<Response<string>> DeleteEmployeeAsync(int id)
        {
            var transaction = unitOfWork.BeginTransaction();
            try

            {
                Expression<Func<HrEmployee, bool>> filter = x => x.Id == id;
                var obj = await unitOfWork.Employees.GetFirstOrDefaultAsync(filter,
                    includeProperties: $"{nameof(_instanceEmployee.ListOfAttachments)}");

                if (obj == null)
                {
                    string resultMsg = string.Format(shareLocalizer[Localization.CannotBeFound],
                        shareLocalizer[Localization.Employee]);

                    return new()
                    {
                        Data = string.Empty,
                        Error = resultMsg,
                        Msg = resultMsg
                    };
                }

                var transactionAllowance = await unitOfWork.TransAllowances.ExistAsync(t => t.EmployeeId == id);
                var transactionVacations = await unitOfWork.TransVacations.ExistAsync(t => t.EmployeeId == id);
                var transactionBenefits = await unitOfWork.TransBenefits.ExistAsync(t => t.EmployeeId == id);
                var transactionDeductions = await unitOfWork.TransDeductions.ExistAsync(t => t.EmployeeId == id);
                var transactionCovenants = await unitOfWork.TransCovenants.ExistAsync(t => t.EmployeeId == id);
                if (transactionAllowance || transactionVacations || transactionBenefits || transactionDeductions ||
                    transactionCovenants)
                {
                    return new()
                    {
                        Check = false,
                        Error = string.Format(shareLocalizer[Localization.CannotDeleteItemHasRelativeData], shareLocalizer[Localization.Employee]),
                        Msg = string.Format(shareLocalizer[Localization.CannotDeleteItemHasRelativeData], shareLocalizer[Localization.Employee])
                    };
                }


                if (obj.IsDeleted)
                {
                    if (!string.IsNullOrEmpty(obj.EmployeeImage))
                    {
                        string file = Path.Combine(GoRootPath.EmployeeImagesPath, obj.EmployeeImage);
                        ManageFilesHelper.RemoveFile(file);
                    }

                    if (obj.ListOfAttachments.Any())
                    {
                        ManageFilesHelper.RemoveFiles(obj.ListOfAttachments.Select(f => Path.Combine(GoRootPath.HRFilesPath, f.FileName)).ToList());
                        unitOfWork.EmployeeAttachments.RemoveRange(obj.ListOfAttachments);
                    }
                }


                unitOfWork.Employees.Remove(obj);
                if (!string.IsNullOrEmpty(obj.UserId))
                {
                    var userData = await unitOfWork.Users.GetFirstOrDefaultAsync(c => c.Id == obj.UserId);
                    if (userData != null)
                    {
                        var userRoles = await unitOfWork.UserRoles.GetAllAsync(c => c.UserId == userData.Id);
                        if (userRoles.Any())
                        {
                            unitOfWork.UserRoles.RemoveRange(userRoles);
                        }
                        unitOfWork.Users.Remove(userData);
                    }
                }

                await unitOfWork.CompleteAsync();


                transaction.Commit();
                return new()
                {
                    Check = true,
                    Data = string.Empty,
                    Msg = shareLocalizer[Localization.Deleted]
                };
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return new()
                {
                    Check = false,
                    Error = e.Message,
                    Msg = shareLocalizer[Localization.Error],

                };
            }
        }
        #endregion




    }
}
