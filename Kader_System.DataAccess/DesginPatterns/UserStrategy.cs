
using Microsoft.Data.SqlClient;


namespace Kader_System.DataAccess.DesginPatterns
{
    public class UserStrategy : IUserStrategy
    {
        private KaderDbContext _db;
        private string _userId;
        private int _titleId;
        private string _lang;
        public UserStrategy(KaderDbContext db, string userId, int titleId, string lang)
        {
            _db = db;
            _userId = userId;
            _lang = lang;
            _titleId = titleId;

        }
        public async Task<List<SpGetScreen>> GetPermissions()
        {

             return await _db.Set<SpGetScreen>()
            .FromSqlRaw("EXEC sp_get_screen @UserId, @TitleId, @Lang",
                        new SqlParameter("@UserId", _userId),
                        new SqlParameter("@TitleId", _titleId),
                        new SqlParameter("@Lang", _lang)).AsNoTracking().ToListAsync();
        }
    }
}
