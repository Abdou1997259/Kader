using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.DataAccess.Repositories.HR
{
    public class EmployeeNotesRepository(KaderDbContext context) : BaseRepository<HrEmployeeNotes>(context), IEmployeeNotesRepository
    {
    }
}
