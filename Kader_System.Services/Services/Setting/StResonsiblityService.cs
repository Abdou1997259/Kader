using Kader_System.Domain.DTOs;

namespace Kader_System.Services.Services.Setting
{
    public class StResonsiblityService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : IStResonsiblityService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;











        #region Retreive

        public async Task<Response<GetAllPaginatedStResonsiblityResponse>> GetAllStResonsiblitysAsync(string lang, string host, GetAllFilterationStResonsiblity model)
        {
            Expression<Func<StResonsiblity, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                                          && (string.IsNullOrEmpty(model.Word)

                                                             || x.StResponsiblity_Name_En.Contains(model.Word)
                                                             || x.StResponsiblity_Name_Ar.Contains(model.Word));
            var totalRecords = await unitOfWork.StResonsiblityRepository.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllPaginatedStResonsiblityResponse
            {
                TotalRecords = totalRecords,

                Items = (await _unitOfWork.StResonsiblityRepository.GetSpecificSelectAsync(filter: filter,
                     take: model.PageSize,
                     skip: (model.PageNumber - 1) * model.PageSize,
                     select: x => new PaginatedStResonsiblity
                     {
                         Id = x.Id,
                         StResponsiblity_Name = Localization.Arabic == lang ? x.StResponsiblity_Name_Ar : x.StResponsiblity_Name_En

                     }, orderBy: x =>
                       x.OrderByDescending(x => x.Id))).ToList()

                 ,
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
                string resultMsg = sharLocalizer[Localization.NotFoundData];

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

        public async Task<Response<GetStResonsiblityByIdReponse>> GetStResonsiblityByIdAsync(int id, string lang)
        {
            var obj = await unitOfWork.StResonsiblityRepository.GetByIdAsync(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Error = string.Empty,
                Check = true,
                Data = new()
                {
                    Id = obj.Id,
                    StResponsiblity_Name = Localization.Arabic == lang ? obj.StResponsiblity_Name_Ar : obj.StResponsiblity_Name_En
                }
            };
        }

        #endregion

        #region Insert
        public async Task<Response<CreateStResonsiblityRequest>> CreateStResonsiblityAsync(CreateStResonsiblityRequest model)
        {
            var exists = await unitOfWork.StResonsiblityRepository.ExistAsync(x => x.StResponsiblity_Name_Ar.Trim() == model.StResponsiblity_Name_Ar.Trim()
                                                                  && x.StResponsiblity_Name_En.Trim() == model.StResponsiblity_Name_En.Trim());

            if (exists)
            {
                string resultMsg = string.Format(sharLocalizer[Localization.IsExist],
                    sharLocalizer[Localization.Resonsiblitites]);

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }


            var result = _mapper.Map<StResonsiblity>(model);

            await unitOfWork.StResonsiblityRepository.AddAsync(result);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }
        #endregion


        #region Update
        public async Task<Response<UpdateStResonsiblityRequest>> UpdateStResonsiblityAsync(int id, UpdateStResonsiblityRequest model)
        {
            var obj = await unitOfWork.StResonsiblityRepository.GetByIdAsync(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }


            var updateObj = _mapper.Map(model, obj);

            _unitOfWork.StResonsiblityRepository.Update(updateObj);

            await unitOfWork.CompleteAsync();

            return new()
            {
                Error = string.Empty,
                Msg = sharLocalizer[Localization.Updated],
                Check = true,
                Data = model,
            };


        }

        public async Task<Response<GetStResonsiblityByIdReponse>> RestoreStResonsiblityAsync(int id, string lang)
        {
            var obj = await unitOfWork.StResonsiblityRepository.GetByIdAsync(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }
            obj.IsDeleted = false;



            unitOfWork.StResonsiblityRepository.Update(obj);

            await unitOfWork.CompleteAsync();

            return new()
            {
                Check = true,
                Error = string.Empty,
                Msg = sharLocalizer[Localization.Restored],
                Data = new()
                {
                    StResponsiblity_Name = Localization.Arabic == lang ? obj.StResponsiblity_Name_Ar : obj.StResponsiblity_Name_En
                }
            };
        }
        #endregion

        #region Delete

        public async Task<Response<string>> DeleteStResonsiblityAsync(int id)
        {
            var obj = await unitOfWork.StResonsiblityRepository.GetByIdAsync(id);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }



            unitOfWork.StResonsiblityRepository.Remove(obj);

            await unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = string.Empty,
                Msg = sharLocalizer[Localization.Deleted]
            };
        }

        #endregion
    }
}
