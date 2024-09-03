using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DesginPatterns
{
    public class SuperAdminUserStrategy : IUserStrategy
    {
        private KaderDbContext _db;
    

        private string _lang;
        public SuperAdminUserStrategy(KaderDbContext db, string lang)
        {
           _db=db;
           
            _lang = lang;
            
            
        }
        public async Task<List<SpGetScreen>> GetPermissions()
        {

            return
                await _db.Set<SpGetScreen>()
                   .FromSqlRaw("EXEC sp_get_Super_Admin_screen @Lang",
                   new SqlParameter("@Lang", _lang)).AsNoTracking().ToListAsync();
        }
    }
}
