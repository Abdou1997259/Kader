
namespace Kader_System.Domain.Constants;

public static class SD
{
    public static class GoRootPath
    {
        public const string SettingImagesPath = "/wwwroot/Images/Setting/";
        public const string GetSettingImagesPath = "/Images/Setting/";
        public const string SettingFilesPath = "/wwwroot/Files/Setting/";
        public const string SettingAudiosPath = "/wwwroot/Audios/Setting/";
        public const string SettingVideosPath = "/wwwroot/Videos/Setting/";
        public const string EmployeeImagesPath = "/wwwroot/Images/HR/Employees/";
        public const string HRImagesPath = "/wwwroot/Images/HR/";
        public const string HRFilesPath = "/wwwroot/Files/HR/";
        public const string HRAudiosPath = "/wwwroot/Audios/HR/";
        public const string HRVideosPath = "/wwwroot/Videos/HR/";
        public const string TransFilesPath = "/wwwroot/Files/Trans/";
        public const string EmployeeRequestPath = "/wwwroot/Files/EmployeeRequest/";

    }
    public static class ReadRootPath
    {
        public const string SettingImagesPath = "Images/Setting/";
        public const string SettingFilesPath = "Files/Setting/";
        public const string SettingAudiosPath = "Audios/Setting/";
        public const string SettingVideosPath = "Videos/Setting/";
        public const string EmployeeImagesPath = "Images/HR/Employees/";

        public const string HRImagesPath = "Images/HR/";
        public const string HRFilesPath = "Files/HR/";
        public const string HRAudiosPath = "Audios/HR/";
        public const string HRVideosPath = "Videos/HR/";
        public const string TransFilesPath = "Files/Trans/";
    }

    public static class FileSettings
    {
        public const string SpecialChar = @"|!#$%&[]=?»«@£§€{};<>";
        public const int Length = 50;
        public const string AllowedExtension = ".jpg,.png,.jpeg";
    }
    public static class SysFileServer
    {
        public const string UploadFolderNamder = "upload";

    }
    public static class Modules
    {
        public const string Auth = "Auth";
        public const string Setting = "Setting";
        public const string EmployeeRequest = "Employee_Request";
        public const string HR = "HR";
        public const string Trans = "Trans";
        public const string V1 = "v1";
        public const string Bearer = "Bearer";

    }
    public static class SuperAdmin
    {
        public const string Id = "b74ddd14-6340-4840-95c2-db12554843e5basb1";

        public const string RoleId = "fab4fac1-c546-41de-aebc-a14da68957ab1";
        public static string Password = "123456";
        public static string RoleNameInAr = "سوبر أدمن";
    }
    public static class UserRole
    {

        public static string Id = "0ffa8112-ba0d-4416-b0ed-992897ac896e";
        public static string RoleNameInAr = "مستخدم";
        public static string RoleNameInEn = "User";
    }
    public static class Roles
    {
        public const string Administrative = "Administrative";
        public const string User = "User";
        public const string SuperAdmin = "SuperAdmin";

    }
    public static class RequestClaims
    {
        public const string Permission = "Permission";
        public const string RolePermission = "RolePermission";
        public const string UserPermission = "UserPermission";
        public const string DomainRestricted = "DomainRestricted";
        public const string Company = "CompanyId";
        public const string UserId = "uid";
        public const string Titles = "Titles";
        public const string Mobile = "Mobile";
        public const string Image = "Image";
        public const string Email = "Email";
        public const string FullName = "FullName";
        public const string CurrentCompany = "CurrentCompany";
        public const string CurrentTitle = "CurrentTitle";

    }
    public static class Shared
    {
        public const string KaderSystem = "KaderSystem";
        public const string KaderSystemConnection = "KaderSystemConnection";
        public const string JwtSettings = "JwtSettings";
        public const string AccessToken = "access_token";
        public const string CorsPolicy = "CorsPolicy";
        public const string Development = "Development";
        public const string Production = "Production";
        public const string Productions = "Productions";
        public const string Local = "Local";
        public const string Notify = "/notify";
        public static string[] Cultures = ["en-US", "ar-EG"];
        public const string Resources = "Resources";
        public const string Company = "Company";
        public const string Department = "Department";
        public const string Task = "Task";
    }
    public static class ApiRoutes
    {
        public class MainScreenCategory
        {

            public const string ListOfMainScreensCategories = "screen_cat/getListOfMainScreens";
            public const string GetAllMainScreenCategories = "screen_cat";
            public const string CreateMainScreenCategory = "screen_cat";
            public const string UpdateMainScreenCategory = "screen_cat/update/{id}";
            public const string GetMainScreenCategoryById = "screen_cat/getById/{id}";
            public const string DeleteMainScreenCategory = "screen_cat/{id}";

            public const string restore = "screen_cat/restore/{id}";

            public const string OrderbyPattern = "screen_cat/order_by_pattern";
        }

        public class MainScreen
        {
            public const string ListOfMainScreens = "screen_main/getListOfMainScreens";
            public const string GetMainScreensWithRelatedData = "screen_main/myendpoint";
            public const string GetAllMainScreens = "screen_main";
            public const string CreateMainScreen = "screen_main";
            public const string UpdateMainScreen = "screen_main/update/{id}";
            public const string GetMainScreenById = "screen_main/getById/{id}";
            public const string DeleteMainScreen = "screen_main/{id}";
            public const string RestoreMainScreen = "screen_main/restore/{id}";
            public const string OrderbyPattern = "screen_main/order_by_pattern";

        }

