using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.DataAccess.Repositories.Setting
{
    public class PermessionStructureRepository(KaderDbContext context) : BaseRepository<StScreenAction>(context), IPermessionStructureRepository
    {

    }
}
