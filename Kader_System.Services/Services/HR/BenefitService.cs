using Kader_System.Domain.DTOs;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.HR;

public class BenefitService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : IBenefitService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IMapper _mapper = mapper;

    #region Allowance

    public async Task<Response<IEnumerable<SelectListResponse>>> ListOfBenefitsAsync(string lang)
    {
        var result =
                await _unitOfWork.Benefits.GetSpecificSelectAsync(null!,
                select: x => new SelectListResponse
                {
                    Id = x.Id,
                    Name = lang == Localization.Arabic ? x.Name_ar : x.Name_en
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

    public async Task<Response<HrGetAllBenefitsResponse>> GetAllBenefitsAsync(string lang, HrGetAllFiltrationsForBenefitsRequest model,string host)
    {
        Expression<Func<HrBenefit, bool>> filter = x => x.IsDeleted == model.IsDeleted &&
            (string.IsNullOrEmpty(model.Word) || x.Name_ar.Contains(model.Word )|| x.Name_en.Contains(model.Word));
        var totalRecords =await _unitOfWork.Benefits.CountAsync(filter: filter);
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();
        var result = new HrGetAllBenefitsResponse
        {
            TotalRecords = totalRecords,

            Items = (await _unitOfWork.Benefits.GetSpecificSelectAsync(filter: filter,
                 take: model.PageSize,
                 skip: (model.PageNumber - 1) * model.PageSize,
                 select: x => new BenefitData
                 {
                     Id = x.Id,
                     Name = lang == Localization.Arabic ? x.Name_ar : x.Name_en
                 }, orderBy: x =>
                   x.OrderByDescending(x => x.Id))).ToList(),
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

    public async Task<Response<HrCreateBenefitRequest>> CreateBenefitAsync(HrCreateBenefitRequest model)
    {
        bool exists = false;
        exists = await _unitOfWork.Benefits.ExistAsync(x => x.Name_ar.Trim() == model.Name_ar
        && x.Name_en.Trim() == model.Name_en.Trim());

        if (exists)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.IsExist],
                _sharLocalizer[Localization.Benefit]);

            return new()
            {
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        await _unitOfWork.Benefits.AddAsync(new()
        {
            Name_en = model.Name_en,
            Name_ar = model.Name_ar
        });
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Msg = _sharLocalizer[Localization.Done],
            Check = true,
            Data = model
        };
    }

    public async Task<Response<HrGetBenefitByIdResponse>> GetBenefitByIdAsync(int id)
    {
        var obj = await _unitOfWork.Benefits.GetByIdAsync(id);

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
                Name_ar = obj.Name_ar,
                Name_en = obj.Name_en
            },
            Check = true
        };
    }

    public async Task<Response<HrUpdateBenefitRequest>> UpdateBenefitAsync(int id, HrUpdateBenefitRequest model)
    {
        var obj = await _unitOfWork.Benefits.GetByIdAsync(id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Benefit]);

            return new()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        obj.Name_ar = model.Name_ar;
        obj.Name_en = model.Name_en;

        _unitOfWork.Benefits.Update(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data = model,
            Msg = _sharLocalizer[Localization.Updated]
        };
    }

    public Task<Response<string>> UpdateActiveOrNotBenefitAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<HrGetBenefitByIdResponse>> RestoreBenefitAsync(int id)
    {
        var obj = await _unitOfWork.Benefits.GetByIdAsync(id);
        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Benefit]);

            return new()
            {
                Data = null,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        obj.IsDeleted = false;
        unitOfWork.Benefits.Update(obj);
        await unitOfWork.CompleteAsync();
        return new()
        {
            Check = true,
            Data =new()
            {
                Id = obj.Id,
                Name_ar = obj.Name_ar,
                Name_en = obj.Name_en
            },
            Msg = _sharLocalizer[Localization.Deleted]
        };
    }
    public async Task<Response<string>> DeleteBenefitAsync(int id)
    {
        var obj = await _unitOfWork.Benefits.GetByIdAsync(id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Benefit]);

            return new()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        _unitOfWork.Benefits.Remove(obj);
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


