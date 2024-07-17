using Kader_System.Domain.Dtos.Request.HR;
using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.DataAccess.Repositories.HR;

public class SalaryIncreaseTypeRepository(KaderDbContext context) : BaseRepository<HrValueType>(context), ISalaryIncreaseTypeRepository
{
    public Task<int> AddSalaryIncreaseType(HrCreateSalaryIncreaseTypesRequest increaseTypesRequest )
    {
        HrValueType increaseTypes = new()
        {
            Name = increaseTypesRequest.Name_ar,
            NameInEnglish = increaseTypesRequest.Name_en,
        };
        context.ValueTypes.Add(increaseTypes);
        return context.SaveChangesAsync();
    }

    public Task<int> DeleteSalaryIncreaseType(int id)
    {
        var result = context.ValueTypes.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true));
        return result;
    }

    public Task<List<SelectListLookupResponse>> GetAllSalaryIncreaseTypes()
    {
        var result = (from q in context.ValueTypes.AsNoTracking()
                      select new SelectListLookupResponse()
                      {
                          Id = q.Id,
                          Name_ar = q.Name,
                          Name_en = q.NameInEnglish,
                      }).ToListAsync();
        return result;


    }
    public Task<SelectListLookupResponse> GetHrValueTypeById(int id)
    {
        var result = (from q in context.ValueTypes.AsNoTracking()
                      select new SelectListLookupResponse()
                      {
                          Id = q.Id,
                          Name_ar = q.Name,
                          Name_en = q.NameInEnglish,
                      }).FirstOrDefaultAsync();
        return result;
    }

    public Task<SelectListLookupResponse> GetSalaryIncreaseTypesById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateSalaryIncreaseType(SelectListLookupResponse selectList)
    {
        var result = context.ValueTypes.ExecuteUpdateAsync(x => x.
        SetProperty(p => p.NameInEnglish, selectList.Name_en).
        SetProperty(p => p.Name, selectList.Name_ar).
        SetProperty(p => p.UpdateDate, DateTime.Now)
        );
        return result;
    }
    public async Task<object> GetSalaryIncreaseType(string lang)
    {
        return await context.ValueTypes.
            Where(e => !e.IsDeleted && e.IsActive)
            .Select(e => new
            {
                id = e.Id,
                salary_increase_type = lang == Localization.Arabic ? e.Name : e.NameInEnglish,
            }).ToArrayAsync();
    }
}