        public class Screen
        {
            public const string GetAllScreens = "screen/getAllScreens";
            public const string CreateScreen = "screen/create";
            public const string UpdateScreen = "screen/update/{id}";
            public const string GetScreenById = "screen/getById/{id}";
            public const string DeleteScreen = "screen/delete/{id}";
            public const string RestoreScreen = "screen/restore/{id}";
        }

        public class SubMainScreen
        {
            public const string ListOfSubMainScreens = "screen_sub/getListOfSubMainScreens";
            public const string GetAllSubMainScreens = "screen_sub";
            public const string CreateSubMainScreen = "screen_sub";
            public const string RemoveSceenCodeSpace = "screen_sub/remove_space";
            public const string UpdateSubMainScreen = "screen_sub/update/{id}";
            public const string GetSubMainScreenById = "screen_sub/getById/{id}";
            public const string DeleteSubMainScreen = "screen_sub/delete/{id}";
            public const string RestoreScreen = "screen_sub/restore/{id}";
            public const string OrderbyPattern = "screen_sub/order_by_pattern";
        }
        public class StResponsiblity
        {
            public const string GetAllResponsiblitites = "responsiblity/getAllResponsiblities";
            public const string CreateResponsiblity = "responsiblity/create";
            public const string UpdateResponsiblity = "responsiblity/update/{id}";
            public const string GetResponsiblityById = "responsiblity/getById/{id}";
            public const string DeleteResponsiblity = "responsiblity/delelte/{id}";

        }
        public class User
        {
            public const string GetAllUsers = "GetAllUsers";
            public const string AddUser = "AddUser";
            public const string LoginUser = "LoginUser";
            public const string LogOutUser = "LogOutUser";
            public const string LoginUser1 = "LoginUser1";
            public const string ChangeActiveOrNot = "ChangeActiveOrNotUser/{id}";
            public const string UpdateUser = "UpdateUser/{id}";
            public const string AssginPermssionToUser = "assginPermissionToUser/{id}";
            public const string ShowPasswordToSpecificUser = "ShowPasswordToSpecificUser/{id}";
            public const string ChangePassword = "ChangePassword";
            public const string DeleteUser = "DeleteUser/{id}";
            public const string SetNewPasswordToSpecificUser = "SetNewPasswordToSpecificUser";
            public const string SetNewPasswordToSuperAdmin = "SetNewPasswordToSuperAdmin/{newPassword}";
            public const string GetMyProfile = "getMyProfile";
            public const string UpdateTitle = "UpdateTitle/{title}";
            public const string UpdateCompany = "UpdateCompany/{company}";
            public const string GetUserById = "GetUserById/{id}";
            public const string RestoreUser = "RestoreUser/{id}";
            public const string GetListOfUser = "GetListOfUsers";
            public const string GetLookups = "GetLookups";
            public const string GetTitleLookups = "TitleLookups";


        }

        public class Perm
        {
            public const string GetAllRoles = "GetAllRoles";
            public const string CreateRole = "CreateRole";
            public const string UpdateRole = "UpdateRole/{id}";
            public const string DeleteRoleById = "DeleteRoleById/{id}";

            public const string GetEachUserWithHisRoles = "GetEachUserWithHisRoles";
            public const string ManageUserRoles = "ManageUserRoles/{userId}";
            public const string UpdateUserRoles = "UpdateUserRoles";

            public const string GetAllPermissionsByCategoryName = "GetAllPermissionsByCategoryName";
            public const string ManageRolePermissions = "ManageRolePermissions/{roleId}";
            public const string UpdateRolePermissions = "UpdateRolePermissions";
            public const string ManageUserPermissions = "ManageUserPermissions/{userId}";
            public const string UpdateUserPermissions = "UpdateUserPermissions";
        }

