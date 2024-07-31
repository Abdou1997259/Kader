using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.Setting
{
    public interface IPermessionStructureService
    {
        public Task<Response<object>> GetAllPermessionStructure(string lang);
    }
}
