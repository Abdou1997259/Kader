
namespace Kader_System.DataAccess.Repositories.Setting
{
    public class ScreenRepository(KaderDbContext context) : BaseRepository<Screen>(context), IScreenRepository
    {

        public async Task<List<ScreenInfoData>> GetScreenInfoData(Expression<Func<Screen, bool>> filter,
            int? skip = null,
            int? take = null, string lang = "ar")
        {
           var query= context.Screens.AsNoTracking().Where(filter)
                .Include(s => s.ParentScreen)
                .Include(a => a.Actions)
                .ThenInclude(a => a.Action)
                .Select(s => new ScreenInfoData()
                {
                    Actions = s.Actions.Any() ? s.Actions.Select(a => a.Action.Id).ToList() : new List<int>(),
                    ActiveIcon = $"{ReadRootPath.SettingImagesPath}{s.ActiveIcon}",
                    Code = s.Code,
                    EndPoint = s.EndPoint,
                    Icon = $"{ReadRootPath.SettingImagesPath}{s.Icon}",
                    Id = s.Id,
                    NameAr = s.NameAr,
                    NameEn = s.NameEn,
                    Url = s.Url,
                    ScreenType = s.ScreenType,
                    Sort = s.Sort,
                    ParentId = s.ParentId,
                    ParentName = lang == Localization.Arabic ? s.ParentScreen!.NameAr : s.ParentScreen!.NameEn,

                });

           if (skip.HasValue)
               query = query.Skip(skip.Value);
           if (take.HasValue)
               query = query.Take(take.Value);

            return await query.ToListAsync();

        }

        public async Task<Screen?> GetScreenDataById(int id)
        {
             return await  context.Screens
                .Include(s => s.ParentScreen)
                .Include(a => a.Actions)
                .ThenInclude(a => a.Action)
                .FirstOrDefaultAsync(s=>s.Id==id);
        }
        public async Task<int> GenerateNewCode_Async(int? parentId)
        {
            int result;

            if (parentId == null || parentId == 0)
            {

                //في حالة ان الحساب الجديد يقع في الروت الخاص بدليل الحسابات اذا ليس له اب
                var maxAccountNoForFirstLevel = await context.Screens.Where(c => c.ParentId == null || c.ParentId == 0).MaxAsync(c => (int?)c.Code);

                result = (int)(maxAccountNoForFirstLevel == null ? 1 : maxAccountNoForFirstLevel + 1);


                while (context.Screens.Any(c => c.Code == result))
                {
                    result++;
                }

                return result;
            }
            else
            {
                //في حالة ان الحساب الجديد له اب
                //Get Max AccountNo For Parent children
                var maxAccountNoForParentChildren = await context.Screens.Where(c => c.ParentId == parentId).MaxAsync(c => (int?)c.Code);

                result = (int)(maxAccountNoForParentChildren == null ? int.Parse($"{parentId}1") : maxAccountNoForParentChildren + 1);
                while (context.Screens.Any(c => c.Code == result))
                {
                    result++;
                }
                return result;
            }

        }
    }
}