        public class Company
        {
            public const string ListOfCompanies = "company/getListOfCompanies";
            public const string GetAllCompanies = "company";
            public const string CreateCompany = "company/create";
            public const string UpdateCompany = "company/update/{id}";
            public const string RestoreCompany = "company/restore/{id}";
            public const string GetCompanyById = "company/getById/{id}";
            public const string DeleteCompany = "company/delete/{id}";
        }
        public class Contract
        {
            public const string ListOfContracts = "contract/getListOfContracts";
            public const string GetAllContracts = "contract";
            public const string GetAllEndContracts = "getAllEndContracts";
            public const string CreateContract = "contract/create";
            public const string UpdateContract = "contract/update/{id}";
            public const string RestoreContract = "contract/restore/{id}";
            public const string GetContractById = "contract/getById/{id}";
            public const string GetContractLookups = "contract/getLookups";
            public const string DeleteContract = "contract/delete/{id}";
            public const string GetContractByUser = "contract/getcontractbyuser/{empId}";
            public const string DownloadDocument = "contract/downloaddocument/{contractId}";
        }
        public class Allowance
        {
            public const string ListOfAllowances = "allowance/getListOfAllowances";
            public const string GetAllAllowances = "allowance/getAll";
            public const string CreateAllowance = "allowance/create";
            public const string UpdateAllowance = "allowance/update/{id}";
            public const string RestoreAllowance = "allowance/restore/{id}";
            public const string GetAllowanceById = "allowance/getById/{id}";
            public const string DeleteAllowance = "allowance/delete/{id}";
            public const string OrderbyPattern = "allowance/order_by_pattern";
        }
        public class SalaryIncreaseType
        {
            public const string ListOfSalaryIncreaseTypes = "allowance/getListOfSalaryIncreaseTypes";
            public const string GetAllSalaryIncreaseTypes = "salaryIncreaseTypes/getAll";
            public const string CreateSalaryIncreaseTypes = "salaryIncreaseTypes/create";
            public const string UpdateSalaryIncreaseTypes = "salaryIncreaseTypes/update/{id}";
            public const string RestoreSalaryIncreaseTypes = "salaryIncreaseTypes/restore/{id}";
            public const string GetSalaryIncreaseTypesById = "salaryIncreaseTypes/getById/{id}";
            public const string DeleteSalaryIncreaseTypes = "salaryIncreaseTypes/delete/{id}";
        }
        public class SalaryIncrease
        {
            public const string ListOfSalaryIncrease = "salaryIncrease/getListOfSalaryIncrease";
            public const string GetAllSalaryIncrease = "salaryIncrease/getAll";
            public const string CreateSalaryIncrease = "salaryIncrease/create";
            public const string UpdateSalaryIncrease = "salaryIncrease/update/{id}";
            public const string RestoreSalaryIncrease = "salaryIncrease/restore/{id}";
            public const string GetSalaryIncreaseById = "salaryIncrease/getById/{id}";
            public const string DeleteSalaryIncrease = "salaryIncrease/delete/{id}";
            public const string GetEmployeesLookups = "salaryIncrease/getemployeeslookups";
        }
        public class TransAllowance
        {
            public const string ListOfTransAllowances = "transAllowance/getListOftransAllowances";
            public const string GetAllTransAllowances = "transAllowance/getAll";
            public const string GetLookupsTransAllowances = "transAllowance/getLookUps";
            public const string CreateTransAllowance = "transAllowance/create";
            public const string UpdateTransAllowance = "transAllowance/update/{id}";
            public const string RestoreTransAllowance = "transAllowance/restore/{id}";
            public const string GetTransAllowanceById = "transAllowance/getById/{id}";
            public const string DeleteTransAllowance = "transAllowance/delete/{id}";
        }

        public class TransBenefit
        {
            public const string ListOfTransBenefits = "transBenefit/getListOftransBenefits";
            public const string GetTransBenefits = "transBenefit/getAll";
            public const string GetLookUps = "transBenefit/getLookUps";
            public const string CreateTransBenefit = "transBenefit/create";
            public const string RestoreTransBenefit = "transBenefit/restore/{id}";
            public const string UpdateTransBenefit = "transBenefit/update/{id}";
            public const string GetTransBenefitById = "transBenefit/getById/{id}";
            public const string DeleteTransBenefit = "transBenefit/delete/{id}";
        }

        public class TransDeduction
        {
            public const string ListOfTransDeductions = "transDeduction/getListOftransDeductions";
            public const string GetTransDeductions = "transDeduction/getAll";
            public const string CreateTransDeduction = "transDeduction/create";
            public const string UpdateTransDeduction = "transDeduction/update/{id}";
            public const string RestoreTransDeduction = "transDeduction/restore/{id}";
            public const string GetTransDeductionById = "transDeduction/getById/{id}";
            public const string DeleteTransDeduction = "transDeduction/delete/{id}";
            public const string GetLookUps = "transDeduction/getLookUps";
        }

        public class TransVacation
        {
            public const string ListOfTransVacations = "transVacation/getListOftransVacations";
            public const string GetTransVacations = "transVacation/getAll";
            public const string GetTransVacationsLookUps = "transVacation/getlookUps";
            public const string CreateTransVacation = "transVacation/create";
            public const string UpdateTransVacation = "transVacation/update/{id}";
            public const string RestoreTransVacation = "transVacation/restore/{id}";
            public const string GetTransVacationById = "transVacation/getById/{id}";
            public const string DeleteTransVacation = "transVacation/delete/{id}";
        }

        public class TransCovenant
        {
            public const string ListOfTransCovenants = "transCovenant/getListOftransCovenants";
            public const string GetTransCovenants = "transCovenant/getAll";
            public const string GetLookUps = "transCovenant/getLookUps";
            public const string CreateTransCovenant = "transCovenant/create";
            public const string UpdateTransCovenant = "transCovenant/update/{id}";
            public const string RestoreTransCovenant = "transCovenant/restore/{id}";
            public const string GetTransCovenantById = "transCovenant/getById/{id}";
            public const string DeleteTransCovenant = "transCovenant/delete/{id}";
        }
        public class TransSalaryCalculatorEndpoint
        {
            public const string DetailedCalculations = "transSalary/getdetailedcalculation";
            public const string Calculate = "transSalary/calculate";
            public const string GetLookUps = "transSalary/getLookUps";
            public const string GetTransCalculator = "transSalary/getAll";

