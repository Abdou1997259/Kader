using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.Setting
{
    public interface IUserPermessionService
    {
        public Task<Response<DTOSPGetUserPermissionsBySubScreen>> GetUserPermissionsBySubScreen(int titleId, string userId, string lang);
    }
}
