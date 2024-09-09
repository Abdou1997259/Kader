

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
