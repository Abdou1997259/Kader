using Kader_System.Domain.Models.HR;

namespace Kader_System.Api.Profiles;

public class HrProfile : Profile
{
    public HrProfile()
    {
        #region Company

        CreateMap<StScreenSub, StUpdateSubMainScreenRequest>()
                .ReverseMap();

        #endregion


        #region Department

        CreateMap<HrDepartment,CreateDepartmentRequest>()
            .ReverseMap();

        #endregion

        #region Employee
        CreateMap<HrEmployee, CreateEmployeeRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ForMember(dest=>dest.account_no,opt=>opt.MapFrom(src=>src.AccountNo))
            .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.birth_date, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.hiring_date, opt => opt.MapFrom(src => src.HiringDate))
            .ForMember(dest => dest.children_number, opt => opt.MapFrom(src => src.ChildrenNumber))
            .ForMember(dest => dest.company_id, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.department_id, opt => opt.MapFrom(src => src.DepartmentId))
            .ForMember(dest => dest.management_id, opt => opt.MapFrom(src => src.ManagementId))
            .ForMember(dest => dest.job_id, opt => opt.MapFrom(src => src.JobId))
            .ForMember(dest => dest.job_number, opt => opt.MapFrom(src => src.JobNumber))
            .ForMember(dest => dest.qualification_id, opt => opt.MapFrom(src => src.QualificationId))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.employee_type_id, opt => opt.MapFrom(src => src.EmployeeTypeId))
            .ForMember(dest => dest.finger_print_code, opt => opt.MapFrom(src => src.FingerPrintCode))
            .ForMember(dest => dest.finger_print_id, opt => opt.MapFrom(src => src.FingerPrintId))
            .ForMember(dest => dest.first_name_ar, opt => opt.MapFrom(src => src.FirstNameAr))
            .ForMember(dest => dest.first_name_en, opt => opt.MapFrom(src => src.FirstNameEn))
            .ForMember(dest => dest.father_name_ar, opt => opt.MapFrom(src => src.FatherNameAr))
            .ForMember(dest => dest.father_name_en, opt => opt.MapFrom(src => src.FatherNameEn))
            .ForMember(dest => dest.grand_father_name_ar, opt => opt.MapFrom(src => src.GrandFatherNameAr))
            .ForMember(dest => dest.grand_father_name_en, opt => opt.MapFrom(src => src.GrandFatherNameEn))
            .ForMember(dest => dest.family_name_ar, opt => opt.MapFrom(src => src.FamilyNameAr))
            .ForMember(dest => dest.family_name_en, opt => opt.MapFrom(src => src.FamilyNameEn))
            .ForMember(dest => dest.gender_id, opt => opt.MapFrom(src => src.GenderId))
            .ForMember(dest => dest.nationality_id, opt => opt.MapFrom(src => src.NationalityId))
            .ForMember(dest => dest.religion_id, opt => opt.MapFrom(src => src.ReligionId))
            .ForMember(dest => dest.marital_status_id, opt => opt.MapFrom(src => src.MaritalStatusId))
            .ForMember(dest => dest.national_id, opt => opt.MapFrom(src => src.NationalId))
            .ForMember(dest => dest.salary_payment_way_id, opt => opt.MapFrom(src => src.SalaryPaymentWayId))
            .ForMember(dest => dest.job_number, opt => opt.MapFrom(src => src.JobNumber))
            .ForMember(dest => dest.vacation_id, opt => opt.MapFrom(src => src.VacationId))
            .ForMember(dest => dest.shift_id, opt => opt.MapFrom(src => src.ShiftId))
            .ReverseMap();
        CreateMap<HrEmployee,EmployeesData>()
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company!.NameAr))
            .ForMember(dest => dest.Management, opt => opt.MapFrom(src => src.Management!.NameAr));


        #endregion


        #region Contract
        CreateMap<HrContract,CreateContractRequest>()
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.ListOfAllowancesDetails.Select(d => new CreateContractDetailsRequest
            {
                AllowanceId = d.AllowanceId,
                Value = d.Value,
                IsPercent = d.IsPercent,
                Id = d.Id
            }).ToList()))
            .ReverseMap();


        #endregion


        #region FingerPrintDevice

        CreateMap<HrFingerPrint, CreateFingerPrintDeviceRequest>()
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();
  
        CreateMap<HrFingerPrint, GetFingerPrintDeviceByIdResponse>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company!.NameAr))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company!.Id))
            .IgnoreAllPropertiesWithAnInaccessibleSetter()
            .ReverseMap();
        #endregion
    }
}
