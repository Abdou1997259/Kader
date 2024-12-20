﻿

using Kader_System.Domain.DTOs.Request.HR.Loan;
using Kader_System.Domain.DTOs.Response.Trans;

namespace Kader_System.Api.Profiles
{
    public class TransProfile : Profile
    {
        public TransProfile()
        {
            #region Allowance

            CreateMap<TransAllowance, CreateTransAllowanceRequest>()
                .ReverseMap();
            CreateMap<TransactionAllowanceGetByIdResponse, TransAllowance>()
                .ForMember(dest => dest.Add_date,
                    opt =>
                        opt.MapFrom(src => src.AddedOn))
                .ReverseMap();

            #endregion

            #region Benefit

            CreateMap<TransBenefit, CreateTransBenefitRequest>()
                .ForMember(dest => dest.increase_type_id
                    , opt =>
                        opt.MapFrom(src => src.amount_type_id))
                .ReverseMap();
            CreateMap<GetTransBenefitById, TransBenefit>()
                .ForMember(dest => dest.Add_date
                    , opt =>
                        opt.MapFrom(src => src.AddedOn))
                .ForMember(dest => dest.amount_type_id
                    , opt =>
                        opt.MapFrom(src => src.increase_type_id))
                .ReverseMap();

            #endregion

            #region Deduction

            CreateMap<TransDeduction, CreateTransDeductionRequest>()
                .ReverseMap();
            CreateMap<GetTransDeductionById, TransDeduction>()
                .ForMember(dest => dest.Add_date
                    , opt =>
                        opt.MapFrom(src => src.AddedOn))
                .ReverseMap();

            #endregion

            #region Vacation

            CreateMap<TransVacation, CreateTransVacationRequest>()
                .ReverseMap();
            CreateMap<GetTransVacationById, TransVacation>()
                            .ReverseMap();

            #endregion

            #region Covenant

            CreateMap<TransCovenant, CreateTransCovenantRequest>()
                .ReverseMap();
            CreateMap<GetTransCovenantById, TransCovenant>()
                .ReverseMap();

            #endregion

            #region Loans
            CreateMap<TransLoan, CreateLoanRequest>()
           .ReverseMap();

            CreateMap<TransLoan, UpdateLoanRequest>()
             .ReverseMap();
            #endregion

            #region SalaryIncrease
            CreateMap<CreateTransSalaryIncreaseRequest, TransSalaryIncrease>();
            #endregion
        }
    }
}
