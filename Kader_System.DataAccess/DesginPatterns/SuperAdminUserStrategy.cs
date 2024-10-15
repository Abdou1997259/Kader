namespace Kader_System.DataAccess.DesginPatterns
{
    public class SuperAdminUserStrategy : IUserStrategy
    {
        private KaderDbContext _db;


        private string _lang;
        public SuperAdminUserStrategy(KaderDbContext db, string lang)
        {
            _db = db;

            _lang = lang;


        }
        public async Task<List<SpGetScreen>> GetPermissions()
        {
            var resultAsQuery = _db.Set<SpGetScreen>()
                    .FromSql($"EXEC sp_get_Super_Admin_screen {_lang}");



            var queryString = resultAsQuery.ToQueryString();

            var result = await resultAsQuery.ToListAsync();
            return result;


        }
    }
}
