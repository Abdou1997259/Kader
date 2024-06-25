
namespace Kader_System.Domain.Interfaces.Trans
{
    public interface IAdvancedTypesRepository
    {
        Task<IEnumerable<AdvancedType>> GetAllAdvancedTypes();
    }
}