            public const string DeleteCalculator = "transSalary/delete/{id}";
            public const string GetbyId = "transSalary/getById/{id}";


        }
        public class Management
        {
            public const string ListOfManagements = "management/getListOfManagements";
            public const string GetAllManagements = "management/getAll";
            public const string CreateManagement = "management/create";
            public const string UpdateManagement = "management/update/{id}";
            public const string GetManagementById = "management/getById/{id}";
            public const string DeleteManagement = "management/delete/{id}";
            public const string GetStructure = "management/getstructre/{companyid}";
        }
        public class Department
        {
            public const string ListOfDepartments = "department/getListOfDepartments";
            public const string GetAllDepartments = "department/getAll";
            public const string CreateDepartment = "department/create";
            public const string UpdateDepartment = "department/update/{id}";
            public const string GetDepartmentById = "department/getById/{id}";
            public const string DeleteDepartment = "department/delete/{id}";
            public const string AddEmp = "department/addemployee/{id}";
        }
        public class Loan
        {
            public const string ListOfLoans = "loan/getListOfloans";
            public const string GetAllloans = "loan/getAll";
            public const string Createloan = "loan/create";
            public const string Updateloan = "loan/update/{id}";
            public const string RestoreLoan = "loan/restore/{id}";
            public const string UpdatePaymentLoan = "loan/payloan/{id}";
            public const string UpdateDelayLoan = "loan/delayloan/{id}";
            public const string ReInstallment = "loan/reinstallment/{id}";
            public const string GetloanById = "loan/getById/{id}";
            public const string Deleteloan = "loan/delete/{id}";
            public const string GetLookups = "loan/geLookups";
        }
        public class Benefit
        {
            public const string ListOfBenefits = "benefit/getListOfBenefits";
            public const string GetAllBenefits = "benefit/getAll";
            public const string CreateBenefit = "benefit/create";
            public const string UpdateBenefit = "benefit/update/{id}";
            public const string RestoreBenefit = "benefit/restore/{id}";
            public const string GetBenefitById = "benefit/getById/{id}";
            public const string DeleteBenefit = "benefit/delete/{id}";
        }

        public class Deduction
        {
            public const string ListOfDeductions = "deduction/getListOfDeductions";
            public const string GetAllDeductions = "deduction/getAll";
            public const string CreateDeduction = "deduction/create";
            public const string UpdateDeduction = "deduction/update/{id}";
            public const string RestoreDeduction = "deduction/restore/{id}";
            public const string GetDeductionById = "deduction/getById/{id}";
            public const string DeleteDeduction = "deduction/delete/{id}";
        }

        public class Employee
        {
            public const string ListOfEmployees = "employee/getListOfEmployees";
            public const string GetAllEmployees = "employee/getAll";
            public const string CreateEmployee = "employee/create";
            public const string UpdateEmployee = "employee/update/{id}";
            public const string GetEmployeeById = "employee/getById/{id}";
            public const string DeleteEmployee = "employee/delete/{id}";
            public const string GetLookUps = "employee/getLookUps";
            public const string Restore = "employee/restore/{id}";
            public const string GetAllEmpByCompanyId = "employee/getAllEmpsByCompanyId/{companyId}";
        }
        public class FingerPrint
        {
            public const string ListOfFingerPrintDevices = "fingerPrint/getListOfFingerPrintDevices";
            public const string GetAllFingerPrintDevices = "fingerPrint/getAll";
            public const string GetLookups = "fingerPrint/getLookups";
            public const string CreateFingerPrintDevice = "fingerPrint/create";
            public const string UpdateFingerPrintDevice = "fingerPrint/update/{id}";
            public const string RestoreFingerPrint = "fingerPrint/restore/{id}";
            public const string GetFingerPrintDeviceById = "fingerPrint/getById/{id}";
            public const string DeleteFingerPrintDevice = "fingerPrint/delete/{id}";
        }
        public class Qualification
        {
            public const string ListOfQualifications = "qualification/getListOfQualifications";
            public const string GetAllQualifications = "qualification/getAll";
            public const string CreateQualification = "qualification/create";
            public const string UpdateQualification = "qualification/update/{id}";
            public const string RestoreQualification = "qualification/restore/{id}";
            public const string GetQualificationById = "qualification/getById/{id}";
            public const string DeleteQualification = "qualification/delete/{id}";
        }

        public class EmployeeRequests
        {
            public class LeavePermessionasRequests
            {
                public const string GetAllLeavePermessionasRequests = "leavePermessionasRequests/getAll";
                public const string CreateLeavePermessionasRequests = "leavePermessionasRequests/create";
                public const string UpdateLeavePermessionasRequests = "leavePermessionasRequests/update/{id}";
                public const string DeleteLeavePermessionasRequests = "leavePermessionasRequests/delete/{id}";
                public const string ApproveLeavePermessionasRequests = "leavePermessionasRequests/approve/{id}";
                public const string RejectLeavePermessionasRequests = "leavePermessionasRequests/reject/{id}";
            }
            public class LoanRequests
            {
                public const string ListOfLoanRequests = "loanrequests/getListOfLoanRequests";
                public const string GetAllLoanRequests = "loanrequests/getAll";
                public const string CreateLoanRequests = "loanrequests/create";
                public const string UpdateLoanRequests = "loanrequests/update/{id}";
                public const string RestoreLoanRequests = "loanrequests/restore/{id}";
                public const string GetLoanRequestsById = "loanrequests/getById/{id}";
                public const string DeleteLoanRequests = "loanrequests/delete/{id}";
                public const string ApproveLoanRequests = "loanrequests/approve/{id}";
                public const string RejectLoanRequests = "loanrequests/reject/{id}";
            }
            public class ResignationRequests
            {
                public const string ListOfResignationRequests = "resignation/getListOfResignationRequests";
                public const string GetAllResignationRequests = "resignation/getAll";
                public const string CreateResignationRequests = "resignationRequests/create";
                public const string UpdateResignationRequests = "resignation/update/{id}";
                public const string RestoreResignationRequests = "resignation/restore/{id}";
                public const string GetResignationRequestsById = "resignation/getById/{id}";
                public const string DeleteResignationRequests = "resignation/delete/{id}";
                public const string ApproveResignationRequests = "resignation/approve/{id}";
                public const string RejectResignationRequests = "resignation/reject/{id}";
            }
            public class DelayPermessionasRequests
            {
                public const string CreateDelayPermissionRequests = "delayPermissionRequests/create";
                public const string GetAllDelayPermissionRequests = "delayPermissionRequests/getAll";
                public const string GetDelayPermissionRequestsById = "delayPermissionRequests/getById/{id}";
                public const string UpdateDelayPermissionRequests = "delayPermissionRequests/update/{id}";
                public const string DeleteDelayPermissionRequests = "delayPermissionRequests/delete/{id}";
                public const string ApproveDelayPermissionRequests = "delayPermissionRequests/approve/{id}";
                public const string RejectDelayPermissionRequests = "delayPermissionRequests/reject/{id}";
            }
            public class VacationRequests
            {

