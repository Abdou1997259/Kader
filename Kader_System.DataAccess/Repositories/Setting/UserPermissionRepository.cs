using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.DataAccess.Repositories.Setting
{
    public  class UserPermissionRepository(KaderDbContext db):BaseRepository<UserPermission>(db),IUserPermssionRepositroy
    {
    }
}
