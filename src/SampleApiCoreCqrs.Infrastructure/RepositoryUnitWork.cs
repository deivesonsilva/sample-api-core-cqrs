using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using SampleApiCoreCqrs.Infrastructure.Interfaces;

namespace SampleApiCoreCqrs.Infrastructure
{
    public class RepositoryUnitWork : IRepositoryUnitWork, IDisposable
    {
        private readonly RepositoryContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed = false;

        public RepositoryUnitWork(RepositoryContext context) => _context = context;
        public async Task CompleteAsync() => await _context.SaveChangesAsync();
        public async Task CompleteAsync(CancellationToken token) => await _context.SaveChangesAsync(token);
        public async Task BeginTransactionAsync() => _transaction = await _context.Database.BeginTransactionAsync();
        public async Task BeginTransactionAsync(CancellationToken token) => await _context.Database.BeginTransactionAsync(token);
        public void Commit() => _transaction.Commit();
        public void Rollback() => _transaction.Rollback();

        public void Dispose()
        {
            if (_disposed)
                return;

            _context.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