                public const string ListOfVacationRequests = "vacationRequests/getListOfvacationRequests";
                public const string GetAllVacationRequests = "vacationRequests/getAll";
                public const string CreateVacationRequests = "vacationRequests/create";
                public const string UpdateVacationRequests = "vacationRequests/update/{id}";
                public const string RestoreVacationRequests = "vacationRequests/restore/{id}";
                public const string GetVacationRequestsById = "vacationRequests/getById/{id}";
                public const string DeleteVacationRequests = "vacationRequests/delete/{id}";
                public const string ApproveVacationRequests = "vacationRequests/approve/{id}";
                public const string RejectVacationRequests = "vacationRequests/reject/{id}";
            }
            public class AllowanceRequests
            {
                public const string GetAllowanceRequests = "allowanceRequests/getAll";
                public const string CreateAllowanceRequests = "allowanceRequests/create";
                public const string UpdateAllowanceRequests = "allowanceRequests/update/{id}";
                public const string DeleteAllowanceRequests = "allowanceRequests/delete/{id}";
                public const string GetAllowanceRequestById = "allowanceRequests/getById/{id}";
                public const string ApproveAllowanceRequests = "allowanceRequests/approve/{id}";
                public const string RejectAllowanceRequests = "allowanceRequests/reject/{id}";

            }
            public const string GetEmployeeRequestsLookups = "employeeRequests/getLookups";

            public class SalaryIncreaseRequest
            {
                public const string CreateSalaryIncreaseRequests = "salaryIncreaseRequests/create";
                public const string GetAllSalaryIncreaseRequests = "salaryIncreaseRequests/getAll";
                public const string GetSalaryIncreaseId = "salaryIncreaseRequests/getById/{id}";
                public const string UpdateIncreaseSalary = "salaryIncreaseRequests/update/{id}";
                public const string DeleteSalaryIncreaseRequest = "salaryIncreaseRequests/delete/{id}";
                public const string ApproveSalaryIncreaseRequest = "salaryIncreaseRequests/approve/{id}";
                public const string RejectSalaryIncreaseRequest = "salaryIncreaseRequests/reject/{id}";

            }
            public class ContractTerminationRequest
            {

                public const string ListOContractTerminationRequest = "contractTerminationRequest/getListOfcontractTerminationRequests";
                public const string GetAllContractTerminationRequest = "contractTerminationRequest/getAll";
                public const string CreateContractTerminationRequest = "contractTerminationRequest/create";
                public const string UpdateContractTerminationRequest = "contractTerminationRequest/update/{id}";
                public const string RestoreContractTerminationRequest = "contractTerminationRequest/restore/{id}";
                public const string GetContractTerminationRequestsById = "contractTerminationRequest/getById/{id}";
                public const string DeleteContractTerminationRequest = "contractTerminationRequest/delete/{id}";
                public const string ApproveContractTerminationRequest = "contractTerminationRequest/approve/{id}";
                public const string RejectContractTerminationRequest = "contractTerminationRequest/reject/{id}";
            }
        }
        public class Job
        {
            public const string ListOfJobs = "Job/getListOfJobs";
            public const string GetAllJobs = "Job/getAll";
            public const string CreateJob = "Job/create";
            public const string UpdateJob = "Job/update/{id}";
            public const string RestoreJob = "Job/Restore/{id}";
            public const string GetJobById = "Job/getById/{id}";
            public const string DeleteJob = "Job/delete/{id}";
        }
        public class Vacation
        {
            public const string ListOfVacations = "Vacation/getListOfVacations";
            public const string GetAllVacations = "Vacation/getAll";
            public const string CreateVacation = "Vacation/create";
            public const string UpdateVacation = "Vacation/update/{id}";
            public const string RestoreVacation = "Vacation/restore/{id}";
            public const string GetVacationById = "Vacation/getById/{id}";
            public const string DeleteVacation = "Vacation/delete/{id}";
        } 
        public class EmployeeNotes
        {
            public const string ListOfEmployeeNotes = "EmployeeNotes/getListOfEmployeeNotes";
            public const string GetAllEmployeeNotes = "EmployeeNotes/getAll";
            public const string CreateEmployeeNotes = "EmployeeNotes/create";
            public const string UpdateEmployeeNotes = "EmployeeNotes/update/{id}";
            public const string RestoreEmployeeNotes = "EmployeeNotes/restore/{id}";
            public const string GetEmployeeNotesById = "EmployeeNotes/getById/{id}";
            public const string DeleteEmployeeNotes = "EmployeeNotes/delete/{id}";
        }
        public class Shift
        {
            public const string ListOfShifts = "shift/getListOfShifts";
            public const string GetAllShifts = "shift/getAll";
            public const string CreateShift = "shift/create";
            public const string UpdateShift = "shift/update/{id}";
            public const string RestoreShift = "shift/restore/{id}";
            public const string ChangeShift = "shift/change";
            public const string GetShiftById = "shift/getById/{id}";
            public const string DeleteShift = "shift/delete/{id}";
        }

