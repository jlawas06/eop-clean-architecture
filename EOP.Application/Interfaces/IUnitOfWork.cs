using EOP.Domain.Common;

namespace EOP.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task CommitAsync();
        Task RollbackAsync();
    }
}
