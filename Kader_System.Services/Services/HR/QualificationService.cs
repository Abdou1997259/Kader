using Kader_System.Domain.DTOs;

namespace Kader_System.Services.Services.HR;

public class QualificationService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : IQualificationService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IMapper _mapper = mapper;

    #region Qualification

    public async Task<Response<IEnumerable<SelectListResponse>>> ListOfQualificationsAsync(string lang)
    {
        var result =
                await _unitOfWork.Qualifications.GetSpecificSelectAsync(null!,
                select: x => new SelectListResponse
                {
                    Id = x.Id,
                    Name = lang == Localization.Arabic ? x.NameAr : x.NameEn
                }, orderBy: x =>
                  x.OrderByDescending(x => x.Id));

        if (!result.Any())
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new()
            {
                Data = [],
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

    public async Task<Response<HrGetAllQualificationsResponse>> GetAllQualificationsAsync(string lang, HrGetAllFiltrationsForQualificationsRequest model,string host)
    {
        Expression<Func<HrQualification, bool>> filter = x => x.IsDeleted == model.IsDeleted
            && (string.IsNullOrEmpty(model.Word) || x.NameAr.Contains(model.Word)
                                                 || x.NameEn.Contains(model.Word)
                                               );
        var totalRecords = await _unitOfWork.Qualifications.CountAsync(filter: filter);
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();
        var result = new HrGetAllQualificationsResponse
        {
            TotalRecords =  totalRecords,

            Items =  _unitOfWork.Qualifications.GetQualificationInfo(qualFilter: filter,
                 take: model.PageSize,
                 skip: (model.PageNumber - 1) * model.PageSize,
                 lang:lang
                ),
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
            CurrentPage = page
        };

        if (result.TotalRecords is 0)
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
            Data = result,
            Check = true
        };
    }

    public async Task<Response<HrCreateQualificationRequest>> CreateQualificationAsync(HrCreateQualificationRequest model)
    {
        bool exists = false;
        exists = await _unitOfWork.Qualifications.ExistAsync(x => x.NameAr.Trim() == model.Name_ar
        && x.NameEn.Trim() == model.Name_en.Trim());

        if (exists)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.IsExist],
                _sharLocalizer[Localization.Qualification]);

            return new()
            {
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        await _unitOfWork.Qualifications.AddAsync(new()
        {
            NameEn = model.Name_en,
            NameAr = model.Name_ar
        });
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Msg = _sharLocalizer[Localization.Done],
            Check = true,
            Data = model
        };
    }

    public async Task<Response<HrGetQualificationByIdResponse>> GetQualificationByIdAsync(int id)
    {
        var obj = await _unitOfWork.Qualifications.GetByIdAsync(id);

        if (obj is null)
        {
            string resultMsg = _sharLocalizer[Localization.NotFoundData];

            return new()
            {
                Data = new(),
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        return new()
        {
            Data = new()
            {
                Id = id,
                Name_ar = obj.NameAr,
                Name_en = obj.NameEn
            },
            Check = true
        };
    }

    public async Task<Response<HrUpdateQualificationRequest>> UpdateQualificationAsync(int id, HrUpdateQualificationRequest model)
    {
        var obj = await _unitOfWork.Qualifications.GetByIdAsync(id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Qualification]);

            return new()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        obj.NameAr = model.Name_ar;
        obj.NameEn = model.Name_en;

        _unitOfWork.Qualifications.Update(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data = model,
            Msg = _sharLocalizer[Localization.Updated]
        };
    }


    public async Task<Response<HrUpdateQualificationRequest>> RestoreQualificationAsync(int id)
    {
        var obj = await _unitOfWork.Qualifications.GetFirstOrDefaultAsync(q=>q.Id==id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Qualification]);

            return new()
            {
                Data = null,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        obj.IsDeleted = false;
        _unitOfWork.Qualifications.Update(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data = new()
            {
                Name_ar = obj.NameAr,
                Name_en = obj.NameEn
            },
            Msg = string.Format(_sharLocalizer[Localization.Restored],
                _sharLocalizer[Localization.Qualification])
        };
    }

    public Task<Response<string>> UpdateActiveOrNotBenefitAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<string>> DeleteQualificationAsync(int id)
    {
        var obj = await _unitOfWork.Qualifications.GetByIdAsync(id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Qualification]);

            return new()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        _unitOfWork.Qualifications.Remove(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data = string.Empty,
            Msg = _sharLocalizer[Localization.Deleted]
        };
    }

    #endregion
}