        public class Title
        {
            public const string GetAllTitles = "title/getAllTitles";
            public const string CreateTitle = "title/create";
            public const string UpdateTitle = "title/update/{id}";
            public const string GetTitleById = "title/getById/{id}";
            public const string DeleteTitle = "title/delete/{id}";
            public const string RestoreTitle = "title/restore/{id}";
        }
        public class PermessionStruct
        {
            public const string GetPermissionsBySubScreen = "PermessionStruct/GetPermissionsBySubScreen";
        }

        public class GetAllScreens
        {
            public const string SpGetAllScreens = "GetAllScreens/SpGetAllScreens";
        }
        public class UserPermession
        {
            public const string GetAllUserPermessions = "userPermession/getAllUserPermessions/{userId}";
            public const string GetUserPermissionsBySubScreen = "userPermession/GetUserPermissionsBySubScreen/{userId}/{titleId}";
        }
        public class TitlePermession
        {
            public const string GetAllTitlePermessions = "titlePermession/getAllTitlePermessions/{titleId}";
        }
    }

    public static class Localization
    {
        public const string Arabic = "ar";
        public const string English = "en";
        public const string IsExist = "IsExist";
        public const string CurrentIsNotExitedInCompanys = "CurrentIsNotExitedInCompanys";
        public const string CurrentIsNotExitedInTitle = "CurrentIsNotExitedInTitle";
        public const string ScreenInAction = "ScreenInAction";
        public const string ViewInclude = "ViewInclude";
        public const string Project = "Project";
        public const string Task = "Task";
        public const string SalaryMoreThanInstallment = "SalaryMoreThanInstallment";
        public const string Notification = "Notification";
        public const string IsNotExistedIn = "IsNotExistedIn";
        public const string CanNotAddingToNotThereIsProjectAndDepartment = "CanNotAddingToNotThereIsProjectAndDepartment";
        public const string CalculatedAready = "CalculatedAready";
        public const string CannotUpdateTaskStatus = "CannotUpdateTaskStatus";
        public const string TaskExist = "TaskExist";
        public const string Department = "Department";
        public const string TaskComment = "TaskComment";
        public const string EmployeeVacation = "EmployeeVacation";
        public const string DepartmentManager = "DepartmentManager";
        public const string Done = "Done";
        public const string HaveContract = "HaveContract";
        public const string BalanceNotEnough = "BalanceNotEnough";
        public const string Error = "Error";
        public const string ThisAmountCannotBePaidFromTheMainTreasuryDueToItsAvailability = "ThisAmountCannotBePaidFromTheMainTreasuryDueToItsAvailability";
        public const string ThisAmountCannotBePaidFromTheTreasuryBranchDueToItsAvailability = "ThisAmountCannotBePaidFromTheTreasuryBranchDueToItsAvailability";
        public const string ThisAmountCannotBeTransferedFromTheTreasuryDueToItsAvailability = "ThisAmountCannotBeTransferedFromTheTreasuryDueToItsAvailability";
        public const string ThisAmountCannotBeTransferedFromTheBranchTreasuryDueToItsAvailability = "ThisAmountCannotBeTransferedFromTheBranchTreasuryDueToItsAvailability";
        public const string ThisAmountCannotBeReceitedAsThisClientHasNotPrice = "ThisAmountCannotBeReceitedAsThisClientHasNotPrice";
        public const string ItIsNecessaryThatAmountMoreThanZero = "ItIsNecessaryThatAmountMoreThanZero";
        public const string Used = "Used";
        public const string CannotBeFound = "CannotBeFound";
        public const string UserInTitle = "UserInTitle";
        public const string UserPermission = "UserPermission";
        public const string UserInCompany = "UserInCompany";
        public const string MangerAlready = "MangerAlready";
        public const string Restored = "Restored";
        public const string Departments = "Departments";
        public const string DelayedSuccessfully = "DelayedSuccessfully";
        public const string MainScreenCategory = "MainScreenCategory";
        public const string MainScreen = "MainScreen";
        public const string InvalidSubId = "InvalidSubId";
        public const string Screen = "Screen";
        public const string SubMainScreen = "SubMainScreen";
        public const string PaidSuccessfuly = "PaidSuccessfuly";
        public const string DepartmentsExist = "DepartmentsExist";
        public const string Jobs = "Jobs";
        public const string JobExist = "JobExist";
        public const string Projects = "Projects";
        public const string ProjectsExisit = "ProjectsExisit";
        public const string Tasks = "Tasks";
        public const string CannotDeletedThisRole = "CannotDeletedThisRole";
        public const string Service = "Service";
        public const string ServicesCategory = "ServicesCategory";
        public const string Policy = "Policy";
        public const string News = "News";
        public const string Updated = "Updated";
        public const string Deleted = "Deleted";
        public const string ApproveRejectDelte = "Cannot delete approve or reject request";
        public const string CurrentAndNewPasswordIsTheSame = "CurrentAndNewPasswordIsTheSame";
        public const string CurrentPasswordIsIncorrect = "CurrentPasswordIsIncorrect";
        public const string UserName = "UserName";
        public const string IsDepartmentInMang = "IsDepartmentInMang";
        public const string UserNameOrEmail = "UserNameOrEmail";
        public const string User = "User";
        public const string ThisEmployeeWasDeleted = "ThisEmployeeWasDeleted";
        public const string NotFound = "NotFound";
        public const string NotApproval = "The Request not Not Approval";
        public const string NotPending = "The Request not Not pending";
        public const string Email = "Email";
        public const string PasswordNotmatch = "PasswordNotmatch";
        public const string Role = "Role";
        public const string IsNotExisted = "IsNotExisted";
        public const string Employee = "Employee";
        public const string Contract = "Contract";

