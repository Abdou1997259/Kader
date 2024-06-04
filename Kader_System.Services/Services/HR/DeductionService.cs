using Kader_System.Domain.DTOs;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.HR;

public class DeductionService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : IDeductionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IMapper _mapper = mapper;

    #region Deduction

    public async Task<Response<IEnumerable<SelectListResponse>>> ListOfDeductionsAsync(string lang)
    {
        var result =
                await _unitOfWork.Deductions.GetSpecificSelectAsync(null!,
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

    public async Task<Response<HrGetAllDeductionsResponse>> GetAllDeductionsAsync(string lang, HrGetAllFiltrationsForDeductionsRequest model,string host)
    {
        Expression<Func<HrDeduction, bool>> filter = x => x.IsDeleted == model.IsDeleted &&
                                                          (string.IsNullOrEmpty(model.Word) || x.Name_ar.Contains(model.Word) || x.Name_en.Contains(model.Word));
        var totalRecords = await _unitOfWork.Deductions.CountAsync(filter: filter);
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();
        var result = new HrGetAllDeductionsResponse
        {
            TotalRecords = await _unitOfWork.Deductions.CountAsync(filter: filter),

            Items = (await _unitOfWork.Deductions.GetSpecificSelectAsync(filter: filter,
                 take: model.PageSize,
                 skip: (model.PageNumber - 1) * model.PageSize,
                 select: x => new DeductionData
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
            Links = pageLinks,
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

    public async Task<Response<HrCreateDeductionRequest>> CreateDeductionAsync(HrCreateDeductionRequest model)
    {
        bool exists = false;
        exists = await _unitOfWork.Deductions.ExistAsync(x => x.Name_ar.Trim() == model.Name_ar
        && x.Name_en.Trim() == model.Name_en.Trim());

        if (exists)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.IsExist],
                _sharLocalizer[Localization.Deduction]);

            return new()
            {
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        await _unitOfWork.Deductions.AddAsync(new()
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

    public async Task<Response<HrGetDeductionByIdResponse>> GetDeductionByIdAsync(int id)
    {
        var obj = await _unitOfWork.Deductions.GetByIdAsync(id);

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

    public async Task<Response<HrUpdateDeductionRequest>> UpdateDeductionAsync(int id, HrUpdateDeductionRequest model)
    {
        var obj = await _unitOfWork.Deductions.GetByIdAsync(id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Deduction]);

            return new()
            {
                Data = model,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        obj.Name_ar = model.Name_ar;
        obj.Name_en = model.Name_en;

        _unitOfWork.Deductions.Update(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data = model,
            Msg = _sharLocalizer[Localization.Updated]
        };
    }


    public async Task<Response<HrGetDeductionByIdResponse>> RestoreDeductionAsync(int id)
    {
        var obj = await _unitOfWork.Deductions.GetByIdAsync(id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Deduction]);

            return new()
            {
                Data = null,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        obj.IsDeleted = false;

        unitOfWork.Deductions.Update(obj);

        await _unitOfWork.CompleteAsync();
        return new()
        {
            Check = true,
            Error = string.Empty,
            LookUps = null,
            Data = new HrGetDeductionByIdResponse()
            {
                Id = obj.Id,
                Name_ar = obj.Name_ar,
                Name_en = obj.Name_en
            },
            Msg = string.Format(_sharLocalizer[Localization.Updated],
                _sharLocalizer[Localization.Deduction]),

        };
    }
    public Task<Response<string>> UpdateActiveOrNotDeductionAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<string>> DeleteDeductionAsync(int id)
    {
        var obj = await _unitOfWork.Deductions.GetByIdAsync(id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Deduction]);

            return new()
            {
                Data = string.Empty,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        _unitOfWork.Deductions.Remove(obj);
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


