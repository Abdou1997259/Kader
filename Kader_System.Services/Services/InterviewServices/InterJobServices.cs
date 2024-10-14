using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Domain.Models.Interviews;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.InterviewServices;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.InterviewServices
{
    public class InterJobServices : IInterJobServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly KaderDbContext _context;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer;
        private readonly IMapper _mapper;
        private readonly IFileServer _fileServer;

        public InterJobServices(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer, IFileServer fileserver, KaderDbContext context)
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

            var applicant = await _unitOfWork.InterJob.GetFirstOrDefaultAsync(x => x.IsDeleted == false);

            if (applicant == null)
            {

                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.CannotBeFound, _sharLocalizer[Localization.Job]]
                };

            }


            return new()
            {
                Check = true,
                Data = new
                {
                    applicant.from,
                    applicant.to,
                    applicant.applicant_count,
                    applicant.name_ar,
                    applicant.name_en,
                    applicant.description,



                }
            };



        }



        public async Task<Response<IEnumerable<object>>> ListOfAsync(string lang)
        {
            try
            {

                Expression<Func<Job, bool>> filter = x => x.IsDeleted == false;

                var result = await _context.InterJobs.Where(filter).Select(x => new
                {
                    x.id,
                    x.from,
                    x.to,
                    x.applicant_count,
                    name = lang == Localization.Arabic ? x.name_ar : x.name_en,


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
        public async Task<Response<CreateInterJobRequest>> CreateAsync(CreateInterJobRequest model, string moduleName, string lang)
        {
            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                if (await _unitOfWork.InterJob.ExistAsync(x =>
                x.name_ar.Trim() == model.name_ar.Trim() ||
                x.name_en.Trim() == model.name_en.Trim()))
                {
                    return new()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.AlreadyExitedWithSameName, _sharLocalizer[Localization.Job]]
                    };

                }
                // Check if the applicant already exists


                await _context.InterJobs.AddAsync(new Job
                {
                    name_en = model.name_en,
                    name_ar = model.name_ar,
                    applicant_count = model.applicant_count,
                    description = model.description,
                    from = model.from,
                    to = model.to,
                });
                await _context.SaveChangesAsync();

                // Handle Educations


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
            var job = await _unitOfWork.InterJob.GetFirstOrDefaultAsync(x => x.id == id);
            if (job == null)
            {

                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Job]]
                };

            }
            _unitOfWork.InterJob.Remove(job);
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
            var job = await _unitOfWork.InterJob.GetFirstOrDefaultAsync(x => x.id == id);
            if (job == null)
            {

                return new()
                {
                    Check = false,
                    Msg = _sharLocalizer[Localization.IsNotExisted, _sharLocalizer[Localization.Job]]
                };

            }
            job.IsDeleted = false;
            _unitOfWork.InterJob.Update(job);
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

        public async Task<Response<CreateInterJobRequest>> UpdateAsync(int id, CreateInterJobRequest model, string lang)
        {
            using var transaction = _unitOfWork.BeginTransaction();
            try
            {
                var job = await _context.InterJobs.FirstOrDefaultAsync(x => x.id == id);
                if (job == null)
                {
                    return new()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.IsNotExisted,
                        _sharLocalizer[Localization.Job]]
                    };


                }
                if (await _unitOfWork.InterJob.ExistAsync(x => x.id != id &&
                (x.name_ar.Trim() == model.name_ar.Trim() ||
                x.name_en.Trim() == model.name_en.Trim())))
                {
                    return new()
                    {
                        Check = false,
                        Msg = _sharLocalizer[Localization.AlreadyExitedWithSameName,
                        _sharLocalizer[Localization.Job]]
                    };

                }


                job.from = model.from;
                job.to = model.to;
                job.name_ar = model.name_ar;
                job.name_en = model.name_en;
                job.description = model.description;
                job.applicant_count = model.applicant_count;

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