        public const string TitleUser = "TitleUser";
        public const string HasNoDocument = "HasNoDocument";
        public const string Calculated = "Calculated";
        public const string ContractNotFound = "ContractNotFound";
        public const string ContractDetail = "ContractDetail";
        public const string EmployeeExist = "EmployeeExist";
        public const string ThereAreNotAttachments = "ThereAreNotAttachments";
        public const string CanNotAssignAnyEmpToFindingOther = "CanNotAssignAnyEmpToFindingOther";
        public const string CanNotAssignAnyEmpToFindingComManager = "CanNotAssignAnyEmpToFindingComManager";
        public const string CanNotRemoveThisEmployeeAsHasAmountInHisTreasury = "CanNotRemoveThisEmployeeAsHasAmountInHisTreasury";
        public const string Request = "Request";
        public const string FileHasNoDirectory = "FileHasNoDirectory";
        public const string Job = "Job";
        public const string Category = "Category";
        //public const string Item = "Item";

        public const string Section = "Section";
        public const string ClientCategory = "ClientCategory";
        public const string ThisEmployeeIsNotTechnician = "ThisEmployeeIsNotTechnician";
        public const string CompanyBranch = "CompanyBranch";
        public const string LockTechnicalsLogins = "LockTechnicalsLogins";
        public const string UnLockTechnicalsLogins = "UnLockTechnicalsLogins";
        public const string ThereIsActiveEmployeesRelatedToThisBranch = "ThereIsActiveEmployeesRelatedToThisBranch";
        public const string Activated = "Activated";
        public const string UserWasLoggedOutBefore = "UserWasLoggedOutBefore";
        public const string PleaseChangeEmployeeActivationState = "PleaseChangeEmployeeActivationState";
        public const string DeActivated = "DeActivated";
        public const string TheEmployeeNotActive = "TheEmployeeNotActive";
        public const string Region = "Region";
        public const string State = "State";
        public const string TaxOffice = "TaxOffice";
        public const string UserNotExist = "UserNotExist";
        public const string UserIsLoggedOut = "UserIsLoggedOut";
        public const string Location = "Location";
        public const string UserWithEmailExists = "UserWithEmailExists";
        public const string UserWithNameExists = "UserWithNameExists";
        public const string CompanyIsNotActivated = "CompanyIsNotActivated";
        public const string Email_Password_Incorrect = "Email_Password_Incorrect";
        public const string UserDataIsIncorrect = "UserDataIsIncorrect";
        public const string Company = "Company";
        public const string Management = "Management";
        public const string Allowance = "Allowance";
        public const string EmployeeNotes = "Employee Notes";
        public const string Benefit = "Benefit";
        public const string TitlePermisson = "TitlePermisson";
        public const string Qualification = "Qualification";
        public const string Shift = "Shift";
        public const string Deduction = "Deduction";
        public const string Loan = "Loan";
        public const string Vacation = "Vacation";
        public const string Image = "Image";
        public const string CompanyIsNotActive = "Company is not active";
        public const string NotActive = "NotActive";
        public const string NotActiveNotUpdate = "NotActiveNotUpdate";
        public const string NotFoundMainBranchToCompany = "NotFoundMainBranchToCompany";
        public const string NotFoundData = "NotFoundData";
        public const string CanNotAddCommentToSpecificComment = "CanNotAddCommentToSpecificComment";
        public const string Technician = "Technician";
        public const string UserIsAlreadyLoggedIn = "UserIsAlreadyLoggedIn";
        public const string HasAnyRelation = "HasAnyRelation";
        public const string Item = "Item";
        public const string Data = "Data";
        public const string Admin = "Admin";
        public const string Client = "Client";
        public const string ClientAppointmentMaking = "ClientAppointmentMaking";
        public const string CompletionStatus = "CompletionStatus";
        public const string ClientNotes = "ClientNotes";
        public const string ClientProcedure = "ClientProcedure";
        public const string FingerPrintDevice = "FingerPrintDevice";
        public const string ReceiptVoucher = "ReceiptVoucher";
        public const string PaymentVoucher = "PaymentVoucher";
        public const string Treasury = "Treasury";
        public const string BranchTreasury = "BranchTreasury";
        public const string PaymentGatway = "PaymentGatway ";
        public const string ClientBranch = "ClientBranch";
        public const string FinancialYear = "FinancialYear";
        public const string CanNotAddFinancialYear = "CanNotAddFinancialYear";
        public const string CanNotAddFinancialYearAsThereIsActiveOne = "CanNotAddFinancialYearAsThereIsActiveOne";
        public const string CanNotAddFinancialYearAsThereIsDateNotConventioned = "CanNotAddFinancialYearAsThereIsDateNotConventioned";
        public const string CanNotActivateFinancialYearAsThereIsActiveOne = "CanNotActivateFinancialYearAsThereIsActiveOne";
        public const string ThisEmployeeAlreadyIsAssignedBefore = "ThisEmployeeAlreadyIsAssignedBefore";
        public const string CanNotRemoveThisBranchTreasuryAsThereIsAmount = "CanNotRemoveThisBranchTreasuryAsThereIsAmount";
        public const string CompanyIsRequired = "CompanyIsRequired";
        public const string FinancialYearIsRequired = "FinancialYearIsRequired";
        public const string BothOfCompanyAndFinancialYearRequired = "BothOfCompanyAndFinancialYearRequired";
        public const string BillBook = "BillBook";
        public const string CanNotAddBillBook = "CanNotAddBillBook";
        public const string FinancialYearIsNotActive = "FinancialYearIsNotActive";
        public const string CannotDeleteItemHasRelativeData = "CannotDeleteItemHasRelativeData";
        public const string ThereIs = "ThereIs";
        public const string NumOfObjectsNotEqualNumOfUploadedImages = "NumOfObjectsNotEqualNumOfUploadedImages";
        public const string IsNotSuperAdmin = "IsNotSuperAdmin";
        public const string PaymentGateway = "PaymentGateway";
        public const string PathRoute = "PathRoute";
        public const string ThisEmployeeIsNotTech = "ThisEmployeeIsNotTech";
        public const string Unit = "Unit";
        public const string UnitOfConversion = "UnitOfConversion";
        public const string RefusedPermission = "RefusedPermission";
        public const string ThisStockHasAlreadyTechnique = "ThisStockHasAlreadyTechnique";
        public const string ThisStockWithOutTechnique = "ThisStockWithOutTechnique";
        public const string Stock = "Stock";
        public const string NotFoundPhotos = "NotFoundPhotos";
        public const string ThereIsOnlineTech = "ThereIsOnlineTech";
        public const string NotValidDate = "NotValidDate";
        public const string StockTrans = "StockTrans";
        public const string InvalidDocNum = "InvalidDocNum";
        public const string StockTransType = "StockTransType";
        public const string ItemSerial = "ItemSerial";
        public const string ThereIsNotEnoughQuantityInTheStock = "ThereIsNotEnoughQuantityInTheStock";
        public const string ThereIsSomeItemsNotEnoughQuantityInTheStock = "ThereIsSomeItemsNotEnoughQuantityInTheStock";
        public const string AvailableAmount = "AvailableAmount";
        public const string RequiredAmount = "RequiredAmount";
        public const string CannotSendDatatoNoOne = "CannotSendDatatoNoOne";

