

namespace Kader_System.Domain.Interfaces
{
    public interface IDatabaseTransactionScope:IDisposable
    {
        void Complete();
    }
}
