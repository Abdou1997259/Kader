namespace Kader_System.Domain.Constants.Enums
{
    public enum HrEmployeeRequestTypesEnums
    {
        #region Permessions
        LeavePermission = 1,
        DelayPermission = 2,
        #endregion

        #region Requests
        VacationRequest = 3,
        SalaryIncreaseRequest = 4,
        AllowanceRequest = 5,
        ResignationRequest = 6,
        TerminateContract = 7,
        LoanRequest = 8,
        #endregion
        None = 0
    }
    public enum HrDirectoryTypes
    {
        CompanyContracts = 1,
        CompanyLicesnses = 2,
        Contracts = 3,
        Attachments = 4,
        User = 5,
        EmployeeProfile = 6,
        Applicant = 7,
        Deductions = 8,
        Benefits = 9,
        Covenant = 10,
        Vacation = 11,
        JobOffers = 12
    }


    public enum UsereEnum
    {
        None = 0,
        Users = 1,

    }
}
