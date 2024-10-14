using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Domain.Models.Interviews;
using Kader_System.Services.IServices.AppServices;
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
        private readonly IFileServer _fileServer;

        public ApplicantServices(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer, IFileServer fileserver, KaderDbContext context)
        {
            _unitOfWork = unitOfWork;
            _sharLocalizer = stringLocalizer;
            _context = context;
            _mapper = mapper;
            _fileServer = fileserver;
        }


        #region Get



        public async Task<Response<object>> GetByIdAsync(int id, string lang)
        {

            var applicant = await _unitOfWork.Applicant.GetFirstOrDefaultAsync(x => x.IsDeleted == false);

            if (applicant == null)
            {

                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.CannotBeFound, _sharLocalizer[Localization.Applicant]]
                };

            }

            var pathType = HrDirectoryTypes.Applicant;
            var pathString = pathType.GetModuleNameWithType(Modules.Interview);
            var experiencesIds = (await _unitOfWork.Experience
                .GetSpecificSelectAsync(x => x.applicant_id == applicant.id, x => new { x.id }))
                .Select(x => x.id);

            var educationIds = (await _unitOfWork.Education
          .GetSpecificSelectAsync(x => x.applicant_id == applicant.id, x => new { x.id }))
          .Select(x => x.id);
            return new()
            {
                Check = true,
                Data = new
                {
                    applicant.full_name,
                    applicant.email,
                    applicant.phone,
                    applicant.year_of_experiences,
                    applicant.date_of_birth,
                    applicant.current_salary,
                    applicant.expected_salary,
                    applicant.gender,
                    image_path = _fileServer.CombinePath(pathString, applicant.image_path ?? ""),
                    cv_file = _fileServer.CombinePath(pathString, applicant.cv_file_path ?? ""),
                    educations = educationIds,
                    experiences = experiencesIds

                }
            };



        }



        public async Task<Response<IEnumerable<object>>> ListOfAsync(string lang)
        {
            try
            {

                Expression<Func<Applicant, bool>> filter = x => x.IsDeleted == false;

                var result = await _context.Applicants.Where(filter).Select(x => new
                {
                    id = x.id,
                    rate = x.rate,
                    state = x.state,
                    geneder = x.gender,

                    applicant_name = x.full_name,
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
        public async Task<Response<CreateApplicantRequest>> CreateAsync(CreateApplicantRequest model, string
            moduleName, string lang)
        {
            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                // Check if the applicant already exists
                if (await _unitOfWork.Applicant.ExistAsync(x => x.full_name.Trim() == model.full_name.Trim()))
                {
                    return new()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.AlreadyExitedWithSameName, _sharLocalizer[Localization.Applicant]]
                    };
                }
                var job = await _unitOfWork.InterJob.GetFirstOrDefaultAsync(x => x.id == model.job_id, includeProperties: "applicants");
                if (job is null)
                {



                    return new()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.CannotBeFound, _sharLocalizer[Localization.Job]]
                    };

                }
                // Map model to entity

                var numberOfApplicantInJob = job.applicants.Count();

                if (job.applicant_count <= numberOfApplicantInJob)
                {
                    return new()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.ApplicantJobNumber]
                    };



                }
                var endJobDate = job.to;
                var dateNo = DateOnly.FromDateTime(DateTime.Now);
                if (dateNo > endJobDate)
                {
                    return new()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.EndJobDate]
                    };

                }


                var applicant = _mapper.Map<Applicant>(model);
                await _context.Applicants.AddAsync(applicant);
                await _context.SaveChangesAsync();

                // Handle Educations
                if (model.educations.Any())
                {
                    foreach (var edu in model.educations)
                    {
                        var university = await _context.Universities.FirstOrDefaultAsync(x => x.id == edu.university_id);
                        if (university == null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.IsNotExisted, Localization.University]
                            };
                        }

                        var faculty = await _context.Faculties.FirstOrDefaultAsync(x => x.id == edu.faculty_id);
                        if (faculty == null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.IsNotExisted, Localization.Faculty]
                            };
                        }

                        var universityContainsFaculty = await _context.Faculties
                            .FirstOrDefaultAsync(x => x.university_id == edu.university_id
                        && x.id == edu.faculty_id);
                        if (universityContainsFaculty is null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.UniversityContainsFaculty,
                                lang == Localization.Arabic ? university.name_ar : university.name_en,
                                lang == Localization.Arabic ? faculty.name_ar : faculty.name_en]
                            };
                        }

                        await _context.Educations.AddAsync(new Education
                        {
                            applicant_id = applicant.id,
                            faculty_id = edu.faculty_id,
                            to = edu.to,
                            from = edu.from,
                        });
                    }
                }

                // Handle Experiences
                if (model.experiences.Any())
                {
                    foreach (var exp in model.experiences)
                    {
                        await _context.Experiences.AddAsync(new Experience
                        {
                            applicant_id = applicant.id,
                            company_name = exp.company_name,
                            job_title = exp.job_title,
                            from = exp.from,
                            to = exp.to,
                        });
                    }
                }
                var directoryTypes = HrDirectoryTypes.Applicant;

                var directoryName = directoryTypes.GetModuleNameWithType(Modules.Interview);
                if (model.image_file != null)
                {
                    applicant.image_path = await _fileServer.UploadFileAsync(directoryName, model.image_file);

                }
                if (model.cv_file != null)
                {


                    applicant.cv_file_path = await _fileServer.UploadFileAsync(directoryName, model.cv_file);

                }


                // Save changes for the applicant and related entities
                await _context.SaveChangesAsync();

                // Commit the transaction
                transaction.Commit();

                return new()
                {
                    Check = true,
                    Data = model,
                    Msg = _sharLocalizer[Localization.SaveSuccessfully]
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
                    Msg = "An error occurred: " + ex.InnerException
                };
            }

        }
        #endregion

        #region Delete
        public async Task<Response<string>> DeleteAsync(int id)
        {
            var applicant = await _unitOfWork.Applicant.GetFirstOrDefaultAsync(x => x.id == id);
            if (applicant == null)
            {

                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Applicant]]
                };

            }
            _unitOfWork.Applicant.Remove(applicant);
            await _unitOfWork.CompleteAsync();


            return new()
            {
                Check = true,
                Msg = _sharLocalizer[Localization.Deleted]
            };
        }
        #endregion

        #region Update
        public async Task<Response<string>> RestoreAsync(int id)
        {
            var applicant = await _unitOfWork.Applicant.GetFirstOrDefaultAsync(x => x.id == id);
            if (applicant == null)
            {

                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Applicant]]
                };

            }
            applicant.IsDeleted = false;
            _unitOfWork.Applicant.Update(applicant);
            await _unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Msg = _sharLocalizer[Localization.Restored]
            };

        }

        public Task<Response<string>> UpdateActiveOrNotContractAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<CreateApplicantRequest>> UpdateAsync(int id, CreateApplicantRequest model, string lang)
        {
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                var applicant = await _context.Applicants.FirstOrDefaultAsync(x => x.id == id);
                if (applicant == null)
                {
                    return new() { Check = false, Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Applicant]] };


                }
                if (await _unitOfWork.Applicant.ExistAsync(x => x.id != id && x.full_name.Trim() == model.full_name.Trim()))
                {
                    return new() { Check = false, Msg = _sharLocalizer[Localization.AlreadyExitedWithSameName, model.full_name] };

                }

                var job = await _unitOfWork.InterJob.GetFirstOrDefaultAsync(x => x.id == model.job_id);
                if (job is null)
                {



                    return new()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.CannotBeFound, _sharLocalizer[Localization.Job]]
                    };

                }

                var directoryTypes = HrDirectoryTypes.Applicant;

                var directoryName = directoryTypes.GetModuleNameWithType(Modules.Interview);
                if (model.image_file != null)
                {
                    applicant.image_path = await _fileServer.UploadFileAsync(directoryName, model.image_file);

                }
                if (model.cv_file != null)
                {


                    applicant.cv_file_path = await _fileServer.UploadFileAsync(directoryName, model.cv_file);

                }

                var updateApplicant = _mapper.Map(model, applicant);

                if (model.educations.Any())
                {
                    var oldEducations = await _unitOfWork.Education.GetSpecificSelectTrackingAsync(x => x.applicant_id == id, x => x);
                    _unitOfWork.Education.RemoveRange(oldEducations);
                    foreach (var edu in model.educations)
                    {
                        var university = await
                            _context.Universities.FirstOrDefaultAsync(x => x.id == edu.university_id);
                        if (university == null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.IsNotExisted, Localization.University]
                            };
                        }

                        var faculty = await _context.Faculties.FirstOrDefaultAsync(x => x.id == edu.faculty_id);
                        if (faculty == null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.IsNotExisted, Localization.Faculty]
                            };
                        }

                        var universityContainsFaculty = await
                            _context.Faculties.FirstOrDefaultAsync(x => x.university_id == edu.university_id && x.id == edu.faculty_id);
                        if (universityContainsFaculty is null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.UniversityContainsFaculty,
                                lang == Localization.Arabic ? university.name_ar : university.name_en,
                                lang == Localization.Arabic ? faculty.name_ar : faculty.name_en]
                            };
                        }

                        await _context.Educations.AddAsync(new Education
                        {
                            applicant_id = applicant.id,
                            faculty_id = edu.faculty_id,
                            to = edu.to,
                            from = edu.from,
                        });
                    }
                    if (model.experiences.Any())
                    {
                        var oldExp = await _unitOfWork.Experience.GetSpecificSelectTrackingAsync(x => x.applicant_id == id, x => x);
                        _unitOfWork.Experience.RemoveRange(oldExp);
                        foreach (var exp in model.experiences)
                        {
                            await _context.Experiences.AddAsync(new Experience
                            {
                                applicant_id = applicant.id,
                                company_name = exp.company_name,
                                from = exp.from,
                                to = exp.to,
                            });
                        }
                    }

                }
                await _context.SaveChangesAsync();

                // Commit the transaction
                transaction.Commit();

                return new()
                {
                    Check = true,
                    Data = model,
                    Msg = _sharLocalizer[Localization.SaveSuccessfully]
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                // Log the exception if needed
                return new()
                {
                    Check = false,
                    Msg = "An error occurred: " + ex.Message
                };

            }
            #endregion


        }


    }
}
