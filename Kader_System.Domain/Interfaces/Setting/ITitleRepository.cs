
using Kader_System.Domain.DTOs.Response.HR;
using Kader_System.Domain.DTOs.Response.Setting;

namespace Kader_System.Domain.Interfaces.Setting
{
    public interface ITitleRepository : IBaseRepository<Title>
    {
        Task<Response<GetTitleByIdResponse>> GetTitleByIdAsync(int id, string lang);
      
    }
}
