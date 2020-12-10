using System.Threading;
using System.Threading.Tasks;

namespace SampleApiCoreCqrs.Infrastructure.Interfaces
{
    public interface IRepositoryUnitWork
    {
        Task CompleteAsync();
        Task CompleteAsync(CancellationToken token);
        Task BeginTransactionAsync();
        Task BeginTransactionAsync(CancellationToken token);
        void Commit();
        void Rollback();
    }
}