        public const string ReqEarlyExit = "ReqEarlyExit";
        public const string ReqLateAttendance = "ReqLateAttendance";
        public const string ReqPermitExit = "ReqPermitExit";
        public const string ReqPermitFromHome = "ReqPermitFromHome";
        public const string ReqPermitFingerprint = "ReqPermitFingerprint";
        public const string ReqResign = "ReqResign";
        public const string ReqSalaryInc = "ReqSalaryInc";
        public const string PermitTrust = "PermitTrust";
        public const string ReqVacation = "ReqVacation";
        public const string ReqTrustCash = "ReqTrustCash";
        public const string ReqTransfer = "ReqTransfer";
        public const string ReqReward = "ReqReward";
        public const string ReqWorkDayCalc = "ReqWorkDayCalc";
        public const string ReqExtraTimeCalc = "ReqExtraTimeCalc";
        public const string ReqExitPermission = "ReqExitPermission";
        public const string ReqAddvance = "ReqAddvance";
        public const string ThisCannotBeDoneDueToThePresenceOf = "ThisCannotBeDoneDueToThePresenceOf";
        public const string Resonsiblitites = "Resonsiblities";
    }

    public class LoggingMessages
    {
        public const string ErrorWhileDeleting = "Error while deleting for";
        public const string Id = "id: ";
        public const string Obj = "and obj: ";
    }

    public class Annotations
    {
        public const string Name = "Name";
        public const string ConfirmationPassword = "Confirmation password";
        public const string DepartmentName = "Department";
        public const string ConfirmationPasswordNotMatch = "Password and confirmation password must match.";
        public const string AttachmentsNotes = "Attachments notes.";

        public const string AttachmentsType = "Attachments type.";
        public const string FieldIsRequired = "The {0} is required";
        public const string BirthDate = "Birth date.";

        public const string NationalID = "National ID";
        public const string FieldIsEqual = "The {0} field length must be equal 14.";
        public const string ProfilePhoto = "Profile photo.";
        public const string Files = "Personal files.";
        public const string CourseMatrial = "Course matrial.";
        public const string CourseMatrialType = "Course matrial type.";
        public const string Password = "Password";
        public const string Code = "Code";
        public const string Company = "Company";
        public const string Job = "Job";
        public const string Gender = "Gender";
        public const string Region = "Region";
        public const string MaritalStatus = "MaritalStatus";
        public const string MilitaryStatus = "MilitaryStatus";

        public const string NameInArabic = "Name in Arabic";
        public const string NameInEnglish = "Name in English";
        public const string CompanyOwner = "Company owner";
        public const string ManagementId = "Management ID";
        public const string PersonalEmail = "Personal Email";
        public const string PhoneNumber = "Phone number";
        public const string HireDate = "Hire date";
        public const string Task = "Task";
        public const string UserName = "User name";
        public const string UserNameOrEmail = "User name or email";

        public const string CourseAsset = "Course asset";
        public const string CourseAssetDescription = "Course asset description";//
        public const string CourseAssetType = "Course asset type";
        public const string CourseAssetTypeDescription = "Course asset type description";

        public const string CourseDate = "Course date";
        public const string CourseType = "Course type";
        public const string CourseDescription = "Course description";

        public const string StartDate = "Start date";
        public const string EndDate = "End date";
        public const string RememberMe = "Remember me?";




    }
}

