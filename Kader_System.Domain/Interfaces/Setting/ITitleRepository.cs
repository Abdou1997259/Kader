
namespace Kader_System.Domain.Interfaces.Setting
{
    public interface ITitleRepository : IBaseRepository<Title>
    {
        Task<Response<GetTitleByIdResponse>> GetTitleByIdAsync(int id, string lang);
    }
}
