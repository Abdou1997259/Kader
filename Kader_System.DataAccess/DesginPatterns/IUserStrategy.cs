
namespace Kader_System.DataAccess.DesginPatterns
{
    public  interface IUserStrategy
    {
        Task<List<SpGetScreen>> GetPermissions();
    }
}
