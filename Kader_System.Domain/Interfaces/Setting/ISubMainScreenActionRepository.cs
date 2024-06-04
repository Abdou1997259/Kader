namespace Kader_System.Domain.Interfaces.Setting;

public interface ISubMainScreenActionRepository : IBaseRepository<StSubMainScreenAction>
{
    Task<IEnumerable<GetEachSubMainWithActions>> GetEachSubMainWithActionsAsync(string lang);
}
