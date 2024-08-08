using Kader_System.Domain.Interfaces;

namespace Kader_System.DataAccess.Repositories.Setting;

public class MainScreenRepository(KaderDbContext context) : BaseRepository<StMainScreen>(context), IMainScreenRepository
{
    

    
}
