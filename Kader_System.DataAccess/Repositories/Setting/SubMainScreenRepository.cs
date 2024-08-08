using Kader_System.DataAccesss.Context;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.DataAccess.Repositories.Setting;

public class SubMainScreenRepository(KaderDbContext context) : BaseRepository<StScreenSub>(context), ISubMainScreenRepository
{
    
  
}
