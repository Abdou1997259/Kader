using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.Interview;
using Kader_System.Domain.DTOs.Response.Interview;
using Kader_System.Domain.Models.Interviews;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.InterviewServices;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Kader_System.Services.Services.InterviewServices
{
    public class InterJobServices : IInterJobServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly KaderDbContext _context;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer;
        private readonly IMapper _mapper;
        private readonly IFileServer _fileServer;


        public InterJobServices(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> stringLocalizer,
            IFileServer fileserver,
            KaderDbContext context)
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
                    applicant_id = applicant.id,



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
                var dateNow = DateOnly.FromDateTime(DateTime.Now);

                await _context.InterJobs.AddAsync(new Job
                {
                    name_en = model.name_en,
                    name_ar = model.name_ar,
                    applicant_count = model.applicant_count,
                    description = model.description,
                    from = model.from,
                    to = model.to,
                    state_id = model.to < dateNow ? 3 : 1
                });


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
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                job.state_id = job.to < dateNow ? 3 : 1;
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

        public async Task<Response<GetAllResponse>> GetPaginatedJobs
            (GetAllFilteredJobRequests model, string lang, string host)
        {

            var dateNow = DateOnly.FromDateTime(DateTime.Now);
            Expression<Func<Job, bool>> filters = x =>
      !x.IsDeleted && (
          (!model.IsFinished.HasValue ||
              (model.IsFinished == true && (x.to < dateNow || x.to == null)) ||
              (model.IsFinished == false && x.to > dateNow && x.to != null)) &&
          (string.IsNullOrEmpty(model.Word) ||
              x.name_ar.Contains(model.Word) ||
              x.name_en.Contains(model.Word)) &&
          (!model.From.HasValue || x.from == model.From) &&
          (!model.To.HasValue || x.to == model.To)
      );
            var totalRecords = await _unitOfWork.InterJob.CountAsync(filters);
            var result = await _unitOfWork.InterJob.GetSpecificSelectAsync(filters, includeProperties: "state", select: x =>
            new JobList
            {
                id = x.id,
                to = x.to,
                from = x.from,

                name = lang == Localization.Arabic ? x.name_ar : x.name_en,
                applicant_count = x.applicant_count,
                state = x.state_id,

            }, orderBy: x => x.OrderBy(x => x.id), skip: (model.PageSize) * (model.PageNumber - 1), take: model.PageSize);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();


            if (totalRecords is 0)
            {
                string resultMsg = _sharLocalizer[Localization.NotFoundData];

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
                Data = new()
                {
                    TotalRecords = totalRecords,
                    Items = result.ToList(),
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
                    Links = pageLinks,
                    finished_job_count = _context.InterJobs.Where(x => x.to < dateNow || x.to == null).Count(),
                    job_count = _context.InterJobs.Count(),
                    all_applicant_count = _context.InterJobs
                                .SelectMany(job => job.applicants.Where(applicant => !applicant.IsDeleted))
                                .Count()

                },
                Check = true
            };
        }

        public async Task<Response<string>> SuspendJob(int id)
        {
            try
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
                job.state_id = 2;
                _unitOfWork.InterJob.Update(job);
                await _unitOfWork.CompleteAsync();
                return new()
                {
                    Check = true,
                    Msg = Localization.SuspendedSuccessfully
                };
            }
            catch (Exception ex)
            {

                return new()
                {
                    Check = false,
                    Msg = ex?.InnerException?.Message
                };

            }
        }

        public async Task<Response<string>> ResumeJob(int id)
        {
            try
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
                job.state_id = 1;
                _unitOfWork.InterJob.Update(job);
                await _unitOfWork.CompleteAsync();
                return new()
                {
                    Check = true,
                    Msg = Localization.ResumeSuccessfully
                };
            }
            catch (Exception ex)
            {

                return new()
                {
                    Check = false,
                    Msg = ex?.InnerException?.Message
                };

            }
        }

        public async Task<Response<string>> FinishJob(int id)
        {
            try
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
                job.to = null;
                job.state_id = 3;
                _unitOfWork.InterJob.Update(job);
                await _unitOfWork.CompleteAsync();
                return new()
                {
                    Check = true,
                    Msg = Localization.FinishedSuccessfully
                };
            }
            catch (Exception ex)
            {

                return new()
                {
                    Check = false,
                    Msg = ex?.InnerException?.Message
                };

            }
        }

        public async Task<Response<string>> ReplayJob(int id, ReplayJobRequest model)
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



                job.from = model.from;
                job.to = model.to;
                if (model.applicant_count.HasValue)
                {
                    job.applicant_count = model.applicant_count.Value;

                }
                var dateNow = DateOnly.FromDateTime(DateTime.Now);
                job.state_id = 1;
                await _context.SaveChangesAsync();

                // Commit the transaction
                transaction.Commit();

                return new()
                {
                    Check = true,

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
        }
    }
}
