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
                                Msg = _sharLocalizer[Localization.IsNotExisted, Localization.University]
                            };
                        }

                        var faculty = await _context.Faculties.FirstOrDefaultAsync(x => x.Id == edu.FacultyId);
                        if (faculty == null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.IsNotExisted, Localization.Faculty]
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
                            JobTitle = exp.JobTitle,
                            From = exp.From,
                            To = exp.To,
                        });
                    }
                }
                var directoryTypes = HrDirectoryTypes.Applicant;

                var directoryName = directoryTypes.GetModuleNameWithType(Modules.Interview);
                if (model.ImageFile != null)
                {
                    applicant.ImagePath = await _fileServer.UploadFileAsync(directoryName, model.ImageFile);

                }
                if (model.CVFile != null)
                {


                    applicant.CvFilesPath = await _fileServer.UploadFileAsync(directoryName, model.CVFile);

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
                    Msg = "An error occurred: " + ex.InnerException
                };
            }

        }
        #endregion

        #region Delete
        public async Task<Response<string>> DeleteAsync(int id)
        {
            var applicant = await _unitOfWork.Applicant.GetFirstOrDefaultAsync(x => x.Id == id);
            if (applicant == null)
            {

                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, applicant.FullName]
                };

            }
            _unitOfWork.Applicant.Remove(applicant);
            await _unitOfWork.CompleteAsync();


            return new()
            {
                Check = true,
                Msg = _sharLocalizer[Localization.DelayedSuccessfully]
            };
        }
        #endregion

        #region Update
        public async Task<Response<string>> RestoreAsync(int id)
        {
            var applicant = await _unitOfWork.Applicant.GetFirstOrDefaultAsync(x => x.Id == id);
            if (applicant == null)
            {

                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, applicant.FullName]
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
                var applicant = await _context.Applicants.FirstOrDefaultAsync(x => x.Id == id);
                if (applicant == null)
                {
                    return new() { Check = false, Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Applicant]] };


                }
                if (await _unitOfWork.Applicant.ExistAsync(x => x.Id != id && x.FullName == model.FullName))
                {
                    return new() { Check = false, Msg = _sharLocalizer[Localization.IsExist, model.FullName] };

                }



                var directoryTypes = HrDirectoryTypes.Applicant;

                var directoryName = directoryTypes.GetModuleNameWithType(Modules.Interview);
                if (model.ImageFile != null)
                {
                    applicant.ImagePath = await _fileServer.UploadFileAsync(directoryName, model.ImageFile);

                }
                if (model.CVFile != null)
                {


                    applicant.CvFilesPath = await _fileServer.UploadFileAsync(directoryName, model.CVFile);

                }

                var updateApplicant = _mapper.Map(model, applicant);

                if (model.Educations.Any())
                {
                    var oldEducations = await _unitOfWork.Education.GetSpecificSelectTrackingAsync(x => x.ApplicantId == id, x => x);
                    _unitOfWork.Education.RemoveRange(oldEducations);
                    foreach (var edu in model.Educations)
                    {
                        var university = await _context.Universities.FirstOrDefaultAsync(x => x.Id == edu.UniversityId);
                        if (university == null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.IsNotExisted, Localization.University]
                            };
                        }

                        var faculty = await _context.Faculties.FirstOrDefaultAsync(x => x.Id == edu.FacultyId);
                        if (faculty == null)
                        {
                            return new()
                            {
                                Check = false,
                                Msg = _sharLocalizer[Localization.IsNotExisted, Localization.Faculty]
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
                    if (model.Experiences.Any())
                    {
                        var oldExp = await _unitOfWork.Experience.GetSpecificSelectTrackingAsync(x => x.ApplicantId == id, x => x);
                        _unitOfWork.Experience.RemoveRange(oldExp);
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

                }
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
