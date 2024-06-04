using Kader_System.Domain.DTOs;
using Microsoft.Extensions.Hosting;

namespace Kader_System.Services.Services.HR;

public class ShiftService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : IShiftService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
    private readonly IMapper _mapper = mapper;

    #region Shift

    public async Task<Response<IEnumerable<SelectListResponse>>> ListOfShiftsAsync(string lang)
    {
        var result =
                await _unitOfWork.Shifts.GetSpecificSelectAsync(null!,
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

    public async Task<Response<HrGetAllShiftsResponse>> GetAllShiftsAsync(string lang,
        HrGetAllFiltrationsForShiftsRequest model,string host)
    {
        Expression<Func<HrShift, bool>> filter = x => x.IsDeleted == model.IsDeleted
                                                      && (string.IsNullOrEmpty(model.Word) ||
                                                          x.Name_ar.Contains(model.Word)
                                                          || x.Name_en.Contains(model.Word)
                                                          || x.Start_shift.ToString().Contains(model.Word)
                                                          || x.End_shift.ToString().Contains(model.Word));
                                               
        var totalRecords = await _unitOfWork.Shifts.CountAsync(filter: filter);
        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();
        var result = new HrGetAllShiftsResponse
        {
            TotalRecords = totalRecords,

            Items = ( _unitOfWork.Shifts.GetShiftInfo(shiftFilter: filter,
                 take: model.PageSize,
                 skip: (model.PageNumber - 1) * model.PageSize)),
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

    public async Task<Response<HrCreateShiftRequest>> CreateShiftAsync(HrCreateShiftRequest model)
    {
        bool exists = false;
        exists = await _unitOfWork.Shifts.ExistAsync(x => x.Name_ar.Trim() == model.Name_ar
        && x.Name_en.Trim() == model.Name_en.Trim());

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

        await _unitOfWork.Shifts.AddAsync(new()
        {
            Name_en = model.Name_en,
            Name_ar = model.Name_ar,
            Start_shift = model.Start_shift.ToTimeOnly(),
            End_shift = model.End_shift.ToTimeOnly(),
            
        });
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Msg = _sharLocalizer[Localization.Done],
            Check = true,
            Data = model
        };
    }

    public async Task<Response<HrGetShiftByIdResponse>> GetShiftByIdAsync(int id)
    {
        var obj = await _unitOfWork.Shifts.GetByIdAsync(id);

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
                Name_en = obj.Name_en,
                End_shift = obj.End_shift,
                Start_shift = obj.Start_shift,
            },
            Check = true
        };
    }

    public async Task<Response<HrUpdateShiftRequest>> UpdateShiftAsync(int id, HrUpdateShiftRequest model)
    {
        var obj = await _unitOfWork.Shifts.GetByIdAsync(id);

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

        obj.Name_ar = model.Name_ar;
        obj.Name_en = model.Name_en;
        obj.Start_shift=model.Start_shift.ToTimeOnly();
        obj.End_shift=model.End_shift.ToTimeOnly();
        _unitOfWork.Shifts.Update(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data = model,
            Msg = _sharLocalizer[Localization.Updated]
        };
    }

    public async Task<Response<HrUpdateShiftRequest>> RestoreShiftAsync(int id)
    {
        var obj = await _unitOfWork.Shifts.GetFirstOrDefaultAsync(s=>s.Id==id);

        if (obj == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Shift]);

            return new()
            {
                Data = null,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        obj.IsDeleted = false;
        _unitOfWork.Shifts.Update(obj);
        await _unitOfWork.CompleteAsync();

        return new()
        {
            Check = true,
            Data =new ()
            {
                Name_ar = obj.Name_ar,
                Name_en = obj.Name_en,
                End_shift = obj.End_shift.ToString(),
                Start_shift = obj.Start_shift.ToString()
            },
            Msg = _sharLocalizer[Localization.Restored]
        };
    }

    public async Task<Response<string>> ChangeShift(int from, int to)
    {
        var oldShift = await _unitOfWork.Shifts.GetByIdAsync(from);
        var newShift = await _unitOfWork.Shifts.GetByIdAsync(to);
        if (oldShift == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Shift]);

            return new()
            {
                Data = null,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        if (newShift == null)
        {
            string resultMsg = string.Format(_sharLocalizer[Localization.CannotBeFound],
                _sharLocalizer[Localization.Shift]);

            return new()
            {
                Data = null,
                Error = resultMsg,
                Msg = resultMsg
            };
        }

        if (oldShift == newShift)
        {
            return new()
            {
                Data = null,
                Error = "Can not change to the Shift",
                Msg = "Can not change to the Shift"
            };
        }

        var oldShiftEmpls =await unitOfWork.Employees.GetAllAsync(e => e.ShiftId == from);
        if (oldShiftEmpls!=null &&oldShiftEmpls.Any())
        {
            foreach (var emp in oldShiftEmpls.ToList())
            {
                emp.ShiftId = to;
            }
            unitOfWork.Employees.UpdateRange(oldShiftEmpls);
            await unitOfWork.CompleteAsync();
        }

        return new Response<string>()
        {
            Check = true,
            Error = string.Empty,
            Data = $"The Shift of {oldShiftEmpls.Count()} employees have been changed",
            Msg = "Shift Changed Successfully"
        };

    }

    public Task<Response<string>> UpdateActiveOrNotShiftAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<string>> DeleteShiftAsync(int id)
    {
        var obj = await _unitOfWork.Shifts.GetByIdAsync(id);

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

        _unitOfWork.Shifts.Remove(obj);
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


