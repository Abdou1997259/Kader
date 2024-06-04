using Kader_System.Domain.DTOs.Response.Setting;

namespace Kader_System.Domain.Interfaces.Setting
{
    public interface IScreenRepository : IBaseRepository<Screen>
    {
        Task<List<ScreenInfoData>> GetScreenInfoData(Expression<Func<Screen, bool>> filter,
            int? skip = null,
            int? take = null, string lang = "ar");

        Task<Screen?> GetScreenDataById(int id);
        Task<int> GenerateNewCode_Async(int? parentAccountNo);
    }
}
