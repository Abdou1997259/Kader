using Kader_System.Domain.DesginPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.DataAccess.DesginPatterns
{
    public  class userPerrmissionLoginContext
    {
        private  IUserStrategy _userStrategy;
    
        public void SetPermissionStrategy(IUserStrategy userStrategy)
        {
            _userStrategy = userStrategy;
        }
        public Task<List<SpGetScreen>> GetPermissions()
        {
            return _userStrategy.GetPermissions();
        }
    }
}
