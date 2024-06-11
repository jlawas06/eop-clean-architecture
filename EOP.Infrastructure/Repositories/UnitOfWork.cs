using EOP.Application.Interfaces;
using EOP.Domain.Common;
using EOP.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EOP.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private bool _disposed;

        public UnitOfWork(AppDbContext context) => _context = context;

        public async Task CommitAsync() => await _context.SaveChangesAsync();

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
            => new Repository<TEntity>(_context);

        public Task RollbackAsync()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(entry => entry.State = EntityState.Unchanged);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
