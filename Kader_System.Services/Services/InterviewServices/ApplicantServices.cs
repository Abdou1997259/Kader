using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Domain.Models.Interviews;
using Kader_System.Services.IServices.InterviewServices;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.InterviewServices
{
    public class ApplicantServices : IApplicantServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly KaderDbContext _context;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer;
        private readonly IMapper _mapper;
        public ApplicantServices(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer, KaderDbContext context)
        {
            _unitOfWork = unitOfWork;
            _sharLocalizer = stringLocalizer;
            _context = context;
            _mapper = mapper;
        }


        #region Get

        //public async Task<Response<GetAllResponse>> GetAllAsync(string lang, GetAllFilteredRequests model, string host)
        //{


        //    if (!result.Any())
        //    {
        //        string resultMsg = _sharLocalizer[Localization.NotFoundData];

        //        return new()
        //        {
        //            Data = null,
        //            Error = resultMsg,
        //            Msg = resultMsg
        //        };
        //    }
        //    return new Response<GetAllResponse>
        //    {
        //        Data = result

        //    }
        //}

        public Task<Response<object>> GetById(int id, string lang)
        {
            throw new NotImplementedException();
        }



        public async Task<Response<IEnumerable<object>>> ListOfAsync(string lang)
        {
            try
            {

                Expression<Func<Applicant, bool>> filter = x => x.IsDeleted == false;

                var result = await _context.Applicants.Where(filter).Select(x => new
                {
                    id = x.Id,
                    rate = x.Rate,
                    state = x.State,
                    geneder = x.Gender,

                    applicant_name = x.FullName,
                }).ToListAsync();
                return new()
                {
                    Check = true,
                    Data = result
                };

            }
            catch (Exception ex)
            {

                return new()
                {
                    Check = false,
                    Msg = ex.Message,
                };
            }




        }
        #endregion

        #region Create
        public async Task<Response<CreateApplicantRequest>> CreateAsync(CreateApplicantRequest model, string moduleName, string lang)
        {
            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                // Check if the applicant already exists
                if (await _unitOfWork.Applicant.ExistAsync(x => x.FullName == model.FullName))
                {
                    return new()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.IsExist, _sharLocalizer[Localization.Applicant]]
                    };
                }

                // Map model to entity
                var applicant = _mapper.Map<Applicant>(model);
                await _context.Applicants.AddAsync(applicant);
                await _context.SaveChangesAsync();

                // Handle Educations
                if (model.Educations.Any())
                {
                    foreach (var edu in model.Educations)
                    {
                        var university = await _context.Universities.FirstOrDefaultAsync(x => x.Id == edu.UniversityId);
                        if (university == null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.NotFound, Localization.University]
                            };
                        }

                        var faculty = await _context.Faculties.FirstOrDefaultAsync(x => x.Id == edu.FacultyId);
                        if (faculty == null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.NotFound, Localization.Faculty]
                            };
                        }

                        var universityContainsFaculty = await _context.Faculties.FirstOrDefaultAsync(x => x.UniversityId == edu.UniversityId && x.Id == edu.FacultyId);
                        if (universityContainsFaculty is null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.UniversityContainsFaculty,
                                lang == Localization.Arabic ? university.NameAr : university.NameEn,
                                lang == Localization.Arabic ? faculty.NameAr : faculty.NameEn]
                            };
                        }

                        await _context.Educations.AddAsync(new Education
                        {
                            ApplicantId = applicant.Id,
                            FacultyId = edu.FacultyId,
                            To = edu.To,
                            From = edu.From,
                        });
                    }
                }

                // Handle Experiences
                if (model.Experiences.Any())
                {
                    foreach (var exp in model.Experiences)
                    {
                        await _context.Experiences.AddAsync(new Experience
                        {
                            ApplicantId = applicant.Id,
                            CompanyName = exp.CompanyName,
                            From = exp.From,
                            To = exp.To,
                        });
                    }
                }

                // Save changes for the applicant and related entities
                await _context.SaveChangesAsync();

                // Commit the transaction
                transaction.Commit();

                return new()
                {
                    Check = true,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                transaction.Rollback();
                // Log the exception if needed
                return new()
                {
                    Check = false,
                    Msg = "An error occurred: " + ex.Message
                };
            }

        }
        #endregion

        #region Delete
        public Task<Response<string>> DeleteContractAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public Task<Response<string>> RestoreContractAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> UpdateActiveOrNotContractAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<CreateApplicantRequest>> UpdateAsync(int id, CreateApplicantRequest model, string moduleName)
        {
            throw new NotImplementedException();
        }
        #endregion



    }
}
