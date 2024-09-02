

using System.Runtime.CompilerServices;
using System.Transactions;

namespace Kader_System.DataAccess.Repositories
{
    public class EntityDatabaseTransactionScope : IDatabaseTransactionScope
    {
        private TransactionScope scope;
        private bool disposed = false;
        public EntityDatabaseTransactionScope()
        {
            scope = new TransactionScope();
        }
        public void Complete()
        {
           if(disposed)
                throw new ObjectDisposedException(nameof(EntityDatabaseTransactionScope));
           scope.Complete();
        }

        public void Dispose()
        {
             Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual  void Dispose(bool disposing)
        {
            if (!disposed) {
                if (disposing) {


                    scope?.Dispose();    
                  
                 }
                disposed = true;
               
            }
        }
        ~EntityDatabaseTransactionScope()
        {
            Dispose(false);
        }

    }
}
