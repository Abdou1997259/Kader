using Kader_System.Domain.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.HR
{
    public interface IStructureMangement
    {
        Task<Response<CompanyResponse>> GetStructureMangementAsync(int companyId, string lang);

    }
}
